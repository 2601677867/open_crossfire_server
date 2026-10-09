using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct HOST_RUNNING_GAME_USER_INFO
    {
        private byte bUsed;

        private uint iUserID;

        private short iHealth;

        private short iNumKill;

        private short iNumDeath;

        private byte iNumRound;

        private byte iNumWin;

        private short iNumHeadShot;

        private byte iNumRoundAlive;

        private byte bDead;

        private byte bJoinedThisRound;

        private short fTimeToRespawn;

        private short fBuyTimeLeft;

        private WEAPONSLOT eSelectedSlotIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        private short[] tWeaponType;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        private byte[] bZeroStates;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        private string iAmmoLeftInMagazine;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        private short[] iAmmoLeftTotal;

        private byte tArmorTypeKevlar;

        private byte tArmorTypeHelmet;

        private short iArmorPointKevlar;

        private short iArmorPointHelmet;

        private int iEP;

        private int iGP;

        private int iEPExcludeThisRound;

        private int iGPExcludeThisRound;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        private ushort[] aaSackUseCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        private ushort[] iWeaponTypeKillCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        private ushort[] iWeaponTypeHeadshotCount;

        private byte iNumC4Plant;

        private byte iNumC4Explode;

        private byte iNumC4Defuse;

        private byte iTotalDamage;

        private byte iTeamKillCount;
    }
}