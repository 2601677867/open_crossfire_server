using System.Runtime.InteropServices;
using Network.ProtocolStruct;

namespace Network.Protocol
{
	public static class P_IGC
	{
		public const int PROTOCOL_CLANMGMT = 16;
		
		public const int PROTOCOL_INGAME_CLAN_MANAGE = 1;
		
		public const int CLANMGRSERVER_CREATE_CLAN = 1;
		public const int CLANMGRSERVER_CREATE_CLAN_RESULT = 2;
		public const int CLANMGRSERVER_DELETE_CLAN = 3;
		public const int CLANMGRSERVER_DELETE_CLAN_RESULT = 4;
		public const int CLANMGRSERVER_SHOW_CLAN = 5;
		public const int CLANMGRSERVER_SHOW_CLAN_RESULT = 6;
		public const int CLANMGRSERVER_SEARCH_NAME = 7;
		public const int CLANMGRSERVER_SEARCH_NAME_RESULT = 8;
		public const int CLANMGRSERVER_SEARCH_MASTER_NAME = 9;
		public const int CLANMGRSERVER_SEARCH_MASTER_NAME_RESULT = 10;
		public const int CLANMGRSERVER_SEARCH_MEMBER_NAME = 11;
		public const int CLANMGRSERVER_SEARCH_MEMBER_NAME_RESULT = 12;
		public const int CLANMGRSERVER_GET_MYCLAN = 13;
		public const int CLANMGRSERVER_GET_MYCLAN_RESULT = 14;
		public const int CLANMGRSERVER_GET_MYCLAN_MEMBER = 15;
		public const int CLANMGRSERVER_GET_MYCLAN_MEMBER_RESULT = 16;
		public const int CLANMGRSERVER_MOVE_MEMBER = 17;
		public const int CLANMGRSERVER_MOVE_MEMBER_RESULT = 18;
		public const int CLANMGRSERVER_MOVE_MEMBER_NOTIFY = 19;
		public const int CLANMGRSERVER_JOIN_CLAN = 20;
		public const int CLANMGRSERVER_JOIN_CLAN_RESULT = 21;
		public const int CLANMGRSERVER_JOIN_CANCLE_CLAN = 22;
		public const int CLANMGRSERVER_JOIN_CANCLE_CLAN_RESULT = 23;
		public const int CLANMGRSERVER_CONFIRM = 24;
		public const int CLANMGRSERVER_CONFIRM_RESULT = 25;
		public const int CLANMGRSERVER_CONFIRM_NOTIFY = 26;
		public const int CLANMGRSERVER_CANCLE_CONFIRM = 27;
		public const int CLANMGRSERVER_CANCLE_CONFIRM_RESULT = 28;
		public const int CLANMGRSERVER_CANCLE_CONFIRM_NOTIFY = 29;
		public const int CLANMGRSERVER_QUIT_CLAN = 30;
		public const int CLANMGRSERVER_QUIT_CLAN_RESULT = 31;
		public const int CLANMGRSERVER_CHANGE_GRADE = 32;
		public const int CLANMGRSERVER_CHANGE_GRADE_RESULT = 33;
		public const int CLANMGRSERVER_CHANGE_GRADE_NOTIFY = 34;
		public const int CLANMGRSERVER_CHANGE_LIMIT_LEVEL = 35;
		public const int CLANMGRSERVER_CHANGE_LIMIT_LEVEL_RESULT = 36;
		public const int CLANMGRSERVER_CHANGE_APPROVAL = 37;
		public const int CLANMGRSERVER_CHANGE_APPROVAL_RESULT = 38;
		public const int CLANMGRSERVER_CHANGE_STRING = 39;
		public const int CLANMGRSERVER_CHANGE_STRING_RESULT = 40;
		public const int CLANMGRSERVER_CHANGE_REGION = 41;
		public const int CLANMGRSERVER_CHANGE_REGION_RESULT = 42;
		public const int CLANMGRSERVER_GET_WAIT_MEMBER = 43;
		public const int CLANMGRSERVER_GET_WAIT_MEMBER_RESULT = 44;
		public const int CLANMGRSERVER_BANISH = 45;
		public const int CLANMGRSERVER_BANISH_RESULT = 46;
		public const int CLANMGRSERVER_BANISH_NOTIFY = 47;
		public const int CLANMGRSERVER_CANCEL_DELETE_MEMBER = 48;
		public const int CLANMGRSERVER_CANCEL_DELETE_MEMBER_RESULT = 49;
		public const int CLANMGRSERVER_CHECK_NAME = 50;
		public const int CLANMGRSERVER_CHECK_NAME_RESULT = 51;
		public const int CLANMGRSERVER_CHECK_MARK = 52;
		public const int CLANMGRSERVER_CHECK_MARK_RESULT = 53;
		public const int CLANMGRSERVER_CHECK_DOMAIN = 54;
		public const int CLANMGRSERVER_CHECK_DOMAIN_RESULT = 55;
		public const int CLANMGRSERVER_CHANGE_MARK = 56;
		public const int CLANMGRSERVER_CHANGE_MARK_RESULT = 57;
		public const int CLANMGRSERVER_CHANGE_CLAN_NAME = 58;
		public const int CLANMGRSERVER_CHANGE_CLAN_NAME_RESULT = 59;
		public const int CLANMGRSERVER_REQUEST_TRANSFER_MASTER = 60;
		public const int CLANMGRSERVER_REQUEST_TRANSFER_MASTER_RESULT = 61;
		public const int CLANMGRSERVER_REQUEST_TRANSFER_MASTER_NOTIFY = 62;
		public const int CLANMGRSERVER_CANCLE_TRANSFER_MASTER = 63;
		public const int CLANMGRSERVER_CANCLE_TRANSFER_MASTER_RESULT = 64;
		public const int CLANMGRSERVER_CANCLE_TRANSFER_MASTER_NOTIFY = 65;
		public const int CLANMGRSERVER_CONFIRM_NEXT_MASTER = 66;
		public const int CLANMGRSERVER_CONFIRM_NEXT_MASTER_RESULT = 67;
		public const int CLANMGRSERVER_CONFIRM_NEXT_MASTER_NOTIFY = 68;
		public const int CLANMGRSERVER_GET_DELETE_MEMBER = 69;
		public const int CLANMGRSERVER_GET_DELETE_MEMBER_RESULT = 70;
		public const int CLANMGRSERVER_SHOW_POP_UP = 71;
		public const int CLANMGRSERVER_SHOW_POP_UP_RESULT = 72;
		public const int CLANMGRSERVER_GET_TOTAL_UNIT_INFO = 73;
		public const int CLANMGRSERVER_GET_TOTAL_UNIT_INFO_RESULT = 74;
		public const int CLANMGRSERVER_GET_STAFF_INFO = 75;
		public const int CLANMGRSERVER_GET_STAFF_INFO_RESULT = 76;
		public const int CLANMGRSERVER_GET_CLAN_LOG = 77;
		public const int CLANMGRSERVER_GET_CLAN_LOG_RESULT = 78;
		public const int CLANMGRSERVER_INCREASE_PERSON = 79;
		public const int CLANMGRSERVER_INCREASE_PERSON_RESULT = 80;
		public const int CLANMGRSERVER_IS_ABLE_TO_CONFIRM = 81;
		public const int CLANMGRSERVER_IS_ABLE_TO_CONFIRM_RESULT = 82;
		public const int CLANMGRSERVER_DIFF_7DAYS = 83;
		public const int CLANMGRSERVER_DIFF_7DAYS_RESULT = 84;
		public const int CLANMGRSERVER_GET_RECORD_COUNT = 85;
		public const int CLANMGRSERVER_GET_RECORD_COUNT_RESULT = 86;
		public const int CLANMGRSERVER_GET_COMMANDER_COUNT = 87;
		public const int CLANMGRSERVER_GET_COMMANDER_COUNT_RESULT = 88;
		public const int CLANMGRSERVER_GET_STAFF_COUNT = 89;
		public const int CLANMGRSERVER_GET_STAFF_COUNT_RESULT = 90;
		public const int CLANMGRSERVER_GET_ALL_CLAN_COUNT = 91;
		public const int CLANMGRSERVER_GET_ALL_CLAN_COUNT_RESULT = 92;
		public const int CLANMGRSERVER_GET_BASE_INFO = 93;
		public const int CLANMGRSERVER_GET_BASE_INFO_RESULT = 94;
		public const int CLANMGRSERVER_CLAN_CHAT = 97;
		public const int CLANMGRSERVER_CLAN_CHAT_RESULT = 98;
		public const int CLANMGRSERVER_CLAN_CHAT_NOTIFY = 99;
		public const int CLANMGRSERVER_DISCONNECT_USER_RESULT = 100;
		public const int CLANMGRSERVER_CLAN_PW_INFO_REQUEST = 101;
		public const int CLANMGRSERVER_CLAN_PW_INFO_RESULT = 102;
		public const int CLANMGRSERVER_CLAN_PW_CHECK_REQUEST = 103;
		public const int CLANMGRSERVER_CLAN_PW_CHECK_RESULT = 104;
		public const int CLANMGRSERVER_CLAN_PW_TIME_CHECK_REQUST = 105;
		public const int CLANMGRSERVER_CLAN_PW_TIME_CHECK_RESULT = 106;
		public const int CLANMGRSERVER_CLAN_NEW_PW_REQUEST = 107;
		public const int CLANMGRSERVER_CLAN_NEW_PW_RESULT = 108;
		public const int CLANMGRSERVER_CLAN_PW_CHANGE_REQUEST = 109;
		public const int CLANMGRSERVER_CLAN_PW_CHANGE_RESULT = 110;
		public const int CLANMGRSERVER_CLAN_PW_Q_AND_A_CHANGE_REQUEST = 111;
		public const int CLANMGRSERVER_CLAN_PW_Q_AND_A_CHANGE_RESULT = 112;
		public const int CLANMGRSERVER_CLAN_PW_GET_BACK_REQUEST = 113;
		public const int CLANMGRSERVER_CLAN_PW_GET_BACK_RESULT = 114;
		public const int CLANMGRSERVER_CHANGE_CLAN_NAME_BY_CASH = 115;
		public const int CLANMGRSERVER_CHANGE_CLAN_NAME_BY_CASH_RESULT = 116;
		public const int CLANMGRSERVER_MEMBER_EXPAND_BY_CASH = 117;
		public const int CLANMGRSERVER_MEMBER_EXPAND_BY_CASH_RESULT = 118;
		public const int CLANMGRSERVER_CHANGE_CLAN_MARK_BY_CASH = 119;
		public const int CLANMGRSERVER_CHANGE_CLAN_MARK_BY_CASH_RESULT = 120;
		public const int CLANMGRSERVER_CLAN_MARK_LIST = 121;
		public const int CLANMGRSERVER_CLAN_MARK_LIST_RESULT = 122;
		public const int CLANMGRSERVER_SEARCH_CLANKEY = 123;
		public const int CLANMGRSERVER_SEARCH_CLANKEY_RESULT = 124;
		
		public const int CLANMGRSERVER_NEW_UNKNOWN0 = 126;
		public const int CLANMGRSERVER_NEW_UNKNOWN0_RESULT = 127;
		public const int CLANMGRSERVER_NEW_UNKNOWN1 = 162;
		public const int CLANMGRSERVER_NEW_UNKNOWN1_RESULT = 163;
		public const int CLANMGRSERVER_NEW_UNKNOWN2 = 164;
		public const int CLANMGRSERVER_NEW_UNKNOWN2_RESULT = 165;

		public enum CLAN_GRADE : byte
		{
			E_CLAN_GRADE_START,
			E_CLAN_GRADE_WAITNG,
			E_CLAN_GRADE_MEMBER,
			E_CLAN_GRADE_MASTER,
			E_CLAN_GRADE_SUB_MASTER,
			E_CLAN_GRADE_SMALL_BOSS,
			E_CLAN_GRADE_MIDDLE_BOSS,
			E_CLAN_GRADE_LARGE_BOSS,
			E_CLAN_GRADE_ULTRA_BOSS,
			E_CLAN_GRADE_TEAM_MEMBER,
			E_CLAN_GRADE_TEAM_LEADER,
			E_CLAN_GRADE_STAFF_01,
			E_CLAN_GRADE_STAFF_02,
			E_CLAN_GRADE_STAFF_03,
			E_CLAN_GRADE_STAFF_04,
			E_CLAN_GRADE_END
		}

		public enum CLAN_STRING_TYPE : byte
		{
			STRING_OF_NOTICE,
			STRING_OF_INTRO
		}

		public enum CLAN_MEMBER_STAT : byte
		{
			E_CLAN_STAT_SELF_NONE,
			E_CLAN_STAT_SELF_LEAVE_MEMBER,
			E_CLAN_STAT_FORCE_LEAVE_MEMBER,
			E_CLAN_STAT_ETERNAL_LEAVE_MEMBER
		}

		public enum E_CLAN_POPUP
		{
			E_CLAN_POPUP_DISMISSED_DUE_TO_INSUFFICENT_MEMBER,
			E_CLAN_POPUP_DISMISS_WARNING,
			E_CLAN_POPUP_EXPANDED,
			E_CLAN_POPUP_DIMISSED_AUTO_LEAVE,
			E_CLAN_POPUP_KICKED,
			E_CLAN_POPUP_PERMANENTLY_BANNED,
			E_CLAN_POPUP_KICKED_DUE_TO_LONG_OFFLINE,
			E_CLAN_POPUP_TYPE8,
			E_CLAN_POPUP_YOU_ARE_NEW_LEADER,
			E_CLAN_POPUP_CLAN_APPROVED_MSG,
			E_CLAN_POPUP_CLAN_REJECTED_MSG,
			E_CLAN_POPUP_CLAN_PERMISSION_CHANGED,
			E_CLAN_POPUP_CLAN_PERMISSION_CHANGED_2,
			E_CLAN_POPUP_CLAN_APPROVED,
			E_CLEN_POPUP_END
		}

		public enum CLAN_MARK_TYPE : byte
		{
			CLAN_MARK_BG,
			CLAN_MARK_BORDER,
			CLAN_MARK_IMAGE,
			CLAN_MARK_EVENT,
			CLAN_MARK_OWN,
			CLAN_MARK_MAX
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct CLAN_MARK_LIST_UNIT_INfO
		{
			public CLAN_MARK_TYPE ClanMarkType;

			public ushort ClanMarkUnitIndex;

			public int ClanMarkUnitCost;

			public byte IsTopList;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct CLAN_MARK_INFO
		{
			public ushort wMarkBGLayer;
			public ushort wMarkOutLineLayer;
			public ushort wMarkSymbolLayer;
			public ushort wMarkEventLayer;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public class CLAN_BASE_INFO
		{
			public long ClanKey;

			public ulong MasterUsn;

			public byte byClanLevel;

			public byte byLimitLevel;

			public ushort wTotalMember;

			public ushort wMaxMember;

			public CLAN_MARK_INFO ClanMarkInfo;

			public int nActivePoint;

			public int nRegion;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 221)]
			public string szIntro;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string szCreateDate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szMasterNick;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct CLAN_BASE_INFO_ST
		{
			public long ClanKey;

			public ulong MasterUsn;

			public byte byClanLevel;

			public byte byLimitLevel;

			public ushort wTotalMember;

			public ushort wMaxMember;

			public CLAN_MARK_INFO ClanMarkInfo;

			public int nActivePoint;

			public int nRegion;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 221)]
			public string szIntro;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string szCreateDate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szMasterNick;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanName;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 286)]
			public string aDummy;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public class CLAN_INFO : CLAN_BASE_INFO
		{
			public byte ucClanLevelMax;

			public byte bApproval;

			public int nPlayCount;

			public int nClanWin;

			public int nClanLose;

			public int nClanDraw;

			public byte ucSubMasterCount = 0;

			public byte ucLargeBossCount = 8;

			public byte ucMiddleBossCount = 9;

			public byte ucSmallBossCount = 10;

			public byte ucStaff01Count = 0;

			public byte ucStaff02Count = 0;

			public byte ucStaff03Count = 0;

			public byte ucStaff04Count = 0;

			public byte ucMaxUnitCount = 15;

			public byte ucMaxTeamCount = 16;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szDomain;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 221)]
			public string szNotice;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct IN_CLAN_MEMBER_INFO
		{
			public long ClanKey;

			public ulong lUsn;

			public long ClanUnitID;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szMemberNick;

			public CLAN_GRADE ucGrade;

			public byte ucLevel;

			public byte ucServer;

			public byte ucChannel;

			public ushort ssRoomID;

			public int nPLevelPoint;

			public int nPower;
			
			public int nUnk; // ???

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string szLastDate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string szRegDate;

			public int nCalcPointExp;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string szLastCalcPointDate;

			public CLAN_UNIT_INFO UnitInfo;
			
			public int nUnk2;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct WAIT_MEMBER_INTRO_INFO
		{
			public ulong usn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 42)]
			public string intro;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct MemberStatInfo
		{
			public long clan_key_;

			public ulong usn_;

			public CLAN_MEMBER_STAT stat_;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string member_name_;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string reg_date_;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct CLAN_GRADE_INFO
		{
			public ulong lUsn;

			public byte byLevel;

			public CLAN_GRADE Grade;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szMemberNick;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct CLAN_TOTAL_UNIT_INFO
		{
			public CLAN_UNIT_INFO UnitInfo;

			public CLAN_GRADE_INFO GradeInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct USER_POP_UP_INFO
		{
			public E_CLAN_POPUP PopUpType;
			public int Param01;
			public int Param02;
			public int Param03;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct IN_CLAN_LOG_INFO
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szBossNick;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szNick;
			
			public byte ucGroupType;

			public int nActionKey01;

			public CLAN_GRADE Grade;

			public int nParam01;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
			public string szParam02;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string szDate;
		}

		public enum CREATE_CLAN_RESULT : byte
		{
			CREATE_CLAN_SUCCESS = 1,
			CREATE_CLAN_NOT_ENOUGH_LEVEL,
			CREATE_CLAN_INVALID_NAME,
			CREATE_CLAN_INVALID_DOMAIN,
			CREATE_CLAN_OVER_MAX_LEAVE,
			CREATE_CLAN_ALEADY_CLAN_MEMBER,
			CREATE_CLAN_NOT_INVALID_PW_COUNT,
			CREATE_CLAN_INCORRECT_PW,
			CREATE_CLAN_INVALID_QA_COUNT,
			CREATE_CLAN_INCORRECT_QA,
			CREATE_CLAN_UNKNOWN_ERROR,
			CREATE_CLAN_NO_MEMORY
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CREATE_CLAN
		{
			public int nRegion;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szDomain;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 221)]
			public string szIntro;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string szClanPassWord;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
			public string szClanPassWordQuestion;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
			public string szClanPassWordAnswer;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CREATE_CLAN_RESULT
		{
			public CREATE_CLAN_RESULT nResult;

			public long ClanKey;
		}

		public enum DELETE_CLAN_RESULT : byte
		{
			DELETE_CLAN_SUCCESS = 1,
			DELETE_CLAN_CLAN_IS_NOT_EXIST,
			DELETE_CLAN_MASTER_IS_NOT_EXIST,
			DELETE_CLAN_INVALID_GRADE,
			DELETE_CLAN_MEMBER_IS_EXIST,
			DELETE_CLAN_INVALID_PASSWORD,
			DELETE_CLAN_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_DELETE_CLAN
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string szClanPassWord;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_DELETE_CLAN_RESULT
		{
			public DELETE_CLAN_RESULT nResult;
		}

		public enum SHOW_CLAN_RESULT : byte
		{
			SHOW_CLAN_SUCCESS = 1,
			SHOW_CLAN_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_SHOW_CLAN
		{
			public int nPage;

			public byte byRequestFlag;

			public long ClanKey;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_SHOW_CLAN_RESULT
		{
			public SHOW_CLAN_RESULT nResult;

			public int nPage;

			public ushort wCountOfLIst;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public CLAN_BASE_INFO_ST[] tClanInfo;
		}

		public enum SEARCH_NAME_RESULT : byte
		{
			SEARCH_NAME_SUCCESS = 1,
			SEARCH_NAME_FAIL,
			SEARCH_NAME_CLAN_IS_NOT_EXIST,
			SEARCH_NAME_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_SEARCH_NAME
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_SEARCH_NAME_RESULT
		{
			public SEARCH_NAME_RESULT nResult;

			public CLAN_BASE_INFO tClanInfo;
		}

		public enum SEARCH_MASTER_NAME_RESULT : byte
		{
			SEARCH_MASTER_NAME_SUCCESS = 1,
			SEARCH_MASTER_NAME_FAIL,
			SEARCH_MASTER_NAME_CLAN_IS_NOT_EXIST,
			SEARCH_MASTER_NAME_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_SEARCH_MASTER_NAME
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_SEARCH_MASTER_NAME_RESULT
		{
			public SEARCH_MASTER_NAME_RESULT nResult;

			public CLAN_BASE_INFO tClanInfo;
		}

		public enum SEARCH_MEMBER_NAME_RESULT : byte
		{
			SEARCH_MEMBER_SUCCESS = 1,
			SEARCH_MEMBER_FAIL,
			SEARCH_MEMBER_CLAN_IS_NOT_EXIST,
			SEARCH_MEMBER_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_SEARCH_MEMBER_NAME
		{
			public CLAN_UNIT_INFO ClanUnitInfo;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_SEARCH_MEMBER_NAME_RESULT
		{
			public SEARCH_MEMBER_NAME_RESULT nResult;

			public IN_CLAN_MEMBER_INFO MemberInfo;
		}

		public enum GET_MYCLAN_RESULT : byte
		{
			GET_MYCLAN_SUCCESS = 1,
			GET_MYCLAN_DISCONNECTED,
			GET_MYCLAN_NOT_CLAN_MEMBER,
			GET_MYCLAN_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_GET_MYCLAN
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_GET_MYCLAN_RESULT
		{
			public GET_MYCLAN_RESULT nResult;

			public CLAN_INFO tClanInfo;
		}

		public enum GET_UNIT_MEMBER_RESULT : byte
		{
			GET_MYCLAN_MEMBER_SUCCESS = 1,
			GET_MYCLAN_MEMBER_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_GET_MYCLAN_MEMBER
		{
			public CLAN_UNIT_INFO ClanUnitInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_GET_MYCLAN_MEMBER_RESULT
		{
			public GET_UNIT_MEMBER_RESULT nResult;

			public CLAN_UNIT_INFO ClanUnitInfo;

			public ushort wCountOfLIst;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public IN_CLAN_MEMBER_INFO[] tClanMemberInfo;
		}

		public enum MOVE_MEMBER_RESULT : byte
		{
			MOVE_MEMBER_SUCCESS = 1,
			MOVE_MEMBER_CLAN_IS_NOT_EXIST,
			MOVE_MEMBER_MASTER_IS_NOT_EXIST,
			MOVE_MEMBER_MEMBER_IS_NOT_EXIST,
			MOVE_MEMBER_INVALID_GRADE,
			MOVE_MEMBER_INVALID_MOVE,
			MOVE_MEMBER_UNIT_IS_FULL,
			MOVE_MEMBER_MEMBER_IS_DELEGATE_MASTER,
			MOVE_MEMBER_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_MOVE_MEMBER
		{
			public CLAN_UNIT_INFO FromUnitInfo;

			public CLAN_UNIT_INFO ToUnitInfo;

			public byte CountOfUSN;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public uint[] UsnList;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_MOVE_MEMBER_RESULT
		{
			public MOVE_MEMBER_RESULT nResult;

			public byte CountOfUSN;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public uint[] UsnList;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_MOVE_MEMBER_NOTIFY
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szRequestMember;

			public CLAN_UNIT_INFO MoveUnitInfo;
		}

		public enum JOIN_CLAN_RESULT : byte
		{
			JOIN_CLAN_SUCCESS = 1,
			JOIN_CLAN_SUCCESS_REG_WAITING,
			JOIN_CLAN_CLAN_IS_NOT_EXIST,
			JOIN_CLAN_MEMBER_IS_NOT_EXIST,
			JOIN_CLAN_ALREADY_WAITING_CONFIRM,
			JOIN_CLAN_NOT_ENOUGH_LEVEL,
			JOIN_CLAN_WAITING_MEMBER_IS_FULL,
			JOIN_CLAN_MEMBER_IS_FULL,
			JOIN_CLAN_YOU_ARE_BANISHED,
			JOIN_CLAN_OVER_MAX_LEAVE,
			JOIN_CLAN_WAITING_MEMBER_IS_NOT_EXIST,
			JOIN_CLAN_ALEADY_CLAN_MEMBER,
			JOIN_CLAN_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_JOIN_CLAN
		{
			public long ClanKey;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
			public string Intro;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_JOIN_CLAN_RESULT
		{
			public long ClanKey;

			public JOIN_CLAN_RESULT nResult;
		}

		public enum JOIN_CANCLE_CLAN_RESULT : byte
		{
			JOIN_CANCLE_CLAN_SUCCESS = 1,
			JOIN_CANCLE_CLAN_CLAN_IS_NOT_EXIST,
			JOIN_CANCLE_CLAN_MEMBER_IS_NOT_EXIST,
			JOIN_CANCLE_CLAN_ALREADY_WAITING_CONFIRM,
			JOIN_CANCLE_CLAN_NOT_ENOUGH_LEVEL,
			JOIN_CANCLE_CLAN_WAITING_MEMBER_IS_FULL,
			JOIN_CANCLE_CLAN_MEMBER_IS_FULL,
			JOIN_CANCLE_CLAN_YOU_ARE_BANISHED,
			JOIN_CANCLE_CLAN_OVER_MAX_LEAVE,
			JOIN_CANCLE_CLAN_WAITING_MEMBER_IS_NOT_EXIST,
			JOIN_CANCLE_DIS_CLANSERVER,
			JOIN_CANCLE_CLAN_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_JOIN_CANCLE_CLAN
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_JOIN_CANCLE_CLAN_RESULT
		{
			public JOIN_CANCLE_CLAN_RESULT nResult;
		}

		public enum CONFIRM_RESULT : byte
		{
			CONFIRM_SUCCESS = 1,
			CONFIRM_CLAN_IS_NOT_EXIST,
			CONFIRM_MASTER_IS_NOT_EXIST,
			CONFIRM_MEMBER_IS_NOT_EXIST,
			CONFIRM_INVALID_GRADE,
			CONFIRM_MEMBER_IS_FULL,
			CONFIRM_DIS_CLANSERVER,
			CONFIRM_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CONFIRM
		{
			public uint lMemberUsn;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CONFIRM_RESULT
		{
			public uint lMemberUsn;

			public CONFIRM_RESULT nResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CONFIRM_NOTIFY
		{
			public CONFIRM_RESULT nResult;

			public CLAN_UNIT_INFO ClanUnitInfo;
		}

		public enum CANCLE_CONFIRM_RESULT : byte
		{
			CANCLE_CONFIRM_SUCCESS = 1,
			CANCLE_CONFIRM_CLAN_IS_NOT_EXIST,
			CANCLE_CONFIRM_MASTER_IS_NOT_EXIST,
			CANCLE_CONFIRM_MEMBER_IS_NOT_EXIST,
			CANCLE_CONFIRM_INVALID_GRADE,
			CANCLE_CONFIRM_MEMBER_IS_FULL,
			CANCLE_CONFIRM_DIS_CLANSERVER,
			CANCLE_CONFIRM_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CANCLE_CONFIRM
		{
			public uint lMemberUsn;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CANCLE_CONFIRM_RESULT
		{
			public uint lMemberUsn;

			public CANCLE_CONFIRM_RESULT nResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CANCLE_CONFIRM_NOTIFY
		{
			public CONFIRM_RESULT nResult;

			public CLAN_UNIT_INFO ClanUnitInfo;
		}

		public enum QUIT_CLAN_RESULT : byte
		{
			QUIT_CLAN_SUCCESS = 1,
			QUIT_CLAN_CLAN_IS_NOT_EXIST,
			QUIT_CLAN_MEMBER_IS_NOT_EXIST,
			QUIT_CLAN_ALREADY_WAITING_CONFIRM,
			QUIT_CLAN_NOT_ENOUGH_LEVEL,
			QUIT_CLAN_WAITING_MEMBER_IS_FULL,
			QUIT_CLAN_MEMBER_IS_FULL,
			QUIT_CLAN_YOU_ARE_BANISHED,
			QUIT_CLAN_OVER_MAX_LEAVE,
			QUIT_CLAN_WAITING_MEMBER_IS_NOT_EXIST,
			QUIT_CLAN_DIS_CLAN_SERVER,
			QUIT_CLAN_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_QUIT_CLAN
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_QUIT_CLAN_RESULT
		{
			public QUIT_CLAN_RESULT nResult;

			public long ClanKey;
		}

		public enum CHANGE_GRADE_RESULT : byte
		{
			CHANGE_GRADE_SUCCESS = 1,
			CHANGE_GRADE_CLAN_IS_NOT_EXIST,
			CHANGE_GRADE_MASTER_IS_NOT_EXIST,
			CHANGE_GRADE_MEMBER_IS_NOT_EXIST,
			CHANGE_GRADE_INVALID_GRADE,
			CHANGE_GRADE_INVALID_CHANGE,
			CHANGE_GRADE_STEP_IS_FULL,
			CHANGE_GRADE_DIS_CLAN_SERVER,
			CHANGE_GRADE_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_GRADE
		{
			public uint lMemberUsn;

			public CLAN_GRADE ucBeforeGrade;

			public CLAN_GRADE ucAfterGrade;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CHANGE_GRADE_RESULT
		{
			public CHANGE_GRADE_RESULT nResult;

			public ulong lMemberUsn;

			public CLAN_GRADE ucBeforeGrade;

			public CLAN_GRADE ucAfterGrade;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_GRADE_NOTIFY
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szRequestMember;

			public CLAN_GRADE ucMemberGrade;
		}

		public enum CHANGE_LIMIT_LEVEL_RESULT : byte
		{
			CHANGE_LIMIT_LEVEL_SUCCESS = 1,
			CHANGE_LIMIT_LEVEL_CLAN_IS_NOT_EXIST,
			CHANGE_LIMIT_LEVEL_MASTER_IS_NOT_EXIST,
			CHANGE_LIMIT_LEVEL_INVALID_GRADE,
			CHANGE_LIMIT_LEVEL_DIS_CLAN_SERVER,
			CHANGE_LIMIT_LEVEL_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_LIMIT_LEVEL
		{
			public byte byLimitLevel;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CHANGE_LIMIT_LEVEL_RESULT
		{
			public CHANGE_LIMIT_LEVEL_RESULT nResult;

			public byte ucLimitLevel;
		}

		public enum CHANGE_APPROVAL_RESULT : byte
		{
			CHANGE_APPROVAL_SUCCESS = 1,
			CHANGE_APPROVAL_CLAN_IS_NOT_EXIST,
			CHANGE_APPROVAL_MASTER_IS_NOT_EXIST,
			CHANGE_APPROVAL_INVALID_GRADE,
			CHANGE_APPROVAL_DIS_CLAN_SERVER,
			CHANGE_APPROVAL_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_APPROVAL
		{
			public byte bApproval;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CHANGE_APPROVAL_RESULT
		{
			public CHANGE_APPROVAL_RESULT nResult;

			public byte bApproval;
		}

		public enum CHANGE_STRING_RESULT : byte
		{
			CHANGE_NOTICE_SUCCESS = 1,
			CHANGE_NOTICE_CLAN_IS_NOT_EXIST,
			CHANGE_NOTICE_MASTER_IS_NOT_EXIST,
			CHANGE_NOTICE_INVALID_GRADE,
			CHANGE_NOTICE_DIS_CLAN_SERVER,
			CHANGE_NOTICE_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CHANGE_STRING
		{
			public CLAN_STRING_TYPE ClanStringType;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 221)]
			public string szNotice;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CHANGE_STRING_RESULT
		{
			public CHANGE_STRING_RESULT nResult;

			public CLAN_STRING_TYPE ClanStringType;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 221)]
			public string szNotice;
		}

		public enum CHANGE_REGION_RESULT : byte
		{
			CHANGE_REGION_SUCCESS = 1,
			CHANGE_REGION_CLAN_IS_NOT_EXIST,
			CHANGE_REGION_MASTER_IS_NOT_EXIST,
			CHANGE_REGION_INVALID_GRADE,
			CHANGE_REGION_DIS_CLAN_SERVER,
			CHANGE_IS_NOT_CLAN_MEMBER,
			CHANGE_REGION_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_REGION
		{
			public ushort nRegion;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CHANGE_REGION_RESULT
		{
			public CHANGE_REGION_RESULT nResult;

			public ushort nRegion;
		}

		public enum GET_WAIT_MEMBER_RESULT : byte
		{
			GET_WAIT_MEMBER_SUCCESS = 1,
			GET_WAIT_MEMBER_CLAN_IS_NOT_EXIST,
			GET_WAIT_MEMBER_MASTER_IS_NOT_EXIST,
			GET_WAIT_MEMBER_INVALID_GRADE,
			GET_WAIT_MEMBER_DIS_CLAN_SERVER,
			GET_WAIT_MEMBER_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_GET_WAIT_MEMBER
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_GET_WAIT_MEMBER_RESULT
		{
			public GET_WAIT_MEMBER_RESULT nResult;

			public ushort wCountOfList;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
			public WAIT_MEMBER_INTRO_INFO[] tClanMemberIntro;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public IN_CLAN_MEMBER_INFO[] tClanMemberInfo;
		}

		public enum BANISH_RESULT : byte
		{
			BANISH_SUCCESS = 1,
			BANISH_CLAN_IS_NOT_EXIST,
			BANISH_MASTER_IS_NOT_EXIST,
			BANISH_MEMBER_IS_NOT_EXIST,
			BANISH_INVALID_GRADE,
			BANISH_BANISH_MEMBER_IS_FULL,
			BANISH_CLAN_DIS_CLAN_SERVER,
			BANISH_CLAN_INVALID_PASSWORD,
			BANISH_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_BANISH
		{
			public uint lMemberUsn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szMemberNick;

			public byte byIsEverBAnish;

			public ushort CountOfUSNList;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string szClanPassWord;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public uint[] USNList;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGRSERVER_BANISH_RESULT
		{
			public BANISH_RESULT nResult;

			public byte byIsEverBAnish;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public ulong[] USNList;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_BANISH_NOTIFY
		{
			public byte byIsEverBAnish;
		}

		public enum CANCLE_DELETE_MEMBER_RESULT : byte
		{
			CANCLE_DELETE_SUCCESS = 1,
			CANCLE_DELETE_DIS_CLAN_SERVER,
			CANCLE_DELETE_FAIL
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CANCEL_DELETE_MEMBER
		{
			public ulong lMemberUsn;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGRSERVER_CANCEL_DELETE_MEMBER_RESULT
		{
			public CANCLE_DELETE_MEMBER_RESULT nResult;

			public ulong lMemberUsn;
		}

		public enum CHECK_NAME_RESULT : byte
		{
			CHECK_NAME_SUCCESS = 1,
			CHECK_NAME_DUP_DOMAIN,
			CHECK_NAME_SPECIAL_CHAR,
			CHECK_NAME_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHECK_NAME
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CHECK_NAME_RESULT
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanName;

			public CHECK_NAME_RESULT nResult;
		}

		public enum CHECK_MARK_RESULT : byte
		{
			CHECK_MARK_SUCCESS = 1,
			CHECK_MARK_INVALID_NAME,
			CHECK_MARK_INVALID_DOMAIN,
			CHECK_MARK_INVALID_MARK,
			CHECK_MARK_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHECK_MARK
		{
			public CLAN_MARK_INFO ClanMarkInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CHECK_MARK_RESULT
		{
			public CHECK_MARK_RESULT nResult;

			public CLAN_MARK_INFO ClanMarkInfo;
		}

		public enum CHECK_DOMAIN_RESULT : byte
		{
			CHECK_DOMAIN_SUCCESS = 1,
			CHECK_DOMAIN_DUP_DOMAIN,
			CHECK_DOMAIN_SPECIAL_CHAR,
			CHECK_DOMAIN_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHECK_DOMAIN
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanDomain;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CHECK_DOMAIN_RESULT
		{
			public CHECK_DOMAIN_RESULT nResult;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanDomain;
		}

		public enum CHANGE_MARK_RESULT : byte
		{
			CHANGE_MARK_SUCCESS = 1,
			CHANGE_MARK_CLAN_IS_NOT_EXIST,
			CHANGE_MARK_MASTER_IS_NOT_EXIST,
			CHANGE_MARK_INVALID_GRADE,
			CHANGE_MARK_INVALID_MARK,
			CHANGE_MARK_CHANGE_ITEM_IS_NOT_EXIST,
			CHANGE_MARK_NOT_ENOUGH_ITEM_COUNT,
			CHANGE_MARK_NOT_FOUND_ITEM,
			CHANGE_MARK_NOT_MISMATCH_ITEM_TYPE,
			CHANGE_MARK_IS_NOT_CLAN_MEMBER,
			CHANGE_MARK_DIS_CLAN_SERVER,
			CHANGE_MARK_UPDATE_FAIL,
			CHANGE_MARK_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_MARK
		{
			public long nnClanSrl;

			public CLAN_MARK_INFO ClanMarkInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CHANGE_MARK_RESULT
		{
			public CHANGE_MARK_RESULT nResult;

			public long nInvenSrl;

			public CLAN_MARK_INFO ClanMarkInfo;

			public int nGauge;
		}

		public enum CHANGE_CLAN_NAME_RESULT : byte
		{
			CHANGE_CLAN_NAME_SUCCESS = 1,
			CHANGE_CLAN_NAME_CLAN_IS_NOT_EXIST,
			CHANGE_CLAN_NAME_MASTER_IS_NOT_EXIST,
			CHANGE_CLAN_NAME_INVALID_GRADE,
			CHANGE_CLAN_NAME_INVALID_NAME,
			CHANGE_CLAN_NAME_CHANGE_ITEM_IS_NOT_EXIST,
			CHANGE_CLAN_NAME_NOT_ENOUGH_ITEM_COUNT,
			CHANGE_CLAN_NAME_NOT_FOUND_ITEM,
			CHANGE_CLAN_NAME_NOT_MISMATCH_ITEM_TYPE,
			CHANGE_CLAN_NAME_IS_NOT_CLAN_MEMBER,
			CHANGE_CLAN_NAME_DIS_CLAN_SERVER,
			CHANGE_CLAN_NAME_UPDATE_FAIL,
			CHANGE_CLAN_NAME_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_CLAN_NAME
		{
			public long nInvenSrl;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_CLAN_NAME_RESULT
		{
			public long nInvenSrl;

			public CHANGE_CLAN_NAME_RESULT nResult;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanName;

			public int nGauge;
		}

		public enum REQUEST_TRANSFER_MASTER_RESULT : byte
		{
			TRANSFER_MASTER_SUCCESS = 1,
			TRANSFER_MASTER_CLAN_IS_NOT_EXIST,
			TRANSFER_MASTER_MASTER_IS_NOT_EXIST,
			TRANSFER_MASTER_MEMBER_IS_NOT_EXIST,
			TRANSFER_MASTER_INVALID_GRADE,
			TRANSFER_MASTER_MEMBER_IS_TEAM,
			TRANSFER_MASTER_SEND_FAIL_CLAN,
			TRNASFER_MASTER_MASTER_IS_ME,
			TRNASFER_MASTER_CLAN_SERVER_ERROR,
			TRNASFER_NOT_FIND_CLAN,
			TRANSFER_NOT_SAME_PASSWORD,
			TRANSFER_MASTER_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_REQUEST_TRANSFER_MASTER
		{
			public uint lNextMasterUsn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string szClanPassWord;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_REQUEST_TRANSFER_MASTER_RESULT
		{
			public REQUEST_TRANSFER_MASTER_RESULT nResult;

			public ulong lNextMasterUsn;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_REQUEST_TRANSFER_MASTER_NOTIFY
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szMasterNick;
		}

		public enum CANCLE_TRANSFER_MASTER_RESULT : byte
		{
			CANCLE_MASTER_SUCCESS = 1,
			CANCLE_MASTER_CLAN_IS_NOT_EXIST,
			CANCLE_MASTER_MASTER_IS_NOT_EXIST,
			CANCLE_MASTER_MEMBER_IS_NOT_EXIST,
			CANCLE_MASTER_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CANCLE_TRANSFER_MASTER
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CANCLE_TRANSFER_MASTER_RESULT
		{
			public CANCLE_TRANSFER_MASTER_RESULT nResult;

			//public ulong lNextMasterUsn;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CANCLE_TRANSFER_MASTER_NOTIFY
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szMasterNick;
		}

		public enum CONFIRM_TRANSFER_MASTER_RESULT : byte
		{
			CONFIRM_TRANSFER_MASTER_SUCCESS = 1,
			CONFIRM_TRANSFER_MASTER_CLAN_IS_NOT_EXIST,
			CONFIRM_TRANSFER_MASTER_MASTER_IS_NOT_EXIST,
			CONFIRM_TRANSFER_MASTER_MEMBER_IS_NOT_EXIST,
			CONFIRM_TRANSFER_MASTER_INVALID_GRADE,
			CONFIRM_TRANSFER_MASTER_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CONFIRM_NEXT_MASTER
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CONFIRM_NEXT_MASTER_RESULT
		{
			public CONFIRM_TRANSFER_MASTER_RESULT nResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CONFIRM_NEXT_MASTER_NOTIFY
		{
			public CONFIRM_TRANSFER_MASTER_RESULT nResult;

			public uint NextmasterUsn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szNextMasterNick;
		}

		public enum GET_DELETE_MEMBER_RESULT : byte
		{
			GET_DELETE_MEMBER_SUCCESS = 1,
			GET_DELETE_MEMBER_CLAN_IS_NOT_EXIST,
			GET_DELETE_MEMBER_MASTER_IS_NOT_EXIST,
			GET_DELETE_MEMBER_INVALID_GRADE,
			GET_DELETE_DIS_CLAN_SERVER,
			GET_DELETE_MEMBER_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_GET_DELETE_MEMBER
		{
			public CLAN_MEMBER_STAT RequestType;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_GET_DELETE_MEMBER_RESULT
		{
			public GET_DELETE_MEMBER_RESULT nResult;

			public CLAN_MEMBER_STAT RequestType;

			public ushort wCountOfList;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 70)]
			public MemberStatInfo[] tClanMemberInfo;
		}

		public enum SHOW_POP_UP_RESULT : byte
		{
			SHOW_POP_UP_SUCCESS = 1,
			SHOW_POP_UP_DIS_CLANSERVER,
			SHOW_POP_UP_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_SHOW_POP_UP
		{
			public uint lUsn;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_SHOW_POP_UP_RESULT
		{
			public SHOW_POP_UP_RESULT nResult;

			public ushort wCountOfList;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public USER_POP_UP_INFO[] PopUpInfo;

			public ushort GetSize()
			{
				return (ushort) (3 + wCountOfList * Marshal.SizeOf<USER_POP_UP_INFO>());
			}
		}

		public enum GET_TOTAL_UNIT_RESULT : byte
		{
			TOTAL_UNIT_INFO_SUCCESS = 1,
			TOTAL_UNIT_INFO_CLAN_IS_NOT_EXIST,
			TOTAL_UNIT_INFO_DIS_CLANSERVER,
			TOTAL_UNIT_INFO_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_GET_TOTAL_UNIT_INFO
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGRSERVER_GET_TOTAL_UNIT_RESULT
		{
			public GET_TOTAL_UNIT_RESULT nResult;

			public ushort wCountOfList;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
			public CLAN_TOTAL_UNIT_INFO[] TotalUnitInfo;
		}

		public enum GET_STAFF_INFO_RESULT : byte
		{
			GET_STAFF_INFO_SUCCESS = 1,
			GET_STAFF_INFO_CLAN_IS_NOT_EXIST,
			GET_STAFF_INFO_DIS_CLAN_SERVER,
			GET_STAFF_INFO_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_GET_STAFF_INFO
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGRSERVER_GET_STAFF_INFO_RESULT
		{
			public GET_STAFF_INFO_RESULT nResult;

			public ushort wCountOfList;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public CLAN_GRADE_INFO[] GradeInfo;
		}

		public enum GET_CLAN_LOG_RESULT : byte
		{
			GET_CLAN_LOG_SUCCESS = 1,
			GET_CLAN_LOG_NOT_CLAN,
			GET_CLAN_LOG_NOT_LOGBUFFER1,
			GET_CLAN_LOG_NO_SET
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_GET_CLAN_LOG
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGRSERVER_GET_CLAN_LOG_RESULT
		{
			public GET_CLAN_LOG_RESULT nResult;

			public ushort wCountOfList;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public IN_CLAN_LOG_INFO[] tClanLogInfo;
		}

		public enum INCREASE_PERSON_RESULT : byte
		{
			INCREASE_PERSON_SUCCESS = 1,
			INCREASE_PERSON_CLAN_IS_NOT_EXIST,
			INCREASE_PERSON_MEMBER_IS_NOT_EXIST,
			INCREASE_PERSON_NOT_7_DAYS,
			INCREASE_PERSON_ITEM_IS_NOT_EXIST,
			INCREASE_PERSON_NOT_ENOUGH_ITEM_COUNT,
			INCREASE_PERSON_NOT_FOUND_ITEM,
			INCREASE_PERSON_NOT_MISMATCH_ITEM_TYPE,
			INCREASE_PERSON_IS_NOT_CLAN_MEMBER,
			INCREASE_PERSON_DIS_CLAN_SERVER,
			INCREASE_PERSON_UPDATE_FAIL,
			INCREASE_PERSON_IS_FULL,
			INCREASE_PERSON_NOT_SAME_MAX_COUNT,
			INCREASE_PERSON_INCORRECT_ITEM_COUNT,
			INCREASE_PERSON_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_INCREASE_PERSON
		{
			public long nInvenSrl;

			public ushort UseItemCount;

			public short ssCurrentPerson;

			public short ssMaxPerson;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_INCREASE_PERSON_RESULT
		{
			public INCREASE_PERSON_RESULT nResult;

			public long nInvenSrl;

			public int nGauge;

			public short ssMaxPerson;

			public byte ucUnitCount;
		}

		public enum IS_ABLE_TO_CONFIRM_RESULT : byte
		{
			IS_ABLE_TO_CONFIRM_SUCCESS = 1,
			IS_ABLE_TO_CONFIRM_CLAN_IS_NOT_EXIST,
			IS_ABLE_TO_CONFIRM_MASTER_IS_NOT_EXIST,
			IS_ABLE_TO_CONFIRM_INVALID_GRADE,
			IS_ABLE_TO_CONFIRM_DIS_CLANSERVER,
			IS_ABLE_TO_TRANSFER_MASTER_CLAN_SERVER_ERROR,
			IS_ABLE_TO_CONFIRM_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_IS_ABLE_TO_CONFIRM
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGRSERVER_IS_ABLE_TO_CONFIRM_RESULT
		{
			public IS_ABLE_TO_CONFIRM_RESULT nResult;

			public byte bIsProgress;

			public byte bIsAbleConfirm;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szTransferMasterNick;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string szConfirmDate;
		}

		public enum DIFF_7DAYS_RESULT : byte
		{
			DIFF_7DAYS_SUCCESS = 1,
			DIFF_7DAYS_NOT_7_DAYS,
			DIFF_7DAYS_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_DIFF_7DAYS
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGRSERVER_DIFF_7DAYS_RESULT
		{
			public DIFF_7DAYS_RESULT nResult;

			public short ssMaxCount;
		}

		public enum REQUEST_CLAN_COUNT_INFO_RESULT : byte
		{
			REQUEST_CLAN_COUNT_INFO_SUCCESS = 1,
			REQUEST_CLAN_COUNT_DIS_CLAN_SERVER,
			REQUEST_CLAN_COUNT_INFO_FAIL
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GET_RECORD_COUNT
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GET_RECORD_COUNT_RESULT
		{
			public REQUEST_CLAN_COUNT_INFO_RESULT nResult;

			public int nClanWin;

			public int nClanLose;

			public int nClanDraw;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GET_COMMANDER_COUNT
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GET_COMMANDER_COUNT_RESULT
		{
			public REQUEST_CLAN_COUNT_INFO_RESULT nResult;

			public byte ucLargeBossCount;

			public byte ucMiddleBossCount;

			public byte ucSmallBossCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GET_STAFF_COUNT
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GET_STAFF_COUNT_RESULT
		{
			public REQUEST_CLAN_COUNT_INFO_RESULT nResult;

			public byte ucSubMasterCount;

			public byte ucStaff01Count;

			public byte ucStaff02Count;

			public byte ucStaff03Count;

			public byte ucStaff04Count;
		}

		public enum GET_ALL_CLAN_COUNT_RESULT : byte
		{
			GET_ALL_CLAN_COUNT_SUCCESS = 1,
			GET_ALL_CLAN_COUNT_FAIL
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GET_ALL_CLAN_COUNT
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GET_ALL_CLAN_COUNT_RESULT
		{
			public GET_ALL_CLAN_COUNT_RESULT nResult;

			public uint dwCountOfTotalClan;
		}

		public enum GET_CLAN_BASE_INFO_RESULT : byte
		{
			GET_BASE_INFO_SUCCESS = 1,
			GET_BASE_INFO_IS_NOT_CLAN_MEMBER,
			GET_BASE_DIS_CLAN_SERVER,
			GET_BASE_INFO_FAIL
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GET_BASE_CLAN_INFO
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GET_BASE_CLAN_INFO_RESULT
		{
			public GET_CLAN_BASE_INFO_RESULT nResult;
			public long ClanKey;
			public CLAN_GRADE ClanGrade;
			public CLAN_UNIT_INFO UnitInfo;
		}

		public enum CLAN_CHAT_RESULT : byte
		{
			CLAN_CHAT_SUCCESS = 1,
			CLAN_CHAT_DIS_CLAN_SERVER,
			CLAN_CHAT_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CLAN_CHAT
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string szContents;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CLAN_CHAT_RESULT
		{
			public CLAN_CHAT_RESULT nResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CLANMGR_CLAN_CHAT_NOTIFY
		{
			public CLAN_GRADE RequesterGrade;

			public CLAN_UNIT_INFO ClanUnitInfo;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string RequesterNick;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string szContents;
		}

		public enum DISCONNECT_USER_RESULT
		{
			DISCONNECT_USER_RESULT_SUCCESS = 1,
			DISCONNECT_USER_RESULT_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_DISCONNECT_USER_RESULT
		{
			public DISCONNECT_USER_RESULT nResult;
		}

		public enum CLAN_PASSWORD_RESULT
		{
			CLAN_PW_SUCCESS = 1,
			CLAN_PW_NOT_SAME_PASSWORD,
			CLAN_PW_INVALID_PASSWORD_KIND,
			CLAN_PW_INVALID_PASSWORD_COUNT,
			CLAN_PW_INVALID_ANSWER,
			CLAN_PW_INVALID_ANSWER_COUNT,
			CLAN_PW_INVALID_ANSWER_LANGUEGE,
			CLAN_PW_INVALID_QUESTION_COUNT,
			CLAN_PW_INVALID_QUESTION_LANGUEGE,
			CLAN_PW_DIS_CLAN_SERVER,
			CLAN_PW_USER_IS_NOT_MASTER,
			CLAN_PW_INCORRECT_GRADE,
			CLAN_PW_FAIL
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_INFO_REQUEST
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_INFO_RESULT
		{
			public CLAN_PASSWORD_RESULT nResult;

			public byte bIsRegisterNewPassword;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
			public string szPasswordQuestion;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_CHECK_REQUEST
		{
			public byte bIsCheckTime;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string szPassWord;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_CHECK_RESULT
		{
			public CLAN_PASSWORD_RESULT nResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_TIME_CHECK_REQUST
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_TIME_CHECK_RESULT
		{
			public CLAN_PASSWORD_RESULT nResult;

			public byte bIsInsertPassword;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_NEW_PW_REQUEST
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string szNewPassword;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
			public string szNewQeustion;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
			public string szNewAnswer;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_NEW_PW_RESULT
		{
			public CLAN_PASSWORD_RESULT nResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_CHANGE_REQUEST
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string szCurrentPassword;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string szChangePassword;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_CHANGE_RESULT
		{
			public CLAN_PASSWORD_RESULT nResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_Q_AND_A_CHANGE_REQUEST
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
			public string szAnswer;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
			public string szNewQuestion;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
			public string szNewAnswer;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_Q_AND_A_CHANGE_RESULT
		{
			public CLAN_PASSWORD_RESULT nResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_GET_BACK_REQUEST
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
			public string szAnswer;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string szNewPassword;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGRSERVER_CLAN_PW_GET_BACK_RESULT
		{
			public CLAN_PASSWORD_RESULT nResult;
		}

		public enum CLAN_PAYMENT_CASH_RESULT : byte
		{
			CLAN_PAYMENT_CASH_SUCCESS = 1,
			CLAN_PAYMENT_CASH_CLAN_IS_NOT_EXIST,
			CLAN_PAYMENT_CASH_IS_NOT_MASTER,
			CLAN_PAYMENT_CASH_INVALID_GRADE,
			CLAN_PAYMENT_CASH_INVALID_NAME,
			CLAN_PAYMeNT_CASH_INVALID_MARK,
			CLAN_PAYMENT_CASH_NOT_ENOUGH_CASH,
			CLAN_PAYMENT_CASH_IS_NOT_CLAN_MEMBER,
			CLAN_PAYMENT_CASH_DIS_CLAN_SERVER,
			CLAN_PAYMENT_CASH_UPDATE_FAIL,
			CLAN_PAYMENT_CASH_FAIL
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_CLAN_NAME_BY_CASH
		{
			public int nPaymentCost;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_CLAN_NAME_BY_CASH_RESULT
		{
			public CLAN_PAYMENT_CASH_RESULT nResult;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
			public string szClanName;

			public int nRemainCash;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_MEMBER_EXPAND_BY_CASH
		{
			public int nPaymentCost;

			public short ssCurrentPerson;

			public short ssMaxPerson;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_MEMBER_EXPAND_BY_CASH_RESULT
		{
			public CLAN_PAYMENT_CASH_RESULT nResult;

			public int nRemainCash;

			public short ssMaxPerson;

			public byte byUnitCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_CLAN_MARK_BY_CASH
		{
			public int nPaymentCost;

			public CLAN_MARK_INFO ClanMarkInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CHANGE_CLAN_MARK_BY_CASH_RESULT
		{
			public CLAN_PAYMENT_CASH_RESULT nResult;

			public CLAN_MARK_INFO ClanMarkInfo;

			public int nRemainCash;
		}

		public enum CLAN_MARK_LIST_RESULT : byte
		{
			CLAN_MARK_LIST_SUCCESS = 1,
			CLAN_MARK_LIST_CLAN_IS_NOT_EXIST,
			CLAN_MARK_LIST_IS_NOT_MASTER,
			CLAN_MARK_LIST_INVALID_GRADE,
			CLAN_MARK_LIST_IS_NOT_CLAN_MEMBER,
			CLAN_MARK_LIST_DIS_CLAN_SERVER,
			CLAN_MARK_LIST_INVALID_TYPE,
			CLAN_MARK_LIST_FAIL
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CLAN_MARK_LIST
		{
			public CLAN_MARK_TYPE ClanMarkType;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_CLAN_MARK_LIST_RESULT
		{
			public CLAN_MARK_LIST_RESULT nResult;

			public ushort wCountOfList;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 300)]
			public CLAN_MARK_LIST_UNIT_INfO[] ClanMarkUnitInfo;
		}

		public enum SEARCH_CLANKEY_RESULT : byte
		{
			SEARCH_CLANKEY_SUCCESS = 1,
			SEARCH_CLANKEY_FAIL,
			SEARCH_CLANKEY_CLAN_IS_NOT_EXIST,
			SEARCH_CLANKEY_DIS_CLAN_SERVER,
			SEARCH_CLANKEY_UNKNOWN_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_SEARCH_CLANKEY
		{
			public long ClanKey;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CLANMGR_SEARCH_CLANKEY_RESULT
		{
			public SEARCH_CLANKEY_RESULT nResult;

			public CLAN_BASE_INFO tClanInfo;
		}
	}
}
