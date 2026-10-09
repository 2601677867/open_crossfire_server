using System.Security.Cryptography;
using System.Text;
using Network.ProtocolStruct;

namespace Network.HashMaker
{
    public static class CHashMaker
    {
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        
        public static string CalculateBuddyReturnInfoHash(int iTimeStamp, string szClanID,
            long lUSN, string szCallName, short sServerNo)
        {
            var input = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                iTimeStamp, szClanID, lUSN, szCallName, sServerNo,
                "CrossShotPmang");

            using var md5Hash = MD5.Create();
            return GetMd5Hash(md5Hash, input);
        }
        
        public static string CalculateMMServerReturnHash(PROTO_RETURN_INFO tReturnInfo)
        {
            var input = string.Format("{0:F6}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}",
                tReturnInfo.dConnectTime, tReturnInfo.lTimeStamp, tReturnInfo.szPartKeyDay,
                tReturnInfo.lLoginTime, tReturnInfo.szLoginDate, tReturnInfo.nClanSuperVisor,
                tReturnInfo.nClanUser, tReturnInfo.nClanLLevel, tReturnInfo.szClanSrl,
                tReturnInfo.szClanID, tReturnInfo.szClanUserSrl, tReturnInfo.nSuperVisor, 
                tReturnInfo.nLev, tReturnInfo.ClanUnitInfo.byUnitLevel01,
                "CrossShotPmang");

            using var md5Hash = MD5.Create();
            return GetMd5Hash(md5Hash, input);
        }
        
        public static string CalculateServerConnectHash(PROTO_RETURN_INFO tReturnInfo)
        {
            var input = string.Format("{0:F6}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}",
                tReturnInfo.dConnectTime, tReturnInfo.lTimeStamp, tReturnInfo.szPartKeyDay,
                tReturnInfo.lLoginTime, tReturnInfo.szLoginDate, tReturnInfo.nClanSuperVisor,
                tReturnInfo.nClanUser, tReturnInfo.nClanLLevel, tReturnInfo.szClanSrl,
                tReturnInfo.szClanID, tReturnInfo.szClanUserSrl, tReturnInfo.nSuperVisor, 
                tReturnInfo.nLev, tReturnInfo.ClanUnitInfo.byUnitLevel01,
                "ShotPmangCross");

            using var md5Hash = MD5.Create();
            return GetMd5Hash(md5Hash, input);
        }
    }
}