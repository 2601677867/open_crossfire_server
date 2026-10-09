using System.Runtime.InteropServices;
using Network.ProtocolStruct;
using Network.SharedFolder;

namespace Network.Protocol
{
	public class P_ATNM
	{
		public const int PROTOCOL_MM_FIRST = 10;

		public const int PROTOCOL_AUTOLEAGUE_SYSTEM = 17;

		public const int PROTOCOL_AUTOLEAGUE_SERVERINFO_REQ = 1;
		public const int PROTOCOL_AUTOLEAGUE_SERVERINFO_RES = 2;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUELIST_REQ = 3;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUELIST_RES_START = 4;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUELIST_RES = 5;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUELIST_RES_END = 6;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUEINFO_REQ = 7;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUEINFO_RES = 8;
		public const int PROTOCOL_AUTOLEAGUE_TEAMLIST_REQ = 9;
		public const int PROTOCOL_AUTOLEAGUE_TEAMLIST_RES = 10;
		public const int PROTOCOL_AUTOLEAGUE_TOURNAMENTLIST_REQ = 11;
		public const int PROTOCOL_AUTOLEAGUE_TOURNAMENTLIST_RES = 12;
		public const int PROTOCOL_AUTOLEAGUE_CREATELEAGUE_REQ = 13;
		public const int PROTOCOL_AUTOLEAGUE_CREATELEAGUE_RES = 14;
		public const int PROTOCOL_AUTOLEAGUE_UPDATELEAGUE_REQ = 15;
		public const int PROTOCOL_AUTOLEAGUE_UPDATELEAGUE_RES = 16;
		public const int PROTOCOL_AUTOLEAGUE_CANCELLEAGUE_REQ = 17;
		public const int PROTOCOL_AUTOLEAGUE_CANCELLEAGUE_RES = 18;
		public const int PROTOCOL_AUTOLEAGUE_CREATETEAM_REQ = 19;
		public const int PROTOCOL_AUTOLEAGUE_CREATETEAM_RES = 20;
		public const int PROTOCOL_AUTOLEAGUE_DELETETEAM_REQ = 21;
		public const int PROTOCOL_AUTOLEAGUE_DELETETEAM_RES = 22;
		public const int PROTOCOL_AUTOLEAGUE_JOINTEAMUSER_REQ = 23;
		public const int PROTOCOL_AUTOLEAGUE_JOINTEAMUSER_RES = 24;
		public const int PROTOCOL_AUTOLEAGUE_LEAVETEAMUSER_REQ = 25;
		public const int PROTOCOL_AUTOLEAGUE_LEAVETEAMUSER_RES = 26;
		public const int PROTOCOL_AUTOLEAGUE_BANTEAMUSER_REQ = 27;
		public const int PROTOCOL_AUTOLEAGUE_BANTEAMUSER_RES = 28;
		public const int PROTOCOL_AUTOLEAGUE_BANTEAMUSER_TO_DELUSER = 29;
		public const int PROTOCOL_AUTOLEAGUE_MYLEAGUEINFO_REQ = 30;
		public const int PROTOCOL_AUTOLEAGUE_MYLEAGUEINFO_RES = 31;
		public const int PROTOCOL_AUTOLEAGUE_JOINTEAMROOMUSER_REQ = 32;
		public const int PROTOCOL_AUTOLEAGUE_JOINTEAMROOMUSER_RES = 33;
		public const int PROTOCOL_AUTOLEAGUE_LEAVETEAMROOMUSER_REQ = 34;
		public const int PROTOCOL_AUTOLEAGUE_LEAVETEAMROOMUSER_RES = 35;
		public const int PROTOCOL_AUTOLEAGUE_CREATELEAGUE_CMD = 36;
		public const int PROTOCOL_AUTOLEAGUE_UPDATELEAGUE_CMD = 37;
		public const int PROTOCOL_AUTOLEAGUE_DELETELEAGUE_CMD = 38;
		public const int PROTOCOL_AUTOLEAGUE_CREATETEAM_CMD = 39;
		public const int PROTOCOL_AUTOLEAGUE_DELETETEAM_CMD = 40;
		public const int PROTOCOL_AUTOLEAGUE_UPDATETEAM_CMD = 41;
		public const int PROTOCOL_AUTOLEAGUE_ENTERUSER_TEAMROOM_CMD = 42;
		public const int PROTOCOL_AUTOLEAGUE_LEAVEUSER_TEAMROOM_CMD = 43;
		public const int PROTOCOL_AUTOLEAGUE_TEAM_STATUS_CHAGE_USER_CMD = 44;
		public const int PROTOCOL_AUTOLEAGUE_USER_REWARD_STORAGE_REQ = 45;
		public const int PROTOCOL_AUTOLEAGUE_USER_REWARD_STORAGE_RES = 46;
		public const int PROTOCOL_AUTOLEAGUE_JOINUSER_TEAMROOM_CMD = 47;
		public const int PROTOCOL_AUTOLEAGUE_DELETEUSER_TEAMROOM_CMD = 48;
		public const int PROTOCOL_AUTOLEAGUE_CREATELEAGUE_BEFINFO_REQ = 49;
		public const int PROTOCOL_AUTOLEAGUE_CREATELEAGUE_BEFINFO_RES = 50;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUEBASEREWARD_REQ = 51;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUEBASEREWARD_RES = 52;
		public const int PROTOCOL_AUTOLEAGUE_DELETELEAGUE_USER_CMD = 53;
		public const int PROTOCOL_AUTOLEAGUE_WAITINGNO_CHANGE_USER_CMD = 54;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUE_GAME_5_MINUTES_USER_CMD = 55;
		public const int PROTOCOL_LEAGUE_GAME_RESULT_GIVE_REWARD_ITEM = 56;
		public const int PROTOCOL_AUTOLEAGUE_STORAGE_TO_MASTER_INVENTORY_REQ = 57;
		public const int PROTOCOL_AUTOLEAGUE_STORAGE_TO_MASTER_INVENTORY_RES = 58;
		public const int PROTOCOL_AUTOLEAGUE_CREATELEAGUEGAMEROOM_REQ = 60;
		public const int PROTOCOL_AUTOLEAGUE_TEAM_DELETE_USER_CMD = 64;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUE_GAME_15_MINUTES_USER_CMD = 65;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUE_GAME_NOT_PLAY_USER_DELETE_CMD = 66;
		public const int PROTOCOL_AUTOLEAGUE_LEAGUE_GAME_RESULT = 67;
		public const int PROTOCOL_AUTOLEAUGE_LEAGUE_GAME_30SEC_CMD = 68;
		public const int PROTOCOL_AUTOLEAGUE_GIVE_LEAGUEREWARD_RESULT = 69;
		public const int PROTOCOL_AUTOLEAGUE_CHECK_LEAGUENAME_DUPLICATE_REQ = 70;
		public const int PROTOCOL_AUTOLEAGUE_CHECK_LEAGUENAME_DUPLICATE_RES = 71;
		public const int PROTOCOL_AUTOLEAGUE_CLIENT_LEAVE_AUTOLEAGUE_CMD = 72;
		public const int PROTOCOL_AUTOLEAGUE_CHANGE_ROOMID_CMD = 73;
		public const int PROTOCOL_AUTOLEAGUE_CHANGE_TEAMSERVERINFO_CMD = 74;
		public const int PROTOCOL_AUTOLEAGUE_GAMEROOM_TIMEINFO_REQ = 75;
		public const int PROTOCOL_AUTOLEAGUE_GAMEROOM_TIMEINFO_RES = 76;
		public const int PROTOCOL_AUTOLEAGUE_GIVE_LEAGUETICKET_TO_MASTER = 77;
		public const int PROTOCOL_AUTOLEAGUE_USER_LOG = 78;
		public const int PROTOCOL_AUTOLEAGUE_BUY_LEAGUEREWARD_REQ = 100;
		public const int PROTOCOL_AUTOLEAGUE_BUY_LEAGUEREWARD_RES = 101;
		
		public enum LeagueStatus
		{
			RECRUIT_LEAGUE_STATUS,
			PLAY_JOIN_LEAGUE_STATUS,
			PLAY_LEAGUE_STATUS,
			FINISH_LEAGUE_STATUS
		}

		public enum TeamStatus
		{
			RECRUIT_TEAM_STATUS,
			CONFIRM_TEAM_STATUS,
			WAIT_TEAM_STATUS
		}

		public enum TeamDeleteStatus
		{
			LEAGUE_MASTER_DELETE,
			NOT_CONFIRM_TEAM_DELETE
		}

		public enum TeamGameResult
		{
			GAME_WAIT,
			GAME_WIN,
			GAME_LOSE
		}

		public enum TeamUserStatus
		{
			WAIT_TEAM_USER_STATUS,
			CONFIRM_TEAM_USER_STATUS,
			BANISH_TEAM_USER_STATUS
		}

		public enum MatchType
		{
			MATCH_32_TYPE,
			MATCH_16_TYPE,
			MATCH_8_TYPE,
			MATCH_4_TYPE,
			MATCH_2_TYPE,
			MAX_MATCH_TYPE
		}

		public enum MatchTypeTeamNum
		{
			MATCH_32_TEAM_NUM = 32,
			MATCH_16_TEAM_NUM = 16,
			MATCH_8_TEAM_NUM = 8,
			MATCH_4_TEAM_NUM = 4,
			MATCH_2_TEAM_NUM = 2
		}

		public enum ScheduleType
		{
			HOUR_SCHEDULE_TYPE,
			DAY_SCHEDULE_TYPE
		}

		public enum RewardType
		{
			FIRST_REWARD_TYPE = 1,
			SECOND_REWARD_TYPE
		}

		public enum TeamRoomUserStatus
		{
			JOINED_TEAM_ROOM_USER = 1,
			OBSERVER_TEAM_ROOM_USER
		}

		public enum TeamGameStatus
		{
			WAIT_TEAM_GAME_STATUS,
			CREATE_ROOM_TEAM_GAME_STATUS,
			PLAYING_TEAM_GAME_STATUS,
			GAME_END_TEAM_GAME_STATUS
		}

		public enum DeleteUserReason
		{
			SELF_DELETE_USER = 2,
			TEAM_MASTER_DELETE_USER,
			LEAGUE_TEAM_DELETE_USER,
			TEAM_ROOM_DELETE_USER
		}

		public enum LeagueGameResult
		{
			LEAGUE_GAME_WIN = 1,
			LEAGUE_GAME_LOSE,
			LEAGUE_GAME_WIN_BY_NOMATCH,
			LEAGUE_GAME_END_TO_OBSERVER,
			LEAGUE_GAME_TIMEOUT,
			LEAGUE_GAME_DRAW_WIN,
			LEAGUE_GAME_DRAW_LOSE
		}

		public enum GameStartStatus
		{
			WAIT_GAME_START_STATUS,
			READY_GAME_START_STATUS,
			STARTING_GAME_START_STATUS
		}

		public enum DayofWeekScheduleType
		{
			SUNDAY_TYPE,
			MONDAY_TYPE,
			TUESDAY_TYPE,
			WEDNESDAY_TYPE,
			THURSDAY_TYPE,
			FRIDAY_TYPE,
			SATURDAY_TYPE,
			MAX_DAY_OF_WEEK_NUM
		}

		public enum CreateLeagueType
		{
			LEAGUE_32_TYPE,
			LEAGUE_16_TYPE,
			LEAGUE_8_TYPE,
			MAX_LEAGUE_TYPE
		}

		public enum UserLeagueLocate
		{
			USER_NOT_IN_LEAGUE,
			USER_IN_LEAGUE_LOBBY,
			USER_IN_LEAGUETEAM_LOBBY,
			USER_IN_TEAMROOM
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct VALID_USER_INFO
		{
			public long USN;

			public ushort wKey;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Channel_Info
		{
			public int key_;

			public int user_count_;

			public int max_user_;

			public int room_count_;

			public int max_room_;

			public int limit_level_;

			public int league_room_;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Server_Info
		{
			public ushort m_wMaxTotalUserCount;

			public ushort m_wCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
			public Channel_Info[] m_ChannelInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct League_Info
		{
			public uint m_dwLeagueIndex;

			public long m_MasterUsn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string m_szMasterNickName;

			public uint m_dwLeagueScheduleType;

			public MatchType m_dwLeagueMatchType;

			public uint m_dwMap;

			public ROUNDTYPE m_dwMode;

			public int m_bVisit;

			public SYSTEMTIME m_tStartTime;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string m_szLeagueName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
			public string m_szLeaguePW;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 251)]
			public string m_szLeagueDesc;

			public uint m_dwLeagueStatus;

			public int m_bEnableCancel;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string m_szLeagueItemID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Team_Match
		{
			public uint m_dwFirstTeamIndex;

			public uint m_dwSecondTeamIndex;

			public uint m_dwTeamMatchStatus;

			public uint m_dwIsWinTeam;

			public uint m_dwServerNo;

			public int m_nChannelNo;

			public int m_nRoomNo;

			public uint m_dwFirstTeamRecordIndex;

			public uint m_dwSecondTeamRecordIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Team_Record
		{
			public uint m_dwTeamIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string m_szTeamName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
			public string m_szTeamPW;

			public byte m_TeamRecord;

			public byte m_LastTeamRecordMatchType;

			public uint m_dwOrdering;

			public uint m_dwServerNo;

			public int m_nChannelNo;

			public int m_nRoomNo;

			public uint m_TeamUserCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			public uint[] m_arrTeamUserUSN;

			public int m_bMatchFinished;

			public uint m_dwTeamRoomUserCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Team_Info
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public long m_TeamMasterUsn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string m_szTeamName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
			public string m_szTeamPW;

			public uint m_dwStatus;

			public uint m_dwRecord;

			public uint m_dwTeamUserCount;

			public uint m_dwWaitingNo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Team_User_Info
		{
			public uint m_dwTeamIndex;

			public uint m_dwTeamUserIndex;

			public uint m_USN;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string m_szNickName;

			public int m_nLevel;

			public uint m_dwUserStatus;

			public SYSTEMTIME m_tRegDate;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Reward_Item
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string m_szItemID;

			public uint m_dwRewardType;

			public byte m_bBaseItem;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct League_Reward_Item
		{
			public uint m_dwLeagueIndex;

			public uint m_dwRewardCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public Reward_Item[] m_arrRewardItem;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Addtion_Reward
		{
			public uint m_dwLeagueIndex;

			public uint m_MasterUsn;

			public uint m_RewardCount;

			public uint m_dwRewardStatus;

			public uint m_dwRewardUseStatus;

			public Reward_Item m_RewardItem;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Schedule_Info
		{
			public int m_bActive;

			public uint m_dwLeagueIndex;

			public uint m_dwMatchType;

			public SYSTEMTIME m_tStartDate;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Match_Info
		{
			public uint m_dwLeagueIndex;

			public uint m_dwMatchIndex;

			public uint m_dwFirstTeamIndex;

			public uint m_dwSecondTeamIndex;

			public uint m_dwCurrentMatchType;

			public uint m_dwWinnerTeamIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Team_WaitingNo
		{
			public uint m_dwTeamIndex;

			public uint m_dwWaitingNo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct User_Info
		{
			public VALID_USER_INFO m_ValidUser;

			public uint m_dwTeamIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct UserLeagueCancelInfo
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public uint[] m_nLeagueCancelCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct UserLeagueRewardStorage
		{
			public uint m_StorageSrl;

			public uint m_SubSrl;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string m_ItemID;

			public byte m_IsReg;

			public uint m_dwLeagueIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct LeagueMatchSchedule
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 168)]
			public uint[] m_arrMatchSchedule;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct LeagueList
		{
			public League_Info m_stLeagueInfo;

			public League_Reward_Item m_stLeagueReward;

			public uint m_dwCurrentTeamCount;

			public uint m_dwMaxTeamCount;

			public int m_isJoinedLeague;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct TeamList
		{
			public Team_Info m_stTeamInfo;

			public SYSTEMTIME m_StartDate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string m_szLeagueName;

			public MatchType m_eMatchType;

			public ScheduleType m_eScheduleType;

			public uint m_dwServerNo;

			public int m_nChannelNo;

			public int m_nRoomNo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct GiveRewardItem
		{
			public int m_nItemIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string m_szItemID;

			public long m_nInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
			public long[] m_nDefaultDressInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
			public long[] m_nDefaultFuncInvenSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct GiveRewardItemList
		{
			public uint m_dwRewardInvenCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 180)]
			public GiveRewardItem[] m_RewardItem;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct TeamUserVVIPItemChange
		{
			public VALID_USER_INFO m_UserInfo;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_dwVVIPItemCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Team_Server_Info
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_dwServerNo;

			public int m_nChannelNo;

			public int m_nRoomNo;
		}

		public enum PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE
		{
			MM_AUTOLEAGUE_RESULT_SUCCESS,
			MM_AUTOLEAGUE_RESULT_ERROR_DB_CONNECTION_ERROR,
			MM_AUTOLEAGUE_RESULT_ERROR_DB_EXECUTE,
			MM_AUTOLEAGUE_RESULT_ERROR_LEAGUE_CREATE_FAIL,
			MM_AUTOLEAGUE_RESULT_ERROR_LEAGUE_CREATE_EQUAL_NAME,
			MM_AUTOLEAGUE_RESULT_ERROR_LEAGUE_CREATE_LIMIT,
			MM_AUTOLEAGUE_RESULT_ERROR_LEAGUE_CREATE_START_DATE,
			MM_AUTOLEAGUE_RESULT_ERROR_LEAGUE_CREATE_PER_USER_LIMIT,
			MM_AUTOLEAGUE_RESULT_ERROR_LEAGUE_MODIFY_FAIL,
			MM_AUTOLEAGUE_RESULT_ERROR_NOT_EXIST_LEAGUE,
			MM_AUTOLEAGUE_RESULT_ERROR_NOT_EXIST_TEAM,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_FULL,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_CREATE_EQUAL_NAME,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_DELETE_MIN_COUNT,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_USER_FULL,
			MM_AUTOLEAGUE_RESULT_ERROR_INVALID_TEAMNAME,
			MM_AUTOLEAGUE_RESULT_ERROR_INVALID_PASSWORD,
			MM_AUTOLEAGEU_RESULT_ERROR_SWAPTEAM_FAIL,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_LEAGUE_MASTER_CANCEL,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_LEAGUE_NOT_COMFIRM,
			MM_AUTOLEAGUE_RESULT_ERROR_NOT_EXIST_TEAM_USER,
			MM_AUTOLEAGUE_RESULT_ERROR_NOT_EXIST_TEAM_ROOM_USER,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_USER_JOIN_ALREADY_USER,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_USER_DELETE_ALREADY_USER,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_OBSERVER_USER,
			MM_AUTOLEAGUE_RESULT_ERROR_USER_NOT_TEAM_MASTER,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_USER_JOINED_LIMIT,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_USER_DUPLICATION_SCHEDULE,
			MM_AUTOLEAGUE_RESULT_ERROR_NOT_LEAGUE_MASTER,
			MM_AUTOLEAGUE_RESULT_ERROR_NOT_AVAILABLE_CANCEL_LEAGUE,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_USER_BANISH_USER,
			MM_AUTOLEAGUE_RESULT_ERROR_TEAM_ROOM_ENTER,
			MM_AUTOLEAGUE_RESULT_ERROR_LEAGUE_CANCEL,
			MM_AUTOLEAGUE_RESULT_ERROR_LEAGUE_STATUS_IS_NOT_RECRUIT,
			MM_AUTOLEAGUE_RESULT_NOT_EXIST_ADD_REWARD,
			MM_AUTOLEAGUE_RESULT_ADD_REWARD_BE_SHORT,
			MM_AUTOLEAGUE_RESULT_ADD_REWARD_OVER_COUNT,
			MM_AUTOLEAGUE_RESULT_ALREADY_REG_REWARD,
			MM_AUTOLEAGUE_RESULT_NOT_VALID_TICKET,
			MM_AUTOLEAGUE_RESULT_TOURNAMENTLIST_IS_NOT_OPEN,
			MM_AUTOLEAGUE_RESULT_SERVER_NOT_CONNECTED,
			MM_AUTOLEAGUE_RESULT_ERROR_ROOM_CREATE_FAIL,
			MM_AUTOLEAGUE_RESULT_ERROR_ALREADY_ROOM,
			MM_AUTOLEAGUE_RESULT_ERROR_NOT_PLAY_GAME,
			MM_AUTOLEAGUE_RESULT_ERROR_BUYITEM_STORAGE_IS_FULL,
			MM_AUTOLEAGUE_RESULT_ERROR_BUYITEM_NOT_CASH_ITEM,
			MM_AUTOLEAGUE_RESULT_ERROR_BUYITEM_FAIL,
			MM_AUTOLEAGUE_RESULT_ERROR_BUYITEM_NOT_ENOUGH_CASH,
			MM_AUTOLEAGUE_RESULT_ERROR_NOT_PLAYING_GAME,
			MM_AUTOLEAGUE_RESULT_ERROR_RECEIVE_ITEM_DUPLICATED_CHARCTER,
			MM_AUTOLEAGUE_RESULT_ERROR_RECEIVE_ITEM_FAILED,
			MM_AUTOLEAGUE_RESULT_ERROR_LOSE_TEAM_ROOM_ENTER,
			MM_AUTOLEAGUE_RESULT_ERROR_PLAYING_LEAGUE_TEAM_ROOM_ENTER,
			MM_AUTOLEAGUE_RESULT_SERVER_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_SERVERINFO_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_LEAGUELIST_REQ
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AUTOLEAGUE_LEAGUELIST_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
			public LeagueList[] m_arrLeagueList;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_LEAGUEINFO_REQ
		{
			public uint m_dwLeagueIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
			public string m_szPassword;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_LEAGUEINFO_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public int m_bIsTournamentOpen;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_TEAMLIST_REQ
		{
			public uint m_dwLeagueIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_TEAMLIST_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public uint m_TeamCount;

			public uint m_dwJoinedTeamIndex;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
			public Team_Info[] m_arrTeamInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_TOURNAMENTLIST_REQ
		{
			public uint m_dwLeagueIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_TOURNAMENTLIST_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamCount;

			public uint m_dwMatchType;

			public uint m_dwCurrentMatchType;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public Team_Record[] m_arrGameTeamIndex;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 160)]
			public uint[] m_arrRecordTeamIndex;

			public uint m_dwJoinedTeamIndex;

			public uint m_dw1stTeamIndex;

			public uint m_dw2ndTeamIndex;

			public uint m_LeagueMasterUSN;

			public int m_bVisit;

			public SYSTEMTIME m_tGameStartTime;

			public SYSTEMTIME m_tCurrentTime;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AUTOLEAGUE_CREATELEAGUE_REQ
		{
			public long m_AutoLeagueItemInvenSRL;

			public League_Info m_AutoLeagueInfo;

			public uint m_dwFirstRewardItemCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public uint[] m_arrFirstRewawrdItemSrl;

			public uint m_dwSecondRewarditemCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public uint[] m_arrSecondRewardItemSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CREATELEAGUE_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public League_Info m_AutoLeagueInfo;

			public long m_nLeagueItemInvenSrl;

			public uint m_dwLeagueItemRemainCount;

			public uint m_dwFirstRewardItemCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public uint[] m_arrFirstRewawrdItemSrl;

			public uint m_dwSecondRewardItemCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public uint[] m_arrSecondRewawrdItemSrl;

			public League_Reward_Item m_stLeagueReward;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_UPDATELEAGUE_REQ
		{
			public uint m_dwLeagueIndex;

			public int m_bVisit;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string m_szLeagueName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
			public string m_szLeaguePW;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 251)]
			public string m_szLeagueDesc;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_UPDATELEAGUE_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public int m_bVisit;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string m_szLeagueName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
			public string m_szLeaguePW;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 251)]
			public string m_szLeagueDesc;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CANCELLEAGUE_REQ
		{
			public uint m_dwLeagueIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AUTOLEAGUE_CANCELLEAGUE_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string m_szLeagueItemID;

			public uint m_dwLeagueItemCnt;

			public long m_szLeagueItemInvenSrl;

			public uint m_dwLeagueItemIndex;

			public uint m_dwMatchType;

			public int m_nCancelCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CREATETEAM_REQ
		{
			public uint m_dwLeagueIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string m_szName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
			public string m_szPW;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CREATETEAM_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_DELETETEAM_REQ
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_DELETETEAM_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_JOINTEAMUSER_REQ
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public int m_bInvite;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AUTOLEAGUE_JOINTEAMUSER_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_dwTeamStatus;

			public Team_Room_User m_TeamRoomUser;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_LEAVETEAMUSER_REQ
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_LEAVETEAMUSER_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_dwTeamStatus;

			public int m_bTeamDestroy;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_BANTEAMUSER_REQ
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_DeleteUSN;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_BANTEAMUSER_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_DeleteUSN;

			public uint m_dwDeleteReason;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_BANTEAMUSER_TO_DELUSER
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string m_szTeamName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_MYLEAGUEINFO_REQ
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AUTOLEAGUE_MYLEAGUEINFO_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint dwJoinedLeagueCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
			public TeamList[] JoinTeamInfo;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 90)]
			public string szDummy;
			
			public uint dwCreatedLeagueCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
			public League_Info[] CreateLeagueInfo;

			public SYSTEMTIME m_tCurrentTime;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_JOINTEAMROOMUSER_REQ
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
			public string m_szPassword;

			public int m_bInvited;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AUTOLEAGUE_JOINTEAMROOMUSER_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_dwTeamRoomUserCount;

			public LeagueStatus m_eLeagueStatus;

			public Team_Info m_stTeamInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			public Team_Room_User[] m_arrTeamRoomUser;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
			public string m_szLeaguePW;

			public SYSTEMTIME m_tGameStartTime;

			public SYSTEMTIME m_tCurrentTime;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_LEAVETEAMROOMUSER_REQ
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_LEAVETEAMROOMUSER_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public int m_bIsTournamentOpen;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CREATELEAGUE_CMD
		{
			public LeagueList m_stLeagueList;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_UPDATELEAGUE_CMD
		{
			public uint m_dwLeagueIndex;

			public int m_bVisit;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string m_szLeagueName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
			public string m_szLeaguePW;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 251)]
			public string m_szLeagueDesc;

			public uint m_dwCurrentTeamCount;

			public uint m_dwMaxTeamCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_DELETELEAGUE_CMD
		{
			public uint m_dwLeagueIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CREATETEAM_CMD
		{
			public Team_Info m_stTeamInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_DELETETEAM_CMD
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_UPDATETEAM_CMD
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_dwCurrentUserCount;

			public uint m_dwStatus;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_ENTERUSER_TEAMROOM_CMD
		{
			public uint m_dwLeagueIndex;
			
			public uint m_dwTeamIndex;

			public Team_Room_User m_TeamRoomUser;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAEGUE_LEAVEUSER_TEAMROOM_CMD
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_LeaveUSN;

			public int m_bJoinedUser;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_TEAM_STATUS_CHANGE_USER_CMD
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_dwTeamStatus;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_USER_REWARD_STORAGE_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwStorageItemCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
			public UserLeagueRewardStorage[] m_arrRewardStorageItem;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AUTOLEAGUE_JOINUSER_TEAMROOM_CMD
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_dwTeamStatus;

			public long m_JoinedUSN;

			public Team_Room_User m_TeamRoomUser;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AUTOLEAGUE_DELETEUSER_TEAMROOM_CMD
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_dwTeamStatus;

			public long m_DeleteUSN;

			public uint m_dwDeleteReason;

			public uint m_dwTeamUserIndex;

			public int m_bTeamDestory;

			public long m_TeamMasterUSN;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CREATELEGUE_BEFINFO_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 168)]
			public byte[] m_arrEnableMatch;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] m_dummy;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public uint[] m_arrCancelCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public uint[] m_arrEnableCreateCount;

			public SYSTEMTIME m_EnableCreateTime;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AUTOLEAGUE_LEAGUEBASEREWARD_RES
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 272)]
			public string m_szDummy;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public League_Reward_Item[] m_arrLeagueRewardItem;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_DELETELEAGUE_USER_CMD
		{
			public uint m_dwLeagueIndex;

			public uint m_dwReason;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string m_szLeagueName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_WAITINGNO_CHANGE_USER_CMD
		{
			public uint m_dwWaitingNo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_LEAGUE_GAME_5_MINUTES_USER_CMD
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public uint m_dwServerNo;

			public int m_nChannelNo;

			public int m_nRoomNo;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string m_szLeagueName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string m_szTeamName;

			public short m_MapIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_LEAGUE_GAME_RESULT_GIVE_REWARD_ITEM
		{
			public uint m_dwLeagueIndex;

			public uint m_dwRewardType;

			public uint m_dwRewardInvenCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 180)]
			public GiveRewardItem[] m_RewardItem;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_STORAGE_TO_MASTER_INVENTORY_REQ
		{
			public uint m_dwStorageSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_STORAGE_TO_MASTER_INVENTORY_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public uint m_dwStorageSrl;

			public uint m_dwRewardInvenCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
			public GiveRewardItem[] m_RewardItem;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CREATELEAGUEGAMEROOM_REQ
		{
			public uint m_dwLeagueIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_TEAM_DELETE_USER_CMD
		{
			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string m_szTeamName;

			public TeamDeleteStatus m_dwDeleteReason;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEGUE_LEAGUE_GAME_NOT_PLAY_USER_DELETE_CMD
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string m_szLeagueName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string m_szTeamName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_LEAGUE_GAME_RESULT
		{
			public LeagueGameResult m_eGameResult;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string m_szLeagueName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_GIVE_LEAGUEREWARD_RESULT
		{
			public uint m_dwGiveRewardType;

			public uint m_dwRewardCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
			public uint[] m_arrRewardSrl;

			public uint m_dwUnRegistRewardCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public uint[] m_arrunRegistRewardSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CHECK_LEAGUENAME_DUPLICATE_REQ
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string m_szLeagueName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CHECK_LEAGUENAME_DUPLICATE_RES
		{
			public int m_bDuplicated;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_CHANGE_ROOMID_CMD
		{
			public uint m_dwLeagueIndex;

			public uint m_dwFirstTeamIndex;

			public uint m_dwSecondTeamIndex;

			public int m_nRoomID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_ChANGE_TEAMSERVERINFO_CMD
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public Team_Server_Info[] m_arrTeamServerInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_GAMEROOM_TIMEINFO_REQ
		{
			public int nChannelIndex;

			public int nRoomID;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_GAMEROOM_TIMEINFO_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE m_eResult;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public SYSTEMTIME m_CurrentTime;

			public SYSTEMTIME m_StartTime;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_GIVE_LEAGUETICKET_TO_MASTER
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE m_eResult;

			public short nCount;

			public int nItemIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string szItemID;

			public long nInvenSrl;

			public uint m_dwMatchType;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_USER_LOG
		{
			public int m_USN;

			public byte m_PacketType;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public int m_nMatchType;

			public uint m_dwLogStatus;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_BUY_LEAGUEREWARD_REQ
		{
			public short nCount;

			public int nItemIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string szItemID;

			public long nInvenSRL;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AUTOLEAGUE_BUY_LEAGUEREWARD_RES
		{
			public PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eResult;

			public short nCount;

			public int nItemIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string szItemID;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			public long[] nRewardSrl;

			//public int nBonusGP;
			
			//public Clan_Instance tClanInfo;

			//public uint dwBuyTime;
		}
	}
#pragma warning restore 169
}
