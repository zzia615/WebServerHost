namespace FPServer
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Web;

    internal class Messages
    {
        public static string VersionString = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static string FormatBytesStr(long bytes)
        {
            if (bytes >= 0x40000000L)
            {
                return (string.Format("{0:F}", ((double) bytes) / 1073741824.0) + "G");
            }
            if (bytes >= 0x100000L)
            {
                return (string.Format("{0:F}", ((double) bytes) / 1048576.0) + "M");
            }
            if (bytes >= 0x400L)
            {
                return (string.Format("{0:F}", ((double) bytes) / 1024.0) + "K");
            }
            return (bytes.ToString() + "B");
        }

        public static string FormatDirectoryListing(string dirPath, string parentPath, FileSystemInfo[] elements)
        {
            string str = "";
            string[] strArray = VersionString.Split(new char[] { '.' });
            str = string.Format("V{0} Powered by Jaylosy.com", strArray[0] + "." + strArray[1]);
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<html>\r\n    <head>\r\n    <title>站点目录列表 -- {0}</title>\r\n", dirPath));
            builder.Append("        <style>\r\n        \tbody {font-family:\"宋体\";font-weight:normal;font-size: 10pt;color:black;} \r\n        \tp {font-family:\"宋体\";font-weight:normal;color:black;margin-top: -5px}\r\n        \tb {font-family:\"宋体\";font-weight:bold;color:black;margin-top: -5px}\r\n        \tH1 { font-family:\"宋体\";font-weight:normal;font-size:18pt;color:red }\r\n        \tH2 { font-family:\"宋体\";font-weight:normal;font-size:14pt;color:maroon }\r\n        \tpre {font-family:\"宋体\";font-size: 10pt; LINE-HEIGHT: 20px;}\r\n        \t.marker {font-weight: bold; color: black;text-decoration: none;}\r\n        \t.version {color: gray;}\r\n        \t.error {margin-bottom: 10px;}\r\n        \t.expandable { text-decoration:underline; font-weight:bold; color:navy; cursor:hand; }\r\n        </style>\r\n");
            builder.Append(string.Format("    </head>\r\n    <body bgcolor=\"white\">\r\n\r\n    <h2> <i>站点目录列表 -- {0}</i> </h2></span>\r\n\r\n            <hr width=100% size=1 color=silver>\r\n\r\n<PRE>\r\n", dirPath));
            if (parentPath != null)
            {
                if (!parentPath.EndsWith("/"))
                {
                    parentPath = parentPath + "/";
                }
                builder.Append(string.Format("<A href=\"{0}\">[返回上一级]</A>\r\n\r\n", parentPath));
            }
            if (elements != null)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i] is FileInfo)
                    {
                        FileInfo info = (FileInfo) elements[i];
                        builder.Append(string.Format("{0,38:yyyy-MM-dd hh:mm:ss}{1,12:n0} <A href=\"{2}\">{3}</A>\r\n", new object[] { info.LastWriteTime, FormatBytesStr(info.Length), info.Name, info.Name }));
                    }
                    else if (elements[i] is DirectoryInfo)
                    {
                        DirectoryInfo info2 = (DirectoryInfo) elements[i];
                        builder.Append(string.Format("{0,38:yyyy-MM-dd hh:mm:ss}     &lt;文件夹&gt; <A href=\"{1}/\">{2}</A>\r\n", info2.LastWriteTime, info2.Name, info2.Name));
                    }
                }
            }
            builder.Append("</PRE>\r\n            <hr width=100% size=1 color=silver>\r\n\r\n            <b>版本信息:</b>&nbsp;<a href=\"http://www.Jaylosy.com\" target=\"_black\">网站服务器</a> " + str + "\r\n\r\n    </body>\r\n</html>\r\n");
            return builder.ToString();
        }

        public static string FormatErrorMessageBody(int statusCode, string appName)
        {
            string str = "";
            string[] strArray = VersionString.Split(new char[] { '.' });
            str = string.Format("V{0} Powered by Jaylosy.com", strArray[0] + "." + strArray[1]);
            string statusDescription = HttpWorkerRequest.GetStatusDescription(statusCode);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<html>\r\n    <head>\r\n        <title>{0}</title>\r\n", statusDescription);
            builder.Append("<style>\r\n        \tbody {font-family:\"宋体\";font-weight:normal;font-size: 10pt;color:black;} \r\n        \tp {font-family:\"宋体\";font-weight:normal;color:black;margin-top: -5px}\r\n        \tb {font-family:\"宋体\";font-weight:bold;color:black;margin-top: -5px}\r\n        \tH1 { font-family:\"宋体\";font-weight:normal;font-size:18pt;color:red }\r\n        \tH2 { font-family:\"宋体\";font-weight:normal;font-size:14pt;color:maroon }\r\n        \tpre {font-family:\"宋体\";font-size: 10pt; LINE-HEIGHT: 20px;}\r\n        \t.marker {font-weight: bold; color: black;text-decoration: none;}\r\n        \t.version {color: gray;}\r\n        \t.error {margin-bottom: 10px;}\r\n        \t.expandable { text-decoration:underline; font-weight:bold; color:navy; cursor:hand; }\r\n        </style>\r\n");
            builder.AppendFormat("    </head>\r\n    <body bgcolor=\"white\">\r\n\r\n            <span><H1>Server Error in '{0}' Application.<hr width=100% size=1 color=silver></H1>\r\n\r\n            <h2> <i>HTTP Error {1} - {2}.</i> </h2></span>\r\n\r\n            <hr width=100% size=1 color=silver>\r\n\r\n            <b>版本信息:</b>&nbsp;<a href=\"http://www.Jaylosy.com\" target=\"_black\">网站服务器</a> " + VersionString + "\r\n\r\n    </body>\r\n</html>\r\n", appName, statusCode, statusDescription);
            return builder.ToString();
        }
    }
}

