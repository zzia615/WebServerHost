namespace WMSServer
{
    using System;

    public class SiteConfigs
    {
        public static SiteInfo GetSiteInfo()
        {
            SiteInfo info = new SiteInfo();
            try
            {
                IniFile file = new IniFile();
                info.RootUrl = file.IniReadValue("ServerConfig", "RootUrl").ToString();
                info.Port = Utils.StrToInt(file.IniReadValue("ServerConfig", "Port").ToString());
                info.VirtualPath = file.IniReadValue("ServerConfig", "VirtualPath").ToString();
                info.PhysicalPath = file.IniReadValue("ServerConfig", "PhysicalPath");
                info.AutoRun = Utils.StrToInt(file.IniReadValue("ServerConfig", "AutoRun"));
                info.Key = Utils.StrToInt(file.IniReadValue("ServerConfig", "Key"));
                info.FixPort = Utils.StrToInt(file.IniReadValue("ServerConfig", "FixPort"));
            }
            catch
            {
            }
            return info;
        }

        public static bool SaveSiteInfo(SiteInfo siteinfo)
        {
            try
            {
                IniFile file = new IniFile();
                file.IniWriteValue("ServerConfig", "RootUrl", siteinfo.RootUrl);
                file.IniWriteValue("ServerConfig", "Port", siteinfo.Port.ToString());
                file.IniWriteValue("ServerConfig", "VirtualPath", siteinfo.VirtualPath);
                file.IniWriteValue("ServerConfig", "PhysicalPath", siteinfo.PhysicalPath);
                file.IniWriteValue("ServerConfig", "AutoRun", siteinfo.AutoRun.ToString());
                file.IniWriteValue("ServerConfig", "Key", siteinfo.Key.ToString());
                file.IniWriteValue("ServerConfig", "FixPort", siteinfo.FixPort.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

