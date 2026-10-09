using System.Runtime.InteropServices;
using Network.Protocol;

namespace Network.ProtocolStruct.Gacha
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ST_AI_GACHA_GROUP
    {
        public int m_nGachaID;
        public int m_nMaxDropItemCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_AIGACHA_ITEM_DROP_NUM)]
        public double[] m_nGroupRate;

        public void SetData(int nGachaID, int nGroupID, double dWinRate, int nMaxDropItemCount)
        {
            m_nGachaID = nGachaID;
            m_nGroupRate[nGroupID] = dWinRate;
            m_nMaxDropItemCount = nMaxDropItemCount;
        }
    }
}