using System.Collections.Generic;
using System.Runtime.InteropServices;
using Network.Protocol;

namespace Network.ProtocolStruct.Achievement
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ST_ACHIEVE_USER_DATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public ST_ACHIEVE_DISPLAY[] m_achieveDisplay;

        public List<ST_OBJECT> m_achieves;
        public List<MM_PK.PROTO_ACHIEVEMENT_RESULT.ST_OBJECT> m_ntyClientAchieves;
        public List<ST_OBJECT> m_ntyAchieves;

        public void GetAchieveEffectValue(ref int nAchieveCallNameEffectValue, ref int nAchieveNameCardEffectValue)
        {
            nAchieveCallNameEffectValue = -1;
            nAchieveNameCardEffectValue = -1;
        }
    }
}