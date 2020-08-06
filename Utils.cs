namespace WMSServer
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class Utils
    {
        public static string CutString(string str, int len)
        {
            Regex regex = new Regex(@"^[\u4e00-\u9fa5]+$", RegexOptions.Compiled);
            char[] chArray = str.ToCharArray();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            for (int i = 0; i < chArray.Length; i++)
            {
                if (regex.IsMatch(chArray[i].ToString()))
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num <= len)
                {
                    builder.Append(chArray[i]);
                }
                else
                {
                    break;
                }
            }
            if (builder.ToString() != str)
            {
                builder.Append("...");
            }
            return builder.ToString();
        }

        public static bool InArray(int id, string stringArray)
        {
            return InArray(id, stringArray, ",");
        }

        public static bool InArray(string str, string stringArray)
        {
            return InArray(str, stringArray, ",");
        }

        public static bool InArray(int id, string stringArray, string strsplit)
        {
            return InArray(id.ToString(), stringArray, strsplit);
        }

        public static bool InArray(string str, string stringArray, string strsplit)
        {
            if ((stringArray != null) && (stringArray != ""))
            {
                string[] strArray = SplitString(stringArray, strsplit);
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i] == str)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
        }

        public static bool IsValidDomain(string host)
        {
            if (((host == "localhost") || (host == "http://localhost")) || (host == "*"))
            {
                return true;
            }
            Regex regex = new Regex(@"^\d+$");
            if (host.IndexOf(".") == -1)
            {
                return false;
            }
            return !regex.IsMatch(host.Replace(".", string.Empty));
        }

        public static void OpenUrl(string host, int port)
        {
            OpenUrl(host, port, "");
        }

        public static void OpenUrl(string host, int port, string pagename)
        {
            if ((host == "*") || (host == ""))
            {
                host = "localhost";
            }
            string url = "http://" + host.TrimEnd(new char[] { ':' });
            if (port != 80)
            {
                url = url + ":" + port;
            }
            if (pagename != "")
            {
                url = url + "/" + pagename;
            }
            StartHttp(url);
        }

        public static string RunCmd(string command)
        {
            Process process = new Process();
            string str = string.Empty;
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            try
            {
                if (process.Start())
                {
                    process.StandardInput.WriteLine(command);
                    process.StandardInput.WriteLine("exit");
                    str = process.StandardOutput.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "文件目录错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (process != null)
                {
                    process.Close();
                }
                process = null;
            }
            return str;
        }

        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                return new string[] { strContent };
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }

        public static string[] SplitString(string strContent, string strSplit, int p_3)
        {
            string[] strArray = new string[p_3];
            string[] strArray2 = SplitString(strContent, strSplit);
            for (int i = 0; i < p_3; i++)
            {
                if (i < strArray2.Length)
                {
                    strArray[i] = strArray2[i];
                }
                else
                {
                    strArray[i] = string.Empty;
                }
            }
            return strArray;
        }

        public static void StartHttp(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                Process.Start("IEXPLORE.EXE", url);
            }
        }

        public static int StrToInt(object Expression)
        {
            if (Expression != null)
            {
                string input = Expression.ToString();
                if ((((input.Length > 0) && (input.Length <= 11)) && Regex.IsMatch(input, "^[-]?[0-9]*$")) && (((input.Length < 10) || ((input.Length == 10) && (input[0] == '1'))) || (((input.Length == 11) && (input[0] == '-')) && (input[1] == '1'))))
                {
                    return Convert.ToInt32(input);
                }
            }
            return 0;
        }
    }
}

