using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Achievement
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ST_ACHIEVE_OBJECT
    {
        public int m_nIndex;
        public E_ACHIEVE_CHECK_POINT m_section;
        public E_ACHIEVE_CATEGORY m_category;
        public byte m_cCondition;
        
        public List<ST_SUB_OBJECT> m_subObjects;
        public List<ST_ACHIEVE_PASSIVE> m_passives;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public int[] m_passiveMainCode;

        public long lStartDate;
        public long lEndDate;

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ST_TARGET_INFO
        {
            public byte m_byOrder;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] m_byDefalutValue;

            public byte m_byType;
            public byte m_bySubType;

            public int m_nValue;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ST_REWARD_INFO
        {
            public int m_nRewardItemCount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public E_REWARD_TYPE[] m_reward_type;

            //public int padding;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public long[] m_nRewardValue;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct ST_SUB_OBJECT
        {
            public bool m_bParallel;
            public int m_nStep; // default -1
            public int m_nSubIndex; // default -1
            public int m_nTargetCount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public ST_TARGET_INFO[] m_targets;
            
            public ST_REWARD_INFO m_rewardInfo;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public int[] m_nPassiveLev;
        }
    }
}