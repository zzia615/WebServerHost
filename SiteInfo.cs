namespace WMSServer
{
    using System;
    using System.Runtime.CompilerServices;

    public class SiteInfo
    {
        private string m_physicalpath = "";
        private int m_port = 0;
        private string m_rooturl = "localhost";
        private string m_virtualpath = "/";

        public int AutoRun { get; set; }

        public int FixPort { get; set; }

        public int Key { get; set; }

        public string PhysicalPath
        {
            get
            {
                return this.m_physicalpath;
            }
            set
            {
                this.m_physicalpath = value;
            }
        }

        public int Port
        {
            get
            {
                return this.m_port;
            }
            set
            {
                this.m_port = value;
            }
        }

        public string RootUrl
        {
            get
            {
                return this.m_rooturl;
            }
            set
            {
                this.m_rooturl = value;
            }
        }

        public string VirtualPath
        {
            get
            {
                return this.m_virtualpath;
            }
            set
            {
                this.m_virtualpath = value;
            }
        }
    }
}

