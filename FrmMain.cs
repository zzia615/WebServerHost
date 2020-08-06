using FPServer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WMSServer;

namespace WebServer
{
    public partial class FrmMain : Form
    {
        private static int runauto = 0;
        private Server server;
        private static SiteInfo siteinfo = new SiteInfo();
        public FrmMain()
        {
            InitializeComponent();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            base.SizeChanged += new EventHandler(this.FrmMain_SizeChanged);
            this.txtRootUrl.Text = siteinfo.RootUrl;
            this.txtPort.Text = siteinfo.Port.ToString();
            this.txtPhysicalPath.Text = siteinfo.PhysicalPath;
            this.ChkRunwin.Checked = siteinfo.AutoRun == 1;
            this.notifyIcon1.Text = "网站服务器(" + Utils.CutString(siteinfo.PhysicalPath, 50) + ")";
            this.notifyIcon1.Visible = true;
            base.Visible = false;
            if (this.RunServer() && (runauto != 3))
            {
                this.btnRootUrl_Click(null, null);
            }
        }
        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            if (base.WindowState == FormWindowState.Minimized)
            {
                base.Visible = false;
            }
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.OnClosing(e);
            if (!e.Cancel)
            {
                try
                {
                    if (this.server != null)
                    {
                        this.server.Stop();
                    }
                }
                catch
                {
                }
                this.notifyIcon1.Dispose();
            }
        }
        private void btnAddress_Click(object sender, EventArgs e)
        {
            string ip = this.txtRootUrl.Text.TrimEnd(new char[] { ':' });
            if (ip == "")
            {
                ip = "*";
            }
            if (ip.StartsWith("http://"))
            {
                ip = ip.Substring(7, ip.Length - 7);
            }
            if (!(Utils.IsIPSect(ip) || Utils.IsValidDomain(ip)))
            {
                MessageBox.Show("不是有效的站点地址，请输入IP地址或域名。", "地址错误");
            }
            else
            {
                siteinfo.RootUrl = ip;
                SiteConfigs.SaveSiteInfo(siteinfo);
                this.btnRestart_Click(null, null);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            string str = "http://" + siteinfo.RootUrl.TrimEnd(new char[] { ':' });
            if (siteinfo.Port != 80)
            {
                str = str + ":" + siteinfo.Port;
            }
            Utils.StartHttp(str.TrimEnd(new char[] { '/' }) + "/admin/index.aspx");
        }

        private void btnChangePort_Click(object sender, EventArgs e)
        {
            int num = Utils.StrToInt(this.txtPort.Text);
            if (num == 0)
            {
                num = new Random().Next(0x401, 0xffff);
            }
            if ((num <= 0) || (num > 0xffff))
            {
                MessageBox.Show("端口只能是 1 ~ 65535 之间的数字，并且还不能被占用！");
            }
            else
            {
                siteinfo.Port = num;
                SiteConfigs.SaveSiteInfo(siteinfo);
                this.btnRestart_Click(null, null);
            }
        }

        private void btnOpenPath_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", siteinfo.PhysicalPath);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (this.RunServer())
            {
                this.btnRootUrl_Click(null, null);
            }
        }

        private void btnRootUrl_Click(object sender, EventArgs e)
        {
            Utils.OpenUrl(siteinfo.RootUrl, siteinfo.Port);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.server != null)
                {
                    this.server.Stop();
                }
            }
            catch
            {
            }
            Application.Exit();
        }

        private void ChkRunwin_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                string str = Application.ExecutablePath + " 3";
                string name = "WMSServer" + siteinfo.Key;
                if (this.ChkRunwin.Checked)
                {
                    siteinfo.AutoRun = 1;
                    if (key == null)
                    {
                        key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    }
                    key.SetValue(name, str);
                }
                else
                {
                    siteinfo.AutoRun = 0;
                    if (key.GetValue(name) != null)
                    {
                        key.DeleteValue(name, true);
                    }
                }
                SiteConfigs.SaveSiteInfo(siteinfo);
            }
            catch (Exception)
            {
                MessageBox.Show("对不起，操作系统注册表已被锁住，不能操作。", "注册表已锁");
                this.ChkRunwin.Checked = false;
            }
        }

        private void lnkHome_Click(object sender, EventArgs e)
        {
            Utils.StartHttp("http://www.Jaylosy.com");
        }

        private void MenuBack_Click(object sender, EventArgs e)
        {
            this.btnBack_Click(null, null);
        }

        private void MenuJaylosy_Click(object sender, EventArgs e)
        {
            Utils.StartHttp("http://www.Jaylosy.com");
        }

        private void MenuOpenPath_Click(object sender, EventArgs e)
        {
            this.btnOpenPath_Click(null, null);
        }

        private void MenuOpenURL_Click(object sender, EventArgs e)
        {
            this.btnRootUrl_Click(sender, null);
        }

        private void MenuReStart_Click(object sender, EventArgs e)
        {
            this.btnRestart_Click(sender, null);
        }

        private void MenuShowInfo_Click(object sender, EventArgs e)
        {
            base.Visible = true;
            base.WindowState = FormWindowState.Normal;
            this.notifyIcon1.Visible = false;
        }

        private void MenuStop_Click(object sender, EventArgs e)
        {
            this.btnStop_Click(null, null);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                base.Visible = true;
                base.WindowState = FormWindowState.Normal;
            }
        }

        

        private bool RunServer()
        {
            try
            {
                if (this.server != null)
                {
                    this.server.Stop();
                }
            }
            catch
            {
            }
            this.txtRootUrl.Text = siteinfo.RootUrl;
            this.txtPort.Text = siteinfo.Port.ToString();
            this.txtPhysicalPath.Text = siteinfo.PhysicalPath;
            this.ChkRunwin.Checked = siteinfo.AutoRun == 1;
            this.server = new Server(siteinfo.RootUrl, siteinfo.Port, siteinfo.VirtualPath, siteinfo.PhysicalPath);
            try
            {
                this.server.Start();
                this.btnRootUrl.Enabled = true;
                this.btnBack.Enabled = true;
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("启动错误：" + exception.Message, "启动失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                base.Visible = true;
                base.WindowState = FormWindowState.Normal;
                this.btnRootUrl.Enabled = false;
                this.btnBack.Enabled = false;
                this.server = null;
                return false;
            }
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Environment.CurrentDirectory = Application.StartupPath;
            siteinfo = SiteConfigs.GetSiteInfo();
            if (args.Length > 0)
            {
                runauto = Utils.StrToInt(args[0]);
            }
            if (runauto == 1)
            {
                Process.Start(Application.ExecutablePath, "2");
            }
            else if ((RunningInstance() > 1) && (runauto == 0))
            {
                Utils.OpenUrl(siteinfo.RootUrl, siteinfo.Port);
            }
            else
            {
                if ((RunningInstance() == 2) && (runauto == 2))
                {
                    Thread.Sleep(0x7d0);
                    if (RunningInstance() > 1)
                    {
                        Utils.OpenUrl(siteinfo.RootUrl, siteinfo.Port);
                        return;
                    }
                }
                else if ((RunningInstance() > 2) && (runauto == 2))
                {
                    Utils.OpenUrl(siteinfo.RootUrl, siteinfo.Port);
                    return;
                }
                bool flag = false;
                if (siteinfo.RootUrl == "")
                {
                    siteinfo.RootUrl = "localhost";
                    flag = true;
                }
                if (siteinfo.PhysicalPath != Application.StartupPath)
                {
                    if (siteinfo.FixPort == 0)
                    {
                        siteinfo.Port = 0;
                    }
                    siteinfo.Key = 0;
                }
                if (siteinfo.Port == 0)
                {
                    siteinfo.Port = new Random().Next(0x401, 0xffff);
                    flag = true;
                }
                if (siteinfo.Key == 0)
                {
                    siteinfo.Key = new Random().Next(100, 0x270f);
                    flag = true;
                }
                if (siteinfo.VirtualPath == "")
                {
                    siteinfo.VirtualPath = "/";
                    flag = true;
                }
                if (!siteinfo.VirtualPath.StartsWith("/"))
                {
                    siteinfo.VirtualPath = "/" + siteinfo.VirtualPath;
                    flag = true;
                }
                siteinfo.PhysicalPath = Application.StartupPath;
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    string str = "";
                    if (key.GetValue("WMSServer" + siteinfo.Key) != null)
                    {
                        str = key.GetValue("WMSServer" + siteinfo.Key).ToString();
                    }
                    if ((siteinfo.AutoRun == 1) && (str == ""))
                    {
                        siteinfo.AutoRun = 0;
                        flag = true;
                    }
                    else if (((siteinfo.AutoRun == 0) && (str != "")) && (key.GetValue("WMSServer" + siteinfo.Key) != null))
                    {
                        key.DeleteValue("WMSServer" + siteinfo.Key, true);
                    }
                }
                catch
                {
                    siteinfo.AutoRun = 0;
                    flag = true;
                }
                if (flag)
                {
                    SiteConfigs.SaveSiteInfo(siteinfo);
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Environment.CurrentDirectory = Application.StartupPath;
                Application.Run(new FrmMain());
            }
        }
        public static int RunningInstance()
        {
            Process[] processesByName = Process.GetProcessesByName("WMSServer");
            int num = 0;
            foreach (Process process in processesByName)
            {
                if (Assembly.GetExecutingAssembly().Location.Replace("/", @"\") == process.MainModule.FileName)
                {
                    num++;
                }
            }
            return num;
        }
    }
    
}
