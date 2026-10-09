using System;
using System.Text;

namespace Commons.Native
{
    public static class CBase64
    {
        public static string Encode(byte[] aBytes)
        {
            return Convert.ToBase64String(aBytes);
        }
        
        public static string Encode(string szToEncode)
        {
            var aBytes = Global.Encoding.GetBytes(szToEncode);
            return Convert.ToBase64String(aBytes);
        }
        
        public static byte[] DecodeToByteArr(string szEncoded)
        {
            return Convert.FromBase64String(szEncoded);
        }
        
        public static string Decode(string szEncoded)
        {
            var data = Convert.FromBase64String(szEncoded);
            return Global.Encoding.GetString(data);
        }
    }
}