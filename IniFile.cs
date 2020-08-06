namespace WMSServer
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class IniFile
    {
        public string Path;

        public IniFile()
        {
            this.Path = Application.StartupPath + @"\config\server.config";
            if (!Directory.Exists(Application.StartupPath + @"\config"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\config");
            }
        }

        public IniFile(string filepath)
        {
            this.Path = Application.StartupPath + @"\config\server.config";
            this.Path = filepath;
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            int num = GetPrivateProfileString(Section, Key, "", retVal, 0xff, this.Path);
            return retVal.ToString();
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.Path);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}

