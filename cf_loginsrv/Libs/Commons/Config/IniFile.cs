using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Commons.Native;

namespace Commons.Config
{
    public static class CIniFile
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
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval,
            int size, string filePath);

        static StringBuilder temp = new StringBuilder(8 * 1024);

        public static string GetIniString(string section, string key, string path)
        {
            //if (CNativeUtil.IsUnix())
            {
                const StringComparison ComparisonMode = StringComparison.Ordinal;
                var datas = File.ReadAllLines(path);
                var correctSection = false;
                foreach (var line in datas)
                {
                    if (line.StartsWith("#", ComparisonMode) ||
                        line.StartsWith(";", ComparisonMode))
                        continue;

                    var v_line = line.Trim();
                    if (v_line.StartsWith("[", ComparisonMode) &&
                        v_line.EndsWith("]", ComparisonMode))
                        correctSection = string.Equals(v_line.Substring(1, v_line.Length - 2), section);

                    if (!correctSection || v_line.IndexOf("=", ComparisonMode) == -1) continue;

                    //var _key = v_line.Substring(0, v_line.IndexOf("=", ComparisonMode));
                    var _key = v_line.Substring(0, v_line.IndexOf("=", ComparisonMode)).Trim();
                    if (!string.Equals(key, _key)) continue;

                    var _value = v_line.Substring(v_line.IndexOf("=", ComparisonMode) + 1,
                        v_line.Length - 1 - v_line.IndexOf("=", ComparisonMode)).Trim();
                    
                    //Console.WriteLine(_key + " " + _value + " (" + v_line + ")");

                    if (v_line.IndexOf("#", ComparisonMode) > 0)
                    {
                        _value = _value.Substring(0, _value.IndexOf("#", ComparisonMode)).Trim();
                    }
                    if (v_line.IndexOf("//", ComparisonMode) > 0)
                    {
                        _value = _value.Substring(0, _value.IndexOf("//", ComparisonMode)).Trim();
                    }
                    // remove annotation
                    
                    //Console.WriteLine(_value);
                    
                    return _value;
                }

                return "";
            }

            //GetPrivateProfileString(section, key, "", temp, 8 * 1024, path);
            //return temp.ToString();
        }

        //private string strFilePath = Environment.CurrentDirectory + "\\FileConfig.ini";
        //private string strSec = ""; 
    }
}