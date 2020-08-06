namespace FPServer
{
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Threading;
    using System.Web;
    using System.Web.Hosting;
    using WMSServer;

    public class Server : MarshalByRefObject
    {
        private string _domain;
        private Host _host;
        private string _installPath;
        private string _physicalPath;
        private int _port;
        public WaitCallback _restartCallback;
        private string _virtualPath;
        private SiteInfo siteinfo = new SiteInfo();

        public Server(string domain, int port, string virtualPath, string physicalPath)
        {
            this._domain = domain;
            if (this._domain.StartsWith("http://"))
            {
                this._domain = this._domain.Substring(7, this._domain.Length - 7);
            }
            this._port = port;
            this._virtualPath = virtualPath;
            this._physicalPath = physicalPath.EndsWith(@"\") ? physicalPath : (physicalPath + @"\");
            this._restartCallback = new WaitCallback(this.RestartCallback);
            this._installPath = this.GetInstallPathAndConfigureAspNetIfNeeded();
            this.CreateHost();
        }

        private void CreateHost()
        {
            this._host = (Host) CreateWorkerAppDomainWithHost(this._virtualPath, this._physicalPath, typeof(Host));
            this._host.Configure(this, this._domain, this._port, this._virtualPath, this._physicalPath, this._installPath);
        }

        private static object CreateWorkerAppDomainWithHost(string virtualPath, string physicalPath, Type hostType)
        {
            string appId = (virtualPath + physicalPath).ToLowerInvariant().GetHashCode().ToString("x", CultureInfo.InvariantCulture);
            ApplicationManager applicationManager = ApplicationManager.GetApplicationManager();
            Type type = typeof(HttpRuntime).Assembly.GetType("System.Web.Compilation.BuildManagerHost");
            IRegisteredObject target = applicationManager.CreateObject(appId, type, virtualPath, physicalPath, false);
            type.InvokeMember("RegisterAssembly", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, target, new object[] { hostType.Assembly.FullName, hostType.Assembly.Location });
            return applicationManager.CreateObject(appId, hostType, virtualPath, physicalPath, false);
        }

        private string GetInstallPathAndConfigureAspNetIfNeeded()
        {
            RegistryKey key = null;
            RegistryKey key2 = null;
            RegistryKey key3 = null;
            string str = null;
            try
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(typeof(HttpRuntime).Module.FullyQualifiedName);
                string str2 = string.Format("{0}.{1}.{2}.{3}", new object[] { versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart, versionInfo.FilePrivatePart });
                string name = @"Software\Microsoft\ASP.NET\" + str2;
                if (!str2.StartsWith("1.0."))
                {
                    name = name.Substring(0, name.LastIndexOf('.') + 1) + "0";
                }
                key2 = Registry.LocalMachine.OpenSubKey(name);
                if (key2 != null)
                {
                    return (string) key2.GetValue("Path");
                }
                key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\ASP.NET");
                if (key == null)
                {
                    key = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\ASP.NET");
                    key.SetValue("RootVer", str2);
                }
                string str4 = "v" + str2.Substring(0, str2.LastIndexOf('.'));
                key3 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\.NETFramework");
                string str5 = (string) key3.GetValue("InstallRoot");
                if (str5.EndsWith(@"\"))
                {
                    str5 = str5.Substring(0, str5.Length - 1);
                }
                key2 = Registry.LocalMachine.CreateSubKey(name);
                str = str5 + @"\" + str4;
                key2.SetValue("Path", str);
                key2.SetValue("DllFullPath", str + @"\aspnet_isapi.dll");
                return str;
            }
            catch
            {
            }
            finally
            {
                if (key2 != null)
                {
                    key2.Close();
                }
                if (key != null)
                {
                    key.Close();
                }
                if (key3 != null)
                {
                    key3.Close();
                }
            }
            return str;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Restart()
        {
            ThreadPool.QueueUserWorkItem(this._restartCallback);
        }

        private void RestartCallback(object unused)
        {
            this.Stop();
            this.CreateHost();
            this.Start();
        }

        public void Start()
        {
            if (this._host != null)
            {
                this._host.Start();
            }
        }

        public void Stop()
        {
            if (this._host != null)
            {
                try
                {
                    this._host.Stop();
                }
                catch
                {
                }
            }
        }
    }
}

