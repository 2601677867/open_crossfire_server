using Network.ProtocolStruct.Inven;

using static Network.Protocol.SP_PK;

namespace Network.Protocol
{
	public enum SAVEDITEMINSTTYPE
	{
		SAVEDITEMINSTTYPE_CLIENT,
		SAVEDITEMINSTTYPE_SERVER
	}
	
	public static class P_SZ
	{
		public static SAVEDITEMINSTTYPE g_eSavedItemInstType = SAVEDITEMINSTTYPE.SAVEDITEMINSTTYPE_SERVER;
		public static string g_szDefaultKnife = "2010000601";
		public static string g_szDefaultKnifeCode = "C0006";

		public const int LENGTH_SOCKET_BUFFER = 17408;
		public const int PROTOCOL_NON_USER_AREA_SIZE = 9;
		public const int PROTOCOL_START_BYTE = 0xF1;
		public const int PROTOCOL_END_BYTE = 0xF2;
		
		public static readonly unsafe int[] PROTO_INVEN_ITEM_SIZE_ARRAY =
		{
			sizeof(PROTO_WEAPONITEM_INST), // IT_WEAPON
			sizeof(PROTO_DRESSITEM_INST),  // IT_DRESS
			sizeof(PROTO_CHARITEM_INST),   // IT_CHAR
			sizeof(PROTO_FUNCITEM_INST),   // IT_FUNC
			sizeof(PROTO_SACKITEM_INST),   // IT_SACK
			0							   // IT_PACKAGE
		};

		public static readonly int[] g_aWeaponSlotCount =
		{
			1, 		// WC_MAIN
			1,		// WC_SUB
			1,		// WC_KNIFE
			3		// WC_THROW
		};

		public const int MAX_ITEM = 7168;
		public const int MAX_MAP_INDEX = 1045;
		public const int MAX_CHARACTER_ITEM = 200;
		public const int MAX_LIST_ITEM = 1022;
		public const int MAX_GIFT_KEEP_DAY = 14;
		public const int MAX_DUSTBIN_ITEM_COUNT = 2047;
		public const int MAX_BAG_SET_COUNT = 2;
		public const int MAX_VVIP_ITEM_SPECIAL_BUFFS = 3;
		public const int MAX_VVIP_WEAPON_CC = 200;
		public const int MAX_CHAR_OPTION = 5;
		public const int MAX_ITEM_NAME = 101;
		public const int MAX_ITEM_GAUGE = 99999;

		public const int MAX_DRESS_PART = 7;
		public const int MAX_DRESS_LAYER = 2;
		public const int MAX_PERIOD_CHAR_FUNC = 10;
		
		public const int MAX_VVIP_UPGRADE_LEVEL_TYPE = 3;
		public const int MAX_VVIP_WEAPON_CC_PART_NUM = 5;
		public const int MAX_VVIP_WEAPON_CC_PART_COLOR_NUM = 4;
		public const int MAX_VVIP_WEAPON_CC_EX_PALETTE_NUM = 2;

		public const int MAX_SEAL_ITEM_COUNT = 7;
		public const int MAX_GIFT_RECVLIST = 50;
		
		public const int PROTO_CHAT_COLOR_EX_MAX = 10;

		public const int SF_MAX_USER_IN_ROOM = 50;
		public const int MM_CHANNEL_MAX_COUNT = 13;
		public const int MAX_CHANNEL_NAME = 32;
		public const int MAX_CHANNEL_USERLIST_NUM = 100;
		public const int MM_CHANNEL_MAX_USER = 300;
		public const int MM_ROOM_LIST = 300;
		public const int MM_CHANNEL_MAX_ROOM = 50;
		public const int MM_ROOM_TEAM_MAX_USER = SF_MAX_USER_IN_ROOM / 2;
		public const int MM_CHANNEL_PW_MAXLENGTH = 11;
		public const int MM_ROOM_PW_MAXLENGTH = 20;
		public const int MM_ROOM_NAME_MAXLENGTH = 27;
		public const int MAX_ROOM_NAME_HASH_LENGTH = 100;
		public const int MAX_UCC_MAP_PATH_LENGTH = 100;
		public const int MM_PROTO_INVITEE_LIST = 50;
		public const int MAX_ROOM_LOCATION_LIMIT_TIME = 300;
		public const int MAX_ROOM_PERMISSION_TIME_SEC = 30;
		
		public const int MAX_GAME_SERIAL = 15;
		public const int MAX_ITEM_ID = 11;
		public const int MAX_ITEM_CODE = 6;
		public const int SF_MAX_CHARACTER_NAME_LENGTH = 13;
		public const int SF_USER_CHARNAME_LENGTH = 13;
		public const int MAX_SACK_PER_USER = 7;
		public const int ITEM_PER_SACK = 6;
		public const int MAX_VVIP_ITEM_NUM = 32;
		public const int MAX_VVIP_ITEM_FUNCTION_NUM = 32;
		
		public const int MAX_INSTALMENT_ITEM_COUNT = 2;
		public const int MAX_INSTALMENT_PAY_COUNT = 10;
		
		public const int MAX_BILLING_CHARGE_NO_LENGTH = 32;
		public const int MAX_BILLING_MSG_LENGTH = 256;
		public const int MAX_BILLING_CHARGENO_LENGTH = 16;

		public const int MAX_SECOND_AUTH_URL_LEN = 4096;
		
		public const int MAX_VVIP_ATTACHMENT_WEAPON_TYPE = 3;
		
		public const int MAX_PROFILE_DISPLAY_RANK_INFO_COUNT = 3;
		
		public const int MAX_COUPON_LIST_NUM = 200;
		public const int MAX_GACHA_WINNER_LIST = 50;
		public const int MAX_GACHA_ITEMLIST_NUM = 21;
		public const int MAX_COUPON_EVENT_SRL = 3;
		public const int MAX_GACHA_STORAGE_LIST_NUM = 300;
		public const int MAX_GACHA_STORAGE_SRL_STR_LENGTH = 200;
		public const int MAX_GACHA_DISMANTLE_COUNT = 16;
		public const int MAX_GACHA_TRANSFER_ITEM_COUNT = 16;
		public const int MAX_GACHA_DISMANTLE_SRL_STR_LENGTH = 160;
		public const int MAX_GACHA_EVENT_ITEM_COUNT = 100;
		public const int MAX_GACHA_TYPE_NUM = 30;
		public const int MAX_GACHA_INDEX_NUM = 200;
		public const int MAX_GACHA_GROUP_NUM = 4;
		
		public const int MAX_AI_GACHA_LIST_NUM = 30;
		public const int MAX_AIGACHA_ITEM_NUM = 10;
		public const int MAX_AIGACHA_ITEM_GAME_NUM = 100;
		public const int MAX_AIGACHA_ITEM_DROP_NUM = 4;

		public const int MAX_WAVE_SLOT_COUNT = 10;
		
		public const int MAX_ACHIEVEMENT_PASSIVE_LIST = 100;
		public const int MAX_ACHIEVEMENT_LIST = 30;
		public const int MAX_ACHIEVEMENT_REWARD_COUNT = 2;
		public const int MAX_ACHIEVEMENT_PASSIVE_COUNT = 6;
		public const int MAX_ACHIEVEMENT_RESULT = 6;
		public const int MAX_ACHIEVEMENT_USERDATA_LIST = 100;
		
		public const int NEWBIEMISSION_REWARD_COUNT = 2;
		public const int NEWBIEMISSION_FINAL_REWARD_SELECTION_COUNT = 6;
		public const int MAX_BEGINNER_GUIDE_REWARD_ITEM_COUNT = 30;

		public const int MAX_CHATTING_MESSAGE_LENGTH = 256;

		public const int MAX_FUNC_ITEM = 50;
		public const int MAX_CASH_ITEM_COUNT_INFO = 30;
		public const int MAX_SEND_SPRAYINFO_TO_HOST = 30;
		public const int MAX_SEND_ACHIEVEINFO_TO_HOST = 21;
		public const int MAX_HOST_USERID = 21;
		public const int MAX_HOST_STARTDATE = 15;
		public const int GAME_SERIAL_LENGTH = 13;
		public const int MAX_REPORT_MSG_LENGTH = 101;

		public const int TEAM_INDEX_MAX_COUNT = 2;
		public const int TEAM_INDEX_BLACK_LIST = 0;
		public const int TEAM_INDEX_GLOBAL_RISK = 1;
		public const int TEAM_INDEX_INVALID = 2;

		public const int AIBOT_TM_DEFAULT_BOT_COUNT = 5;
		public const int AIBOT_TDM_DEFAULT_BOT_COUNT = 4;
		public const int AIBOT_MIX_DEFAULT_BOT_COUNT_EACH_TEAM = 4;
		
		public const int MAX_LEVEL = 101;
		public const int MAX_FEVER_LEVEL = 3;
		public const int MAX_MILEAGE_COOLTIME_ITEM_COUNT = 100;
		public const int MAX_WEEKLY_WEAPON_COUNT = 10;

		public const int SF_MAX_WEAPON_SLOT_NUM = 7;
		public const int GAMEENDTYPE_NUM = 7;
		public const int MAX_WEAPON_CATEGORY2 = 12;

		public const int MAX_SHOP_LIST = 500;
		public const int MAX_SHOP_ITEMINFO_NTY = 1000;
		public const int MAX_ITEM_TAB_LIST = 200;

		public const int MAX_SET_EFFECT_LIST = 200;
		public const int MAX_SET_EFFECT_GROUP_ID = 50;
		public const int MAX_SET_EFFECT_GROUP_COUNT = 10;
		public const int MAX_SET_EFFECT_GROUP_TYPE = 10;
		
		public const int SF_MAX_CLAN_NAME_LENGTH = 26;
		public const int SF_MAX_CLAN_INTRO_LENTH = 221;
		public const int SF_MAX_CLAN_DOMAIN_LENGTH = 25;
		public const int SF_MAX_CLAN_NOTICE_LENGTH = 221;
		public const int SF_MAX_CLAN_CLAN_CHAT_LENGTH = 128;
		public const int DB_CLAN_ID_LENGTH = 20;
		public const int DB_CLAN_SRL_LENGTH = 10;

		public const int MAX_CLAN_MARK_LENGTH = 8;
		public const int MAX_CLAN_NAME_LENGTH = 33;
		public const int MAX_CLAN_SHOW_INFO_FOR_ONE_PAGE = 3;
		public const int MAX_CLAN_MEMBER_SHOW_INFO_FOR_ONE_PAGE = 100;
		public const int MAX_CLAN_LEAVE_SHOW_INFO_FOR_ONE_PAGE = 70;
		public const int MAX_POP_UP_COUNT = 100;
		public const int MAX_UNIT_INFO_COUNT = 200;
		public const int MAX_CLAN_STAFF_COUNT = 100;
		public const int MAX_CLAN_MEMBER_GET_LOG = 100;
		public const int MAX_CLAN_PASSWORD_LENGTH = 21;
		public const int MAX_CLAN_PASSWORD_QUESTION_LENGTH = 51;
		public const int MAX_CLAN_PASSWORD_ANSWER_LENGTH = 51;
		public const int MAX_CLAN_MARK_LIST_COUNT = 300;
		public const int MAX_CLAN_MEMBER_INTRO_LENGTH = 41;
		
		public const int MAX_NEOWIZ_ID_LENGTH = 15;
		public const int MAX_NEOWIZ_DATE_LENGTH = 14;
		
		public const int MAX_HASHED_VALUE_LENGTH = 32;
		public const int MAX_AI_BOSS_CNT = 9;
		public const int MAX_AI_SUBMODE = 2;
		public const int MAX_AI_LEVEL = 3;
		public const int MAX_AI_MAP_CNT = 7;
		public const int MAX_GMS = 10;
		public const int MAX_SPECIAL_SERVER_PASSWORD = 10;

		public const int MAX_ACCESSORY_ENCHANT_ITEM = 999;
		public const int MAX_AI_INTRUSION_COIN = 99000;
		public const int MAX_AI_MEDICAL_GUN = 999;
		public const int MAX_COUPON_COUNT = 100;
		public const int MAX_AIGACHA_LIMIT_NUM = 100;

		public const int MAX_FPOINT_LEVELUP_REWARD = 3;
		public const int MAX_FPOINT_LIST_PER_PACKET = 9;
		
		public const int MAX_STORAGE_ITEM_NUM = 100;
		public const int MAX_STORAGE_ITEM_DATE_LEN = 15;
		public const int MAX_STORAGE_MOVEMENT_NUM = 10;

		public const int MAX_AI_SCORE_REWARD_CNT = 3;
		public const int MAX_AI_BOSS_REWARD_CNT = 3;
		public const int MAX_AI_BOSS_STAMP = 5;
		public const int MAX_RECOMPENSE_WEAPON = 5;
		public const int MAX_WISHLIST_ITEM = 10;
		
		public const int BADGE_KIND_MAX = 5;
		public const int MAX_BADGE_LEVEL = 9;
		public const int MAX_BADGE_LEVEL_C = 3;
		public const int MAX_BADGE_LEVEL_B = 6;
		public const int MAX_BADGE_LEVEL_A = 9;
		public const int MAX_BADGE_WEAPON_COUNT = 3;
		public const int MAX_BADGE_CONDITION_COUNT = 3;
		public const int MAX_BADGE_REWARD_COUNT = 2;
		
		public const int MAX_DAILY_MISSION = 3;
		public const int MAX_MONTHLY_MISSION = 4;
		public const int MAX_CARDSET_LEN = 16;
		public const int MAX_MISSION_DATE_LEN = 15;
		public const int MAX_DAILY_MISSION_RANKTYPE = 4;
		public const int MAX_LOGTYPE_LEN = 3;
		public const int MAX_CARD_PROBABILITY = 9;
		public const int KICK_TABLE_MSG_COL_LENGTH = 51;

		public const int MAX_REPAIRALLITEM_COUNT = 500;
		public const int MAX_WEAPONBUYLIST_SIZE = 36;
		public const int MAX_BUY_ITEM_COUNT = 8;
		public const int MAX_PACKAGE_ITEM_COUNT = 30;
		
		public const int MAX_UCC_SPRAY_TEXT = 13;
		public const int MAX_CLANID_LENGTH = 21;
		public const int MAX_MACRO_CHATTING_LENGTH = 151;
		public const int MAX_MACRO_SAVE_COUNT = 4;
		public const int MAX_WEAPON_SLOT = 3;

		public const int MAX_RING_COUNT = 2;
		public const int MAX_RING_BUFF_COUNT = 3;
		
		public const int CF_VERIFIER_HASH_SIZE = 128;
		public const int CF_VERIFIER_MAX_REQ_ITEM_NUM_PER_PACKET = 5;
		public const int CF_VERIFIER_KEY_SIZE = 32;
		public const int SIZEOF_REQMSG = 160;
		public const int SIZEOF_REQINFO = 88;
		public const int SIZEOF_ACKMSG = 72;
		public const int SIZEOF_GUIDREQMSG = 20;
		public const int SIZEOF_GUIDREQINFO = 20;
		public const int SIZEOF_GUIDACKMSG = 20;
		
		public const int MAX_TEAMNAME_LENGTH = 30;
		public const int TOUR_STEP_MAX = 8;
		public const int TOUR_KILLDEATH_MAX = 2;

		public const int MAX_LEAGUE_NUM = 100;
		public const int MAX_LEAGUELIST_NUM = 10;
		public const int MAX_LEAGUE_NAME_LENGTH = 30;
		public const int MAX_LEAGUE_PASSWORD_LENGTH = 4;
		public const int MAX_LEAGUE_DESCRIPTION_LENGTH = 250;
		public const int MAX_8_LEAGUE_CONFIRM_TEAM_NUM = 8;
		public const int MAX_16_LEAGUE_CONFIRM_TEAM_NUM = 16;
		public const int MAX_32_LEAGUE_CONFIRM_TEAM_NUM = 32;
		public const int MAX_8_LEAGUE_WAITING_TEAM_NUM = 4;
		public const int MAX_16_LEAGUE_WAITING_TEAM_NUM = 8;
		public const int MAX_32_LEAGUE_WAITING_TEAM_NUM = 16;
		public const int MAX_8_LEAGUE_TEAM_NUM = 12;
		public const int MAX_16_LEAGUE_TEAM_NUM = 24;
		public const int MAX_32_LEAGUE_TEAM_NUM = 48;
		public const int MIN_LEAGUE_TEAM_NUM = 2;
		public const int MAX_LEAGUE_TEAM_NUM = 32;
		public const int MAX_LEAGUE_TEAM_NAME_LENGTH = 32;
		public const int MAX_LEAGUE_TEAM_PW_LENGTH = 5;
		public const int MAX_TEAM_USER_COUNT = 5;
		public const int MAX_TEAM_OBSERVER_COUNT = 3;
		public const int MAX_LEAGUE_ONE_GAME_PLAY_USER_COUNT = 10;
		public const int MAX_LEAGUE_ONE_GAME_USER_COUNT = 16;
		public const int MAX_LEAGUE_ONE_GAME_NUM = 1;
		public const int MAX_MATCH_LIST_NUM = 16;
		public const int MAX_BASE_REWARD_ITEM_NUM = 2;
		public const int MAX_REWARD_ITEM_NUM = 4;
		public const int MAX_ALL_REWARD_ITEM_NUM = 6;
		public const int MAX_GIVE_REWARD_PER_ITEM = 5;
		public const int MAX_LEAGUE_DATE_LENGTH = 14;
		
		public const int MAX_HOURS_OF_DAY_NUM = 24;
		public const int MAX_HOUR_PER_PLAY = 32;
		
		public const int MAX_ENABLE_LEAGUE_PLAY_RATE = 70;
		public const int SEPARATION_JOINED_TEAM_USER = -2147483648;
		
		public const int GAME_START_NONE = 0;
		public const int GAME_START_FOURTY_HOUR = 40;
		public const int GAME_START_QUARTER_HOUR = 45;
		public const int GAME_START_FIVE_MINUTES = 55;
		public const int GAME_START_FIFTHY_NINE_MINUTES = 59;
		public const int GAME_START_THIRTY_SECONDS = 30;
		
		public const int MAX_JOIN_LEAGUENUM_PER_USER = 10;
		public const int MAX_CREATE_LEAGUENUM_PER_USER = 10;
		public const int USER_IS_LEAGUE_ENTRY = 0;
		public const int USER_IS_LEAGUE_MASTER = 1;
		public const int USER_IS_LEAGUE_ENTRY_AND_MASTER = 2;
		public const int USER_NOT_LEAGUE_ENTRY_AND_MASTER = 3;
		public const int LEAGUE_CANCEL_BY_MASTER = 0;
		public const int LEAGUE_CANCEL_BY_SYSTEM = 1;
		public const int MAX_REWARD_STORAGE_ITEM_COUNT = 512;
		public const int MIN_ADD_REWARD_COUNT = 5;
		public const int MAX_ENABLE_CREATE_LEAGUE_TOTAL = 100;
		public const int MAX_ENABLE_CREATELLEAGUE_8 = 40;
		public const int MAX_ENABLE_CREATELLEAGUE_16 = 30;
		public const int MAX_ENABLE_CREATELLEAGUE_32 = 30;
		
		public const int MAX_QQ_ENCRYPTION_STRING_LEN = 256;
		
		// Login ...
		public const int LG_SERVER_NO = 100;
		public const int MAX_RAS_LENGTH = 9;
		public const int MAX_CONNECT_USERNAME_LENGTH = 16;
		//public const int MAX_CONNECT_PASSWORD_LENGTH = 16;
		public const int MAX_CONNECT_PASSWORD_LENGTH = 257;
		public const int MAX_PCBID_LENGTH = 13;
		//public const int MAX_MAC_ADDRESS_LENGTH = 13;
		public const int MAX_MAC_ADDRESS_LENGTH = 17;
		public const int MAX_SERVER_DISPLAY_NAME = 31;
		public const int MAX_GUID_LENGTH = 33;
		public const int MAX_HGW_SECRETKEY_LENGTH = 256;
		public const int MAX_GAMESERVER_COUNT = 100;
		public const int MAX_PROTECTED_KEYS = 100;
		
		// LMS...
		public const int MAX_EXCEPTION_DATA = 4500;
		public const int MAX_EXCEPTION_DATA_PER_PACKET = 750;
		//public const int MAX_CRYPTED_DATA_SIZE = 32;
		public const int MAX_CRYPTED_DATA_SIZE = 0;
		public const int MAX_AUTO_EVENT_MEMO_LENGTH = 160;
		
		// NCLS...
		public const int MAX_USERNAME_LENGTH = 17;
		public const int MAX_PASSWORD_LENGTH = 17;
		public const int MAX_TOKEN_LENGTH = 50;
		public const int MAX_AREA_COUNT = 100;
		public const int MAX_AREA_NAME_LENGTH = 20;
		public const int MAX_AREA_VERSION_LENGTH = 20;
		public const int MAX_NEWS_URL_BLOCK_COUNT = 4;
		public const int MAX_NEWS_URL_POSITION_LENGTH = 11;
		public const int MAX_NEWS_URL_LENGTH = 101;
		public const int MAX_NICKNAME_LENGTH = 19;
		public const int MAX_IP_ADDRESS_LENGTH = 16;
		public const int MAX_RSA_KEY_LENGTH = 512;
		public const int MAX_RECENT_AREA_COUNT = 4;
		
		// Supervisor ...
		public const int MAX_ROLLING_MEMO_LENGTH = 160;
		
		// GameMgmt...
		public const int MAX_MM_SERVER_COUNT = 100;
		public const int MAX_GSM_COUNT = 300;
		
		// GDBGW...
		public const int MAX_GDBGW_QUERY_RESULT_DATA_SIZE = 15000;
		public const int GDBGW_TIMEOUT_LIMIT = 1000;
		
		// Chatting...
		public const int MAX_CHATTING_SEND_LENGTH = 200;
		public const int MAX_MEGAPHONE_LENGTH = 200;
		public const int MAX_SPEAKER_NOTICE_LENGTH = 70;
		public const int MAX_CHATTING_LENGTH = 200;
		
		// Buddy...
		public const int SZ_MIN_GROUP_NAME_LENGTH = 4;
		public const int MAX_BUDDY_RESTORE_LIST = 8;
		public const int MAX_BUDDY_GROUP_COUNT = 10;
		public const int MAX_BUDDY_GROUP_NAME_LENGTH = 13;
		public const int MAX_MEMO_NAME_LENGTH = 29;
		public const int MAX_BUDDY_COUNT = 100;
		public const int MAX_CLAN_USER_COUNT = 150;
		public const int MAX_POST_CONTENT_LENGTH = 100;
		public const int MAX_FRIEND_CHAT_CONTENT_LENGTH = 200;
		public const int MAX_WHISPER_CONTENT_LENGTH = 200;
		
		public static ushort SIZEOF_ROLLING(ref PROTO_ROLLING tProtoRolling)
		{
			return (ushort) (sizeof(short) + sizeof(short) + sizeof(short) + sizeof(short) + tProtoRolling.nRollLength + 1);
		}
        
		public static ushort SIZEOF_NOTIFY(ref PROTO_SUPERVISOR_NOTIFY tProtoNotify)
		{
			return (ushort) (sizeof(short) + sizeof(short) + sizeof(short) + tProtoNotify.nNotifyLength + 1);
		}
	}
}
