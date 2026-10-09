using System.Collections.Concurrent;
using System.Collections.Generic;
using static Network.Protocol.P_SZ;

namespace cf_loginsrv.Util
{
    public static class CSharedVariable
    {
        public static int iRealAbuseCount;
        public static List<string> aszRealAbuseNames;

        public static int[] aLevelLimits = new int[MAX_LEVEL];
        
        public static readonly ConcurrentDictionary<long, LauncherAuthKey> LSKeys =
            new ConcurrentDictionary<long, LauncherAuthKey>();

        public class LauncherAuthKey
        {
            public string szKey { get; }
            public long lUSN { get; }
            public string szIPAddress { get; set; }

            public LauncherAuthKey(string szKey, long lUSN, string szIPAddress)
            {
                this.szKey = szKey;
                this.lUSN = lUSN;
                this.szIPAddress = szIPAddress;
            }
        }
    }
}