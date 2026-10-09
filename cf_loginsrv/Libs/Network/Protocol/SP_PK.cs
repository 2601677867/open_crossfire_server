using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.Protocol
{
    public class SP_PK
    {
        public const int PROTOCOL_SUPERVISOR = 4;

        public const int PROTOCOL_SUPERVISOR_NOTIFY_SEND = 1;
        public const int PROTOCOL_SUPERVISOR_NOTIFY_SEND_NOTIFY = 2;
        public const int PROTOCOL_SUPERVISOR_NOTIFY_SEND_CH = 3;
        public const int PROTOCOL_SUPERVISOR_NOTIFY_SEND_CH_NOTIFY = 4;
        public const int PROTOCOL_SUPERVISOR_NOTIFY = 5;
        public const int PROTOCOL_SUPERVISOR_NOTIFY_NTY = 6;
        public const int PROTOCOL_SUPERVISOR_ROLLING = 7;
        public const int PROTOCOL_ROLLING = 8;
        public const int PROTOCOL_SUPERVISOR_ROLLSTOP = 9;
        public const int PROTOCOL_ROLLING_END = 10;
        public const int PROTOCOL_SUPERVISOR_KICK = 11;
        public const int PROTOCOL_SUPERVISOR_KICK_RESULT = 12;
        public const int PROTOCOL_SUPERVISOR_KICK_NTY = 13;
        public const int PROTOCOL_SUPERVISOR_QKICK = 14;
        public const int PROTOCOL_SUPERVISOR_QKICK_RESULT = 15;
        public const int PROTOCOL_SUPERVISOR_QKICK_NTY = 16;
        public const int PROTOCOL_SUPERVISOR_CHATOFF = 17;
        public const int PROTOCOL_SUPERVISOR_CHATOFF_RESULT = 18;
        public const int PROTOCOL_SUPERVISOR_CHATOFF_NTY = 19;
        public const int PROTOCOL_SUPERVISOR_CHATON = 20;
        public const int PROTOCOL_SUPERVISOR_CHATON_RESULT = 21;
        public const int PROTOCOL_SUPERVISOR_CHATON_NTY = 22;
        public const int PROTOCOL_SUPERVISOR_WHISPER = 23;
        public const int PROTOCOL_SUPERVISOR_WHISPER_RESULT = 24;
        public const int PROTOCOL_SUPERVISOR_WHISPER_NTY = 25;
        public const int PROTOCOL_SUPERVISOR_CHANGE_TITLE = 26;
        public const int PROTOCOL_SUPERVISOR_CHANGE_TITLE_RESULT = 27;
        public const int PROTOCOL_SUPERVISOR_UNBLOCK = 28;
        public const int PROTOCOL_SUPERVISOR_UNBLOCK_RESULT = 29;

        public enum GM_MACRO
        {
            GM_MACRO_NONE,
            GM_MACRO_TEMPORARY,
            GM_MACRO_5_MINS,
            GM_MACRO_ETERNAL,
            GM_MACRO_10_DAYS,
            GM_MACRO_30_DAYS,
            GM_MACRO_90_DAYS,
        }

        public enum SP_RESULT
        {
            SP_RESULT_SUCCESS,
            SP_RESULT_ERROR
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_KICK
        {
            public GM_MACRO eMacro;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_KICK_RESULT
        {
            public SP_RESULT eResult;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_QKICK_RESULT
        {
            public SP_RESULT eResult;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_ROLLING_END
        {
            public short nServerID;
            public short nChannelID;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_ROLLSTOP
        {
            public short nServerNo;
            public short nChannelNo;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_NOTIFY_SEND
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_LENGTH)]
            public byte[] aszContent;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_NOTIFY_SEND_CH
        {
            public int nChannel;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_LENGTH)]
            public byte[] aszContent;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_NOTIFY_SEND_CH_NTY
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;

            public PROTO_SUPERVISOR_NOTIFY_SEND_CH tProtoNotifySendCh;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_NOTIFY
        {
            public short nServerNo;
            public short nChannelNo;
            public short nNotifyLength;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_LENGTH)]
            public byte[] aszContent;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_NOTIFY_NTY
        {
            public short nNotifyLength;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_LENGTH)]
            public byte[] aszContent;

            public int GetSize()
            {
                return 2 + nNotifyLength + 1;
            }
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_ROLLING
        {
            public short nServerNo;
            public short nChannelNo;
            public short nRollCount;
            public short nRollLength;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ROLLING_MEMO_LENGTH)]
            public byte[] aszContent;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_WHISPER
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_LENGTH)]
            public byte[] aszContent;
        }
        
         
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_WHISPER_RESULT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_LENGTH)]
            public byte[] aszContent;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_WHISPER_NTY
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszFromCharacterName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_LENGTH)]
            public byte[] aszContent;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_CHANGE_TITLE
        {
            public short nRoomNumbber;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            public byte[] aszNewRoomName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_CHATOFF
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_CHATOFF_RESULT
        {
            public SP_RESULT eResult;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_CHATON
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_CHATON_RESULT
        {
            public SP_RESULT eResult;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_UNBLOCK
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SUPERVISOR_UNBLOCK_RESULT
        {
            public SP_RESULT eResult;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
    }
}