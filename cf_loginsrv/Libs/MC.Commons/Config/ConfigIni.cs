using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MC.Commons.Config
{
    public class IniFile
    {
        /// <summary>
        ///     写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        /// <summary>
        ///     读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval,
            int size, string filePath);

        public static string GetIniString(string section, string key, string path)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                var datas = File.ReadAllLines(path);
                var correctSection = false;
                foreach (var line in datas)
                {
                    if (line.StartsWith("#", StringComparison.Ordinal) ||
                        line.StartsWith(";", StringComparison.Ordinal))
                        continue;

                    var v_line = line.Trim();
                    if (v_line.StartsWith("[", StringComparison.Ordinal) &&
                        v_line.EndsWith("]", StringComparison.Ordinal))
                    {
                        if (v_line.Substring(1, v_line.Length - 2).ToLower() == section.ToLower())
                            correctSection = true;
                        else
                            correctSection = false;
                    }

                    if (correctSection && v_line.IndexOf("=", StringComparison.Ordinal) != -1)
                    {
                        var _key = v_line.Substring(0, v_line.IndexOf("=", StringComparison.Ordinal) - 1);
                        if (key.ToLower() == _key.Trim().ToLower())
                        {
                            var _value = v_line.Substring(v_line.IndexOf("=", StringComparison.Ordinal) + 1,
                                    v_line.Length - 1 - v_line.IndexOf("=", StringComparison.Ordinal))
                                .Trim();
                            return _value;
                        }
                    }
                }

                return "";
            }

            var temp = new StringBuilder(1024);
            GetPrivateProfileString(section, key, "", temp, 1024, path);
            return temp.ToString();
        }

        //private string strFilePath = Environment.CurrentDirectory + "\\FileConfig.ini";
        //private string strSec = ""; 
    }
}