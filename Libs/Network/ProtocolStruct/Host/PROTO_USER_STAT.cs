using System.Runtime.InteropServices;
using Network.SharedFolder;

using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_USER_STAT
    {
        public long iUserID;
        public byte tTeamIndex;
        public short iKill;
        public short iDeath;
        public short iHeadShotDeath;
        public int iAcquiredEP;
        public int iAcquiredGP;
        public short iWinRounds;
        public short iLoseRounds;
        public short iDrawRounds;
        public short iAliveRounds;
        public short iPerfactRounds;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_HOST_STARTDATE)]
        public string szDate;

        public byte bGracefulDisconnect;
        public uint iTime;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SACK_PER_USER)]
        public ushort[] aSackUseCount;

        public ROUNDTYPE eRoundType;
        public CLANGAMETYPE eClanGameType;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WEAPON_CATEGORY2)]
        public ushort[] iWeaponTypeKillCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WEAPON_CATEGORY2)]
        public ushort[] iWeaponTypeHeadshotCount;

        public byte iNumC4Plant;
        public byte iNumC4Explode;
        public byte iNumC4Defuse;
        public ushort iTotalDamage;
        public byte iTeamKillCount;
        public ushort nDoubleKills;
        public byte cEscapeCount;
        // array...
        public byte bShowResult;
        public int nAIScore;
        public int nAIPlayRound;

        public int nAim_Hit_Avg;
        public int nAim_Shot_Avg;
        public int nAim_Headshot_Avg;
        public int nAim_Kill_Avg;
        public int nAim_Assist_Avg;
        public int nAim_Play_Time_Avg;
    }
}