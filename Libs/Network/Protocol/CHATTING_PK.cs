using System.Runtime.InteropServices;
using Network.ProtocolStruct.Achievement;

using static Network.Protocol.P_SZ;

namespace Network.Protocol
{
	public enum MEGAPHONETYPE
	{
		NONE,
		SMALL_MEGAPHONE,
		BIG_MEGAPHONE,
		WORLD_MEGAPHONE
	}
	
	public class CHATTING_PK
	{
		public const int PROTOCOL_CHATTING = 2;
		
		public const int PROTOCOL_CHATTING_SEND = 0;
		public const int PROTOCOL_CHATTING_NOTI = 1;
		public const int PROTOCOL_TEAM_CHATTING_SEND = 2;
		public const int PROTOCOL_TEAM_CHATTING_NOTI = 3;
		public const int PROTOCOL_BROADMSG_SEND = 4;
		public const int PROTOCOL_BROADMSG_NOTI = 5;
		public const int PROTOCOL_WHISPER_SEND = 6;
		public const int PROTOCOL_WHISPER_NOTI = 7;
		public const int PROTOCOL_WHISPER_SEND_RESULT = 8;
		public const int PROTOCOL_MACRO_CHATTING_SEND = 9;
		public const int PROTOCOL_MACRO_CHATTING_SEND_NOTI = 10;
		public const int PROTOCOL_MACRO_TEAM_CHATTING_SEND = 11;
		public const int PROTOCOL_MACRO_TEAM_CHATTING_NOTI = 12;
		public const int PROTOCOL_SPEAKER_NOTICE_CS = 13;
		public const int PROTOCOL_SPEAKER_NOTICE_SC = 14;
		public const int PROTOCOL_TOUR_TEAM_CHAT_SEND = 15;
		public const int PROTOCOL_TOUR_TEAM_CHAT_NOTI = 16;
		public const int PROTOCOL_NANO_TEAM_CHATTING_SEND = 17;
		public const int PROTOCOL_NANO_TEAM_CHATTING_NOTI = 18;
		public const int PROTOCOL_CHAT_FRIEND_CHAT = 19;
		public const int PROTOCOL_WHISPER_SEARCH_USER = 20;
		public const int PROTOCOL_WHISPER_SEARCH_USER_RESULT = 21;

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SPEAKER_NOTICE_CS
		{
			public short nServerNo;
			public short nChannelNo;
			public ushort wLen;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MEGAPHONE_LENGTH)]
			public byte[] aszMsg;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszSender;

			public MEGAPHONETYPE eType;
			[MarshalAs(UnmanagedType.Bool)] public bool bSpecialUser;
			[MarshalAs(UnmanagedType.Bool)] public bool bNoFormat;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CRYPTED_PACKET
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] m_aHashData;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = LENGTH_SOCKET_BUFFER)]
			public byte[] m_aData;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHATTING_SEND
		{
			public byte tUserType;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_MESSAGE_LENGTH)]
			public byte[] szContent;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MACRO_CHATTING_SEND
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MACRO_CHATTING_LENGTH)]
			public byte[] szContent;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHATTING_NOTI
		{
			[MarshalAs(UnmanagedType.I1)] public bool bSupervisor;
#if USE_CFVIP_SYSTEM
			public byte tUserType;
			public byte bySpecialUserLevel;
#endif
			public byte nChattingColorIndex;
			public byte nCallNameColorIndex;
			public ST_ACHIEVE_DISPLAY achieveDisplayInfo;
			public long nUserID;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string szCallName;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHATTING_MESSAGE_LENGTH)]
			public string szContent;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ENCRYPTED_CHATTING
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] m_aHashData;
			public short nLength;
			
			public byte byFirst;
			public byte bySecond;
			public byte byThird;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_MESSAGE_LENGTH)]
			public byte[] m_szContent;
		}
	}
}
