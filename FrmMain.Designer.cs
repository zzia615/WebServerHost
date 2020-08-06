using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebServer.Properties;

namespace WebServer
{
    partial class FrmMain
    {
        private Button btnAddress;
        private Button btnBack;
        private Button btnChangePort;
        private Button btnOpenPath;
        private Button btnRestart;
        private Button btnRootUrl;
        private Button btnStop;
        private CheckBox ChkRunwin;
        private ContextMenuStrip contextMenuStrip1;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label6;
        private LinkLabel linkLabel1;
        private ToolStripMenuItem MenuBack;
        private ToolStripMenuItem MenuFangPage;
        private ToolStripMenuItem MenuOpenPath;
        private ToolStripMenuItem MenuOpenURL;
        private ToolStripMenuItem MenuReStart;
        private ToolStripMenuItem MenuShowInfo;
        private ToolStripMenuItem MenuStop;
        private NotifyIcon notifyIcon1;
        private PictureBox pictureBox1;
        private TextBox txtPhysicalPath;
        private TextBox txtPort;
        private TextBox txtRootUrl;
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtPhysicalPath = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuShowInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpenURL = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBack = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpenPath = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFangPage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReStart = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnChangePort = new System.Windows.Forms.Button();
            this.txtRootUrl = new System.Windows.Forms.TextBox();
            this.ChkRunwin = new System.Windows.Forms.CheckBox();
            this.btnOpenPath = new System.Windows.Forms.Button();
            this.btnRootUrl = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAddress = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "站点路径：";
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.SystemColors.Window;
            this.txtPort.Location = new System.Drawing.Point(106, 123);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(268, 21);
            this.txtPort.TabIndex = 3;
            // 
            // txtPhysicalPath
            // 
            this.txtPhysicalPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtPhysicalPath.Location = new System.Drawing.Point(106, 163);
            this.txtPhysicalPath.Name = "txtPhysicalPath";
            this.txtPhysicalPath.ReadOnly = true;
            this.txtPhysicalPath.Size = new System.Drawing.Size(268, 21);
            this.txtPhysicalPath.TabIndex = 3;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(404, 209);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(61, 25);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "停止服务";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuShowInfo,
            this.MenuOpenURL,
            this.MenuBack,
            this.MenuOpenPath,
            this.MenuFangPage,
            this.MenuReStart,
            this.MenuStop});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 158);
            // 
            // MenuShowInfo
            // 
            this.MenuShowInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuShowInfo.Name = "MenuShowInfo";
            this.MenuShowInfo.Size = new System.Drawing.Size(160, 22);
            this.MenuShowInfo.Text = "打开服务器窗口";
            this.MenuShowInfo.Click += new System.EventHandler(this.MenuShowInfo_Click);
            // 
            // MenuOpenURL
            // 
            this.MenuOpenURL.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuOpenURL.Name = "MenuOpenURL";
            this.MenuOpenURL.Size = new System.Drawing.Size(160, 22);
            this.MenuOpenURL.Text = "打开站点首页";
            this.MenuOpenURL.Click += new System.EventHandler(this.MenuOpenURL_Click);
            // 
            // MenuBack
            // 
            this.MenuBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuBack.Name = "MenuBack";
            this.MenuBack.Size = new System.Drawing.Size(160, 22);
            this.MenuBack.Text = "打开站点后台";
            this.MenuBack.Click += new System.EventHandler(this.MenuBack_Click);
            // 
            // MenuOpenPath
            // 
            this.MenuOpenPath.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuOpenPath.Name = "MenuOpenPath";
            this.MenuOpenPath.Size = new System.Drawing.Size(160, 22);
            this.MenuOpenPath.Text = "打开站点目录";
            this.MenuOpenPath.Click += new System.EventHandler(this.MenuOpenPath_Click);
            // 
            // MenuFangPage
            // 
            this.MenuFangPage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuFangPage.Name = "MenuFangPage";
            this.MenuFangPage.Size = new System.Drawing.Size(160, 22);
            this.MenuFangPage.Text = "方配官方网站";
            this.MenuFangPage.Click += new System.EventHandler(this.MenuFangPage_Click);
            // 
            // MenuReStart
            // 
            this.MenuReStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuReStart.Name = "MenuReStart";
            this.MenuReStart.Size = new System.Drawing.Size(160, 22);
            this.MenuReStart.Text = "重启站点服务";
            this.MenuReStart.Click += new System.EventHandler(this.MenuReStart_Click);
            // 
            // MenuStop
            // 
            this.MenuStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuStop.Name = "MenuStop";
            this.MenuStop.Size = new System.Drawing.Size(160, 22);
            this.MenuStop.Text = "停止站点服务";
            this.MenuStop.Click += new System.EventHandler(this.MenuStop_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(334, 209);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(61, 25);
            this.btnRestart.TabIndex = 5;
            this.btnRestart.Text = "重启服务";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnChangePort
            // 
            this.btnChangePort.Location = new System.Drawing.Point(391, 121);
            this.btnChangePort.Name = "btnChangePort";
            this.btnChangePort.Size = new System.Drawing.Size(75, 23);
            this.btnChangePort.TabIndex = 6;
            this.btnChangePort.Text = "更改端口";
            this.btnChangePort.UseVisualStyleBackColor = true;
            this.btnChangePort.Click += new System.EventHandler(this.btnChangePort_Click);
            // 
            // txtRootUrl
            // 
            this.txtRootUrl.Location = new System.Drawing.Point(106, 83);
            this.txtRootUrl.Name = "txtRootUrl";
            this.txtRootUrl.Size = new System.Drawing.Size(268, 21);
            this.txtRootUrl.TabIndex = 8;
            // 
            // ChkRunwin
            // 
            this.ChkRunwin.AutoSize = true;
            this.ChkRunwin.Location = new System.Drawing.Point(16, 215);
            this.ChkRunwin.Name = "ChkRunwin";
            this.ChkRunwin.Size = new System.Drawing.Size(96, 16);
            this.ChkRunwin.TabIndex = 11;
            this.ChkRunwin.Text = "开机自动运行";
            this.ChkRunwin.UseVisualStyleBackColor = true;
            this.ChkRunwin.Click += new System.EventHandler(this.ChkRunwin_Click);
            // 
            // btnOpenPath
            // 
            this.btnOpenPath.Location = new System.Drawing.Point(391, 160);
            this.btnOpenPath.Name = "btnOpenPath";
            this.btnOpenPath.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPath.TabIndex = 12;
            this.btnOpenPath.Text = "打开目录";
            this.btnOpenPath.UseVisualStyleBackColor = true;
            this.btnOpenPath.Click += new System.EventHandler(this.btnOpenPath_Click);
            // 
            // btnRootUrl
            // 
            this.btnRootUrl.Location = new System.Drawing.Point(189, 209);
            this.btnRootUrl.Name = "btnRootUrl";
            this.btnRootUrl.Size = new System.Drawing.Size(64, 25);
            this.btnRootUrl.TabIndex = 14;
            this.btnRootUrl.Text = "打开网站";
            this.btnRootUrl.UseVisualStyleBackColor = true;
            this.btnRootUrl.Click += new System.EventHandler(this.btnRootUrl_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "http://";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(263, 209);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(62, 25);
            this.btnBack.TabIndex = 18;
            this.btnBack.Text = "打开后台";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnAddress
            // 
            this.btnAddress.Location = new System.Drawing.Point(391, 81);
            this.btnAddress.Name = "btnAddress";
            this.btnAddress.Size = new System.Drawing.Size(75, 23);
            this.btnAddress.TabIndex = 19;
            this.btnAddress.Text = "更改地址";
            this.btnAddress.UseVisualStyleBackColor = true;
            this.btnAddress.Click += new System.EventHandler(this.btnAddress_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(421, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 12);
            this.linkLabel1.TabIndex = 13;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(-1, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(484, 62);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 255);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.btnAddress);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtRootUrl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnRootUrl);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnOpenPath);
            this.Controls.Add(this.ChkRunwin);
            this.Controls.Add(this.btnChangePort);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtPhysicalPath);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网站服务器";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

