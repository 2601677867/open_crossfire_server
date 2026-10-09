using System;
using System.Runtime.InteropServices;
using Network.ProtocolStruct;
using Network.ProtocolStruct.Achievement;
using static Network.Protocol.P_SZ;

// ReSharper disable FieldCanBeMadeReadOnly.Global

#pragma warning disable 169

namespace Network.Protocol
{
    public static class BUDDY_PK
    { 
	    public const int PROTOCOL_NEW_BUDDY_CTM = 191; 		// Client -> MM
	    public const int PROTOCOL_NEW_BUDDY_MTC = 192; 		// MM -> Client
		public const int PROTOCOL_NEW_BUDDY_MTR = 193; 		// MM -> Relay
		public const int PROTOCOL_NEW_BUDDY_RTM = 194; 		// Relay -> MM
		public const int PROTOCOL_NEW_BUDDY_BCAST = 195; 	// 1st MM -> Relay -> 2nd MM
		public const int PROTOCOL_NEW_BUDDY_GROUP = 196;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL = 199; // Internal Push Packet
		
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_PULSE = 0;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_SECOND_LOGIN = 1;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_LOCAL_USER_LOCATION_CHANGED = 10;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_SECOND_LOCAL_CLIENT_CLOSED = 11;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_LOCAL_CLIENT_INFO_CHANGED = 12;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_SUGGEST_FRIEND_POINT = 13;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_SUGGEST_FRIEND_REGISTER = 14;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_SUGGEST_FRIEND_DELETE = 15;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_SUGGEST_FRIEND_GRADUATE = 16;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_SUGGEST_FRIEND_STATE = 17;
		public const int PROTOCOL_NEW_BUDDY_INTERNAL_ACHIEVE_CHANGE = 18;
		
		public const int PROTOCOL_NEW_BUDDY_HEARTBEAT = 0;
		public const int PROTOCOL_NEW_BUDDY_DUMPED_ALL_NTY = 1;
		public const int PROTOCOL_NEW_BUDDY_DIAG_ECHO_STRING = 9;
		
		public const int PROTOCOL_NEW_BUDDY_MM_CONNECT_REQUEST = 100;
		public const int PROTOCOL_NEW_BUDDY_RESTORE_REQUEST = 102;
		public const int PROTOCOL_NEW_BUDDY_RESTORE_BUDDY_LIST = 103;
		public const int PROTOCOL_NEW_BUDDY_RESTORE_BUDDY_LIST_END = 104;
		public const int PROTOCOL_NEW_BUDDY_RELAY_STARTED = 110;
		
		public const int PROTOCOL_NEW_BUDDY_USER_LOGIN_REQUEST = 202;
		public const int PROTOCOL_NEW_BUDDY_CACHED_USER_LOGIN_REQUEST = 210;
		public const int PROTOCOL_NEW_BUDDY_CACHED_USER_LOGIN_RESULT = 211;

		public const int PROTOCOL_NEW_BUDDY_USER_LOCATION_CHANGED = 301;
		public const int PROTOCOL_NEW_BUDDY_REQUEST_LOCATION = 302;
		public const int PROTOCOL_NEW_BUDDY_REQUEST_LOCATION_RESULT = 303;
		public const int PROTOCOL_NEW_BUDDY_USER_LOGOUT = 304;
		public const int PROTOCOL_NEW_BUDDY_LARGE_VAR_USER_LOCATION = 305;
		public const int PROTOCOL_NEW_BUDDY_FRIEND_INFO_CHANGED = 310;
		public const int PROTOCOL_NEW_BUDDY_LARGE_VAR_FRIEND_INFO_CHANGED = 311;
		public const int PROTOCOL_NEW_BUDDY_BUDDY_GET_IDCARD = 320;
		public const int PROTOCOL_NEW_BUDDY_BUDDY_GET_IDCARD_RESULT = 321;
		
		public const int PROTOCOL_NEW_BUDDY_ADD_BUDDY = 401;
		public const int PROTOCOL_NEW_BUDDY_ADD_BUDDY_RESULT = 402;
		public const int PROTOCOL_NEW_BUDDY_ADD_BUDDY_ADDED = 403;
		public const int PROTOCOL_NEW_BUDDY_BUDDY_ADD_AGREE = 404;
		public const int PROTOCOL_NEW_BUDDY_BUDDY_ADD_AGREE_RESULT = 405;
		public const int PROTOCOL_NEW_BUDDY_BUDDY_ADD_AGREE_RESULT_NTY = 406;
		public const int PROTOCOL_NEW_BUDDY_BUDDY_REMOVE = 407;
		public const int PROTOCOL_NEW_BUDDY_BUDDY_REMOVE_RESULT = 408;
		public const int PROTOCOL_NEW_BUDDY_BUDDY_REMOVED = 409;
		public const int PROTOCOL_NEW_BUDDY_BLOCK_LIST_REQUEST = 410;
		public const int PROTOCOL_NEW_BUDDY_BLOCK_LIST_RESULT = 411;
		public const int PROTOCOL_NEW_BUDDY_BLOCK = 412;
		public const int PROTOCOL_NEW_BUDDY_BLOCK_RESULT = 413;
		public const int PROTOCOL_NEW_BUDDY_UNBLOCK = 414;
		public const int PROTOCOL_NEW_BUDDY_UNBLOCK_RESULT = 415;
		
		public const int PROTOCOL_NEW_BUDDY_CLAN_MEMBER_LIST = 501;
		public const int PROTOCOL_NEW_BUDDY_CLAN_MEMBER_LOGIN = 502;
		public const int PROTOCOL_NEW_BUDDY_CLAN_MEMBER_LOCATION_CHANGED = 503;
		
		public const int PROTOCOL_NEW_BUDDY_CHAT_WHISPER = 601;
		public const int PROTOCOL_NEW_BUDDY_CHAT_WHISPER_RESULT = 602;
		public const int PROTOCOL_NEW_BUDDY_CLAN_CHAT = 603;
		public const int PROTOCOL_NEW_BUDDY_CLAN_CHAT_NTY = 604;
		public const int PROTOCOL_NEW_BUDDY_FRIEND_CHAT = 605;
		public const int PROTOCOL_NEW_BUDDY_FRIEND_CHAT_RESULT = 606;
		public const int PROTOCOL_NEW_BUDDY_FRIEND_CHAT_2 = 607;
		public const int PROTOCOL_NEW_BUDDY_CHAT_WHISPER_SEARCH_USER = 608;
		public const int PROTOCOL_NEW_BUDDY_CHAT_WHISPER_SEARCH_USER_RESULT = 609;
		
		private const int PROTOCOL_NEW_BUDDY_INVITE_BASE = 700;
		public const int PROTOCOL_NEW_BUDDY_INVITE = PROTOCOL_NEW_BUDDY_INVITE_BASE + 1;
		public const int PROTOCOL_NEW_BUDDY_INVITE_RESULT = PROTOCOL_NEW_BUDDY_INVITE_BASE + 2;
		public const int PROTOCOL_NEW_BUDDY_INVITED = PROTOCOL_NEW_BUDDY_INVITE_BASE + 3;
		public const int PROTOCOL_NEW_BUDDY_INVITE_RESPONSE = PROTOCOL_NEW_BUDDY_INVITE_BASE + 4;
		public const int PROTOCOL_NEW_BUDDY_INVITE_RESPONSE_RESULT = PROTOCOL_NEW_BUDDY_INVITE_BASE + 5;
		public const int PROTOCOL_NEW_BUDDY_AUTOLEAGUE_GAME_INVITE = PROTOCOL_NEW_BUDDY_INVITE_BASE + 6;
		public const int PROTOCOL_NEW_BUDDY_AUTOLEAGUE_GAME_INVITE_RESULT = PROTOCOL_NEW_BUDDY_INVITE_BASE + 7;
		public const int PROTOCOL_NEW_BUDDY_AUTOLEAGUE_GAME_INVITED = PROTOCOL_NEW_BUDDY_INVITE_BASE + 8;
		public const int PROTOCOL_NEW_BUDDY_INVITE_AUTOLEAGUE_GAME_INVITE_RESPONSE = PROTOCOL_NEW_BUDDY_INVITE_BASE + 9;
		
		public const int PROTOCOL_NEW_BUDDY_POST_SEND = 803;
		public const int PROTOCOL_NEW_BUDDY_POST_SEND_RESULT = 804;
		public const int PROTOCOL_NEW_BUDDY_POST_DELETE = 805;
		public const int PROTOCOL_NEW_BUDDY_POST_DELETE_RESULT = 806;
		public const int PROTOCOL_NEW_BUDDY_POST_GET_MESSAGE = 807;
		public const int PROTOCOL_NEW_BUDDY_POST_GET_MESSAGE_RESULT = 808;
		public const int PROTOCOL_NEW_BUDDY_POST_LIST_REQUEST = 809;
		public const int PROTOCOL_NEW_BUDDY_POST_LIST = 810;
		public const int PROTOCOL_NEW_BUDDY_POST_LIST_END = 811;
		public const int PROTOCOL_NEW_BUDDY_POST_SEND_NOTIFY = 812;
		public const int PROTOCOL_NEW_BUDDY_POST_DELETE_ALL_REQUEST = 816;
		public const int PROTOCOL_NEW_BUDDY_POST_DELETE_ALL_RESULT = 817;

		public const int PROTOCOL_NEW_BUDDY_SEARCH_USER = 901;
		public const int PROTOCOL_NEW_BUDDY_SEARCH_USER_RESULT = 902;
		public const int PROTOCOL_NEW_BUDDY_ADD_BY_SEARCH = 903;
		
		public const int PROTOCOL_NEW_BUDDY_SUGGEST_FRIEND_POINT = 1000;
		public const int PROTOCOL_NEW_BUDDY_SUGGEST_FRIEND_REGISTER = 1001;
		public const int PROTOCOL_NEW_BUDDY_SUGGEST_FRIEND_DELETE = 1002;
		public const int PROTOCOL_NEW_BUDDY_SUGGEST_FRIEND_GRADUATE = 1004;
		public const int PROTOCOL_NEW_BUDDY_SUGGEST_FRIEND_STATE = 1005;
		
		public const int PROTOCOL_NEW_BUDDY_GROUP_INFO = 1101;
		public const int PROTOCOL_NEW_BUDDY_CHANGE_FRIEND_MEMO_NAME_REQUEST = 1102;
		public const int PROTOCOL_NEW_BUDDY_CHANGE_FRIEND_MEMO_NAME_RESULT = 1103;
		public const int PROTOCOL_NEW_BUDDY_CREATE_GROUP_REQUEST = 1104;
		public const int PROTOCOL_NEW_BUDDY_CREATE_GROUP_RESULT = 1105;
		public const int PROTOCOL_NEW_BUDDY_CHANGE_GROUP_NAME_REQUEST = 1106;
		public const int PROTOCOL_NEW_BUDDY_CHANGE_GROUP_NAME_RESULT = 1107;
		public const int PROTOCOL_NEW_BUDDY_DELETE_GROUP_REQUEST = 1108;
		public const int PROTOCOL_NEW_BUDDY_DELETE_GROUP_RESULT = 1109;
		public const int PROTOCOL_NEW_BUDDY_GROUP_REORDER_REQUEST = 1110;
		public const int PROTOCOL_NEW_BUDDY_GROUP_REORDER_RESULT = 1111;
		public const int PROTOCOL_NEW_BUDDY_CHANGE_GROUP_MEMBER_NOTIFY_REQUEST = 1112;
		public const int PROTOCOL_NEW_BUDDY_CHANGE_GROUP_MEMBER_NOTIFY_RESULT = 1113;
		public const int PROTOCOL_NEW_BUDDY_DESIGNATE_GROUP_REQUEST = 1114;
		public const int PROTOCOL_NEW_BUDDY_DESIGNATE_GROUP_RESULT = 1115;
		
		public const int PROTOCOL_NEW_BUDDY_CHANGE_AWAY_STATUS = 1201;
		public const int PROTOCOL_NEW_BUDDY_CHANGE_SHOW_OFFINE_REQUEST = 1202;

		public const int PROTOCOL_NEW_BUDDY_ACHIEVE_CHANGE_NTY = 2001;

		public enum BUDDY_RELAY_READY_STATE
		{
			NOT_READY,
			RESTORING,
			READY,
			READY2
		}
		
		[Flags]
		public enum BUDDY_FRIEND_INFO_CHANGE_FLAG
		{
			FLAG_NONE = 0,
			FLAG_LOCATION_CHANGED = 1,
			FLAG_LEVEL_CHANGED = 2,
			FLAG_NAMECARD_CHANGED = 4,
			FLAG_BADGE_CHANGED = 8,
			FLAG_CALLNAME_COLOR_INDEX_CHANGED = 16,
			FLAG_PAY_USER = 32,
			FLAG_ACHIEVE_CHANGED = 64,
			FLAG_GREEN_COMMUNITY_USER = 128,
			FLAG_CLAN_CHANGED = 256,
			FLAG_RANKED_MATCH = 512,
			FLAG_RANKED_MATCH_NEW = 1024
		}
		
		public enum E_BUDDY_STATE : byte
		{
			STATE_ONLINE,
			STATE_INGAME,
			STATE_REFUSED,
			STATE_AWAY,
			STATE_OFFLINE
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_GET_IDCARD
		{
			public long lUSN;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_GET_IDCARD_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				FAIL_1,
				CANNOT_GET_IDCARD,
				DBERROR
			}
			
			public RESULT eResult;
			public long lUSN;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
			
			public PROTO_MYCLANINFO tClan;
			public byte byDummy;
			public short nLevel;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1237)]
			public byte[] aDummy;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHANGE_AWAY_STATUS
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bAway;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_FRIEND_CHAT
		{
			public long lUSN;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszSenderCallName;

			public byte byPayUserLevel;
			public ST_ACHIEVE_DISPLAY tAchieveDisplay;
			public int iMessageLength;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FRIEND_CHAT_CONTENT_LENGTH)]
			public byte[] aszMessage;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_FRIEND_CHAT_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				FAILED
			}

			public RESULT eResult;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_REMOVE
		{
			public long lUSN;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_REMOVE_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				NOT_FOUND_IN_BUDDY_LIST,
				CANNOT_GET_REMOVER_CI
			}
			
			public RESULT eResult;
			public long lUSN;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_REMOVE_RESULT_RELAY
		{
			public PROTO_NEW_BUDDY_REMOVE_RESULT.RESULT eResult;
			public long lRemovedUSN;
			public long lRemoverUSN;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_REMOVED
		{
			public long lUSN;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_ADD_AGREE
		{
			public long lUSN;
			public byte byAccepted;
			public E_BUDDY_STATE eBuddyState;
			public long lPostSrl;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_ADD_AGREE_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				REFUSED,
				NOT_EXIST,
				ALREADY_BUDDY,
				TOO_MUCH_BUDDY,
				DB_ERROR,
				DB_ERROR2,
				UNKNOWN_ERROR
			}

			public RESULT eResult;
			public PROTO_NEW_BUDDY_USER_INFO tTargetInfo;
			public PROTO_NEW_BUDDY_USER_INFO tMyInfo;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_ADD_AGREE_RESULT_NTY
		{
			public PROTO_NEW_BUDDY_USER_INFO tUserInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_USER_LOCATION_REQUEST
		{
			public long lMyUSN;
			public long lTargetUSN;
			public int eReason;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHAT_WHISPER
		{
			public int iMessageLength;
			public byte byPayUser;
			public byte byPayUserLevel;
			public ST_ACHIEVE_DISPLAY tAchieveDisplayInfo;
			public long lSenderUSN;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszSenderCallName;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszRecverCallName;
			public byte byChattingColorIndex;
			public byte byCallNameColorIndex;
			public byte bySeq;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WHISPER_CONTENT_LENGTH)]
			public byte[] aszContent;

			public int GetSize()
			{
				return 55 + iMessageLength + 1;
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHAT_WHISPER_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				CANNOT_REACH,
				FAILED
			}
			public RESULT eResult;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszRecverCallName;
			public byte bySeq;
			
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHAT_WHISPER_SEARCH_USER
		{
			public long lRequesterUSN;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHAT_WHISPER_SEARCH_USER_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				OFFLINE,
				USER_NOT_EXIST
			}
			
			public RESULT eResult;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
		}

		public enum NEW_BUDDY_USER_LOCATION_REQUEST_REASON
		{
			CLIENT_REQUESTED,
			GM_REQUESTED
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_USER_LOCATION_REQUEST_RESULT
		{
			public long lTargetUSN;
			public BUDDY_LOCATION_INFO tLocation;
			public long lRequesterUSN;
			public int eReason;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_INVITE
		{
			public long lInviteeUSN;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_INVITE_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				CANNOT_RECEIVE,
				ROOM_IS_FULL,
				NOT_YOUR_FRIEND,
				CLAN_SERVER_LIMIT,
				CANNOT_RECEIVE3,
			}
			
			public long lInviteeUSN;
			public RESULT eResult;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_INVITED
		{
			public long lInviterUSN;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
			
			public long lInviteeUSN;
			public byte byIsClanServer;
			public BUDDY_LOCATION_INFO tLocation;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_ROOM_PW_MAXLENGTH)]
			public byte[] aszRoomPW;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_NEW_BUDDY_INVITE_RESPONSE
		{
			public long lInviterUsn;
			public long lInviteeUsn;
			public RESULT eResult;

			public enum RESULT
			{
				ACCEPT,
				DENY,
				CANNOT_INVITE
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_LARGE_VAR_FRIEND_INFO_CHANGED
		{
			public short nCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_COUNT)]
			public PROTO_NEW_BUDDY_FRIEND_INFO_CHANGED[] tInfos;

			public ushort GetSize()
			{
				return (ushort) (sizeof(short) + nCount * Marshal.SizeOf<PROTO_NEW_BUDDY_FRIEND_INFO_CHANGED>());
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_LARGE_VAR_USER_LOCATION
		{
			public short nCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_COUNT)]
			public PROTO_USER_LOCATION[] tLocations;
			
			public ushort GetSize()
			{
				return (ushort) (sizeof(short) + nCount * Marshal.SizeOf<PROTO_USER_LOCATION>());
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHANGE_FRIEND_MEMO_NAME_REQUEST
		{
			public long lFriendUSN;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MEMO_NAME_LENGTH)]
			public byte[] aszMemoName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHANGE_FRIEND_MEMO_NAME_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				CI_IS_NULL,
				REFEREE_CI_IS_NULL,
				NAME_IS_EMPTY,
				DBFAIL,
				UNKNOWNERROR
			}
			
			public RESULT eResult;
			public PROTO_NEW_BUDDY_CHANGE_FRIEND_MEMO_NAME_REQUEST tReqData;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_GROUP_REORDER_REQUEST
		{
			public enum ACTION : byte
			{
				MOVE_DOWN,
				MOVE_UP
			}
			
			public ACTION eAction;
			public int iGroupIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_GROUP_REORDER_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				CI_IS_NULL,
				GROUP_NOT_FOUND,
				DBFAIL,
				FAIL
			}
			
			public RESULT eResult;
			public PROTO_NEW_BUDDY_GROUP_REORDER_REQUEST tReqData;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_DELETE_GROUP_REQUEST
		{
			public int iGroupIndex;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_DELETE_GROUP_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				CI_IS_NULL,
				GROUP_NOT_FOUND,
				DBFAIL,
				UNKNOWNERROR
			}
			
			public RESULT eResult;
			public PROTO_NEW_BUDDY_DELETE_GROUP_REQUEST tReqData;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHANGE_SHOW_OFFLINE_REQUEST
		{
			public byte byShowOffline;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHANGE_GROUP_NAME_REQUEST
		{
			public int iGroupIndex;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_GROUP_NAME_LENGTH)]
			public byte[] aszGroupName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHANGE_GROUP_NAME_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				CI_IS_NULL,
				NAME_EXISTS,
				GROUP_NOT_FOUND,
				DBFAIL,
				UNKNOWNERROR,
				NAME_LENGTH_WRONG
			}
			public RESULT eResult;
			public PROTO_NEW_BUDDY_CHANGE_GROUP_NAME_REQUEST tReqData;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHANGE_GROUP_MEMBER_NOTIFY_REQUEST
		{
			public int iGroupIndex;
			public byte byGroupMemberNotify;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CHANGE_GROUP_MEMBER_NOTIFY_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				CI_IS_NULL,
				GROUP_NOT_FOUND,
				DBFAIL,
				UNKNOWNERROR
			}
			
			public RESULT eResult;
			public byte byGroupMemberNotify;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CREATE_GROUP_REQUEST
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_GROUP_NAME_LENGTH)]
			public byte[] aszGroupName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CREATE_GROUP_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				CI_IS_NULL,
				GROUP_FULL,
				NAME_EXISTS,
				DBFAIL,
				UNKNOWNERROR,
				NAME_LENGTH_WRONG
			}
			
			public RESULT eResult;
			public PROTO_NEW_BUDDY_CREATE_GROUP_REQUEST tReqData;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_DESIGNATE_GROUP_REQUEST
		{
			public long lFriendUSN;
			public int iGroupIndex;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_DESIGNATE_GROUP_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				DBFAIL,
				UNKNOWNERROR,
				CI_IS_NULL
			}
			
			public RESULT eResult;
			public PROTO_NEW_BUDDY_DESIGNATE_GROUP_REQUEST tReqData;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_GROUP
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_GROUP_NAME_LENGTH)]
			public byte[] aszGroupName;
			
			public byte byGroupOnlineNotify;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_GROUP_INFO
		{
			public byte byShowOffline;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_GROUP_NAME_LENGTH)]
			public byte[] aszDefaultGroupName;
			
			public byte byDefaultGroupOnlineNotify;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_GROUP_COUNT)]
			public PROTO_NEW_BUDDY_GROUP[] aGroups;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct BUDDY_MY_BUDDY
		{
			public long lUSN;
			public bool bAccepted;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CLIENT_LOGIN_RESULT
		{
			public long lMyUSN;
			public int iBuddyCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_COUNT)]
			public PROTO_NEW_BUDDY_USER_INFO[] aMyBuddies;

			public int GetSize()
			{
				return 12 + iBuddyCount * Marshal.SizeOf<PROTO_NEW_BUDDY_USER_INFO>();
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_USER_INFO // 129 Bytes
		{
			public long lUSN;
			public byte byLevel;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;

			public long lClanKey;

			public BUDDY_LOCATION_INFO tLocation;

			public byte byAccepted;
			public byte byDummy;

			public PROTO_NEW_BUDDY_USER_DETAIL_INFO tUserDetailInfo;

			public PROTO_NEW_BUDDY_FRIEND_GROUP_INFO tGroupInfo;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CLAN_MEMBER
		{
			public long lUSN;
			public byte byLevel;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CLANID_LENGTH)]
			public byte[] aszClanID;

			public BUDDY_LOCATION_INFO tLocation;
			public E_BUDDY_STATE eBuddyState;
			public int iOfflineTime;
			public int iDummy;

			public PROTO_NEW_BUDDY_USER_DETAIL_INFO tUserDetailInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_RESTORE_USER_INFO
		{
			public long lUSN;
			
			public byte byLevel;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CLANID_LENGTH)]
			public byte[] aszClanID;

			public PROTO_USER_LOCATION tLocation;
			
			public PROTO_NEW_BUDDY_USER_DETAIL_INFO tUserDetailInfo;

			public short nMyBuddyCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_COUNT)]
			public BUDDY_MY_BUDDY[] aMyBuddies;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_RESTORE_BUDDY_LIST
		{
			public byte byServerID;
			public short nList;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_RESTORE_LIST)]
			public PROTO_NEW_BUDDY_RESTORE_USER_INFO[] aUserInfos;

			public ushort GetSize()
			{
				return (ushort) (3 + nList * Marshal.SizeOf<PROTO_NEW_BUDDY_RESTORE_USER_INFO>());
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_DIAG_ECHO_STRING
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
			public string szEchoString;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_FRIEND_GROUP_INFO
		{
			public int iGroupNumber;
			public E_BUDDY_STATE eBuddyState;
			public int iOfflineTime;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MEMO_NAME_LENGTH)]
			public byte[] aszMemoName;

			public int iDummy;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_FRIEND_INFO_CHANGED
		{
			public BUDDY_FRIEND_INFO_CHANGE_FLAG eReasonFlag;
			public PROTO_NEW_BUDDY_USER_INFO tFriendInfo;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CLANID_LENGTH)]
			public byte[] aszClanID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_USER_LOGOUT
		{
			public long lUSN;
			public sbyte byServerNo;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_INTERNAL_SECOND_LOCAL_CLIENT_CLOSED
		{
			public long lUSN;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CLANID_LENGTH)]
			public byte[] aszClanID;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CLAN_MEMBER_LOGIN
		{
			public int nCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CLAN_USER_COUNT)]
			public PROTO_NEW_BUDDY_CLAN_MEMBER[] tClanMember;

			public int GetSize()
			{
				return 4 + Marshal.SizeOf<PROTO_NEW_BUDDY_CLAN_MEMBER>() * nCount;
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CLAN_MEMBER_LOCATION
		{
			public PROTO_USER_LOCATION tUserLocation;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CLANID_LENGTH)]
			public byte[] aszClanID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_USER_LOCATION
		{
			public long lUSN;
			public BUDDY_LOCATION_INFO tNewLocation;
			public E_BUDDY_STATE eBuddyState;
			public int iOfflineTime;
			public int iDummy;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_RELAY_READY
		{
			public BUDDY_RELAY_READY_STATE eState;
			public byte byDummy;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_USER_DETAIL_INFO
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ITEM_CODE)]
			public byte[] aszNameCardItemCode; 
			
			public sbyte byMainBadgeType;
			public sbyte byMainBadgeLevel;
			public byte byCallNameColorIndex;
			[MarshalAs(UnmanagedType.I1)] public bool bPayUser;
			public byte byPayUserLevel;
			[MarshalAs(UnmanagedType.I1)] public bool bGreenCommunityUser;
			
			public int nAchieveCallNameEffectValue;
			public int nAchieveNameCardEffectValue;
			
			public ST_ACHIEVE_DISPLAY tAchieveDisplayInfo;

			public short wFameGrade;
			public int iClanAccum;
			public byte byRankingLevel;
			public byte byDisplayRankingLevel;
			
			// FLAG_RANKED_MATCH
			public byte byDummy0_1;
			public byte byDummy0_2;
			public byte byDummy0_3;
			public byte byDummy0_4;
			public byte byDummy0_5;
			public byte byDummy0_6;

			// useless data
			public int iDummy; // -1
			
			// FLAG_RANKED_MATCH_NEW
			public byte byNewRankingLevel;
			public byte byDisplayNewRankingLevel;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_INTERNAL_SECOND_LOGIN
		{
			public int iCurTime;
			public int iClientKey;
			public long lUSN;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CLANID_LENGTH)]
			public byte[] aszClanID;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
			
			public byte byLevel;

			public PROTO_NEW_BUDDY_USER_DETAIL_INFO tUserDetailInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CACHED_USER_LOGIN_REQUEST
		{
			public PROTO_NEW_BUDDY_INTERNAL_SECOND_LOGIN tLoginReq;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_CACHED_USER_LOGIN_RESULT
		{
			public enum CACHED_STATE
			{
				FAIL,
				CACHED_STATE_1,
				CACHED
			}
			
			public CACHED_STATE eResult;
			public PROTO_NEW_BUDDY_INTERNAL_SECOND_LOGIN tMyInfo;

			public short nBuddyCount;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUDDY_COUNT)]
			public BUDDY_MY_BUDDY[] aMyBuddies; // 65bytes each (should be)
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_MM_CONNECT
		{
			public int iServerKey;
			public uint iAddrInet;
			public int iPort;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_BLOCK
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_BLOCK_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				NO_SUCH_USER,
				CANNOT_BLOCK_SELF,
				ALREADY_EXISTS,
				BLOCK_LIST_FULL,
				EXECUTE_QUERY_FAIL,
				FAIL_6,
				CANNOT_GET_CI
			}
			
			public RESULT eResult;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_UNBLOCK
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_UNBLOCK_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				NO_SUCH_USER,
				CANNOT_BLOCK_SELF,
				NAME_IS_NOT_IN_SERVER,
				EXECUTE_QUERY_FAIL,
				CANNOT_GET_CI
			}
			
			public RESULT eResult;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct CALLNAME
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_BLOCK_LIST_RESULT
		{
			public int iBlockCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public CALLNAME[] aBlockList;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_POST_DELETE_ALL_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				FAIL_1,
				FAIL_2,
				UNDEFINED,
				FAIL_4
			}
			
			public RESULT eResult;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_POST_GET_MESSAGE
		{
			public long lPostSrl;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_POST_DELETE_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				FAIL_1,
				FAIL_2,
				FAIL_3,
				UNKNOWNERROR
			}
			
			public RESULT eResult;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_POST_DELETE
		{
			public long lPostSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_POST_GET_MESSAGE_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				FAIL_1,
				FAIL_2,
				FAIL_3,
				UNKNOWNERROR
			}
			
			public RESULT eResult;
			public long lPostSrl;
			public char chReadYN;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 120)]
			public byte[] aszContent;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_POST_LIST
		{
			public long lPostSrl;
			public long lFromUSN;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszFromNick;
			
			public char chType;
			public char chReadYN;
			public int iUDate;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 118)]
			public byte[] aszContent;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_POST_LIST_END
		{
			public enum RESULT
			{
				SUCCESS,
				UNKNOWN_ERROR,
				EXECUTE_QUERY_FAILED
			}

			public RESULT eResult;
			public int nDailySentPostCount;
			public int nDailyOfflineFriendMessageCount;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_POST_NOTIFY
		{
			public long m_lPostSrl;
			public long m_lFromUSN;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] m_aszFromNick;
			
			public byte m_byType;
			public byte m_byReadYN;
			public int m_nTimeStamp;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_POST_CONTENT_LENGTH)]
			public byte[] m_aszContent;

			public byte m_nBorderType;
			public int m_nIconType;
			public int m_nServerProcessType;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
			public byte[] m_aDummy;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_POST_SEND
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] m_aszCallname;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 120)]
			public byte[] m_aszContent;

			public byte byPostType;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_POST_SEND_RESULT
		{
			public RESULT eResult;
			public byte bySendCnt;
			public byte byFriendCnt;

			public enum RESULT
			{
				SUCCESS,
				NAME_OR_CONTENT_WRONG,
				NO_SUCH_USER,
				RECEIVER_MAILBOX_IS_FULL,
				RESULT_4, // SUCCESS
				DBFAIL,
				RESULT_6,
				RESULT_7,
				RESULT_8,
				DAILY_SENT_MAX,
				RECEIVER_MAILBOX_IS_FULL_AND_DAILY_SENT_MAX,
				FRIEND_REQUEST_MAX,
				RECEIVER_MAILBOX_IS_FULL_AND_FRIEND_REQUEST_MAX,
				UNKNOWN_ERROR,
				CONTENT_ABUSE
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_ADD
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszTarget;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_ADD_RESULT
		{
			public RESULT m_eResult;
			
			public PROTO_NEW_BUDDY_USER_INFO tAddeeInfo;
			public PROTO_NEW_BUDDY_USER_INFO tAdderInfo;

			public int iDummy;
			public byte byDummy;
			
			public enum RESULT
			{
				INVITATION_SENT,
				ADD_DIRECTLY,
				ALREADY_ADDED,
				ALREADY_ADDED2,
				INVALID_CALLNAME,
				CALLNAME_NOT_EXISTS,
				SELF,
				LIST_FULL,
				DB_ERROR,
				CANNOT_GET_ADDEE_INFO_FROM_DB,
				BLOCKED_ALL_INVITATIONS,
				IN_BLOCK_LIST,
				TODAY_SENT_MAX = 16,
				MESSAGE_BOX_FULL = 17,
				ALREADY_SENT = 18
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_SEARCH_USER
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszTarget;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct BUDDY_LOCATION_INFO
		{
			public sbyte byServerNo;
			public sbyte byChannelNo;
			public sbyte byRoomNo;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NEW_BUDDY_SEARCH_USER_RESULT
		{
			public enum RESULT
			{
				USER_FOUND,
				USER_NOT_EXIST,
				DB_NOT_EXIST,
				ERROR3,
				ERROR4
			}
			
			public RESULT eResult;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCallName;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CLANID_LENGTH)]
			public byte[] aszClanID;
			
			public long lUSN;
			public byte byLevel;
			public byte byOnline;
			public short wHonor;
			public int iNew1;
			public short wNew2;
			public short wNew3;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_NEW_BUDDY_AUTOLEAGUE_GAME_INVITE
		{
			public long lInviteeUsn;
			public uint dwLeagueIndex;
			public uint dwTeamIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_NEW_BUDDY_AUTOLEAGUE_GAME_INVITE_RESULT
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszInviterName;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszInviteName;
			
			public long lInviteeUsn;
			public uint dwLeagueIndex;
			public uint dwTeamIndex;

			public enum RESULT
			{
				SUCCESS,
				INVALID_LOCATION,
				NOT_IN_ROOM,
				NOT_CLAN_USER,
				NOT_FRIEND,
				FULL,
				UNKNOWN_ERROR
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_NEW_BUDDY_AUTOLEAGUE_GAME_INVITED
		{
			public long lInviterUsn;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszInviterName;
			public long lInviteeUsn;
			public uint dwLeagueIndex;
			public uint dwTeamIndex;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
			public byte[] aszTeamName;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			public byte[] aszLeagePW;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public byte[] aszTeamPW;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
			public byte[] aszRoomPW;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_NEW_BUDDY_INVITE_AUTOLEAGUE_GAME_INVITE_RESPONSE
		{
			public long lInviterUsn;
			public long lInviteeUsn;
			public uint dwLeagueIndex;
			public uint dwTeamIndex;

			public enum RESULT
			{
				ACCEPT,
				DENY,
				CANNOT_INVITE
			}
		}
    }
}