namespace FPServer
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Web;
    using System.Web.Hosting;
    using WMSServer;

    internal class Host : MarshalByRefObject, IRegisteredObject
    {
        private string _domain;
        private string _installPath;
        private string _lowerCasedClientScriptPathWithTrailingSlashV10;
        private string _lowerCasedClientScriptPathWithTrailingSlashV11;
        private string _lowerCasedVirtualPath;
        private string _lowerCasedVirtualPathWithTrailingSlash;
        private EventHandler _onAppDomainUnload;
        private WaitCallback _onSocketAccept;
        private WaitCallback _onStart;
        private string _physicalClientScriptPath;
        private string _physicalPath;
        private int _port;
        private Server _server;
        private Socket _socket;
        private bool _started;
        private bool _stopped;
        private string _virtualPath;

        public void Configure(Server server, string domain, int port, string virtualPath, string physicalPath, string installPath)
        {
            this._server = server;
            this._domain = domain;
            if (this._domain.StartsWith("http://"))
            {
                this._domain = this._domain.Substring(7, this._domain.Length - 7);
            }
            this._port = port;
            this._virtualPath = virtualPath;
            this._lowerCasedVirtualPath = CultureInfo.InvariantCulture.TextInfo.ToLower(this._virtualPath);
            this._lowerCasedVirtualPathWithTrailingSlash = virtualPath.EndsWith("/") ? virtualPath : (virtualPath + "/");
            this._lowerCasedVirtualPathWithTrailingSlash = CultureInfo.InvariantCulture.TextInfo.ToLower(this._lowerCasedVirtualPathWithTrailingSlash);
            this._physicalPath = physicalPath;
            this._installPath = installPath;
            this._physicalClientScriptPath = installPath + @"\asp.netclientfiles\";
            string fileVersion = FileVersionInfo.GetVersionInfo(typeof(HttpRuntime).Module.FullyQualifiedName).FileVersion;
            string str2 = fileVersion.Substring(0, fileVersion.LastIndexOf('.'));
            this._lowerCasedClientScriptPathWithTrailingSlashV10 = "/aspnet_client/system_web/" + fileVersion.Replace('.', '_') + "/";
            this._lowerCasedClientScriptPathWithTrailingSlashV11 = "/aspnet_client/system_web/" + str2.Replace('.', '_') + "/";
            this._onSocketAccept = new WaitCallback(this.OnSocketAccept);
            this._onStart = new WaitCallback(this.OnStart);
            this._onAppDomainUnload = new EventHandler(this.OnAppDomainUnload);
            Thread.GetDomain().DomainUnload += this._onAppDomainUnload;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public bool IsVirtualPathAppPath(string path)
        {
            if (path == null)
            {
                return false;
            }
            path = CultureInfo.InvariantCulture.TextInfo.ToLower(path);
            return ((path == this._lowerCasedVirtualPath) || (path == this._lowerCasedVirtualPathWithTrailingSlash));
        }

        public bool IsVirtualPathInApp(string path)
        {
            bool flag;
            string str;
            return this.IsVirtualPathInApp(path, out flag, out str);
        }

        public bool IsVirtualPathInApp(string path, out bool isClientScriptPath, out string clientScript)
        {
            isClientScriptPath = false;
            clientScript = null;
            if (path != null)
            {
                if ((this._virtualPath == "/") && path.StartsWith("/"))
                {
                    if (path.StartsWith(this._lowerCasedClientScriptPathWithTrailingSlashV10))
                    {
                        isClientScriptPath = true;
                        clientScript = path.Substring(this._lowerCasedClientScriptPathWithTrailingSlashV10.Length);
                    }
                    if (path.StartsWith(this._lowerCasedClientScriptPathWithTrailingSlashV11))
                    {
                        isClientScriptPath = true;
                        clientScript = path.Substring(this._lowerCasedClientScriptPathWithTrailingSlashV11.Length);
                    }
                    return true;
                }
                path = CultureInfo.InvariantCulture.TextInfo.ToLower(path);
                if (path.StartsWith(this._lowerCasedVirtualPathWithTrailingSlash))
                {
                    return true;
                }
                if (path == this._lowerCasedVirtualPath)
                {
                    return true;
                }
                if (path.StartsWith(this._lowerCasedClientScriptPathWithTrailingSlashV10))
                {
                    isClientScriptPath = true;
                    clientScript = path.Substring(this._lowerCasedClientScriptPathWithTrailingSlashV10.Length);
                    return true;
                }
                if (path.StartsWith(this._lowerCasedClientScriptPathWithTrailingSlashV11))
                {
                    isClientScriptPath = true;
                    clientScript = path.Substring(this._lowerCasedClientScriptPathWithTrailingSlashV11.Length);
                    return true;
                }
            }
            return false;
        }

        private void OnAppDomainUnload(object unusedObject, EventArgs unusedEventArgs)
        {
            Thread.GetDomain().DomainUnload -= this._onAppDomainUnload;
            if (!this._stopped)
            {
                this.Stop();
                this._server.Restart();
                this._server = null;
            }
        }

        private void OnSocketAccept(object acceptedSocket)
        {
            new FPServer.Connection(this, (Socket) acceptedSocket).ProcessOneRequest();
        }

        private void OnStart(object unused)
        {
            while (this._started)
            {
                try
                {
                    Socket state = this._socket.Accept();
                    ThreadPool.QueueUserWorkItem(this._onSocketAccept, state);
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }
            this._stopped = true;
        }

        public void Start()
        {
            if (this._started)
            {
                throw new InvalidOperationException();
            }
            this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (((this._domain == "localhost") || (this._domain == "")) || (this._domain == "*"))
            {
                this._socket.Bind(new IPEndPoint(IPAddress.Any, this._port));
            }
            else
            {
                IPAddress address;
                if (Utils.IsIPSect(this._domain))
                {
                    address = IPAddress.Parse(this._domain);
                    this._socket.Bind(new IPEndPoint(address, this._port));
                }
                else
                {
                    address = Dns.GetHostEntry(this._domain).AddressList[0];
                    this._socket.Bind(new IPEndPoint(address, this._port));
                }
            }
            this._socket.Listen(0x7fffffff);
            this._started = true;
            ThreadPool.QueueUserWorkItem(this._onStart);
        }

        public void Stop()
        {
            if (this._started)
            {
                this._started = false;
                try
                {
                    this._socket.Close();
                }
                catch
                {
                }
                finally
                {
                    this._socket = null;
                }
                while (!this._stopped)
                {
                    Thread.Sleep(100);
                }
            }
        }

        void IRegisteredObject.Stop(bool immediate)
        {
            HostingEnvironment.UnregisterObject(this);
        }

        public string Domain
        {
            get
            {
                return this._domain;
            }
        }

        public string InstallPath
        {
            get
            {
                return this._installPath;
            }
        }

        public string NormalizedVirtualPath
        {
            get
            {
                return this._lowerCasedVirtualPathWithTrailingSlash;
            }
        }

        public string PhysicalClientScriptPath
        {
            get
            {
                return this._physicalClientScriptPath;
            }
        }

        public string PhysicalPath
        {
            get
            {
                return this._physicalPath;
            }
        }

        public int Port
        {
            get
            {
                return this._port;
            }
        }

        public string VirtualPath
        {
            get
            {
                return this._virtualPath;
            }
        }
    }
}

