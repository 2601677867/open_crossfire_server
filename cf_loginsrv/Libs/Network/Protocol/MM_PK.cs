using System.Collections.Generic;
using System.Runtime.InteropServices;
using Network.ProtocolStruct;
using Network.ProtocolStruct.Achievement;
using Network.ProtocolStruct.CFVersion;
using Network.ProtocolStruct.Gacha;
using Network.ProtocolStruct.Host;
using Network.ProtocolStruct.Inven;
using Network.ProtocolStruct.Room;
using Network.SharedFolder;
using static Network.Protocol.P_SZ;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

#pragma warning disable 169

namespace Network.Protocol
{
	public static partial class MM_PK
	{
		public const int PROTOCOL_MATCHMAKING = 1;
		public const int PROTOCOL_MM_FIRST = 10;

		public const int PROTOCOL_MMCONNECT = 0;
		public const int PROTOCOL_MMCONNECT_RESULT = 1;
		public const int PROTOCOL_ALIVECHECK = 2;
		public const int PROTOCOL_SAMEIDCONNECT = 3;
		public const int PROTOCOL_SAMEIDCONNECT_FORCE_LEAVE = 4;
		public const int PROTOCOL_MM_DISCONNECT = 5;
		public const int PROTOCOL_MM_DISCONNECT_RESULT = 6;
		public const int PROTOCOL_BUDDY_RETURN_INFO = 7;
		public const int PROTOCOL_BUDDY_RETURN_INFO_RESULT = 8;
		public const int PROTOCOL_ACCEPT_RESULT = 9;
		public const int PROTOCOL_MM_EXIT = 10;
		public const int PROTOCOL_MM_EXIT_RESULT = 11;
		public const int PROTOCOL_CONNTIME_ALARM = 12;
		public const int PROTOCOL_UESI = 13;
		public const int PROTOCOL_CM_CMD_UESI_INSERT = 1;
		public const int MAX_REPORT = 64;
		public const int PROTOCOL_DAILY_ATTEND_QNA = 17;
		public const int PROTOCOL_DAILY_ATTEND_QNA_RESULT = 18;
		
		//public const int PROTOCOL_UCC_MAP = 19;
		public const int PROTOCOL_GLOBALROOM = 19;
		public const int PROTOCOL_GLOBALROOM_ENTER_REQUEST = 1;
		public const int PROTOCOL_GLOBALROOM_RECOMMEND_ENTER_REQUEST = 2;
		public const int PROTOCOL_GLOBALROOM_TUTORIAL_ENTER_REQUEST = 3;
		public const int PROTOCOL_GLOBALROOM_ENTER_RESULT = 4;
		public const int PROTOCOL_GLOBALROOM_ENTER_CMD = 5;
		public const int PROTOCOL_GLOBALROOM_SAVE_CMD = 6;
		
		public const int PROTOCOL_MM_TUTORIAL_POPUP_END = 20;
		public const int PROTOCOL_HACK_USER_KICK_TABLE = 21;
		
		public const int PROTOCOL_RANKMATCH = 22;
		public const int PROTOCOL_RANKMATCH_SCORE_DEDUCT_RESULT = 25;
		public const int PROTO_RANKMATCH_REWARD_ITEM_RESULT = 37;
		public const int PROTO_RANKMATCH_REWARD_ITEM_NTY = 38;
		public const int PROTOCOL_RANKMATCH_INVITE = 36;
		public const int PROTOCOL_RANKMATCH_INVITE_RESULT = 37;
		public const int PROTOCOL_RANKMATCH_INVITE_NTY = 38;
		public const int PROTOCOL_RANKMATCH_DISBAND = 43;
		public const int PROTOCOL_RANKMATCH_DISBAND_RESULT = 44;
		public const int PROTOCOL_RANKMATCH_DECORATION_EFFORT_INFO_RESULT = 95;
		
		public const int PROTOCOL_SAVE_USERSETTING = 23;
		public const int PROTOCOL_SAVE_USERSETTING_RESULT = 24;
		public const int PROTOCOL_SAVE_MOUSESETTING = 25;
		public const int PROTOCOL_DAILY_SCORE = 26;
		public const int PROTOCOL_DAILY_SCORE_RESULT = 27;
		
		public const int PROTOCOL_VIRTUAL_MATCH = 28;
		public const int PROTOCOL_VIRTUAL_MATCH_START_REQ = 0;
		public const int PROTOCOL_VIRTUAL_MATCH_START_NTY = 1;
		public const int PROTOCOL_VIRTUAL_MATCH_INGAME_ENTER_NTY = 2;
		public const int PROTOCOL_VIRTUAL_MATCH_INVITE_REQUEST = 3;
		public const int PROTOCOL_VIRTUAL_MATCH_INVITE_RESULT = 4;
		public const int PROTOCOL_VIRTUAL_MATCH_INVITE_CANCEL_REQUEST = 5;
		public const int PROTOCOL_VIRTUAL_MATCH_INVITED_NOTIFY = 6;
		public const int PROTOCOL_VIRTUAL_MATCH_INVITED_REQUEST = 7;
		public const int PROTOCOL_VIRTUAL_MATCH_INVITED_RESULT = 8;
		public const int PROTOCOL_VIRTUAL_MATCH_PARTY_CANCEL_REQUEST = 9;
		public const int PROTOCOL_VIRTUAL_MATCH_PARTY_CANCEL_RESULT = 10;
		public const int PROTOCOL_VIRTUAL_MATCH_REGAME_READY_REQUEST = 11;
		public const int PROTOCOL_VIRTUAL_MATCH_REGAME_READY_NOTIFY = 12;
		public const int PROTOCOL_VIRTUAL_MATCH_PARTY_INFO = 13;
		public const int PROTOCOL_VIRTUAL_MATCH_CANCEL_REQ = 14;
		public const int PROTOCOL_VIRTUAL_MATCH_CANCEL_NTY = 15;
		public const int PROTOCOL_VIRTUAL_MATCH_PARTY_UPDATE_REQ = 16;
		public const int PROTOCOL_VIRTUAL_MATCH_PARTY_UPDATE_RESULT = 17;
		public const int PROTOCOL_VIRTUAL_MATCH_PARTY_UPDATE_NOTIFY = 18;
		public const int PROTOCOL_VIRTUAL_MATCH_GROUP_USER_COUNT_NOTIFY = 19;
		public const int PROTOCOL__VIRTUAL_MATCH_PARTY_BAN_RESULT = 21;
		public const int PROTOCOL_VIRTUAL_MATCH_REENTER_CANCEL_REQUEST = 22;
		public const int PROTOCOL_VIRTUAL_MATCH_REENTER_CANCEL_RESULT = 23;
		public const int PROTOCOL_VIRTUAL_MATCH_REENTER_CANCEL_NOTIFY = 23;
		
		public const int PROTOCOL_RENEWAL_MATCH = 29;
		
		public const int PROTOCOL_REQUEST_CHANNELLIST = 30;
		public const int PROTOCOL_CHCONNECT = 31;
		public const int PROTOCOL_CHCONNECT_RESULT = 32;
		public const int PROTOCOL_CHANNEL_USERLIST = 33;
		public const int PROTOCOL_CHANNEL_USERLIST_RESULT_START = 34;
		public const int PROTOCOL_CHLEAVE = 35;
		public const int PROTOCOL_CHLEAVE_RESULT = 36;
		public const int PROTOCOL_REQUEST_CHANNELLIST_RESULT = 37;
		public const int PROTOCOL_CHANNEL_USERLIST_RESULT = 38;
		public const int PROTOCOL_CHANNEL_USERLIST_RESULT_END = 39;
		public const int PROTOCOL_ROOMOBSERVER_RESULT = 40;
		public const int PROTOCOL_CHANGE_OBSERVERDATA = 41;
		public const int PROTOCOL_CHANGE_OBSERVERINDEX = 42;
		
		public const int PROTOCOL_MODE_RECORD = 43;
		
		public const int PROTOCOL_BATTLE_ROYAL = 46;
		public const int PROTOCOL_BATTLE_ROYAL_GAME_READY_CMD = 4;
		public const int PROTOCOL_BATTLE_ROYAL_START_REQ = 21;
		public const int PROTOCOL_BATTLE_ROYAL_START_RES = 22;
		public const int PROTOCOL_BATTLE_ROYAL_START_WAITING_NTY = 23;
		
		public const int PROTOCOL_MM_ROOMLIST = 50;
		public const int PROTOCOL_MM_ROOMLIST_RESULT = 51;
		public const int PROTOCOL_MM_ROOMLIST_RESULT_START = 1;
		public const int PROTOCOL_MM_ROOMLIST_RESULT_DATA = 2;
		public const int PROTOCOL_MM_ROOMLIST_RESULT_END = 3;
		public const int PROTOCOL_MM_ROOMCREATE = 52;
		public const int PROTOCOL_MM_ROOMCREATE_RESULT = 53;
		public const int PROTOCOL_MM_ROOMENTER = 54;
		public const int PROTOCOL_MM_ROOMENTER_RESULT = 55;
		public const int PROTOCOL_ROOMBOTS_ADD = 56;
		public const int PROTOCOL_ROOMBOTS_ADD_RESULT = 57;
		public const int PROTOCOL_ROOMBOTS_REMOVE = 58;
		public const int PROTOCOL_ROOMBOTS_REMOVE_RESULT = 59;
		public const int PROTOCOL_ROOMTEAM_CHANGE = 60;
		public const int PROTOCOL_ROOMTEAM_CHANGE_RESULT = 61;
		public const int PROTOCOL_MM_ROOMSETTING_CHANGE = 62;
		public const int PROTOCOL_MM_ROOMSETTING_CHANGE_RESULT = 63;
		public const int PROTOCOL_MM_ROOMLEAVE = 64;
		public const int PROTOCOL_MM_ROOMLEAVE_RESULT = 65;
		public const int PROTOCOL_START_GAME = 66;
		public const int PROTOCOL_STARTGAME_RESULT = 67;
		public const int PROTOCOL_GAMESERVER_INFO = 68;
		public const int PROTOCOL_FORCE_ROOMLEAVE = 69;
		public const int PROTOCOL_FORCE_ROOMLEAVE_RESULT = 70;
		public const int PROTOCOL_SETREADY = 71;
		public const int PROTOCOL_SETREADY_RESULT = 72;
		public const int PROTOCOL_QUICKJOIN = 73;
		public const int PROTOCOL_QUICKJOIN_RESULT = 74;
		public const int PROTOCOL_INVITE = 75;
		public const int PROTOCOL_INVITE_RESULT = 76;
		public const int PROTOCOL_JOIN_GAME = 77;
		public const int PROTOCOL_JOINGAME_RESULT = 78;
		public const int PROTOCOL_ROOM_BOSSCHANGE = 79;
		public const int PROTOCOL_MOVE_CHANNELUSER_COUNT = 80;
		public const int PROTOCOL_CHANGE_CHANNELUSER_LIST = 81;
		public const int PROTOCOL_MM_CHANGE_CHANNELROOM_LIST = 82;
		public const int PROTOCOL_CHANGE_BOTSDATA = 83;
		public const int PROTOCOL_MM_CHANGE_SERVERDATA = 84;
		public const int PROTOCOL_CHANGE_TEAMINDEX = 85;
		public const int PROTOCOL_CHANGE_USERLIST = 86;
		public const int PROTOCOL_CHANGE_READY = 87;
		public const int PROTOCOL_CHANGE_PLAY = 88;
		public const int PROTOCOL_NOTIFY_GAMEFINISHED = 89;
		public const int PROTOCOL_NOTIFY_GAMERESULT = 90;
		public const int PROTOCOL_UPDATE_MYGAMERESULT = 91;
		public const int PROTOCOL_VIEW_IDCARD = 92;
		public const int PROTOCOL_VIEW_MYIDCARD = 93;
		public const int PROTOCOL_VIEW_IDCARD_RESULT = 94;
		public const int PROTOCOL_TUTORIAL_COMPLETE = 95;
		public const int PROTOCOL_TUTORIAL_COMPLETE_RESULT = 96;
		public const int PROTOCOL_CASHITEM_COUNT_INFO = 97;
		public const int PROTOCOL_CASHITEM_COUNT_INFO_RESULT = 98;
		public const int PROTOCOL_HOST_REPORT = 99;
		public const int PROTOCOL_VIEW_TOUR_PROFILE = 100;
		public const int PROTOCOL_VIEW_TOUR_PROFILE_RESULT = 101;
		public const int PROTOCOL_HOST_REPORT_RESULT = 102;
		
		public const int PROTOCOL_PROMOTION_RECOMMENDATION = 103;
		
		public const int PROTOCOL_FORCE_GAME_FINISHED = 104;
		public const int PROTOCOL_FORCE_GAME_FINISHED_RESULT = 105;
		public const int PROTOCOL_INVITEE_LIST = 110;
		public const int PROTOCOL_INVITEE_LIST_RESULT = 111;
		public const int PROTOCOL_ROOM_PLAYER_LIST = 112;
		
		public const int PROTOCOL_BUY_ITEM = 120;
		public const int PROTOCOL_BUY_ITEM_RESULT = 121;
		public const int PROTOCOL_REPAIR_ITEM = 122;
		public const int PROTOCOL_REPAIR_ITEM_RESULT = 123;
		public const int PROTOCOL_REPAIR_ALLITEM = 124;
		public const int PROTOCOL_REPAIR_ALLITEM_RESULT = 125;
		public const int PROTOCOL_RESELL_ITEM = 126;
		public const int PROTOCOL_RESELL_ITEM_RESULT = 127;
		public const int PROTOCOL_CASH = 128;
		public const int PROTOCOL_CASH_RESULT = 129;
		public const int PROTOCOL_RESELL_ITEM_COMPENSATION_REQ = 130;
		public const int PROTOCOL_RESELL_ITEM_COMPENSATION_REQ_RESULT = 131;
		public const int PROTOCOL_TRY_WEAPON_ITEM = 132;
		public const int PROTOCOL_START_HOST = 150;
		public const int PROTOCOL_START_HOST_RESULT = 151;
		public const int PROTOCOL_JOIN_HOST = 152;
		public const int PROTOCOL_JOIN_HOST_RESULT = 153;
		public const int PROTOCOL_JOINHOST_INFO = 154;
		public const int PROTOCOL_GAME_ENDED = 155;
		public const int PROTOCOL_GAME_ENDED_RESULT = 156;
		public const int PROTOCOL_GAME_USER_DISCONNECTED = 157;
		public const int PROTOCOL_HOST_EXIT_REQ = 158;
		public const int PROTOCOL_HOST_EXIT_RESULT = 159;
		public const int PROTOCOL_HOST_SHIFTING_STARTED = 160;
		public const int PROTOCOL_HOST_SHIFTING_FINISHED = 161;
		public const int PROTOCOL_HOST_SHIFTING_REQ = 162;
		public const int PROTOCOL_HOST_SHIFTING_RESULT = 163;
		public const int PROTOCOL_HOST_USER_STAT = 164;
		public const int PROTOCOL_HOST_USER_STAT_RESULT = 165;
		public const int PROTOCOL_STARTED_HOST = 166;
		public const int PROTOCOL_HOST_QUALIFY = 167;
		public const int PROTOCOL_HOST_QUALIFY_RESULT = 168;
		
		public const int PROTOCOL_FEVER = 169;
		public const int PROTOCOL_REQUEST_FEVER_INFO = 1;
		public const int PROTOCOL_REQUEST_FEVER_INFO_RESULT = 2;
		public const int PROTOCOL_FEVER_LUCKY_RESULT = 3;
		public const int PROTOCOL_REQUEST_FEVER_REWARD = 4;
		public const int PROTOCOL_REQUEST_FEVER_REWARD_RESULT = 5;
		public const int PROTOCOL_REQUEST_FEVER_TABLE = 6;
		public const int PROTOCOL_REQUEST_FEVER_TABLE_RESULT = 7;
		
		public const int PROTOCOL_PING = 171;
		public const int PROTOCOL_PONG = 172;
		public const int PROTOCOL_HOST_BANUSER = 173;
		public const int PROTOCOL_HOST_BANUSER_RESULT = 174;
		public const int PROTOCOL_GAME_YOUBANNED = 175;
		//public const int PROTOCOL_178 = 178;
		
		public const int PROTOCOL_GACHA_DISPLAY_LIST = 180;
		public const int PROTOCOL_GACHA_DISPLAY_LIST_ALL = 0;
		public const int PROTOCOL_GACHA_DISPLAY_LIST_PROMOTION = 1;
		
		public const int PROTOCOL_GACHA_DISPLAY_LIST_RESULT = 181;
		public const int PROTOCOL_GACHA_IDENTIFY = 182;
		public const int PROTOCOL_GACHA_IDENTIFY_RESULT = 183;
		public const int PROTOCOL_GACHA_USE_WEAPON_PART = 184;

		public const int PROTOCOL_GIFT_SECOND = 185;
		public const int PROTOCOL_GIFT_CHECK = 0;
		public const int PROTOCOL_GIFT_CHECK_ACK = 1;
		public const int PROTOCOL_GIFT_POST = 2;
		public const int PROTOCOL_GIFT_POST_BY_SERVER = 3;
		public const int PROTOCOL_GIFT_POST_ACK = 4;
		public const int PROTOCOL_GIFT_CONFIRM = 5;
		public const int PROTOCOL_GIFT_CONFIRM_ACK = 6;
		public const int PROTOCOL_GIFT_RETURN = 7;
		public const int PROTOCOL_GIFT_RETURN_ACK = 8;
		public const int PROTOCOL_GIFT_RECONFIRM = 9;
		public const int PROTOCOL_GIFT_RECONFIRM_ACK = 10;
		public const int PROTOCOL_GIFT_POSTLIST = 11;
		public const int PROTOCOL_GIFT_POSTLIST_ACK = 12;
		public const int PROTOCOL_GIFT_POSTLIST_END = 13;
		public const int PROTOCOL_GIFT_RECVLIST = 14;
		public const int PROTOCOL_GIFT_RECVLIST_ACK = 15;
		public const int PROTOCOL_GIFT_RECVLIST_END = 16;
		public const int PROTOCOL_GIFT_ITEMINFO_NOTI = 17;
		public const int PROTOCOL_GIFT_COUNT = 18;
		public const int PROTOCOL_GIFT_COUNT_ACK = 19;
		public const int PROTOCOL_GIFT_COUNT_RETURNED = 20;
		public const int PROTOCOL_GIFT_COUNT_RETURNED_ACK = 21;
		public const int PROTOCOL_GIFT_TAKE_BACK = 22;
		public const int PROTOCOL_GIFT_TAKE_BACK_ACK = 23;
		
		public const int PROTOCOL_GACHA_USE_WEAPON_PART_RESULT = 186;
		public const int PROTOCOL_GACHA_COUPON_LIST = 187;
		public const int PROTOCOL_GACHA_COUPON_LIST_RESULT = 188;
		public const int PROTOCOL_GACHA_USE_COUPON = 189;
		public const int PROTOCOL_GACHA_USE_COUPON_RESULT = 190;
		public const int PROTOCOL_PROMOTION_DISPLAY_LIST_RESULT = 191;
		public const int PROTOCOL_MEGAPHONE_DENY_TIME = 191;

		public const int PROTOCOL_ENTER_INVEN_REQ = 196;
		public const int PROTOCOL_ENTER_INVEN_REQ_RESULT = 197;
		public const int PROTOCOL_EXPIRED_ITEMS = 198;
		public const int PROTOCOL_EXPIRED_ITEMS_DURING_PLAY = 1;
		public const int PROTOCOL_EXPIRED_ITEMS_DURING_LOGOUT = 2;
		public const int PROTOCOL_MINUS_GAUGE = 199;
		public const int PROTOCOL_INVEN_REQ = 200;
		public const int PROTOCOL_INVEN_ITEM = 201;
		public const int PROTOCOL_INVEN_SACK = 202;
		public const int PROTOCOL_UPDATECHARITEM_REQ = 203;
		public const int PROTOCOL_UPDATECHARITEM_RET = 204;
		public const int PROTOCOL_UPDATESACK_REQ = 205;
		public const int PROTOCOL_UPDATESACK_RET = 206;
		public const int PROTOCOL_SETMAINCHAR_REQ = 207;
		public const int PROTOCOL_SETMAINCHAR_RET = 208;
		public const int PROTOCOL_SETMAINSACK_REQ = 209;
		public const int PROTOCOL_SETMAINSACK_RET = 210;
		public const int PROTOCOL_GETMAINCHAR_REQ = 211;
		public const int PROTOCOL_GETMAINCHAR_RET = 212;
		public const int PROTOCOL_GETMAINSACK_REQ = 213;
		public const int PROTOCOL_GETMAINSACK_RET = 214;
		
		public const int PROTOCOL_WEAPONBUYLIST = 215;
		public const int PROTOCOL_WEAPONBUYLIST_REQ = 1;
		public const int PROTOCOL_WEAPONBUYLIST_RET = 2;
		
		public const int PROTOCOL_UPDATEWEAPONBUYLIST_REQ = 216;
		public const int PROTOCOL_UPDATEWEAPONBUYLIST_RET = 217;
		public const int PROTOCOL_HACKSHIELD_GUID_REQ = 220;
		public const int PROTOCOL_HACKSHIELD_GUID_RET = 221;
		public const int PROTOCOL_HACKSHIELD_REQ = 222;
		public const int PROTOCOL_HACKSHIELD_ACK = 223;
		public const int PROTOCOL_CFV_VERSION_REQ = 224;
		public const int PROTOCOL_CFV_VERSION_ACK = 225;
		public const int PROTOCOL_CFV_VALIDATION_REQ = 226;
		public const int PROTOCOL_CFV_VALIDATION_ACK = 227;
		public const int PROTOCOL_UDP_PING_ACK = 228;
		public const int PROTOCOL_HOST_PERIODICAL_RESUME_INFO = 230;
		
		public const int PROTOCOL_DONT_HACK = 231;
		public const int PROTOCOL_CONFESS_HACK = 232;
		public const int PROTOCOL_ANTI_ADDICTION_INFO_REQ = 233;
		public const int PROTOCOL_ANTI_ADDICTION_INFO = 234;
		public const int PROTOCOL_ONLINE_HOUR = 235;
		public const int PROTOCOL_ANTIBOT_ANTIBOT = 236;
		public const int PROTOCOL_ANTIBOT_DYNAMIC = 237;
		public const int PROTOCOL_ANTIBOT_CLOSECLIENT = 238;
		public const int PROTOCOL_ANTIBOT = 239;
		public const int PROTOCOL_USEFUNCITEM_REQ = 240;
		public const int PROTOCOL_USEFUNCITEM_RET = 241;
		public const int PROTOCOL_SETFUNCITEMUSEON_REQ = 242;
		public const int PROTOCOL_SETFUNCITEMUSEON_RET = 243;
		public const int PROTOCOL_WEAPON_HACK_USER = 247;
		public const int PROTOCOL_CASH_GP_UP_EVNET_RET = 248;
		
		public const int PROTOCOL_UCC_SPRAY_ITEM = 249;
		public const int PROTOCOL_UCC_SPRAY_ITEM_C_REQ = 1;
		public const int PROTOCOL_UCC_SPRAY_ITEM_RET = 2;
		public const int PROTOCOL_UCC_SPRAY_ITEM_S_REQ = 3;
		public const int PROTOCOL_COLOR_CHATTING_ITEM_C_REQ = 4;
		public const int PROTOCOL_COLOR_CHATTING_ITEM_RET = 5;
		public const int PROTOCOL_COLOR_CHATTING_ITEM_S_REQ = 6;
		public const int PROTOCOL_COLOR_CALLNAME_ITEM_C_REQ = 7;
		public const int PROTOCOL_COLOR_CALLNAME_ITEM_RET = 8;
		public const int PROTOCOL_COLOR_CALLNAME_ITEM_S_REQ = 9;
		public const int PROTOCOL_SET_CALLNAME_COLOR_CHANGE = 10;
		public const int PROTOCOL_COLOR_BAG_ITEM_C_REQ = 11;
		public const int PROTOCOL_COLOR_BAG_ITEM_RET = 12;
		public const int PROTOCOL_SET_CALLNAME_CHATTING_COLOR_CHANGE = 13;
		
		public const int PROTOCOL_CLAN_MATCH_GROUP = 250;
		
		public const int PROTOCOL_VIEW_PROFILE_IMPROVE = 251;
		public const int PROTOCOL_VIEW_PROFILE_IMPROVE_RESULT = 252;
		public const int PROTOCOL_AAS_USER_REQ = 253;
		public const int PROTOCOL_AAS_USER_MSG = 254;
		
		public const int PROTOCOL_SECOND_AUTH = 255;
		public const int PROTOCOL_SECOND_AUTH_RECYCLE_REQ = 0;
		public const int PROTOCOL_SECOND_AUTH_RECYCLE_RET = 1;
		public const int PROTOCOL_SECOND_AUTH_MANAGE_REQ = 3;
		public const int PROTOCOL_SECOND_AUTH_MANAGE_RET = 4;

		// MM Connect Msg
		
		public const int PROTOCOL_NEW_BUDDY_SECOND = 0;
		public const int PROTOCOL_NEW_BUDDY_THIRD_USER_LOGIN = 0;
		
		public const int PROTOCOL_ROOM_SECOND = 1;
		public const int PROTOCOL_SET_LOCATION = 1;
		public const int PROTOCOL_SET_LOCATION_RESULT = 2;
		public const int PROTOCOL_KICK = 3;
		public const int PROTOCOL_USER_LOCATION_NOTI = 4;
		public const int PROTOCOL_SET_ROOMNAMECARD = 5;
		public const int PROTOCOL_ROOM_INVITE = 6;
		public const int PROTOCOL_ROOM_INVITE_RESULT = 7;
		public const int PROTOCOL_MB_ROOM_INVITE = 8;
		public const int PROTOCOL_SET_ROOMCOLORCALLNAME = 9;
		public const int PROTOCOL_ROOM_RETURN_DURING_PLAYING = 10;
		public const int PROTOCOL_ROOM_LEVEL_UP = 11;
		// 12, room, nickname[13b] + 2bytes flag
		// 13, room, usn[8b] + 1int
		
		public const int PROTOCOL_BUY_ITEM_SECOND = 2;
		
		public const int PROTOCOL_GACHA_SECOND = 3;
		public const int PROTOCOL_GACHA_WINNERLIST_REQ = 1;
		public const int PROTOCOL_GACHA_WINNERLIST_RES = 2;
		public const int PROTOCOL_GACHA_WINNER_NTY = 3;
		public const int PROTOCOL_GACHA_STORAGE_REQUEST = 5;
		public const int PROTOCOL_GACHA_STORAGE_ITEM_LIST_RES_START = 6;
		public const int PROTOCOL_GACHA_STORAGE_ITEM_LIST_RES = 7;
		public const int PROTOCOL_GACHA_STORAGE_ITEM_LIST_RES_END = 8;
		public const int PROTOCOL_GACHA_DISMANTLE = 9;
		public const int PROTOCOL_GACHA_DISMANTLE_RESULT = 10;
		public const int PROTOCOL_GACHA_TRANSFER_ITEM = 11;
		public const int PROTOCOL_GACHA_TRANSFER_ITEM_RESULT = 12;
		public const int PROTOCOL_GACHA_SYSTEM_INFO = 13;
		public const int PROTOCOL_GACHA_EVENT_ITEM_INFO = 14;
		// 15, lead buy?, WORD storage cnt, WORD value, ...
		public const int PROTOCOL_GACHA_USER_INFO_REQ = 16;
		public const int PROTOCOL_GACHA_USER_INFO_RES = 17;
		public const int PROTOCOL_GACHA_COUPON_EXTEND_REQ = 18;
		public const int PROTOCOL_GACHA_COUPON_EXTEND_RES = 19;
		public const int PROTOCOL_GACHA_MP_GROWTH_REQ = 20;
		public const int PROTOCOL_GACHA_MP_GROWTH_RES = 21;
		public const int PROTOCOL_GACHA_USER_STAT_LOG = 22;
		// 22 <- client send on exit, 3 statistics, _, _, coupon use cnt
		// 23 used in send sub_108E6C50 1 byte, storage req
		public const int PROTOCOL_GACHA_TRANSFER_OVERLAPPED = 24;
		public const int PROTOCOL_GACHA_TRANSFER_OVERLAPPED_RESULT = 25;

		public const int PROTOCOL_MISSION_SYSTEM = 4;
		public const int PROTOCOL_MISSION_INFO_REQ = 1;
		public const int PROTOCOL_MISSION_INFO_RES = 2;
		public const int PROTOCOL_MISSION_INFO_NTY = 3;
		public const int PROTOCOL_MISSION_CHANGE_NTY = 4;
		public const int PROTOCOL_MISSION_CARDSET_REWARD_REQ = 5;
		public const int PROTOCOL_MISSION_CARDSET_REWARD_RES = 6;
		public const int PROTOCOL_USER_MISSION_RESULT = 7;
		public const int PROTOCOL_USER_MISSION_INFO_REQ = 8;
		public const int PROTOCOL_USER_MISSION_INFO_RES = 9;
		public const int PROTOCOL_USER_MISSION_INFO_NTY = 10;
		public const int PROTOCOL_MISSION_UNKNOWN_PACKET_ON_QUIT = 35; // 24 bytes

		public const int PROTOCOL_BADGE_SYSTEM = 5;
		public const int PROTOCOL_USER_BADGE_RESULT = 1;
		public const int PROTOCOL_MAINBADGE_CHANGE_REQ = 2;
		public const int PROTOCOL_MAINBADGE_CHANGE_RES = 3;
		public const int PROTOCOL_MAINBADGE_CHANGE_NTY = 8;
		public const int PROTOCOL_USER_BADGE_INFO_REQ = 4;
		public const int PROTOCOL_USER_BADGE_INFO_RES = 5;
		public const int PROTOCOL_BADGE_LEVELUP_NTY = 6;
		public const int PROTOCOL_BADGE_LEVELUP_CLAN_NTY = 7;
		public const int PROTOCOL_BADGE_INFO_REQ = 8;
		public const int PROTOCOL_BADGE_INFO_RES = 9;
		
		public const int PROTOCOL_RECOMMENDED_ITEM_SYSTEM = 6;
		public const int PROTOCOL_RECOMMENDED_USER_STAT = 1;
		
		public const int PROTOCOL_WISHLIST_SYSTEM = 7;
		public const int PROTOCOL_WISHLIST_REQ = 1;
		public const int PROTOCOL_WISHLIST_RES = 2;
		public const int PROTOCOL_WISHLIST_UPDATE_REQ = 3;
		public const int PROTOCOL_WISHLIST_UPDATE_RES = 4;
		
		public const int PROTOCOL_AI_SYSTEM = 8;
		public const int PROTOCOL_AI_GET_FUNCITEM_NTY = 1;
		public const int PROTOCOL_AI_RESULT_NTY = 2;
		public const int PROTOCOL_AI_UPDATE_DAILYLIFE = 3;
		public const int PROTOCOL_AI_GACHA_PRIZE_RESULT = 4;
		public const int PROTOCOL_AI_GACHA_PRIZE_DETAIL_INFO = 5;
		public const int PROTOCOL_AI_GACHA_PRIZE_DETAIL_INFO_RESULT = 6;
		public const int PROTOCOL_AI_STRBOX_OPEN_REQ = 7;
		public const int PROTOCOL_AI_STRBOX_OPEN_RES = 8;
		// 9, 11, 12 client recv
		public const int PROTOCOL_AI_BLUEPRINT_COMBINE_REQ = 13;
		public const int PROTOCOL_AI_BLUEPRINT_COMBINE_RES = 14;
		public const int PROTOCOL_AI_ATTR_UPGRADE_REQ = 15;
		public const int PROTOCOL_AI_ATTR_UPGRADE_RES = 16;
		public const int PROTOCOL_AI_ATTR_RESET_REQ = 17;
		public const int PROTOCOL_AI_ATTR_RESET_RES = 18;
		public const int PROTOCOL_AI_REINFORCE_INFO = 19;
		public const int PROTOCOL_AI_REINFORCE_INFO_RESULT = 20;
		// 21 client recv
		public const int PROTOCOL_AI_WEAPON_PIECE_COMBINE = 23;
		public const int PROTOCOL_AI_WEAPON_PIECE_COMBINE_RESULT = 24;
		public const int PROTOCOL_AI3_USERINFO_TO_ENTERROOMUSER_NTY = 29;
		public const int PROTOCOL_AI3_USERINFO_TO_INROOMUSER_NTY = 30;
		// 33 req?
		// 34 client recv
		// 35 req?
		// 36 client recv
		public const int PROTOCOL_AI_MAP_LEVEL_INFO = 37;
		public const int PROTOCOL_AI_MAP_LEVEL_INFO_RESULT = 38;
		// 39 client recv
		// 40 req ?
		// 41 client recv
		public const int PROTOCOL_BOSS_TOWER_DEATH_INVITATION_REQ = 42;
		public const int PROTOCOL_BOSS_TOWER_DEATH_INVITATION_RES = 43;
		public const int PROTOCOL_TRIAL_PIECE_CHANGE_REQ = 44;
		public const int PROTOCOL_TRIAL_PIECE_CHANGE_RES = 45;
		public const int PROTOCOL_AI_BOSS_TOWER_CHAR_CHANGE_REQ = 46;
		public const int PROTOCOL_AI_BOSS_TOWER_CHAR_CHANGE_RES = 47;
		public const int PROTOCOL_AI_BOSS_TOWER_CHAR_CHANGE_NTY = 48;
		public const int PROTOCOL_AI_BOSS_TOWER_REWARD_RES = 49;
		public const int PROTOCOL_AI_EXTRA_MIDDLE_REWARD = 52;

		public const int PROTOCOL_MISC_SECOND = 9;
		public const int PROTOCOL_SHOP_ITEMINFO_NTY = 1;
		public const int PROTOCOL_ITEM_TAB_INFO_NTY = 2;
		public const int PROTOCOL_ITEM_TAB_INFO_REQ = 3;
		public const int PROTOCOL_ITEM_TAB_INFO_RES = 4;
		// 11, 33615
		public const int PROTOCOL_WEB_EVENT_PAGE_CONFIRM = 12;
		public const int PROTOCOL_WEB_EVENT_PAGE_GIVE_ITEM = 13;
		public const int PROTOCOL_SHOP_ITEMINFO_LIST = 14;
		// 16, 17 US
		public const int PROTOCOL_UCC_DOWNLOAD_REQ = 18;
		public const int PROTOCOL_UCC_DOWNLOAD_RES = 19; // 1 byte ret, useless
		// 22 lobby reneweal link infos
		
		public const int PROTOCOL_INSTANCE_RETURNUSER = 12;
		public const int PROTOCOL_INSTANCE_RETURNUSER_POPUP = 5;
		
		public const int PROTOCOL_RECOMPENSE_SYSTEM = 10;
		public const int PROTOCOL_RECOMPENSE_REQ = 1;
		public const int PROTOCOL_RECOMPENSE_RES = 2;
		public const int PROTOCOL_RECOMPENSE_PROMOTION_REQ = 3;
		public const int PROTOCOL_RECOMPENSE_PROMOTION_RES = 4;
		public const int PROTOCOL_RECOMPENSE_ITEM_ADD = 5;
		
		public const int PROTOCOL_MISSIONCARD_SYSTEM = 11;
		public const int PROTOCOL_MISSIONCARD_TARGET_LIST_REQ = 1;
		public const int PROTOCOL_MISSIONCARD_TARGET_LIST_RES = 2;
		public const int PROTOCOL_MISSIONCARD_REWARD_LIST_REQ = 3;
		public const int PROTOCOL_MISSIONCARD_REWARD_LIST_RES = 4;
		public const int PROTOCOL_GET_MISSIONCARD_USERINFO_REQ = 5;
		public const int PROTOCOL_GET_MISSIONCARD_USERINFO_RES = 6;
		public const int PROTOCOL_SET_MISSIONCARD_USERINFO_REQ = 7;
		public const int PROTOCOL_SET_MISSIONCARD_USERINFO_RES = 8;
		
		public const int PROTOCOL_PROMOTION = 11;
		public const int PROTOCOL_PROMOTION_NOTIFY = 1;
		
		public const int PROTOCOL_MM_SELF = 11;
		public const int PROTOCOL_MISSION_INFO_SELF = 1;
		public const int PROTOCOL_MISSION_SYNC = 1;
		public const int PROTOCOL_UPDATE_AI_LIVES = 2;
		public const int PROTOCOL_NEW_BUDDY_FPOINT_POINT_SELF = 3;
		public const int PROTOCOL_NEW_BUDDY_FPOINT_NEW_SELF = 4;
		public const int PROTOCOL_NEW_BUDDY_FPOINT_DELETE_SELF = 5;
		public const int PROTOCOL_NEW_BUDDY_FPOINT_GRADUATE_SELF = 6;
		public const int PROTOCOL_NEW_BUDDY_FPOINT_STATE_SELF = 7;
		public const int PROTOCOL_UPDATE_ACHIEVEMENT_COUNT = 8;
		public const int PROTOCOL_AUTOLEAGUE_INVITE_USER_SELF = 9;

		public const int PROTOCOL_FPOINT_SYSTEM = 12;
		public const int PROTOCOL_FPOINT_CHECK_REQ = 0;
		public const int PROTOCOL_FPOINT_CHECK_ACK = 1;
		public const int PROTOCOL_FPOINT_FIND_REQ = 2;
		public const int PROTOCOL_FPOINT_FIND_ACK = 3;
		public const int PROTOCOL_FPOINT_NEW_REQ = 4;
		public const int PROTOCOL_FPOINT_REGISTER_ACK = 5;
		public const int PROTOCOL_FPOINT_LIST_REQ = 6;
		public const int PROTOCOL_FPOINT_LIST_ACK = 7;
		public const int PROTOCOL_FPOINT_DELETE_REQ = 8;
		public const int PROTOCOL_FPOINT_DELETE_ACK = 9;
		public const int PROTOCOL_FPOINT_POINT_NTY = 10;
		public const int PROTOCOL_FPOINT_REGISTER_NTY = 11;
		public const int PROTOCOL_FPOINT_DELETED_NTY = 12;
		public const int PROTOCOL_FPOINT_GRADUATE_NTY = 13;
		public const int PROTOCOL_FPOINT_REWARD_REQ = 14;
		public const int PROTOCOL_FPOINT_REWARD_ACK = 15;
		public const int PROTOCOL_FPOINT_UPDATE_NTY = 16;
		
		public const int PROTOCOL_STORAGE_SYSTEM = 13;
		public const int PROTOCOL_STORAGE_LIST = 1;
		public const int PROTOCOL_STORAGE_LIST_RESULT = 2;
		public const int PROTOCOL_STORAGE_ITEM_DELETE = 3;
		public const int PROTOCOL_STORAGE_ITEM_DELETE_RESULT = 4;
		public const int PROTOCOL_STORAGE_ITEM_MOVEMENT = 5;
		public const int PROTOCOL_STORAGE_ITEM_MOVEMENT_RESULT = 6;
		public const int PROTOCOL_STORAGE_INVEN_MOVEMENT = 7;
		public const int PROTOCOL_STORAGE_INVEN_MOVEMENT_RESULT = 8;
		
		public const int PROTOCOL_VVIP_SYSTEM = 14;
		public const int PROTOCOL_ROOM_VVIP_ITEM_CHANGE = 1;
		public const int PROTOCOL_VVIP_ROOM_USER_CHANGE = 2;
		public const int PROTOCOL_VVIP_KILL_DEATH_ROLL_BACK = 3;
		public const int PROTOCOL_VVIP_KILL_DEATH_ROLL_BACK_REQ = 4;
		public const int PROTOCOL_VVIP_KILL_DEATH_ROLL_BACK_RESULT = 5;
		public const int PROTOCOL_VVIP_COLOR_CHANGE_REQ = 10;
		public const int PROTOCOL_VVIP_COLOR_CHANGE_RESULT = 11;
		public const int PROTOCOL_VVIP_COLOR_INFO_REQ = 12;
		public const int PROTOCOL_VVIP_COLOR_INFO_RESULT = 13;
		public const int PROTOCOL_VVIP_COLOR_UNLOCK = 14;
		public const int PROTOCOL_VVIP_COLOR_UNLOCK_RESULT = 15;
		public const int PROTOCOL_VVIP_BUY_WEAPON_CC_EX_PALETTE = 16;
		public const int PROTOCOL_VVIP_BUY_WEAPON_CC_EX_PALETTE_RESULT = 17;
		public const int PROTOCOL_VVIP_LEVEL_UPGRADE_INFO = 18;
		public const int PROTOCOL_VVIP_LEVEL_UPGRADE_REQ = 19;
		public const int PROTOCOL_VVIP_LEVEL_UPGRADE_RESULT = 20;
		public const int PROTOCOL_VVIP_SPECIAL_BUFF_CHANGE_REQ = 21;
		public const int PROTOCOL_VVIP_SPECIAL_BUFF_CHANGE_RESULT = 22;
		public const int PROTOCOL_VVIP_SPECIAL_BUFF_INFO_REQ = 23;
		public const int PROTOCOL_VVIP_SPECIAL_BUFF_INFO_RESULT = 24;
		// 25, 26

		public const int PROTOCOL_NEWBIE_MISSION_SYSTEM = 15;
		public const int PROTOCOL_NEWBIEMISSION_INFO_REQ = 1;
		public const int PROTOCOL_NEWBIEMISSION_INFO_RES = 2;
		public const int PROTOCOL_NEWBIEMISSION_USERINFO_REQ = 3;
		public const int PROTOCOL_NEWBIEMISSION_USERINFO_RES = 4;
		public const int PROTOCOL_NEWBIEMISSION_FIANLREWARD_REQ = 5;
		public const int PROTOCOL_NEWBIEMISSION_FINALREWARD_RES = 6;
		public const int PROTOCOL_USER_NEWBIE_MISSION_RESULT = 7;
		
		public const int PROTOCOL_ACHIEVEMENT_SYSTEM = 16;
		public const int PROTOCOL_ACHIEVEMENT_LIST_REQ = 1;
		public const int PROTOCOL_ACHIEVEMENT_LIST_RES = 2;
		public const int PROTOCOL_ACHIEVEMENT_USERDATA_REQ = 3;
		public const int PROTOCOL_ACHIEVEMENT_USERDATA_RES = 4;
		public const int PROTOCOL_ACHIEVEMENT_RESULT = 5;
		public const int PROTOCOL_ACHIEVEMENT_ACHIEVE_ROOM_NTY = 6;
		public const int PROTOCOL_ACHIEVEMENT_SET_ACHIEVE_REQ = 7;
		public const int PROTOCOL_ACHIEVEMENT_SET_ACHIEVE_RES = 8;
		public const int PROTOCOL_ACHIEVEMENT_SET_ACHIEVE_NTY = 9;
		public const int PROTOCOL_ACHIEVEMENT_GET_ACHIEVE_REQ = 10;
		public const int PROTOCOL_ACHIEVEMENT_GET_ACHIEVE_RES = 12;
		public const int PROTOCOL_ACHIEVEMENT_PASSIVE_LIST_REQ = 13;
		public const int PROTOCOL_ACHIEVEMENT_PASSIVE_LIST_RES = 14;
		public const int PROTOCOL_ACHIEVEMENT_PASSIVE_LIMIT_RESET_NTY = 15; // CN -> 16
		public const int PROTOCOL_ACHIEVEMENT_BUY_ITEM = 16;
		
		public const int PROTOCOL_CODE_HUNTER = 19;
		public const int PROTOCOL_CODE_HUNTER_RECV_DATA = 1;
		public const int PROTOCOL_CODE_HUNTER_SEND_DATA = 2;

		public const int PROTOCOL_HIDDEN2_SND_SET = 20;
		public const int PROTOCOL_HIDDEN2_SND_SET_REQUEST = 1;
		public const int PROTOCOL_HIDDEN2_SND_SET_RESULT = 2;

		public const int PROTOCOL_COUPON_EVENT = 22;
		public const int PROTOCOL_COUPON_EVENT_LIST_REQ = 1;
		public const int PROTOCOL_COUPON_EVENT_LIST_RET = 2;
		
		public const int PROTOCOL_IGN_NAME_CHANGE = 23;
		
		public const int PROTOCOL_RING = 24;
		public const int PROTOCOL_ACCESSORY_ENCHANT_RESULT = 13;
		public const int PROTOCOL_RING_REENCHANT_DISCONN_INFO_REQUEST = 15;
		public const int PROTOCOL_RING_REENCHANT_DISCONN_INFO_RESULT = 16;

		public const int PROTOCOL_ATTENDANCE = 26;
		public const int PROTOCOL_ATTENDANCE_INITIALIZE = 1;
		public const int PROTOCOL_ATTENDANCE_INFO_REQ = 2;
		public const int PROTOCOL_ATTENDANCE_INFO_REP = 3;
		public const int PROTOCOL_ATTENDANCE_REWARD_REQ = 4;
		public const int PROTOCOL_ATTENDANCE_REWARD_REP = 5;
		
		public const int PROTOCOL_BEGINNER_GUIDE = 27;
		public const int PROTOCOL_BEGINNER_GUIDE_REWARD_REQ = 1;
		public const int PROTOCOL_BEGINNER_GUIDE_REWARD_RES = 2;
		public const int PROTOCOL_BEGINNER_GUIDE_END_REQ = 5;
		public const int PROTOCOL_BEGINNER_GUIDE_END_RES = 6;

		public const int PROTOCOL_CODE_HUNTER_2 = 28;
		public const int PROTOCOL_CHS_PUNISHMENT = 29;
		
		public const int PROTOCOL_BLUEPRINT_SYSTEM = 30;
		
		public const int PROTOCOL_HASHKEY = 31;
		public const int PROTOCOL_HASHKEY_REQ = 0;
		public const int PROTOCOL_HASHKEY_RES = 1;
		
		public const int PROTOCOL_VVIP_KILL_COUNTER = 32;
		public const int PROTOCOL_VVIP_KILL_COUNTER_INFO_REQ = 1;
		public const int PROTOCOL_VVIP_KILL_COUNTER_INFO_RES = 2;
		
		public const int PROTOCOL_NANO_SOULSTONE = 34;

		public const int PROTOCOL_MILEAGE_SYSTEM = 35;
		public const int PROTOCOL_MILEAGE_ENTER_REQ = 0;
		public const int PROTOCOL_MILEAGE_ENTER_RES = 1;
		public const int PROTOCOL_MILEAGE_USER_ITEM_COOLTIME = 3;
		public const int PROTOCOL_MILEAGE_SEASON_DAYS_INFO = 6;
		
		public const int PROTOCOL_DUSTBIN = 36;
		public const int PROTOCOL_DUSTBIN_ITEM_INFO_REQ = 1;
		public const int PROTOCOL_DUSTBIN_ITEM_INFO_RES = 2;
		public const int PROTOCOL_DUSTBIN_MOVE_TO_ITEM_REQ = 3;
		public const int PROTOCOL_DUSTBIN_MOVE_TO_ITEM_RES = 4;
		public const int PROTOCOL_DUSTBIN_MOVE_TO_INVEN_REQ = 5;
		public const int PROTOCOL_DUSTBIN_MOVE_TO_INVEN_RES = 6;
		public const int PROTOCOL_DUSTBIN_UCC_SPRAY_INFO_REQ = 7;
		public const int PROTOCOL_DUSTBIN_UCC_SPRAY_INFO_RES = 8;
		public const int PROTOCOL_DUSTBIN_MOVE_TO_ITEM_DUP_REQ = 11;
		public const int PROTOCOL_DUSTBIN_MOVE_TO_ITEM_DUP_RES = 12;
		
		public const int PROTOCOL_COMEBACK_SUPPORT_SYSTEM = 37;
		public const int RROTOCOL_COMEBACK_INFO_REQUEST = 1;
		public const int RROTOCOL_COMEBACK_INFO_RESULT = 2;
		public const int PROTOCOL_COMEBACK_ENTER_REWARD_INFO_REQUEST = 5;
		public const int PROTOCOL_COMEBACK_ENTER_REWARD_INFO_RESULT = 6;
		public const int PROTOCOL_COMEBACK_ENTER_REWARD_REQUEST = 7;
		public const int PROTOCOL_COMEBACK_SECOND_REWARD_REQUEST = 8;
		public const int PROTOCOL_COMEBACK_TIMER_REWARD_INFO_REQUEST = 9;

		public const int PROTOCOL_ROOM_CHARACTER_CHANGE_NTY = 38;

		public const int PROTOCOL_WEEKLY_WEAPON = 39;
		public const int PROTOCOL_REQUEST_WEEKLY_WEAPON_INFO = 0;
		public const int PROTOCOL_REQUEST_WEEKLY_WEAPON_INFO_RESULT = 1;
		public const int PROTOCOL_REQUEST_WEEKLY_WEAPON_BOX_OPEN = 2;
		public const int PROTOCOL_REQUEST_WEEKLY_WEAPON_BOX_OPEN_RESULT = 3;
		public const int PROTOCOL_REQUEST_WEEKLY_WEAPON_BOX_OPEN_ALL = 4;
		public const int PROTOCOL_REQUEST_WEEKLY_WEAPON_BOX_OPEN_ALL_RESULT = 5;
		public const int PROTOCOL_REQUEST_WEEKLY_WEAPON_EXTEND = 6;
		public const int PROTOCOL_REQUEST_WEEKLY_WEAPON_EXTEND_RESULT = 7;
		public const int PROTOCOL_WEEKLY_WEAPON_ARRIVED_NTY = 8;
		public const int PROTOCOL_WEEKLY_WEAPON_REMOVE_ALL_NTY = 9;

		public const int PROTOCOL_UNKNOWN_40 = 40;
		
		public const int PROTOCOL_WEAPON_UPGRADE_SYSTEM = 41;
		public const int PROTOCOL_WEAPON_UPGRADE_USER_INFO = 1;
		public const int PROTOCOL_WEAPON_UPGRADE_CREATE_REQUEST = 2;
		public const int PROTOCOL_WEAPON_UPGRADE_CREATE_RESULT = 3;
		public const int PROTOCOL_WEAPON_UPGRADE_REQUEST = 4;
		public const int PROTOCOL_WEAPON_UPGRADE_RESULT = 5;
		public const int PROTOCOL_WEAPON_UPGRADE_STATUS_REQUEST = 6;
		public const int PROTOCOL_WEAPON_UPGRADE_STATUS_RESULT = 7;
		public const int PROTOCOL_WEAPON_UPGRADE_RETRIEVE_REQUEST = 8;
		public const int PROTOCOL_WEAPON_UPGRADE_RETRIEVE_RESULT = 9;
		
		public const int PROTOCOL_MP_DISTRIBUTE = 42;
		
		public const int PROTOCOL_WEAPON_USE_TICKET = 43;
		public const int PROTOCOL_WEAPON_USE_TICKET_USER_INFO_REQ = 1;
		public const int PROTOCOL_WEAPON_USE_TICKET_USER_INFO_REP = 2; // byte + int
		public const int PROTOCOL_WEAPON_USE_TICKET_CHOICE_REQ = 3;
		public const int PROTOCOL_WEAPON_USE_TICKET_CHOICE_REP = 4;
		
		public const int PROTOCOL_RANKED_MATCH_REWARD = 44;
		
		public const int PROTOCOL_EVENT_PAGE = 45;
		public const int PROTOCOL_EVENT_PAGE_INFO = 1;
		public const int PROTOCOL_EVENT_PAGE_BTN_CLICK = 3;
		public const int PROTOCOL_EVENT_PAGE_STATISTICS = 5;
		
		public const int PROTOCOL_ATTACH = 46;
		public const int PROTOCOL_ATTACH_USERINFO_REQUEST = 1;
		public const int PROTOCOL_ATTACH_USERINFO_RESULT = 2;
		public const int PROTOCOL_ATTACH_USE = 3;
		public const int PROTOCOL_ATTACH_USE_RESULT = 4;
		
		public const int PROTOCOL_TRADE_SYSTEM = 47;
		
		public const int PROTOCOL_PROFILE_RANKING = 48;
		
		public const int PROTOCOL_BOSSBATTLE = 49;
		public const int PROTOCOL_BOSSBATTLE_SETTING_CHANGE = 1;
		public const int PROTOCOL_BOSSBATTLE_SETTING_CHANGE_RES = 2;
		public const int PROTOCOL_BOSSBATTLE_SETTING_CHANGE_NTY = 3;
		public const int PROTOCOL_BOSSBATTLE_USERINFO_TO_ENTERROOMUSER_NTY = 4;
		public const int PROTOCOL_BOSSBATTLE_USERINFO_TO_INROOMUSER_NTY = 5;
		
		public const int PROTOCOL_FAIR_MATCH = 51;
		public const int PROTOCOL_FAIR_MATCH_INFO_REQ = 1;
		public const int PROTOCOL_FAIR_MATCH_INFO_RES = 2;
		public const int PROTOCOL_FAIR_MATCH_START_REQ = 3;
		public const int PROTOCOL_FAIR_MATCH_START_RES = 4;
		// 5req, 6ret
		// 7req, 8ret
		// 9, 10, 11 server msg
		public const int PROTOCOL_FAIR_MATCH_STATUS = 12;
		// 13req, 14ret
		
		public const int PROTOCOL_AUTO_SWAP_SYSTEM = 52;
		public const int PROTOCOL_AUTO_SWAP = 1;	
		
		public const int PROTOCOL_AI_EVENT = 53;
		public const int PROTOCOL_AI_EVENT_REQUEST = 1;
		public const int PROTOCOL_AI_EVENT_RESULT = 2;
		
		public const int PROTOCOL_BATTLE_ROYAL_GAME_LOG = 54;
		
		public const int PROTOCOL_INSTALMENT_SYSTEM = 55;
		public const int PROTOCOL_INSTALMENT_INFO_REQUEST = 1;
		public const int PROTOCOL_INSTALMENT_INFO_RESULT = 2;
		public const int PROTOCOL_INSTALMENT_BUY_RESULT = 3;
		public const int PROTOCOL_INSTALMENT_BUY_RESULT_FAILED = 4;
		
		public const int PROTOCOL_BATTLE_ROYAL_REWARD = 56;
		
		public const int PROTOCOL_COMBINATION = 57;
		
		public const int PROTOCOL_AIM_MODE = 58;
		
		public const int PROTOCOL_BOX_SYSTEM = 60;
		public const int PROTOCOL_BOX_OPEN_REQ = 1;
		public const int PROTOCOL_BOX_OPEN_RES = 2;
		
		public const int PROTOCOL_PVE_WEAPON_UPGRADE = 61;
		
		public const int PROTOCOL_NOBLE_GOLD = 62;
		
		public const int PROTOCOL_BATTLE_RECORD = 64;
		public const int PROTOCOL_BATTLE_RECORD_INFO_REQ = 1;
		public const int PROTOCOL_BATTLE_RECORD_INFO_RES = 2;
		public const int PROTOCOL_BATTLE_RECORD_DISPLAY_INFO_RES = 13;
		
		public const int PROTOCOL_CHAR_OPTION_SYSTEM = 65;
		public const int PROTOCOL_CHAR_OPTION_OPEN_RES = 2;
		public const int PROTOCOL_CHAR_OPTION_EXTRACT_RES = 4;
		public const int PROTOCOL_CHAR_OPTION_NULLSUB_RES = 6;
		public const int PROTOCOL_CHAR_OPTION_CARD_RES = 8;
		public const int PROTOCOL_CHAR_OPTION_INFO_RES = 10;
		public const int PROTOCOL_CHAR_OPTION_RANDOM_RES = 12;
		public const int PROTOCOL_CHAR_OPTION_RANDOM_SELECT_RES = 14;
		public const int PROTOCOL_CHAR_OPTION_RANDOM_CHECK_RES = 16;

		public const int PROTOCOL_CLAN_MATCH_SYSTEM = 67;
		public const int PROTOCOL_CLAN_MATCH_FLAG_NTY = 1;
		public const int PROTOCOL_CLAN_MATCH_INFO_REQUEST = 2;
		public const int PROTOCOL_CLAN_MATCH_INFO_RESULT = 3;
		
		public const int PROTOCOL_PVE = 68;
		public const int PROTOCOL_PVE_RANK_USER_INFO_REQ = 0;
		public const int PROTOCOL_PVE_RANK_USER_INFO_RES = 1;
		
		public const int PROTOCOL_COMMON_REWARD_GIVE_INFO = 69;
		
		public const int PROTOCOL_BAG_SET = 70;
		public const int PROTOCOL_REQUEST_CHANGE_BAG_SET = 0;
		public const int PROTOCOL_REQUEST_CHANGE_BAG_SET_RESULT = 1;
		public const int PROTOCOL_REQUEST_BAG_SET_SACK = 4;
		public const int PROTOCOL_REQUEST_BAG_SET_SACK_RESULT = 5;
		
		public const int PROTOCOL_VIRTUAL_MATCH_PARTY = 74;
		/*public const int PROTOCOL_FIND_PARTY_REQ = 0;
		public const int PROTOCOL_FIND_PARTY_REQ = 2;
		public const int PROTOCOL_FIND_PARTY_REQ = 4;
		public const int PROTOCOL_FIND_PARTY_REQ = 7;
		public const int PROTOCOL_FIND_PARTY_REQ = 10;
		public const int PROTOCOL_APPLY_PARTY = 13;
		public const int PROTOCOL_APPLY_PARTY_CANCEL = 16;
		public const int PROTOCOL_FIND_PARTY_CLOSE = 19;
		public const int PROTOCOL_FIND_PARTY_REQ = 20;
		public const int PROTOCOL_FIND_PARTY_REQ = 21;*/

		public const int PROTOCOL_HONOR_MARSHAL_DECO = 76;
		
		public const int PROTOCOL_DECORATION_SYSTEM = 77;
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VIEW_PROFILE_IMPROVE_RANK_INFO
		{
			public int nRankType;
			public int nRankLevel;
			public int nMyPlace;
			public int nTotalPlace;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VIEW_PROFILE_IMPROVE_RESULT
		{
			public long nUserID;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] szCharacterName;

			public int nNumKill;
			public int nNumDeath;
			public int nNumWin;
			public int nNumLose;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PROFILE_DISPLAY_RANK_INFO_COUNT)]
			public PROTO_VIEW_PROFILE_IMPROVE_RANK_INFO[] aRankInfo;

			public int nAI3Level;
			public int nAI3Exp;
			public int nAI3Unk; // 1
			public int nAI3NextLevelExp;
			public int nTotalAchieve;
			public int nNumAchieve;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_EVENT_ITEM
		{
			public int nItemIndex;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ITEM_NAME)]
			public byte[] szItemName;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_EVENT
		{
			public int nCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GACHA_EVENT_ITEM_COUNT)]
			public PROTO_GACHA_EVENT_ITEM[] aItems;
		}

		public enum SECOND_AUTH_RESULT : byte
		{
			SECOND_AUTH_SUCCESS,
			SECOND_AUTH_FAIL,
			SECOND_AUTH_VERIFY_REQUIRED,
			SECOND_AUTH_VERIFY_PASSED
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_DUSTBIN_ITEM_INFO
		{
			public long nSRL;
			public int nItemIndex;
			public int nGauge;
			public ITEM_TIME tExpireTime;
			public ITEM_TIME tDeleteTime;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_DUSTBIN_INVEN_ITEM
		{
			public long nInvenSRL;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DRESS_PART)]
			public long[] nDefaultDressInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PERIOD_CHAR_FUNC)]
			public long[] nDefaultFuncInvenSrl;
		
			public ITEM_TIME tExpireTime;
			public int nDamage;
			public int nGauge;
			public int nRepairCost;
			public byte byFlag;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_DUSTBIN_MOVE_TO_ITEM_DUP_REQ
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string szItemCode;

			public short wExceptionCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public long[] aExceptionInvenSRL;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_DUSTBIN_MOVE_TO_ITEM_DUP_RES
		{
			public byte nResult;
			private short __padding;
			public short wCount;
			public int nItemIndex;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_LIST_ITEM)]
			public long[] aInvenSRL;

			public ushort GetSize()
			{
				return (ushort) (9 + wCount * sizeof(long));
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_DUSTBIN_MOVE_TO_INVEN_REQ
		{
			public long nSRL;
			public int nItemIndex;
			public int nGauge;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_DUSTBIN_MOVE_TO_INVEN_RES
		{
			public byte nResult;
			public long nSRL;
			public PROTO_DUSTBIN_INVEN_ITEM tInvenItem;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_DUSTBIN_MOVE_TO_ITEM_REQ
		{
			public long nInvenSRL;
			public int nItemIndex;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_DUSTBIN_MOVE_TO_ITEM_RES
		{
			public byte nResult;
			public PROTO_DUSTBIN_ITEM_INFO tItem;
			public long nInvenSRL;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_DUSTBIN_ITEM_INFO_RES
		{
			public byte nResult;
			[MarshalAs(UnmanagedType.I1)]
			public bool bChinaFlag;
			
			public short wCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DUSTBIN_ITEM_COUNT)]
			public PROTO_DUSTBIN_ITEM_INFO[] aItems;

			public unsafe ushort GetSize()
			{
				return (ushort) (4 + wCount * sizeof(PROTO_DUSTBIN_ITEM_INFO));
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SECOND_AUTH_RET
		{
			public SECOND_AUTH_RESULT eResult;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_SECOND_AUTH_URL_LEN)]
			public string szSecondAuthURL;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_BEGINNER_GUIDE_END_REQ
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bEnd;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_BEGINNER_GUIDE_END_RES
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bEnd;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_BEGINNER_GUIDE_REWARD_REQ
		{
			public short wGuideType;
			public short wGuideLevel;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_BEGINNER_GUIDE_REWARD_RES
		{
			public enum RESULT
			{
				BEGINNER_GUIDE_REWARD_SUCCESS,
				BEGINNER_GUIDE_REWARD_FAIL
			}
			
			public RESULT eResult;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bFinished;
			public short wGuideType;
			public short wGuideLevel;

			private char dummy_1;
			private int dummy_2;

			public int nRewardCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BEGINNER_GUIDE_REWARD_ITEM_COUNT)]
			public ST_REWARD_ITEM_INFO[] aReward;

			public ushort GetSize()
			{
				return (ushort) (18 + nRewardCount * Marshal.SizeOf<ST_REWARD_ITEM_INFO>());
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_UPGRADE_LEVEL
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VVIP_UPGRADE_LEVEL_TYPE)]
			public byte[] aLevel;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_BUY_WEAPON_CC_EX_PALETTE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string szWeaponItemCode;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_BUY_WEAPON_CC_EX_PALETTE_RESULT
		{
			public enum RESULT
			{
				SUCCESS = 1,
				FAILED
			}
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;

			public RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_WEAPON_CC_COLOR_INFO
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VVIP_WEAPON_CC_PART_COLOR_NUM)]
			public byte[] aColor;
			
			public int iPreset;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_COLOR_CHANGE_REQ
		{
			public int nPresetNo;
			public PROTO_VVIP_WEAPON_CC tVVIPWeaponCC;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_COLOR_CHANGE_RESULT
		{
			public enum RESULT
			{
				VVIP_WEAPON_CC_CHANGE_SUCCESS,
				VVIP_WEAPON_CC_CHANGE_ERROR,
				VVIP_WEAPON_CC_CHANGE_NOT_OWN_COLOR
			}

			public RESULT eResult;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_WEAPON_CC
		{
			public long lInvenSRL;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VVIP_WEAPON_CC_PART_NUM)]
			public PROTO_VVIP_WEAPON_CC_COLOR_INFO[] aColorInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VVIP_WEAPON_CC_EX_PALETTE_NUM)]
			public byte[] aUnlockedColorSet;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_COLOR_INFO_RESULT
		{
			public enum RESULT
			{
				VVIP_WEAPON_CC_INFO_SUCCESS,
				VVIP_WEAPON_CC_INFO_ERROR
			}

			public RESULT eResult;
			public short wCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VVIP_WEAPON_CC)]
			public PROTO_VVIP_WEAPON_CC[] aVVIPWeaponCC;

			public ushort GetSize()
			{
				return (ushort) (6 + wCount * Marshal.SizeOf<PROTO_VVIP_WEAPON_CC>());
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_HIDDEN2_SND_SET_REQUEST
		{
			public byte nVol_HumanHeard;
			public byte nVol_HiddenHeard;
			public byte nVol_Environment;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_HIDDEN2_SND_SET_RESULT
		{
			public enum RESULT
			{
				HIDDEN2_SND_SET_SUCCESS,
				HIDDEN2_SND_SET_VALUE_OVERFLOW,
				HIDDEN2_SND_SET_NO_FUNCTIONAL_ITEM
			}
			
			public RESULT eResult;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_KILL_COUNTER_INFO
		{
			public long lInvenSrl;
			public int nKill;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_KILL_COUNTER_INFO_RES
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bHaveCounterVVIPWeapon;
			
			public short wCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_LIST_ITEM)]
			public PROTO_VVIP_KILL_COUNTER_INFO[] aKillCounter;

			public unsafe ushort GetSize()
			{
				return (ushort) (3 + wCount * sizeof(PROTO_VVIP_KILL_COUNTER_INFO));
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_ATTACHMENT_ITEM_INFO
		{
			public long lAttachmentInvenSrl;
			public long lTargetInvenSrl;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_ATTACHMENT_INFO
		{
			public E_VVIP_ATTACHMENT_TYPE eType;
			public int nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_LIST_ITEM)]
			public PROTO_VVIP_ATTACHMENT_ITEM_INFO[] aItemInfo;

			public unsafe ushort GetSize()
			{
				return (ushort) (5 + nCount * sizeof(PROTO_VVIP_ATTACHMENT_ITEM_INFO));
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_ATTACHMENT_SAVE
		{
			public long lAttachmentInvenSRL;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bAttach;
			
			public long lTargetInvenSRL;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_ATTACHMENT_SAVE_RESULT
		{
			public enum RESULT : byte
			{
				ITEM_ATTACH_SUCCESS,
				ITEM_ATTACH_INVALID_TYPE,
				ITEM_ATTACH_NOT_FOUND_ATTACH,
				ITEM_ATTACH_NOT_FOUND_TARGET,
				ITEM_ATTACH_NOT_EQUIP_ITEM,
				ITEM_ATTACH_ALREADY_EQUIP,
				ITEM_ATTACH_NOT_EQUIP_INFO,
				ITEM_ATTACH_NOT_ETERNAL
			}
			
			public RESULT eResult;
			public E_VVIP_ATTACHMENT_TYPE eType;

			public PROTO_VVIP_ATTACHMENT_ITEM_INFO tAttachInfo;
			public PROTO_VVIP_ATTACHMENT_ITEM_INFO tDetachInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_INSTALMENT_WEAPON_INFO
		{
			public long lInvenSRL;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;
			
			public int nTotalCount;
			public int nPaidCount;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_INSTALMENT_PURCHASE_INFO
		{
			public int nDummy;
			public int nItemIndex;
			public long lInvenSRL;
			public int nDummy2;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DRESS_PART)]
			public long[] nDefaultDressInvenSrl;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PERIOD_CHAR_FUNC)]
			public long[] nDefaultFuncInvenSrl;
			
			public int nDummy3;
			public int nDummy4;
			public int nDummy5;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_INSTALMENT_BUY_RESULT
		{
			public enum RESULT : byte
			{
				SUCCESS,
				FAIL
			}
			
			public RESULT eResult;
			public int nRemainCash;
			public long lInstallmentInvenSRL;
			public PROTO_INSTALMENT_WEAPON_INFO tInstalmentInfo;
			public PROTO_INSTALMENT_PURCHASE_INFO tPurchaseInfo;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_INSTALMENT_BUY_RESULT_FAILED
		{
			public enum RESULT : byte
			{
				SUCCESS,
				REACHED_MAX,
				UNKNOWNERROR,
				INVEN_FULL = 10
			}
			
			public RESULT eResult;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_INSTALMENT_INFO_RESULT
		{
			public enum RESULT : byte
			{
				SUCCESS,
				FAILED
			}
			
			public RESULT eResult;
			public short wCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_INSTALMENT_ITEM_COUNT)]
			public PROTO_INSTALMENT_WEAPON_INFO[] aInfos;

			public ushort GetSize()
			{
				return (ushort) (3 + wCount * Marshal.SizeOf<PROTO_INSTALMENT_WEAPON_INFO>());
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AI_WEAPON_PIECE_COMBINE
		{
			public long lUSN;
			public int nCouponID;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;
			
			public int nCount;
			public long lInvenSrl;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AI_WEAPON_PIECE_COMBINE_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				PIECE_NOT_ENOUGH,
				PIECE_NOTFOUND_ITEM,
				PIECE_DB_FAIL,
				PIECE_COMBINE_FAIL,
				MAX_ONE_PIECE,
				PIECE_NOT_ENOUGH_6,
			}
			
			public RESULT eResult;
			public short wRemainCount;
			public ST_REWARD_ITEM_INFO tRewardItemInfo;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_RING_REENCHANT_DISCONN_INFO_RESULT
		{
			public byte eResult;
			public byte tFlag;
			public long lInvenSrl;
			public int nEffectType;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANGE_BAG_SET
		{
			public int nBagSet;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANGE_BAG_SET_RESULT
		{
			public int nBagSet;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_BAG_SET_SACK
		{
			public int nBagSet;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public unsafe struct PROTO_REQUEST_BAG_SET_SACK_RESULT
		{
			public int nBagSet;
			public PROTO_INVEN_SACK tInvenSack;
			
			public ushort GetSize()
			{
				return (ushort) (tInvenSack.dwCount * sizeof(PROTO_SACK_INST) + 12);
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MILEAGE_SEASON_DAYS_INFO
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
			public int[] aMonths;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
			public int[] aDays;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_WEEKLY_WEAPON_BOX_OPEN
		{
			public byte byBoxSrl;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_WEEKLY_WEAPON_EXTEND
		{
			public long lInvenSrl;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public unsafe struct PROTO_REQUEST_WEEKLY_WEAPON_EXTEND_RESULT
		{
			public enum RESULT : byte
			{
				SUCCESS = 1,
				UNKNOWNERROR,
				FAIL_3,
				FAIL_4, // not handled in cshell
				FAIL_5,
				MONEY_NOT_ENOUGH,
				INVENTORY_FULL,
				FAIL_8, // not handled in cshell
				FAIL_9
			}
			
			public RESULT eResult;
			public byte iBoxSrl;
			public long lInvenSrl;
			public fixed byte tInstBuffer[0x50];
			public int nRemainCash;
		}
		
		public enum WEEKLY_WEAPON_BOX_OPEN_RESULT : byte
		{
			SUCCESS = 1,
			ALREADY_HAVE_ITEM,
			ALREADY_OPENED,
			CANNOT_OPEN_ON_MONDAY_MORNING,
			UNKNOWNERROR,
			NOT_EXISTS,
			INVENTORY_FULL,
			ALREADY_HAVE_WEAPON,
			NOT_OPEN
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_WEEKLY_WEAPON
		{
			public ST_REWARD_ITEM_INFO tReward;
			public ITEM_TIME tItemTime;
			public byte byOpen;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_WEEKLY_WEAPON_BOX_OPEN_RESULT
		{
			public byte byBoxSrl;
			
			public WEEKLY_WEAPON_BOX_OPEN_RESULT eResult;
			public PROTO_WEEKLY_WEAPON tWeapon;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_WEEKLY_WEAPON_BOX_OPEN_ALL_RESULT
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WEEKLY_WEAPON_COUNT)]
			public WEEKLY_WEAPON_BOX_OPEN_RESULT[] aResults;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WEEKLY_WEAPON_COUNT)]
			public PROTO_WEEKLY_WEAPON[] aWeapons;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_WEEKLY_WEAPON_INFO
		{
			public byte byRequestLoc; // 0: OnMMConnectMsg, 1: Enter Storage/etc
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct WEEKLY_WEAPON_ITEM
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string szItemCode;

			public byte byDummy1;
			public byte byIsOpen;
			public byte byDummy2;
			public byte byDummy3;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct WEEKLY_WEAPON_TYPE
		{
			public int iPosition;
			public byte byGachaType;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_WEEKLY_WEAPON_INFO_RESULT
		{
			public int iNextArriveTime;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WEEKLY_WEAPON_COUNT)]
			public WEEKLY_WEAPON_ITEM[] aWeaponItems;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WEEKLY_WEAPON_COUNT)]
			public WEEKLY_WEAPON_TYPE[] aWeaponTypes;
			
			public byte byFlag;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_MILEAGE_ENTER
		{
			public byte bySeasonNum;
			public byte byFPointMall;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_MILEAGE_ENTER_RESULT
		{
			public enum RESULT : byte
			{
				SUCCESS,
				FAILED
			}
			
			public RESULT eResult;
			public int iMileagePoint;
			public byte bySeasonNum;
			public int iNextResetTime;
			public int iDummy;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct ITEMCODE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string szItemCode;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MILEAGE_USER_ITEM_COOLTIME
		{
			public short sItemCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MILEAGE_COOLTIME_ITEM_COUNT)]
			public ITEMCODE[] aItemCodes;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MILEAGE_COOLTIME_ITEM_COUNT)]
			public ITEM_TIME[] aCoolTimes;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_FEVER_LUCKY_RESULT
		{
			public byte byFeverLevel;
			public int iFeverTime;
			public int iFeverLucky;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_FEVER_INFO_RESULT
		{
			public byte byFeverLevel;
			public int iFeverTime;
			public byte byFeverStep;
			public byte byFeverBoost;
			public short wVVIPBoostValue;
		}
		
		public enum FEVERREWARDTYPE : byte
		{
			REWARD_NONE,
			REWARD_ITEM,
			REWARD_GAUGE,
			REWARD_FP,
			REWARD_MP,
			REWARD_MAX
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_FEVER_REWARD_RESULT
		{
			public enum RESULT : byte
			{
				FAIL,
				SUCCESS
			}

			public RESULT eResult;
			
			[MarshalAs(UnmanagedType.Bool)]
			public bool bNoEnoughSpace;
			
			public FEVERREWARDTYPE eType;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemId;

			public int iRewardValue;
			public long lItemSrl;
			public int iItemIndex;
			public int iMPPlusCount;
			public short wExpireTime;
			public byte byFeverStep;
			public byte byFeverLevel;
			public int iFeverTime;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct FEVERVALUE_TABLE
		{
			public int iFeverLevel;
			public int iPointGauge;
			public int iFeverMinute;
			public int iRewardGauge1;
			public int iRewardGauge2;
			public int iBonusGPPercent;
			public int iBonusEPPercent;
			public int iBonusAPPercent;
			public int iRewardMPCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_FEVER_TABLE
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FEVER_LEVEL)]
			public FEVERVALUE_TABLE[] aNormalFeverValueTable;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FEVER_LEVEL)]
			public FEVERVALUE_TABLE[] aWeekendFeverValueTable;

			public short wIsWeekend;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ATTENDANCE_INFO_REP
		{
			public enum RESULT : byte
			{
				SUCCESS
			}

			public enum WEEKENDREWARD : byte
			{
				NOT_WEEKEND,
				WEEKEND_REWARD_AVAILABLE,
				WEEKEND_REWARDED
			}
			
			public RESULT eResult;
			public SYSTEMTIME tSystemTime;
			public byte byDummy;
			public int iNoOfDay;
			public byte byShowPopup;
			public WEEKENDREWARD eWeekendFlag;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REQUEST_FEVER_REWARD
		{
			public byte byFeverStep;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ATTENDANCE_REWARD_REP
		{
			public enum RESULT : byte
			{
				SUCCESS,
				FAIL1,
				FAIL2,
				FAIL3,
				FAIL4,
				FAIL5,
				FAIL6,
				FAIL7,
			}

			public RESULT eResult;

			[MarshalAs(UnmanagedType.Bool)]
			public bool bMPUpdated;

			public byte byDummy;
			public byte byUserType;
			public ATT_REWARDTYPE eRewardType;
			
			public int iRewardCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public ST_REWARD_ITEM_INFO[] tReward;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANNELLIST
		{
			public short nChannelID;
			public short nMaxUser;
			public short nCurrentUser;
			public int iGMSIndex;
			public int nStep;
			
			[MarshalAs(UnmanagedType.Bool)]
			public bool bPassWord;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANNELLIST_CS
		{
			public short nChannelID;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHANNEL_NAME)]
			public string szChannelName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHANNEL_NAME)]
			public string szChannelShortName;

			public short nMaxUser;
			public short nCurrentUser;
			public int iGMSIndex;
			public int nStep;
			public bool bPassWord;
			public int nChannelRestrictMinLevel;
			public int nChannelRestrictMaxLevel;
			public float fChannelRestrictMinKD;
			public float fChannelRestrictMaxKD;
			public int iAddr;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_BUDDY_RETURN_INFO
		{
			public int lTimeStamp;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string szClanID;

			public uint lUsn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szCallName;

			public short nServerNumber;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string szHashedValue;

			public int bSpecialUser;
			public int bGreenCommunityUser;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
			public string szNameCardItemCode;

			public byte nCallNameColorIndex;

			public PROTO_MAIN_BADGE_INFO stMainBadgeInfo; // 8 bytes

			public short nRegion;
			public short nAge;
			public short nGender;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_DEFAULT_AVATAR
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szCharacterName;

			public uint nUSN;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct CHAT_MACRO
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MACRO_CHATTING_LENGTH)]
			public byte[] szChatMacro;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_USER_SETTING
		{
			public short nKeyUp;
			public short nKeyDown;
			public short nKeyLeft;
			public short nKeyRight;
			public short nKeyWalk;
			public short nKeyJump;
			public short nKeyDuck;
			public short nKeyFire;
			public short nKeyReload;
			public short nKeyWSAction;
			public short nKeyDropEquip;
			public short nKeyPrevEquip;
			public short nKeyAction;
			public short nKeyViewBag;
			public short nKeyEquip1;
			public short nKeyEquip2;
			public short nKeyEquip3;
			public short nKeyEquip4;
			public short nKeyEquip5;
			public short nKeyRadio1;
			public short nKeyRadio2;
			public short nKeyRadio3;
			public short nMouseSens;
			public short nSniperSens;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bRejectInvitation;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bRejectWhisper;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bRejectFriendInvitation;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bViewToolTip;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bGetC4;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bViewHintOnGame;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bMouseReverse;
			
			public short nCrossStyle;
			public short nCrossColor;
			public short nKeyMedicKit;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bIsLeftHandView;
			
			public short nDummy;
			public int nOptionOutputType;
			public int nOptionOutputInChat;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MACRO_SAVE_COUNT)]
			public CHAT_MACRO[] aszChatMacro;

			public byte bClanAllChat;
			public byte bClanStepChat;
			public byte bClanBossChat;
			public byte bClanUnitChat;
			public short nWaveReality;
			public byte byShowBadge;
			public byte byShowKillMark;
			public byte byShowClanMedal;
			public short nKeyMicSwitch;
			public short nKeyMicHold;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_DEFAULT_USERINFO
		{
			public byte bIsSuperVisor;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string szDecodedYID;

			public byte bCreatedCharacter;

			public USERSTYLE eUserStyle;

			public USERMANNERS eUserManners;

			public uint nGamePoint;

			public int nRealPoint;

			public int nEP;

			public int nRankingLevel;

			public int nNumPercentageNextLevelEPs;

			public int nNumNextLevelEPs;

			public int nWin;

			public int nLose;

			public int nNumKill;

			public int nNumDeath;

			public int nNumHeadShot;

			public int nFriendKill;

			public int nBadExit;

			public long tChatDenyTime;

			public uint nMyBuddyIP;

			public uint nMyBuddyPort;

			public PROTO_BUDDY_RETURN_INFO tProtoBuddyReturnInfo;

			public PROTO_DEFAULT_AVATAR tProtoDefaultAvatar;

			public PROTO_USER_SETTING tUserSetting;

			public PROTO_MYCLANINFO tClanInfo;

			public byte byTutorial;

			public PCBGRADE ePCBGrade;

			public int bSpecialUser;

			public int bGreenCommunityUser;

			public E_GREEN_COMMUNITY_USER_STATE eGreenCommunityUserState;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
			public string szNameCardItemCode;

			public byte cEscapeCnt;

			public byte nCallNameColorIndex;

			public byte nColorChattingIndex;

			public int nAchieveCallNameEffectValue;

			public int nAchieveNameCardEffectValue;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public ST_ACHIEVE_DISPLAY[] achieveDisplayInfo; // 24 bytes 

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
			public int[] nAIClearCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public int[] nAIPlayCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
			public int[] nAIBossClearCnt;

			public int nNano4PlayCnt;

			public int FPoint;

			public int FCount;

			public uint m_dwStorageCount;

			public uint m_dwAIItemNotify;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GMSCONNINFO
		{
			public uint dwIP;

			public uint uiPort;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MMCONNECT
		{
			
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MMCONNECTRESULT
		{
			public MMCONNECTRESULT eResult;
			public MMCONNECT_UNKNOWNERROR_DETAIL_INFO eUnknownerrorNumber;
			public PROTO_DEFAULT_USERINFO tDefaultUserInfo;
			public short nServerNumber;
			public int tClientAddr;

			//[MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MM_CHANNEL_MAX_COUNT)]
#if USE_CS_CHANNELLIST
			public PROTO_CHANNELLIST_CS[] tChannelList;
#else
			public PROTO_CHANNELLIST[] tChannelList;
#endif

			public int iGMSCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
			public PROTO_GMSCONNINFO[] aGMSConnInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
			public SHOP_LIST[] aShopList;

			//public int m_nWeaponHackTime;
			public int m_nSSN;

			//public int m_nWeaponHackUser;
			public int m_nDummy1;

			//public int nMaxRoom;
			public int m_nDummy2;

			public int bAMSystemUseFlag;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MMDISCONNECT_RESULT
		{
			public DISCONNECTRESULT eResult;

			public PROTO_RETURN_INFO tProtoReturnInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ACCEPT_RESULT
		{
			public ACCEPTRESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MM_SERVER_EXIT
		{
			public MMSERVER_EXITTYPE eServerExitType;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MM_SERVER_EXIT_RESULT
		{
			public MMSERVER_EXITTYPE eServerExitType;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CONNTIME_ALARM
		{
			public byte byAlarmType;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_DAILY_ATTEND_QNA
		{
			public DAILY_ATTEND eDailyAttendTye;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SAVE_USERSETTING
		{
			public PROTO_USER_SETTING tUserSetting;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SAVE_USERSETTING_RESULT
		{
			public SAVEUSERSETTING eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_DAILY_SCORE_RESULT
		{
			public int nTodayEPAmount;
			public uint nTodayGP;
			public int nTodayKillCnt;
			public int nTodayDeathCnt;
			public int nTodayHeadShotCnt;
			public int nTodayWin;
			public int nTodayLose;
			public long nTotalEP;
			public uint nTotalGP;
			public int nMorePlayCountToNextLevel;
			public short nNextLev;
			public int nTodayMP;
			public int nTotalMP;
			public byte tRankMatchType;
			public int nRankMatchWin;
			public int nRankMatchLose;
			public int nRankMatchTodayKillCnt;
			public int nRankMatchTodayDeathCnt;
			public byte tRankMatchMedal;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bFirstShow;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHCONNECT
		{
			public short nChannelID;
			public uint auto_move_key_;
			public short nDummy;
			public int nFlag;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_CHANNEL_PW_MAXLENGTH)]
			public byte[] szChannelPassword;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANNEL_USERLIST
		{
			public int nRankingLevel;
			public short nFameGrade;
			public int nClientKey;
			public long nClientUSN;
			public PROTO_MYCLANINFO tClan;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] szCharacterName;

			public int nNameCardIndex1;
			public int nNameCardIndex2;
			public PROTO_MAIN_BADGE_INFO stMainBadgeInfo;
			
#if USE_CFVIP_SYSTEM
			[MarshalAs(UnmanagedType.I1)]
			public bool bSpecialUser;
			public byte bySpecialUserLevel;
			public short sDummy;
			[MarshalAs(UnmanagedType.I1)]
			public bool bGreenCommunityUser;
#endif
			
			public int nCallNameColorIndex;
			public int nChattingColorIndex;
			public int nAchieveCallNameEffectValue;
			public int nAchieveNameCardEffectValue;
			public ST_ACHIEVE_DISPLAY achieveDisplayInfo;
			
			public byte byRankedMatchLev1;
			[MarshalAs(UnmanagedType.I1)]
			public bool bDisplayRankedMatchLev1;
			public byte byRankedMatchLev2;
			[MarshalAs(UnmanagedType.I1)]
			public bool bDisplayRankedMatchLev2;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHCONNECT_RESULT
		{
			public CHCONNECTRESULT eResult;
			public short nChannelID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANNELLIST_RESULT
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_CHANNEL_MAX_COUNT)]
			public PROTO_CHANNELLIST[] tChannelList;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANNEL_USERLIST_RESULT
		{
			public short nCurrentChannelUser;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNEL_USERLIST_NUM)]
			public PROTO_CHANNEL_USERLIST[] tProtoChannelUserList;

			public ushort GetSize()
			{
				return (ushort) (2 + nCurrentChannelUser * Marshal.SizeOf<PROTO_CHANNEL_USERLIST>());
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHLEAVE_RESULT
		{
			public CHLEAVERESULT eResult;
			public PROTO_CHANNELLIST_RESULT tChannelInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOMOBSERVER_RESULT
		{
			public ROOMOBSERVER eResult;
			public short nTeamIndex;
			public short nSlotIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANGE_OBSERVER
		{
			public short nTeamIndex;
			public short nSlotIndex;
			public short nNewSlotIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MM_ROOMLIST
		{
			public short nRoomID;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bIsOpen;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bIsFreeCamera;
			
			public ROUNDTYPE eRoomGameRule;
			public byte eWeaponType;
			public byte eItemDropType;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bBanThrows;
			
			public byte bUnknown; // 1
			public short nRoomHostPing;
			public DEATHMATCHTYPE eWinCondition;
			public int nWinGoal;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bEliteMode;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bFriendlyFire;
			
			public short nMapType;
			public short nRoomUser;
			public short nRoomMaxUser;
			public short nRoomObserverMaxUser;
			public byte nBossNameColorIndex;
			public ROOMSTATUS eRoomStatus;
			
			public PROTO_MYCLANINFO tClan;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string szBossName;

			public USERMANNERS eBossManners;
			public int nRankingLevel;
			public short wFameGrade;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_ROOM_NAME_MAXLENGTH)]
			public byte[] szRoomName;

			[MarshalAs(UnmanagedType.I1)]
			public bool bIsPlayingEnter;
			
			public short nRoomBots;
#if USE_CFVIP_SYSTEM
			[MarshalAs(UnmanagedType.I1)]
			public bool bSpecialUser;
			
			public byte bSpecialUserLevel;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bGreenCommunityUser;
#endif
			public TOURROOM_STATE eTourRoomState;
			public byte tRound;
			public uint m_dwVVIPUserCount;
			
			[MarshalAs(UnmanagedType.Bool)]
			public bool m_bLeagueGameRoom;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bWaveBalance;
			
			public byte bFlag2;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bThrowingAxe;
			
			public short bFlag3; // 1
			public short sBotDifficulty;
			public byte bFlag4;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bExtensionPack;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bNewObserver;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bNewObserverViewable;
			
			public byte bFlag5;
			public byte bFlag6;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bTigMatch;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_UCC_MAP_PATH_LENGTH)]
			public string szUCCMapPath;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MM_ROOMLIST_RESULT
		{
			public ROOMLISTRESULT eResult;
			public short nRoomCount;
			public byte bySeq;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_CHANNEL_MAX_ROOM)]
			public PROTO_MM_ROOMLIST[] pProtoRoomList;

			public ushort GetSize()
			{
				return (ushort) (7 + nRoomCount * Marshal.SizeOf<PROTO_MM_ROOMLIST>());
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MM_ROOMCREATE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MM_ROOM_PW_MAXLENGTH)]
			public string szRoomPW;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bIsOpen;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bIsFreeCamera;
			
			public short nRoomMaxUser;
			public byte userslot_cnt_;
			public short nRoomObserverMaxUser;
			public short nMapType;
			public ROUNDTYPE eGameRule;
			public byte eWeaponType;
			public byte eItemDropType;
			public byte bBanFlash;
			public byte nRoomUser;
			public DEATHMATCHTYPE eWinCondition;
			public int nWinGoal;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bEliteMode;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bFriendlyFire;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bIsPlayingEnter;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_ROOM_NAME_MAXLENGTH)]
			public byte[] szRoomName;

			public float fTimeToRespawn;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool m_bLeagueGame;
			
			public uint m_dwLeagueIndex;
			public uint m_dwTeamIndex;
			public uint m_nLeagueRoomID;

			[MarshalAs(UnmanagedType.I1)] 
			public bool bBalance;
			
			public byte byUnknown; // 0
			public byte byUnknown2; // 0xFF
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ROOM_NAME_HASH_LENGTH)]
			public string szHash;

			public short nBotDifficulty;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bFlag3; // 0
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bC4Wire; // 1
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bExtensionPack; // 0
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bFlag4; // 4
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bAllowWatch;

			public byte byDummy2; // 0
			public byte byDummy3; // 1
			public byte byDummy4; // 1, wave = 0
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bAutoSwap;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bTigMatch;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_UCC_MAP_PATH_LENGTH)]
			public string szUCCMapPath;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MM_ROOMCREATE_RESULT
		{
			public ROOMCREATE_RESULT eResult;
			public short nID;
			public uint m_dwTeamEffectCount;
			public uint m_dwVVIPUserCount;
			public uint dwVVIPItemCount;
			public uint dwDummy;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VVIP_ITEM_NUM)]
			public VVIP_ITEM[] arrVVIPItemInfoIndex;

			public P_ATNM.PROTOCOL_MM_AUTOLEAGUE_RESULT_TYPE eLeagueResult;

			[MarshalAs(UnmanagedType.Bool)]
			public bool m_bLeagueGameRoom;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string m_szFirstTeamName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string m_szSecondTeamName;

			public P_ATNM.MatchType m_eMatchType;
			public SYSTEMTIME m_tCurrentTime;
			public SYSTEMTIME m_tStartingTime;

			public int dummy;
			public short nMapType;
			public short newDummy; // 1
			public ROUNDTYPE eGameRule;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 34)]
			public byte[] aDummy;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOMENTER
		{
			public short sRealDummy;
			public short nRoomID;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MM_ROOM_PW_MAXLENGTH)]
			public string szRoomPassword;

			public int nGlobalRoomAutoMoveKey;
			public byte nAutoMove1;
			public byte nAutoMove2;
			[MarshalAs(UnmanagedType.I1)] public bool m_bLeagueGame;
			public uint m_dwLeagueIndex;
			public uint m_dwTeamIndex;
			public byte b3; // 4
			public byte b4; // 1
			public byte b5; // 1
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MM_ROOMENTER_RESULT
		{
			public ROOMENTER_RESULT eResult;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
			public string szWinTeamName;

			public ushort nWinTeamResult;
			public ushort nLoseTeamResult;

			public int nAILevel;
			public int nn1;
			public int nn2_dummy;
			
			public PROTO_MM_ROOMINFO tRoomInfo;

			public int nUserCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_USER_IN_ROOM)]
			public PROTO_SLOTINFO[] tSlotInfos;

			public ushort GetSize()
			{
				return (ushort) (57 + Marshal.SizeOf<PROTO_MM_ROOMINFO>() + nUserCount * Marshal.SizeOf<PROTO_SLOTINFO>());
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOMBOTS_ADD
		{
			public short nTeamIndex;
			public short nSlotIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOMBOTS_ADD_RESULT
		{
			public ROOMBOTSADD eResult;
			public short nTeamIndex;
			public short nSlotIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOMBOTS_REMOVE
		{
			public short nTeamIndex;
			public short nSlotIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOMBOTS_REMOVE_RESULT
		{
			public ROOMBOTSREMOVE eResult;
			public short nTeamIndex;
			public short nSlotIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOMTEAM_CHANGE
		{
			public short nTeamIndex;
			public short nNewSlotIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOMTEAM_CHANGE_RESULT
		{
			public ROOMTEAMCHANGE eRoomTeamChange;
			public short nSlotIndex;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bObserverState;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOMTEAM_CHANGE_AIBOT_RESULT
		{
			public ROOMTEAMCHANGE eRoomTeamChange;
			public int nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_USER_IN_ROOM)]
			public PROTO_SLOTINFO[] aSlotInfo;

			public int GetSize()
			{
				return 8 + nCount * Marshal.SizeOf<PROTO_SLOTINFO>();
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MM_ROOMSETTING_CHANGE
		{
			public ROOMSETTINGCHANGEFLAG eChangeFlag;
		
			[MarshalAs(UnmanagedType.I1)] 
			public bool bIsFreeCamera;
			
			public short nRoomMaxUser;
			public short nRoomObserverMaxUser;
			public byte byModeMaxUser;
			public short nMapType;
			public ROUNDTYPE eGameRule;
			public byte eWeaponType;
			public byte eItemDropType;
			public byte by1;
			public byte by2;
			public DEATHMATCHTYPE eWinCondition;
			public int nWinGoal;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bEliteMode;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bIsPlayingEnter;
			
			public float fTimeToRespawn;
			public ushort wInitialTP;

			[MarshalAs(UnmanagedType.I1)] 
			public bool bFriendlyFire;
			
			public byte cRound;
			public short s1;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_ROOM_NAME_MAXLENGTH)]
			public byte[] szRoomName;

			[MarshalAs(UnmanagedType.I1)] 
			public bool bBalance;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bTagOption;

			public byte byUnknown2; // 0xFF
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ROOM_NAME_HASH_LENGTH)]
			public string szHash;
			
			public short sBotDifficulty;
			public short sBotWeapon;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_USER_IN_ROOM)]
			public long[] aUserID;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_USER_IN_ROOM)]
			public byte[] aIsBots;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bBotsMode;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bUnknown1;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bUnknown2;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bExtensionPack;

			[MarshalAs(UnmanagedType.I1)] 
			public bool bLiveViewing;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bC4Wire;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bAutoSwap;
			
			[MarshalAs(UnmanagedType.I1)] 
			public bool bTigMatch;
		}

		public enum ROOMSETTING
		{
			ROOMSETTING_SUCCESS,
			ROOMSETTING_FAIL,
			ROOMSETTING_UNKNOWNERROR
			// result 4...
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MM_ROOMSETTING_CHANGE_RESULT
		{
			public ROOMSETTING eResult;
			public uint m_dwTeamEffectCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_USER_IN_ROOM)]
			public long[] aUserID;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_USER_IN_ROOM)]
			public byte[] aIsBots;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bBotsMode;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bLiveViewing;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bObserver;
		}

		public enum ROOMLEAVE_RESULT
		{
			ROOMLEAVE_SUCCESS,
			ROOMLEAVE_WRONGROOM,
			ROOMLEAVE_UNKNOWNERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOMLEAVE_RESULT
		{
			public ROOMLEAVE_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_START_GAME
		{
			public short nMapType;
			public byte byUnknown;
			public byte byAI2AddFuel;
			public byte byAI2AddFuelCount;
			public byte byUnknown2;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_STARTGAME_RESULT
		{
			public STARTGAME_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GAMESERVER_INFO
		{
			public byte byResult;
			public uint gamePort;
			public uint gameAddr;
			public short nMapType;
			public short sHostTeam;
			public short sHostIndex;
			public int nGhostPlayCnt;
			public byte byRankMatchMode;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
			public byte[] aDummy;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_GAME_SERIAL + 1)]
			public string szGameLogSerial;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_FORCE_ROOMLEAVE
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCharacterName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_FORCE_ROOMLEAVE_RESULT
		{
			public FORCE_ROOMLEAVE_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SETREADY
		{
			public byte bReady;
			public byte by1;
			public byte by2;
			public byte by3;
		}

		public enum SETREADY_RESULT
		{
			SETREADY_SUCCESS,
			SETREADY_WRONGUSER,
			SETREADY_UNKNOWNERROR,
			SETREADY_PLAYINGROOM
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SETREADY_RESULT
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bReadyState;
			
			public SETREADY_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_QUICKJOIN
		{
			public ROUNDTYPE eRoundType;
			public int bwWeaponType;
			public short iMap;
			public int bwRoomStatus;
			public byte nUserCount;
			public byte bEliteMode;
			public int iAILevel;
			public byte b1; // hidden2 related
			public byte b2;
			public byte b3; // nano4 related, extension pack?
			public byte b4;
			public byte b5;
			public byte b6;
			public byte b7;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 101)]
			public byte[] szRezName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_QUICKJOIN_RESULT
		{
			public QUICKJOIN_RESULT eResult;
			public PROTO_MM_ROOMINFO tRoomInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_INVITE_CALL
		{
			public int nClientKey;
			public long nClientUSN;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_INVITE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CLAN_NAME_LENGTH)]
			public string szInvitorClanName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string szInvitor;

			public short nRoomID;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MM_ROOM_PW_MAXLENGTH)]
			public string szRoomPassword;

			public sbyte byDummy; // 0xFF
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_INVITE_RESULT
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CLAN_NAME_LENGTH)]
			public string szInviteeClanName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string szInvitee;

			public INVITE_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_JOINGAME_RESULT
		{
			public uint gamePort;
			public uint gameAddr;
			public short sHostTeam;
			public short sHostIndex;
			public unsafe fixed byte aDummy[41];
			public JOINGAME_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOM_BOSSCHANGE
		{
			public byte iTeamIndex;
			public int iBossIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_RECVLIST
		{
			public short nGiftCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GIFT_RECVLIST)]
			public PROTO_GIFT_INFO_ACK[] aGiftInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_INFO_ACK
		{
			public long nnSRL;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
			public string szType;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string szNick;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string szGiveNick;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
			public string szConfirmYN;

			public int tRegDate;
			public int tExpDate;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_CHECK
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] strTargetNick;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_CHECK_ACK
		{
			public long nnTargetUSN;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] strTargetNick;

			public E_ACK cResult;

			public enum E_ACK : sbyte
			{
				SUCCESS,
				NO_USER = -1,
				QUERY_ERROR = -2,
				CRITICAL = -3
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_POST
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] strTargetNick;

			//public long nUserID; // CF 3.0 Added
			public int nDummy;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_POST_ACK
		{
			public int dwUserCash;
			public E_ACK cResult;
			public long nnSRL;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] strTargetNick;

			public enum E_ACK : sbyte
			{
				SUCCESS,
                NO_USER = -1,
                NO_ITEM = -2,
                NO_MONEY = -3,
                QUERY_ERROR = -4,
                INVALID_FRIEND = -5,
                BILLING_DOWN = -6,
                DB_DOWN = -7,
                NULL_DATA = -8,
                CRITICAL = -9,
                NOT_CLAN = -10
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_CONFIRM
		{
			public long nnSRL;
			
			public unsafe fixed byte aPRISColor[40];
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_CONFIRM_ACK
		{
			public long nnSRL;
			public E_ACK cResult;

			public enum E_ACK : sbyte
			{
				ALREADY = 1,
				SUCCESS = 0,
				NO_SRL_USER = -1,
				FAIL_CONFIRM = -2,
				INVALID_STATE = -3,
				FAIL_INSERT_ITEM = -4,
				EXPIRE = -5,
				QUERY_ERROR = -6,
				DB_DOWN = -7,
				NULL_DATA = -8,
				CRITICAL = -9,
				MAX_ITEM_COUNT = -10,
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_RETURN
		{
			public long nnSRL;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_RETURN_ACK
		{
			public long nnSRL;
			public E_ACK cResult;

			public enum E_ACK : sbyte
			{
				SUCCESS,
				NO_SRL_USER = -1,
				FAIL_RETURN = -2,
				INVALID_STATE = -3,
				FAIL_INSERT_ITEM = -4,
				EXPIRE = -5,
				QUERY_ERROR = -6,
				DB_DOWN = -7,
				NULL_DATA = -8,
				CRITICAL = -9
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_RECONFIRM
		{
			public long nnSRL;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_RECONFIRM_ACK
		{
			public long nnSRL;
			public E_ACK cResult;

			public enum E_ACK : sbyte
			{
				SUCCESS,
				NO_SRL = -1,
				INVALID_STATE_X = -2,
				INVALID_STATE_N = -3,
				QUERY_ERROR = -4,
				DB_DOWN = -5,
				NULL_DATA = -6,
				CRITICAL = -7
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_ITEMINFO_NOTI
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;

			public long nnInvenSRL;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DRESS_PART)]
			public long[] nnDefaultDressInvenSRL;

			public int nBonusGP;
			//public int nBonusFP;
			
			public PROTO_MYCLANINFO tClanInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PERIOD_CHAR_FUNC)]
			public long[] nnDefaultFuncInvenSRL;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_COUNT_ACK
		{
			public int nRecv;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_COUNT_RETURNED_ACK
		{
			public int nRecv;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_TAKE_BACK
		{
			public long nnSRL;
			
			public unsafe fixed byte aPRISColor[40];
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GIFT_TAKE_BACK_ACK
		{
			public long nnSRL;
			public E_ACK cResult;

			public enum E_ACK : sbyte
			{
				SUCCESS,
				NO_SRL_USER = -1,
				FAIL_TAKE_BACK = -2,
				INVALID_STATE = -3,
				FAIL_INSERT_ITEM = -4,
				NOT_EXPIRED = -5,
				QUERY_ERROR = -6,
				DB_DOWN = -7,
				NULL_DATA = -8,
				CRITICAL = -9
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HOST_QUALIFY
		{
			public byte bQualifyHost;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HOST_QUALIFY_RESULT
		{
			public int dummy;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_PING
		{
			public uint dwTickCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_PONG
		{
			public uint dwTickCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_JOINHOST_INFO
		{
			public byte byDummy;
			public short nMapType;
			public uint gamePort;
			public uint gameAddr;
			public JOIN_HOST_RESULT eResult;
			public int nGhostPlayCnt;
			public PROTO_MAIN_BADGE_INFO stMainBadgeInfo;
			public byte byRankMatchMode;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
			public byte[] aDummy;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_GAME_SERIAL + 1)]
			public string szGameLogSerial;
			
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct FUNC_ITEM_USE
		{
			public int Counts;
			public int Index;
			public long InvenSrl;
			public int Variation;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GAME_ENDED
		{
			public short nRoomID;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string szGameLogSerial;

			public PROTO_TEAM_STAT tTeamStat;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public PROTO_USER_STAT[] aUserStat;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
			public int[] iGameEndTypeCounts;

			public uint iMVPUserID;
			public byte bIs1stPlaceMVP;
			public uint iFirstKillUserID;
			public uint iLastKillUserID;
			public uint iAceUserID;
			public uint lTopEscapeUserID;
			public byte bLastRoundClearFlag;

			public MORE_INFO_FOR_AI AIMoreInfo;
			public PROTO_HASHDATA sProtoHashData;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GAME_ENDED_RESULT
		{
			public GAMEENDED_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GAME_USER_DISCONNECTED
		{
			public uint iUserID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HOST_EXIT_REQ
		{
			public HOST_RUNNING_GAME_INFO sHostRunningGameInfo;

			public PROTO_HASHDATA sProtoHashData;

			public HOST_CASHITEM_DATA sHostCashItemData;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HOST_EXIT_RESULT
		{
			public HOSTEXIT_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HOST_SHIFTING_STARTED
		{
			public SHIFT_HOST eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_HOST_SHIFTING_FINISHED
		{
			public byte bHostGracefullyDisconnected;
			public uint gamePort;
			public uint gameAddr;
			public short sHostTeam;
			public short sHostIndex;
			public SHIFT_HOST_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HOST_RESUME_INFO
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public uint[] iUserID;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public int[] iEP;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public int[] iGP;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public int[] iRoundEP;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public int[] iRoundGP;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string iNumRound;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string iNumWin;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string iNumHeadShot;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string iNumRoundAlive;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
			public int[] iGameEndTypeCounts;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
			public ushort[] aSackUseCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 176)]
			public ushort[] iWeaponTypeKillCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 176)]
			public ushort[] iWeaponTypeHeadshotCount;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string iNumC4Plant;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string iNumC4Explode;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string iNumC4Defuse;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string iTotalDamage;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string iTeamKillCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HOST_SHIFTING_REQ
		{
			public byte bHostGracefullyDisconnected;

			public HOST_RUNNING_GAME_INFO sHostRunningGameInfo;

			public PROTO_START_HOST sProtoStartHost;

			public PROTO_HOST_RESUME_INFO sProtoHostResumeInfo;

			public PROTO_HASHDATA sProtoHashData;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HOST_SHIFTING_RESULT
		{
			public uint gamePort;

			public uint gameAddr;

			public SHIFT_HOST_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_HOST_USER_STAT
		{
			public PROTO_USER_STAT tUserStat;
			public PROTO_HASHDATA sProtoHashData;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_HOST_USER_STAT_RESULT
		{
			public HOST_USER_STAT_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_HOST_BANUSER
		{
			public long iBannedUserUSN;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_HOST_BANUSER_RESULT
		{
			public HOST_BANUSER_RESULT eResult;
			public long iBannedUserUSN;
		}

		public enum HOST_REPORT
		{
			HOST_REPORT_CURSE,
			HOST_REPORT_MAP_BUG,
			HOST_REPORT_SPEED_HACK,
			HOST_REPORT_TRANSPARENCY_HACK,
			HOST_REPORT_AIM_HACK,
			HOST_REPORT_BUG,
			HOST_REPORT_ETC,
			HOST_REPORT_MAX
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_HOST_REPORT
		{
			public long nDestUSN;
			public HOST_REPORT nReportType;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_REPORT_MSG_LENGTH)]
			public byte[] strReportMsg;
		}

		public enum HOST_REPORT_RESULT
		{
			HOST_REPORT_SUCCESS,
			HOST_REPORT_ERROR,
			HOST_REPORT_ERROR_LIMIT_COUNT,
			HOST_REPORT_UNKNOWNERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_HOST_REPORT_RESULT
		{
			public HOST_REPORT_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FORCE_GAME_FINISH_RESULT
		{
			public FORCEFINISH_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct INVITEE
		{
			public int nGender;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string szInvitee;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_INVITEE_LIST_RESULT
		{
			public INVITEE_LIST_RESULT eResult;

			public int nInvitees;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_PROTO_INVITEE_LIST)]
			public INVITEE[] tInvitees;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANGE_CHANNELUSER_LIST
		{
			public CHANNELUSER eChannelUser;
			public PROTO_CHANNEL_USERLIST tProtoChannelUserList;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MM_CHANGE_CHANNELROOM_LIST
		{
			public CHANNELROOM eChannelRoom;
			public PROTO_MM_ROOMLIST tProtoRoomList;
			// public short nFreeRoomCount; // US
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANGE_BOTSDATA
		{
			public CHANGEBOTSDATA eBotsData;
			public short nTeamIndex;
			public short nSlotIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANGE_TEAMINDEX
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string szCharacterName;

			public short nTeamIndex;
			public short nSlotIndex;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bObserverState;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANGE_USERLIST
		{
			public CHANGEUSERLIST eChangeUserList;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string szBossCharacterName;

			//public short nSlotIndex;
			public PROTO_SLOTINFO tProtoSlotInfo;
			public PROTO_MYCLANINFO tClanInfo;
			public uint m_dwTeamEffectCount;
			public uint m_dwVVIPUserCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CHANGE_READY
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCharacterName;

			[MarshalAs(UnmanagedType.I1)]
			public bool bReady;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CHANGE_PLAY
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszCharacterName;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bPlay;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NOTIFY_GAMEFINISHED
		{
			public short nMapType;
			[MarshalAs(UnmanagedType.I1)] public bool bAbnormal;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GAMERESULT_USER
		{
			public long lUsn;

			public short nOldLevel;
			public short nSteppedLevel;
			public short sDummy2;
			public short nNewLevel;

			public byte wave_level_before_;
			public byte wave_level_;
			public byte wave_exp_percent_before_;
			public byte wave_exp_percent_;
			public byte wave_exp_before;
			public byte wave_level_max_exp;

			public short sDummy_1;
			public short sDummy_2;
			public short sDummy_3;
			public short sDummy_4;
			public short sDummy_5;
			public short sDummy_6;

			public int nGetEP;
			public int nPlusEP;

			public byte bUserInPCB;
			public PCBGRADE ePCBGrade;
			public byte bUserHaveGPUpItem;
			public byte bUserHaveEPUpItem;
			public int nNameCardIndex1;
			public int nNameCardIndex2;
			public byte nCallNameColorIndex;
			[MarshalAs(UnmanagedType.I1)] public bool bSpecialUser;
			public byte bySpecialUserLevel;
			[MarshalAs(UnmanagedType.I1)] public bool bGreenCommunityUser;
			public byte bObserverUser;
			public byte cEscapeCount;
			public byte byUnknown1;
			public byte byUnknown2;
			public PROTO_MAIN_BADGE_INFO stMainBadgeInfo;
			public byte bIsFriendSameRoom;
			public uint dwVVIPItemEffect; // guessing
			public uint dwVVIPItemCount;
			public int nAchievePlusGp;
			public int nAchievePlusEp;
			public int nAchieveCallNameEffectValue;
			public int nAchieveNameCardEffectValue;
			public ST_ACHIEVE_DISPLAY achieveDisplayInfo;
			public byte is_global_room_enter_;
			public int nAIScore;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 78)]
			public byte[] aDummy_2;

			public byte byBossClearCount; // always 5?
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_UPDATE_GAMERESULT
		{
			public short nOldLevel;
			public short nNewLevel;
			public short nFamePoint;
			public int bIsRUEventApply;
			public int nFameUpgrade; // flag
			public int nGetGP;
			public int nGetEP;
			public int nCurrentGP;
			public long nCurrentEP;
			public int nNumPercentageNextLevelEPs;
			public int nNumNextLevelEPs;
			public int nMyTotalKill;
			public int nMyTotalDeath;
			public int nMyTotalWin;
			public int nMyTotalLose;
			public int nMyTotalFriendKill;
			public int nMyTotalBadExit;
			public ushort nMyHeadShot;
			public ushort nMyDrawKill;
			public ushort nMyKnifeKill;
			public ushort nMySetupC4;
			public ushort nMyBurstC4;
			public ushort nMyRemoveC4;
			public ushort nMyFriendKill;
			public ushort nMyUnknownKill;
			public int nSuppliedWeaponInfoIndex;
			public int nNoSuppliedWeaponGP;
			public byte bShowResult;
			public int nPlusEP;
			public int nFPointRewardItemIndex;
			public long nFPointRewardItemID;
			public long nFPointRewardInvenSRL;
			public int bIsClanHalf;
			public int nTeamEffectCount;
			public int nAIScore;
			public uint dwVVIPItemCount;
			public int nAchievePlusGp;
			public int nAchievePlusEp;
			public int nAchievePlusFp;
			public byte is_global_room_enter_;
			
			public int nAddALEPPercent;
			
			public int nFeverBonusGP;
			public int nFeverBonusEP;
			public int nFeverLuckyGauge;
			public int nFeverGameGauge;
			public byte byFeverLevel;
			public int iFeverTime;

			public byte byDummy_;
			
			public short s_nano_soulstone_data1;
			public int i_nano_soulstone_data2;

			public byte b_dummy;
			public short s_dummy2;
			
			public byte bMyMPContributionFull;
			public short nMyMPContribution;
			public byte bOtherMPContributionFull;
			public short nOtherMPContribution;
			public float fMyBombTime;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_NOTIFY_GAMERESULT
		{
			public ROUNDTYPE eRoundType;
			public CLANGAMETYPE eClanGameType;
			public int bIsDeathZeroGame;
			public ushort nPCBEventGP;
			public ushort nPCBEventEP;
			public byte tWinTeamIndex;
			public byte tLoseTeamIndex;
			public ushort nWinTeamResult;
			public ushort nLoseTeamResult;
			public long lMVPUsn;
			public long iFirstKillUSN;
			public long lLastKillUsn;
			public long lAceUsn;
			public long lTopEscapeUsn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 26)]
			public string szMVPClanName;

			public byte bClanHalfTime;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 73)]
			public byte[] aDummy;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_USER_IN_ROOM)]
			public PROTO_GAMERESULT_USER[] tProtoGameResultUser;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_VIEW_IDCARD
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szCharacterName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_VIEW_IDCARD_RESULT
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szCharacterName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
			public string szClanName;

			public short nRankingLevel;

			public int nWin;

			public int nLose;

			public int nKill;

			public int nDeath;

			public int nHeadShotKill;

			public int nDrawKill;

			public int nKnifeKill;

			public USERSTYLE eUserStyle;

			public USERMANNERS eUserManners;

			public int nBadExit;

			public int nTeamKill;

			public int nTeamMatchGameWin;

			public int nTeamMatchGameLose;

			public int nTeamMatchGameKill;

			public int nTeamMatchGameDeath;

			public int nTeamDeathMatchGameWin;

			public int nTeamDeathMatchGameLose;

			public int nTeamDeathMatchGameKill;

			public int nTeamDeathMatchGameDeath;

			public int nNewDeathMatchGameWin;

			public int nNewDeathMatchGameLose;

			public int nNewDeathMatchGameKill;

			public int nNewDeathMatchGameDeath;

			public int nHiddenMatchGameWin;

			public int nHiddenMatchGameLose;

			public int nHiddenMatchGameKill;

			public int nHiddenMatchGameDeath;

			public int nCSMatchGameWin;

			public int nCSMatchGameLose;

			public int nCSMatchGameKill;

			public int nCSMatchGameDeath;

			public int nDeathTeamGameWin;

			public int nDeathTeamGameLose;

			public int nDeathTeamGameKill;

			public int nDeathTeamGameDeath;

			public int nClanMainGameWin;

			public int nClanMainGameLose;

			public int nClanMainGameKill;

			public int nClanMainGameDeath;

			public int nClanSubGameWin;

			public int nClanSubGameLose;

			public int nClanSubGameKill;

			public int nClanSubGameDeath;

			public int nClanSubDeathMatchGameWin;

			public int nClanSubDeathMatchGameLose;

			public int nClanSubDeathMatchGameKill;

			public int nClanSubDeathMatchGameDeath;

			public int nClanSubHiddenGameWin;

			public int nClanSubHiddenGameLose;

			public int nClanSubHiddenGameKill;

			public int nClanSubHiddenGameDeath;

			public int nClanSubMoneyDeathMatchGameWin;

			public int nClanSubMoneyDeathMatchGameLose;

			public int nClanSubMoneyDeathMatchGameKill;

			public int nClanSubMoneyDeathMatchGameDeath;

			public int nClanSubDeathTeamGameWin;

			public int nClanSubDeathTeamGameLose;

			public int nClanSubDeathTeamGameKill;

			public int nClanSubDeathTeamGameDeath;

			public PROTO_MAIN_BADGE_INFO stMainBadgeInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			public int[] nBadgeLevel;

			public int nEscapeGameWin;

			public int nEscapeGameLose;

			public int nEscapeGameKill;

			public int nEscapeGameDeath;

			public int nSpecialGameWin;

			public int nSpecialGameLose;

			public int nSpecialGameKill;

			public int nSpecialGameDeath;

			public int nWaveGameWin;

			public int nWaveGameLose;

			public int nWaveGameKill;

			public int nWaveGameDeath;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
			public int[] nAIClearCnt;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_REQUEST_TOUR_PROFILE_CARD
		{
			public int bMyProfile;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szCharacterName;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_TOUR_PROFILE_CARD
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
			public string szTeamName;

			public int nCurrentStep;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
			public ulong[,] nKillDeathPerRoundType;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
			public ulong[,] nWinLosePerRoundType;

			public ulong nHeadShot;

			public ulong nThrowKill;

			public ulong nKnifeKill;

			public ulong nMVPCnt;

			public ulong nACECnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public ulong[] nTournamentResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_VIEW_TOUR_PROFILE_RESULT
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szCharacterName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
			public string szClanName;

			public short nRankingLevel;

			public PROTO_TOUR_PROFILE_CARD tProtoTourProfileCard;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_TUTORIAL_COMPLETE
		{
			public byte byTutorial;
			public byte byBeginnerGuide;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_TUTORIAL_COMPLETE_RESULT
		{
			public short nOldLevel;
			public short nNewLevel;
			
			[MarshalAs(UnmanagedType.Bool)]
			public bool bIsRUEventApply;
			
			public int nGetGP;
			public int nGetEP;
			public uint nCurrentGP;
			public long nCurrentEP;
			public int nNumPercentageNextLevelEPs;
			public int nNumNextLevelEPs;

			[MarshalAs(UnmanagedType.I1)]
			private bool bRewardItem;

			private int nRewardValue;
			private ST_REWARD_ITEM_INFO tRewardItemInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CASHITEM_COUNT_INFO_RESULT
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASH_ITEM_COUNT_INFO)]
			public int[] aFuncItemInfoIndex;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASH_ITEM_COUNT_INFO)]
			public ushort[] ItemCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_BUY_ITEM
		{
			public short nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
			public int[] nItemIndex;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
			public PROTO_ITEM_ID_DATA[] aItemID;

			public long nInvenSRL;
			
			[MarshalAs(UnmanagedType.Bool)]
			public bool bItemPeriodContinue;
			
			public uint dwBuyTime;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bRecommendItem;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
			public int[] aInfoFlag;
			
			public int nLeadBuyEventFlag; // -1
			public int nAutoLeagueFlag;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bLastItem;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
			public PROTO_VVIP_UPGRADE_LEVEL[] aVVIPUpgradeLev;

			public short sSoulStoneIndex;
			public int nWeaponCCFlag;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VVIP_WEAPON_CC_PART_NUM)]
			public PROTO_VVIP_WEAPON_CC_COLOR_INFO[] aVVIPWeaponCC;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bGiftFlag1;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bGiftFlag2;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bDummyFlag;
			
			public byte tGachaFlag;
			
			public int nInstalmentCount;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bInstalmentAll;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_BUY_ITEM_RESULT
		{
			public BUYITEM eResult;
			public short nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
			public int[] nItemIndex;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
			public PROTO_ITEM_ID_DATA[] aItemID;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
			public long[] nInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
			public PROTO_DEFAULT_DRESS_DATA[] nDefaultDressInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
			public PROTO_DEFAULT_FUNC_DATA[] nDefaultFuncInvenSrl;

			public int nBonusGP;
			[MarshalAs(UnmanagedType.Bool)]
			public bool bItemPeriodContinue;
			public int nUserCash;
			public int nUserGP;
			public PROTO_MYCLANINFO tClan;
			public uint dwBuyTime;
			//public int nBonusFP;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
			public byte[] aDummy1;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
            public byte[] aDummy2;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BUY_ITEM_COUNT)]
            public byte[] aDummy3;

			public int nGachaBenefitFreeBuyCooldown;
			public int nGachaBenefitDiscountCooldown;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REPAIR_ITEM
		{
			public long nInvenSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REPAIR_ITEM_RESULT
		{
			public REPAIRITEM eResult;
			public long nInvenSrl;
			public short nNewGaugePercent;
			public int nNewGaugeRepairMoney;
			public int nSpendedGamePoint;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REPAIR_ITEM_DATA
		{
			public long nInvenSrl;
			public int nNewGaugeRepairMoney;
			public short nNewGaugePercent;

			//[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
			//public string _pad;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_REPAIR_ALLITEM_RESULT
		{
			public REPAIRALLITEM eResult;
			public int nSpendedGamePoint;
			public short nRepairCnt;

			//[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
			//public string _pad;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_REPAIRALLITEM_COUNT)]
			public PROTO_REPAIR_ITEM_DATA[] tProtoRepairItemData;

			public unsafe int GetSize()
			{
				return 10 + nRepairCnt * sizeof(PROTO_REPAIR_ITEM_DATA);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_RESELL_ITEM
		{
			public long nInvenSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct LevelUpInfo
		{
			public int nSuppliedWeaponInfoIndex;
			public int nNoSuppliedWeaponGP;
			public int nNumPercentageNextLevelEPs;
			public int nNumNextLevelEPs;
			public short nOldLevel;
			public short nNewLevel;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct SuggestFriendReward
		{
			public int nFPointRewardItemIndex;
			public long nFPointRewardItemID;
			public long nFPointRewardInvenSRL;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_RESELL_ITEM_RESULT
		{
			public RESELLITEM eResult;
			public long nInvenSrl;
			public uint nNewGamePoint;
			//public int bAIGacha;
			//public uint dwAddGP;
			//public uint dwNewTotalExp;
			//public LevelUpInfo stLevelUpInfo;
			//public SuggestFriendReward stSFReward;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_RESELL_ITEM_COMPENSATION_REQ
		{
			public long nInvenSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_RESELL_ITEM_COMPENSATION_REQ_RESULT
		{
			public RESELLITEM_COMPENSATION_REQ eResult;
			public int nCurItemGauge;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CASH_RESULT
		{
			public int nUserCash;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_TRY_WEAPON_ITEM
		{
			public int nItemIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_EXPIRED_ITEMS
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
			public long[] nReUseItemSrl;

			public short nSackCount;
			public short nItemCount;
			public short nCharCount;

			//[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			//public short[] nSackSrl;
			public List<short> nSackSrl;

			//[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			//public long[] nItemSrl;
			// <--- this is dynamic
			public List<long> nItemSrl;

			//[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
			//public string szCharCode;
			public List<string> szCharCode;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct EXPIRED_ITEM_LOGOUT
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string szItemCode;

			public byte byDummy06;
			public byte byDummy07;
			public byte byDummy08;
			public byte byDummy09;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string szDisplayItemCode;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_EXPIRED_ITEMS_LOGOUT
		{
			public short nItemCount;
			public short nSackCount;
			public short nCharCount;
			
			public unsafe fixed byte aSackInst[144 * 7];
			public unsafe fixed byte aCharInst[118 * 8];

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 996)]
			public EXPIRED_ITEM_LOGOUT[] aItemCodes;

			public ushort GetSize()
			{
				return (ushort) (6 + 144 * 7 + 118 * 8 + nItemCount * 16);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public struct PROTO_GAUGE_DATA
		{
			public long nInvenWeaponSrl;
			public int nNewGaugeRepairMoney;
			public short nNewGaugePercent;
			public int nCurrentGauge;

			private short _pad;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_MINUS_GAUGE
		{
			public short nWeaponCnt;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			private byte[] _pad;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public PROTO_GAUGE_DATA[] tProtoGaugeData;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_INVEN_REQ
		{
			public int bInvenReset;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_INVEN_ITEM
		{
			public ITEM_TYPE eType;
			public uint dwCount;
			public unsafe fixed byte aInstBuffer[LENGTH_SOCKET_BUFFER];

			public ushort GetSize()
			{
				return (ushort) (sizeof(ITEM_TYPE) + sizeof(uint) + PROTO_INVEN_ITEM_SIZE_ARRAY[(int) eType] * (int) dwCount + 4);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public unsafe struct PROTO_INVEN_SACK
		{
			public int nDummy;
			public uint dwCount;
			public fixed byte aSackInst[LENGTH_SOCKET_BUFFER];

			public ushort GetSize()
			{
				return (ushort) (sizeof(uint) + sizeof(PROTO_SACK_INST) * (int) dwCount + 4);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_UPDATECHARITEM_REQ
		{
			public int nCharItemIndex;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DRESS_PART * MAX_DRESS_LAYER)]
			public int[] aNewDressItemIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_UPDATECHARITEM_RET
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bSuccess;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_UPDATESACK_REQ
		{
			public int nSackIndex;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = (int) WEAPON_CATEGORY.MAX_WEAPON_CATEGORY * MAX_WEAPON_SLOT)]
			public int[] aaNewWeaponItemIndex;

			public int nWaveCard;
			public int dummy;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_UPDATESACK_RET
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bSuccess;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SETMAINCHAR_REQ
		{
			public int nCharItemIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SETMAINCHAR_RET
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bSuccess;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SETMAINSACK_REQ
		{
			public int nSackIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SETMAINSACK_RET
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bSuccess;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GETMAINCHAR_RET
		{
			public int nCharItemIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GETMAINSACK_RET
		{
			public int nSackIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_WEAPONBUYLIST
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WEAPONBUYLIST_SIZE)]
			public long[] aWeaponInvenSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_UPDATEWEAPONBUYLIST_RET
		{
			public byte bSuccess;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HACKSHIELD_GUID_REQ
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
			public string byGuidReqMsg;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HACKSHIELD_GUID_RET
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
			public string byGuidAckMsg;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HACKSHIELD_REQ
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 160)]
			public string byReqMsg;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HACKSHIELD_ACK
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
			public string byAckMsg;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CFV_VER_REQ
		{
			public CF_VERIFIER_VERSION_REQ_MSG tVerReq;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CFV_VER_ACK
		{
			public CF_VERIFIER_VERSION_ACK_MSG tVerAck;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CFV_VALIDATION_REQ
		{
			public CF_VERIFIER_VALIDATION_REQ_MSG tValidationReq;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CFV_VALIDATION_ACK
		{
			public CF_VERIFIER_VALIDATION_ACK_MSG tValidationAck;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HOST_PERIODICAL_RESUME_INFO
		{
			public PROTO_HOST_RESUME_INFO sProtoHostResumeInfo;

			public PROTO_HASHDATA sProtoHashData;

			public HOST_CASHITEM_DATA sHostCashItemData;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_DONT_HACK
		{
			public HACKTYPE eType;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_CONFESS_HACK
		{
			public int iStruct1;
			public int iStruct2;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ANTI_ADDICTION_INFO
		{
			public byte byIsAdult;

			public uint dwGameTime;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ONLINE_HOUR
		{
			public short sOnlineHour;
		}

		public struct PROTO_ANTIBOT_CLOSECLIENT
		{
			public enum PUNISH_MODE
			{
				NORMAL,
				WRITELOG,
				KICKOUT,
				BANPLAYER,
				TROJAN_DETECT,
				CLOSECLIENT,
				CLIENTMSGTIP = 7,
				MODIFY_ROOM_NAME = 10
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_USEFUNCITEM_REQ
		{
			public long nInvenSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_USEFUNCITEM_RET
		{
			public long nInvenSrl;
			public int wLeftCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SETFUNCITEMUSEON_REQ
		{
			public int nFuncItemIndex;
			[MarshalAs(UnmanagedType.I1)]
			public bool bUseOn;
			public int nInfoIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SETFUNCITEMUSEON_RET
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bSuccess;
			public int nInfoIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CASH_GP_UP_EVNET_RET
		{
			public GIVEITEM eResult;

			public int nItemIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string szItemID;

			public long nInvenSRL;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_UCC_SPRAY_ITEM_C_REQ
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_UCC_SPRAY_TEXT)]
			public byte[] szCSprayText1;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_UCC_SPRAY_TEXT)]
			public byte[] szCSprayText2;

			public byte nCTextColorIndex;
			public byte nCBackGroudIndex;
			public long nInvenSRL;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_UCC_SPRAY_ITEM_RET
		{
			public EUCCSPRAY_RET eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_UCC_SPRAY_ITEM_S_REQ
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_UCC_SPRAY_TEXT)]
			public byte[] szSSprayText1;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_UCC_SPRAY_TEXT)]
			public byte[] szSSprayText2;

			public byte nSTextColorIndex;
			public byte nSBackGroudIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_COLOR_CHATTING_ITEM_C_REQ
		{
			public byte nCChattingColorIndex;
			public long nInvenSRL;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_COLOR_CHATTING_ITEM_RET
		{
			public ECHATTING_RET eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_COLOR_CHATTING_ITEM_S_REQ
		{
			public byte nSChattingColorIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_COLOR_CALLNAME_ITEM_C_REQ
		{
			public byte nCCallNameColorIndex;
			public long nInvenSRL;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_COLOR_CALLNAME_ITEM_RET
		{
			public ECALLNAME_RET eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_COLOR_CALLNAME_ITEM_S_REQ
		{
			public byte nSCallNameColorIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SET_CALLNAME_COLOR_CHANGE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szCharacterName;

			public byte nSCallNameColorIndex;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_SYSTEM_INFO
		{
			public int nMaxLuckyGuage;
			public int nMaxCouponCount;
			public int nCouponExtendPrice;
			public int nCouponExtendCount;
			public int nDiscountPercent;
			public int nFreeBuyCooldownDays;
			public int nDiscountCooldownDays;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_MP_GROWTH_REQ
		{
			public long nInvenSRL;
		}

		public enum GACHA_MP_GROWTH_RESULT : byte
		{
			GACHA_MP_GROWTH_SUCCESS,
			GACHA_MP_GROWTH_FAILED
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_MP_GROWTH_RES
		{
			public int nMyMileagePoint;
			public GACHA_MP_GROWTH_RESULT eResult;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_USER_INFO_REQ
		{
			public byte byFlag;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_USER_INFO_RES
		{
			public int nLuckyGauge;
			public int nFreeBuyCooldown;
			public int nDiscountCooldown;
			public short wMaxCouponCount;
			public short wCouponCount;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_STORAGE_ITEM
		{
			public long nStorageSrl;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string szItemCode;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;

			public int nRegTime;
			public int nRemainTime;
			public byte nHotFlag;
			public short sMarkType;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_STORAGE_RESULT
		{
			public short wCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GACHA_STORAGE_LIST_NUM)]
			public PROTO_GACHA_STORAGE_ITEM[] aItems;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_STORAGE_REQUEST
		{
			public byte byFlag;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_COUPON_EXTEND
		{
			public byte byFlag;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_COUPON_EXTEND_RESULT
		{
			public enum RESULT
			{
				SUCCESS,
				MP_NOT_ENOUGH,
				DB_FAIL,
				UNKNOWN_ERROR
			}
			
			public int nNewCount;
			public int nMileagePoint;
			public RESULT eResult;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct GACHA_STORAGE_TRANSFER_ITEM
		{
			public long lStorageSRL;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_STORAGE_TRANSFER_ITEM
		{
			public byte byContainRareItem;
			public short wCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GACHA_TRANSFER_ITEM_COUNT)]
			public GACHA_STORAGE_TRANSFER_ITEM[] aTransferItems;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_STORAGE_TRANSFER_ITEM_RESULT
		{
			public short wCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GACHA_TRANSFER_ITEM_COUNT)]
			public eGACHA_TRANSFER_ITEM_RESULT[] aResult;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GACHA_TRANSFER_ITEM_COUNT)]
			public ST_REWARD_ITEM_INFO[] aTransferItems;

			public short wMaxCount;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct GACHA_STORAGE_DISMANTLE_ITEM
		{
			public long lStorageSRL;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string szItemCode;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_DISMANTLE
		{
			public short wCount;
			
			[MarshalAs(UnmanagedType.I1)]
			public bool bOverlapped;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GACHA_DISMANTLE_COUNT)]
			public GACHA_STORAGE_DISMANTLE_ITEM[] aItems;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_DISMANTLE_RESULT
		{
			public enum RESULT : byte
			{
				SUCCESS,
				SOME_FAIL,
				UNKNOWN_ERROR,
				DB_FAIL
			}
			
			public RESULT eResult;
			public short wCount;
			public short wCouponCount;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_GACHA_DISMANTLE_SRL_STR_LENGTH)]
			public string szSrlStr;
			
			public ST_REWARD_ITEM_INFO tRewardItem;
			public short wStorageCount;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_DISPLAY_LIST_RESULT
		{
			public int nTotalGachaMachineCount;
			public int nCurrGachaMachineCount;
			public int nGachaID;
			public int nGachaType;
			public int nCount;
			public byte bySale;
			public int nLimitFlag;
			public int nLimitTiming;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GACHA_ITEMLIST_NUM * MAX_GACHA_GROUP_NUM)]
			public ST_DISPLAY_DATA[] stDisplayData;

			public ushort GetSize()
			{
				return (ushort) (29 + nCount * Marshal.SizeOf<ST_DISPLAY_DATA>());
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_IDENTIFY
		{
			public long nInvenSrl;
			public int nGachaItemIndex;
			public byte byDummy;
		}

		/*[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_IDENTIFY_RESULT
		{
			public eGACHA_IDENTIFY_RESULT eResult;

			public int nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public int[] aItemIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 44)]
			public string szItemID;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public int[] aCurrentGauge;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public long[] aInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
			public long[] aDefaultDressInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
			public long[] aDefaultFuncInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public int[] aItemCnt;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
			public string szClanNameWithClanMark;

			public int nCashType;

			public int nTotalStorageCount;
		}*/

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_USE_WEAPON_PART
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = (int) eGACHA_WEAPON_PART.MAX_WEAPON_PART)]
			public int[] aItemIndex;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = (int) eGACHA_WEAPON_PART.MAX_WEAPON_PART)]
			public long[] nInvenSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_USE_WEAPON_PART_RESULT
		{
			public eGACHA_USE_WEAPON_PART eResult;
			public int nItemIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;

			public long nInvenSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_COUPON_LIST_RESULT
		{
			public int nListCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_COUPON_LIST_NUM)]
			public PROTO_COUPON_DATA[] stCouponInfo;

			public ushort GetSize()
			{
				return (ushort) (4 + nListCount * Marshal.SizeOf<PROTO_COUPON_DATA>());
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_USE_COUPON
		{
			public long nInvenSrl;
			public int nCouponID;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string szItemID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_USE_COUPON_RESULT
		{
			public eGACHA_USE_COUPON eResult;
			public int nItemIndex;
			public long nInvenSrl;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DRESS_PART)]
			public long[] aDefaultDressInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PERIOD_CHAR_FUNC)]
			public long[] aDefaultFuncInvenSrl;
		}

		public struct ST_PRMOTION_DATA
		{
			public int nGachaGroup;

			public int nItemCount;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string szItemID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_PROMOTION_DISPLAY_LIST_RESULT
		{
			public int nLevel;
			public int SelectCount;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
			public string ItemID;

			public int nTotalGachaMachineCount;
			public int nCurrGachaMachineCount;
			public int nGachaID;
			public int nGachaType;
			public int nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public ST_PRMOTION_DATA[] stDisplayData;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_MEGAPHONE_DENY_TIME
		{
			public int minute;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SET_LOCATION
		{
			public ROOMLOCATION eRoomLocation;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SET_LOCATION_RESULT
		{
			public E_RESULT cResult;
			public int dwRemainSec;
			public ROOMLOCATION eRoomLocation;

			public enum E_RESULT : byte
			{
				SUCCESS,
				ALREADY_LEFT,
				PLAYING
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_USER_LOCATION_NOTI
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string szCharacterName;

			public ROOMLOCATION eRoomLocation;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SET_ROOMNAMECARD
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] szCharacterName;

			public int nNameCardIndex1;
			public int nNameCardIndex2;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ROOM_INVITE
		{
			public uint nnUSN;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ROOM_INVITE_RESULT
		{
			public E_RESULT eResult;

			public struct E_RESULT
			{
				public enum ENUM
				{
					SUCCESS,
					INVALID_LOCATION,
					FULL,
					NO_AUTHORITY
				}
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_MB_ROOM_INVITE
		{
			public uint nnFromUsn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szFromName;

			public short iServer;
			public short iChannel;
			public short iRoom;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
			public string szPW;

			public CLANID stClanID;
			public uint nnToUSN;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_SET_ROOMCOLORCALLNAME
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szCharacterName;

			public byte nColorIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ROOM_RETURN_DURING_PLAYING
		{
			public int nDummy;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ROOM_LEVEL_UP
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szCharacterName;

			public int nLevel;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_WINNERLIST_REQ
		{
			public int nGachaType;
			public int nGachaID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct ST_WINNER_INFO
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szRareItemID;

			public byte byDummy;

			//[MarshalAs(UnmanagedType.ByValTStr, SizeConst = P_SZ.SF_MAX_CHARACTER_NAME_LENGTH)]
			//public string szWinnerNickname;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] szWinnerNickname;

			public int iLevel;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_WINNERLIST_RES
		{
			public int nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GACHA_WINNER_LIST)]
			public ST_WINNER_INFO[] stWinnerInfo;

			public ushort GetSize()
			{
				return (ushort) (4 + nCount * Marshal.SizeOf<ST_WINNER_INFO>());
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_GACHA_WINNER_NTY
		{
			//public int nGachaType;
			//public int nGachaID;

			public ST_WINNER_INFO stWinnerInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_HACK_USER_KICK_TABLE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = KICK_TABLE_MSG_COL_LENGTH)]
			public string sMsg;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_MISSIONCARD_TARGET_LIST_RES
		{
			public int m_nTargetCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public sMissionCardTargetInfo_tag[] m_data;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_MISSIONCARD_REWARD_LIST_RES
		{
			public int m_nRewardCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public sMissionCardRewardInfo_tag[] m_data;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_MISSIONCARD_USERINFO_RES
		{
			public sMissionCard_UserInfo m_data;

			public int m_nMissionCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_SET_MISSIONCARD_USERINFO_REQ
		{
			public sMissionCardSrl m_srl_data;
		}

		public enum eRESULT
		{
			E_RESULT_OVERLAPED = -2,
			E_RESULT_FAILED,
			E_RESULT_SUCCESS
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_SET_MISSIONCARD_USERINFO_RES
		{
			public eRESULT m_result;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
			public string m_szMissionCardItemCode;

			public int m_nNumOfRemainMissionCard;

			public sMissionCardSrl m_srl_data;

			public sMissionCardTargetValue m_value_data;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_MISSION_REWARD_INFO
		{
			public int nRewardType;

			public long nRewardValue;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_DAILYMISSION_DATA
		{
			public int nIndex;

			public int nMissionID;

			public int nMatchType;

			public int nMapIndex;

			public short nMissionType;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_DAILYMISSION_INFO
		{
			public byte bEnable;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string sEndDate;

			public ST_MISSION_REWARD_INFO stRewardInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public ST_DAILYMISSION_DATA[] stMissionData;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_MONTYLYMISSION_INFO
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string sEndDate;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public int[] nAchieveCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public ST_MISSION_REWARD_INFO[] stRewardInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_CARDMISSION_INFO
		{
			public int nCardSetLen;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string sCardSet;

			public ST_MISSION_REWARD_INFO stRewardInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_MISSION_INFO
		{
			public ST_DAILYMISSION_INFO stDailyMissionInfo;

			public ST_MONTYLYMISSION_INFO stMonthlyMissionInfo;

			public ST_CARDMISSION_INFO stCardMissionInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_USER_MISSION_DATA
		{
			public int nIndex;

			public int nAchieveValue;

			public int nCurrValue;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_USER_MISSION_INFO
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string sDate;

			public int nMonthAchieveCount;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string sCartSet;

			public byte cDailyAchieveStatus;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public ST_USER_MISSION_DATA[] stMissionData;
		}

		// 167 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct ST_REWARD_ITEM_INFO
		{
			public E_REWARD_TYPE eRewardType;
			public int nItemIndex;
			public long nInvenSrl;
			public int nItemCnt;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string szItemID;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DRESS_PART)]
			public long[] nDefaultDressInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PERIOD_CHAR_FUNC)]
			public long[] nDefaultFuncInvenSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_MISSION_CARDSET_REWARD_RES
		{
			public E_MISSION_RESULT eResult;

			public ST_REWARD_ITEM_INFO stRewardItemInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_USER_MISSION_RESULT
		{
			public PROTO_USER_MISSION_INFO stUserMissionInfo;

			public E_MISSION_RESULT eDailyResult;

			public ST_REWARD_ITEM_INFO stDailyRewardInfo;

			public int nCardRewardIndex;

			public E_MISSION_RESULT eMonthlyResult;

			public ST_REWARD_ITEM_INFO stMonthlyRewardInfo;

			public PROTO_MISSIONCARD_USERINFO_RES stMissionCardUserInfo;

			public int nMissionCardCardRewardIndex;

			public E_MISSION_RESULT eMissionCardResult;

			public ST_REWARD_ITEM_INFO stMissionCardRewardInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ACHIEVEMENT_LIST_REQ
		{
			public E_ACHIEVE_CATEGORY m_category;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct sObject
		{
			public int m_nIndex;
			public E_ACHIEVE_CHECK_POINT m_section;
			public byte m_cCondition;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			public ST_ACHIEVE_OBJECT.ST_SUB_OBJECT[] m_subObjects;

			public int m_passiveMainCode;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ACHIEVEMENT_LIST_RES
		{
			public E_ACHIEVE_CATEGORY m_category;
			public int m_nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ACHIEVEMENT_LIST)]
			public sObject[] m_objects;

			public ushort GetSize()
			{
				return (ushort) (8 + Marshal.SizeOf<sObject>() * m_nCount);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ACHIEVEMENT_USERDATA_REQ
		{
			public E_ACHIEVE_CATEGORY m_category;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ACHIEVEMENT_USERDATA_RES
		{
			public PROTO_ACHIEVEMENT_USERDATA_REQ m_req;
			public int m_nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ACHIEVEMENT_USERDATA_LIST)]
			public ST_OBJECT[] m_Objects;

			public ushort GetSize()
			{
				return (ushort) (8 + Marshal.SizeOf<ST_OBJECT>() * m_nCount);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ACHIEVEMENT_RESULT
		{
			public int m_nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ACHIEVEMENT_RESULT)]
			public ST_OBJECT[] m_objectInfos;

			public ushort GetSize()
			{
				return (ushort) (4 + Marshal.SizeOf<ST_OBJECT>() * m_nCount);
			}
			
			[StructLayout(LayoutKind.Sequential, Pack = 8)]
			public struct ST_SUBOBJECT
			{
				public bool m_bSuccess;
				public int m_nIndex;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
				public int[] m_values;

				public E_ACHIEVE_STATUS m_status;
				public int m_nRewardCount;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
				public ST_REWARD_ITEM_INFO[] m_rewardItemInfo;
				
				// cf 3.0 added new 11 bytes (itemid?)
			}

			[StructLayout(LayoutKind.Sequential, Pack = 8)]
			public struct ST_OBJECT
			{
				public int m_nIndex;
				public E_ACHIEVE_CATEGORY m_category;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
				public ST_SUBOBJECT[] m_subObjects;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
				public int[] m_nPassiveLev;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
				public long[] m_nCurPassiveLimitValue;
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ACHIEVEMENT_ACHIEVE_ROOM_NTY
		{
			public uint m_usn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string m_szCallName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
			public string m_szClanID;

			public E_NOTIFY m_eNotify;
			public int m_nCount;
			
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public ST_ACHIEVE_DISPLAY[] m_achieveDisplays;

			public enum E_NOTIFY
			{
				E_CN_LOBBY,
				E_CN_WAIT_ROOM,
				E_CN_BUDDY_CLAN,
				E_CN_MAX
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ACHIEVEMENT_SET_ACHIEVE_REQ
		{
			public E_DISPLAY_ACHIEVE_TYPE m_type;
			public int m_nAchieveIndex;
			public int m_nAchieveSubIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ACHIEVEMENT_SET_ACHIEVE_RES
		{
			public byte m_bSuccess;
			public eErrorCode m_errorCode;
			public E_ACHIEVE_CATEGORY m_category;
			public PROTO_ACHIEVEMENT_SET_ACHIEVE_REQ m_req;

			public enum eErrorCode
			{
				EC_SUCCESS,
				EC_ALREADY_SET = -1,
				EC_DONT_HAVE_ACHIEVE = -2,
				EC_NOT_INPROCESS = -3,
				EC_DATABASE_ERROR = -4,
				EC_UNKNOWN_ERROR = -5
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ACHIEVEMENT_SET_ACHIEVE_NTY
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szNickName;

			public E_ACHIEVE_CATEGORY m_eAchieveCategory;
			public int m_nAchieveIndex;
			public int m_nAchieveSubIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ACHIEVEMENT_GET_ACHIEVE_RES
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public ST_ACHIEVE_DISPLAY[] m_dpInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ACHIEVEMENT_PASSIVE_LIST_RES
		{
			public int m_nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
			public ST_ACHIEVE_PASSIVE[] m_passiveList;

			public ushort GetSize()
			{
				return (ushort) (4 + Marshal.SizeOf<ST_ACHIEVE_PASSIVE>() * m_nCount);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_USER_BADGE_INFO
		{
			public PROTO_MAIN_BADGE_INFO stMainBadgeInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = BADGE_KIND_MAX)]
			public int[] nBadgeLevel;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = BADGE_KIND_MAX * MAX_BADGE_CONDITION_COUNT)]
			public int[,] nAchieveValue;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct ST_BADGE_REWARD_RESULT
		{
			public int nRewardItemCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_BADGE_REWARD_COUNT)]
			public ST_REWARD_ITEM_INFO[] stRewardInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_USER_BADGE_RESULT
		{
			public PROTO_USER_BADGE_INFO stUserBadgeInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = BADGE_KIND_MAX)]
			public E_MISSION_RESULT[] eAchieveResult;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = BADGE_KIND_MAX)]
			public ST_BADGE_REWARD_RESULT[] stRewards;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_MAINBADGE_CHANGE_REQ
		{
			public PROTO_MAIN_BADGE_INFO stMainBadgeInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_MAINBADGE_CHANGE_RES
		{
			public E_MISSION_RESULT eResult;

			public PROTO_MAIN_BADGE_INFO stMainBadgeInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_MAINBADGE_CHANGE_NTY
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szNickName;

			public PROTO_MAIN_BADGE_INFO stMainBadgeInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_USER_BADGE_INFO_RES
		{
			public PROTO_USER_BADGE_INFO stUserBadgeInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_BADGE_LEVELUP_NTY
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] aszNickName;

			public int nLevelupCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = BADGE_KIND_MAX)]
			public int[] nBadgeKind;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = BADGE_KIND_MAX)]
			public int[] nBadgeLevel;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_BADGE_REWARD_INFO
		{
			public int nRewardCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public int[] nRewardType;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public long[] nRewardValue;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_BADGE_INFO
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 45)]
			public ST_BADGE_REWARD_INFO[,] stRewardInfo;
		}

		public enum ECONDITION_KIND
		{
			ECONDITION_NONE = -1,
			ECONDITION_EXP,
			ECONDITION_PLAY_CNT,
			ECONDITION_ROUND_CNT,
			ECONDITION_DEATH_CNT,
			ECONDITION_C4_SETUP_CNT,
			ECONDITION_RIFLE_KILL,
			ECONDITION_RIFLE_KILL_2,
			ECONDITION_SHOT_KILL,
			ECONDITION_SHOT_KILL_2,
			ECONDITION_SMG_KILL,
			ECONDITION_SHOTGUN_KILL,
			ECONDITION_HEAVY_KILL,
			ECONDITION_HEAVY_KILL_2,
			ECONDITION_PISTOL_KILL,
			ECONDITION_PISTOL_KILL_2,
			ECONDITION_KNIFE_KILL,
			ECONDITION_DRAW_KILL,
			ECONDITION_H_DEATH,
			ECONDITION_T_PLAY,
			ECONDITION_NA_PLAY,
			ECONDITION_NA_KILL,
			ECONDITION_NA_DEATH,
			ECONDITION_MAX
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_RECOMMENDED_USER_STAT
		{
			public byte bUserMMConnect;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
			public uint[] RecommendItemValue;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_WISHLIST_RES
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WISHLIST_ITEM * MAX_ITEM_CODE)]
			public byte[] aszItemCode;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_WISHLIST_UPDATE_REQ
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WISHLIST_ITEM * MAX_ITEM_CODE)]
			public byte[] aszItemCode;
		}

		public struct PROTO_WISHLIST_UPDATE_RES
		{
			public enum RESULT
			{
				SUCCESS,
				FAIL_INVALID_CODE,
				FAIL_BY_DB,
				FAIL_UNKNOWN_ERROR
			}

			public RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AI_GET_FUNCITEM_NTY
		{
			public long nInvenSrl;
			public int nItemIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string szItemCode;

			public ushort nCnt;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AI_RESULT_NTY
		{
			public byte bDesertion;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
			public int[,] nAIClearCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
			public int[] nAIBossClearCnt;

			public E_MISSION_RESULT eScoreResult;
			public int nScoreRewardCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public ST_REWARD_ITEM_INFO[] stScoreRewardItemInfo;

			public E_MISSION_RESULT eBossResult;
			public int nBossRewardCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public ST_REWARD_ITEM_INFO[] stBossRewardItemInfo;

			public E_MISSION_RESULT eMiddleBossResult;
			public int nMiddleBossRewardCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public ST_REWARD_ITEM_INFO[] stMiddleBossRewardItemInfo;

			public int bScoreStorage;
			public int bBossStorage;
			public int bMiddleBossStorage;
			public int nTotalStorageCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PPROTO_AI_UPDATE_DAILYLIFE
		{
			public int nNumOfDailyLives;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AI_GACHA_PRIZE_RESULT
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string m_szCharacterName;

			public uint m_dwItemNum;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AIGACHA_ITEM_NUM)]
			public uint[] m_arrGachaGroupID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_AI_GACHA_PRIZE_DETAIL_INFO_RESULT
		{
			public uint m_dwTotalItemNum;
			public uint m_dwStorageItemNum;
			public uint m_dwStorageTotalNum;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AIGACHA_ITEM_GAME_NUM)]
			public AIPrizeItemInfo[] m_AIPrizeItem;

			public int GetSize()
			{
				return (int) (12 + m_dwTotalItemNum * Marshal.SizeOf<AIPrizeItemInfo>());
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SHOP_ITEMINFO_NTY
		{
			public int nShopListInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SHOP_ITEMINFO_NTY)]
			public SHOP_LIST[] ShopListInfo;
			
			public unsafe int GetSize()
			{
				return 4 + nShopListInfo * sizeof(SHOP_LIST);
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_SHOP_ITEMINFO_LIST
		{
			public int nShopListInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SHOP_LIST)]
			public SHOP_LIST[] ShopListInfo;

			public unsafe ushort GetSize()
			{
				return (ushort) (4 + nShopListInfo * sizeof(SHOP_LIST));
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ITEM_TAB_INFO_NTY
		{
			public int nItemTabListInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ITEM_TAB_LIST)]
			public ITEM_TAB_LIST[] ItemTabListInfo;

			public int GetSize()
			{
				return 4 + 4 * nItemTabListInfo;
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ITEM_TAB_INFO_RES
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ITEM_TAB_LIST)]
			public ITEM_TAB_LIST[] aItemTabListInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_RECOMPENSE_RES
		{
			public int iPermitLevel;
			public int iItemCnt;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 55)]
			public string szItemID;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_RECOMPENSE_PROMOTION_RES
		{
			public int iNewRecompenseItemCnt;
			public int iNewRecompenseItemPermitLevel;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			public ST_MISSION_REWARD_INFO[] stNewRecompenseItem;

			public int iUnlockRecompenseItemCnt;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			public ST_MISSION_REWARD_INFO[] stUnlockRecompenseItem;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_ITEM_ADD
		{
			public short nCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			public int[] nItemIndex;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 55)]
			public string szItemID;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			public long[] nInvenSrl;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROMOTION_NOTIFY_USER
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string character_name_;

			public int ranking_leve_;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_PRE_PROMOTION_NOTIFY
		{
			public byte count_;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public PROMOTION_NOTIFY_USER[] user_;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_AUTOLEAGUE_INVITE_USER_SELF
		{
			public uint USN;

			public int iKey;

			public uint m_dwLeagueIndex;

			public uint m_dwTeamIndex;

			public int m_bInvite;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct StorageItem
		{
			public int m_nContinueFlag;
			public long m_StorageSRL;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string m_szItemID;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_STORAGE_ITEM_DATE_LEN)]
			public string m_szEndDate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_STORAGE_ITEM_DATE_LEN)]
			public string m_szRegDate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string m_szFromItemCode;
			
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
			public string m_szGetItemCode;
			
			public int m_nSentType;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct StorageItemInven
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
			public string m_szItemID;

			public long m_InvenSRL;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DRESS_PART)]
			public long[] m_arrDefaultDressInvenSrl;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PERIOD_CHAR_FUNC)]
			public long[] m_arrDefaultFuncInvenSrl;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_STORAGE_ITEM_DATE_LEN)]
			public string m_szEndDate;

			public int m_nContinueFlag;
			public long m_StorageSRL;
			
			[MarshalAs(UnmanagedType.Bool)]
			public bool m_bExist;
		}

		public enum STORAGE_RESULT
		{
			STORAGE_SUCCESS,
			STORAGE_DB_ERROR,
			STORAGE_INVENTORY_FULL,
			STORAGE_SRL_ERROR,
			STORAGE_USER_ERROR,
			STORAGE_ITEM_INFO_ERROR,
			STORAGE_ENDDATE_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_STORAGE_LIST_RESULT
		{
			public int m_nResult;
			public uint m_dwCount;
			public int m_nStorageCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_STORAGE_ITEM_NUM)]
			public StorageItem[] m_StorageItems;

			public ushort GetSize()
			{
				return (ushort) (12 + Marshal.SizeOf<StorageItem>() * m_dwCount);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_STORAGE_ITEM_DELETE
		{
			public long m_StorageSRL;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_STORAGE_ITEM_DELETE_RESULT
		{
			public int m_nResult;
			public long m_StorageSRL;
			public int m_nStorageCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_STORAGE_ITEM_MOVEMENT
		{
			public long m_StorageSRL;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_STORAGE_ITEM_MOVEMENT_RESULT
		{
			public int m_nResult;
			public int m_nStorageCount;
			public StorageItemInven m_InvenInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_STORAGE_INVEN_MOVEMENT_RESULT
		{
			public int m_nResult;
			public uint m_dwCount;
			public int m_nStorageCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_STORAGE_MOVEMENT_NUM)]
			public StorageItemInven[] m_InvenInfos;

			public ushort GetSize()
			{
				return (ushort) (Marshal.SizeOf<StorageItemInven>() * m_dwCount + 16);
			}
		}

		public struct PROTO_FPOINT_CHECK_REQ
		{
		}

		public enum MY_SUGGEST_STATUS
		{
			MY_SUGGEST_STATUS_CAN,
			MY_SUGGEST_STATUS_ALREADY,
			MY_SUGGEST_STATUS_OLD_CANT,
			MY_SUGGEST_STATUS_GRADUATE
		}

		public enum SUGGEST_ME_STATUS
		{
			SUGGEST_ME_STATUS_NONE,
			SUGGEST_ME_STATUS_ALREADY
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_FPOINT_CHECK_ACK
		{
			public MY_SUGGEST_STATUS mySuggestStatus;
			public SUGGEST_ME_STATUS suggestMeStatus;
			public int fpoint;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] sleepNickName;

			public int sleepCount;
			public int bSleep;
			public int totalFriend;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_FIND_REQ
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szToNickName;
		}

		public enum FRIEND_FIND_RESULT
		{
			FRIEND_FIND_RESULT_SUCCESS,
			FRIEND_FIND_RESULT_NO_NICK,
			FRIEND_FIND_RESULT_CANT,
			FRIEND_FIND_RESULT_CANT_SELF,
			FRIEND_FIND_RESULT_LONG_REST,
			FRIEND_FIND_RESULT_GRADUATE,
			FRIEND_FIND_RESULT_BLOCK,
			FRIEND_FIND_RESULT_MAX
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_FIND_ACK
		{
			public FRIEND_FIND_RESULT eResult;

			public uint toUsn;

			public int rankLevel;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_NEW_REQ
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string szToNickName;
		}

		public enum FRIEND_REGISTER_RESULT
		{
			FRIEND_REGISTER_RESULT_SUCCESS,
			FRIEND_REGISTER_RESULT_NO_NICK,
			FRIEND_REGISTER_RESULT_CANT,
			FRIEND_REGISTER_RESULT_CANT_SELF,
			FRIEND_REGISTER_RESULT_LONG_REST,
			FRIEND_REGISTER_RESULT_GRADUATE,
			FRIEND_REGISTER_RESULT_BLOCK,
			FRIEND_REGISTER_RESULT_MAX
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_REGISTER_ACK
		{
			public FRIEND_REGISTER_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_LIST_REQ
		{
			public int page;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct FPOINT_PKT
		{
			public uint usn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public string nickName;

			public int rankLevel;
			public short wFameGrade;
			public byte friendKind;
			public int pointTotal;
			public int pointToday;
			public int pointYesterday;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string szLastPlayTime;

			public int bIsLogin;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct FPOINT_DATA
		{
			public int ref_count;
			public uint fromUsn;
			public uint toUsn;
			public int fromRankLevel;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string fromNickName;

			public int toRankLevel;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string toNickName;

			public byte friendKind;
			public byte dayChanged;
			public int pointTerm;
			public int pointTotal;
			public int pointToday;
			public int pointYesterday;
			public int bIsLoginFrom;
			public int bIsLoginTo;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string szFromLastPlayTime;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
			public string szUpdateTime;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			public string szToLastPlayTime;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_FPOINT_LIST_ACK
		{
			public int totalFriend;
			public int currentPage;
			public int totalPage;
			public FPOINT_PKT sendPointInfo;
			public int numRecv;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FPOINT_LIST_PER_PACKET)]
			public FPOINT_PKT[] recvPointInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_DELETE_REQ
		{
			public int numDelete;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public uint[] fromUsn;
		}

		public enum PROTO_FPOINT_DELETE_ACK_RESULT
		{
			PROTO_FPOINT_DELETE_ACK_RESULT_SUCCESS,
			PROTO_FPOINT_DELETE_ACK_RESULT_FAIL
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_DELETE_ACK
		{
			public PROTO_FPOINT_DELETE_ACK_RESULT eResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_POINT_NTY
		{
			public int pointAdd;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_REGISTER_NTY
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string fromNickName;

			public int rewardItemIndex;

			public int rewardItemId;

			public long rewardInvenSRL;

			public int newCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_DELETED_NTY
		{
			public uint fromUsn;

			public uint toUsn;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_GRADUATE_NTY
		{
			public uint fromUsn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string fromNickName;

			public uint toUsn;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
			public string toNickName;

			public int rewardGP;

			public int rewardEP;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_FPOINT_REWARD_REQ
		{
		}

		public enum FRIEND_REWARD_RESULT
		{
			FRIEND_REWARD_RESULT_SUCCESS,
			FRIEND_REWARD_RESULT_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_FPOINT_REWARD_ACK
		{
			public FRIEND_REWARD_RESULT eResult;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FPOINT_LEVELUP_REWARD)]
			public long[] rewardItem_Levels;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FPOINT_LEVELUP_REWARD)]
			public long[] rewardItem_Types;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FPOINT_LEVELUP_REWARD)]
			public long[] rewardItem_Ids;
		}

		public enum UPDATE_KIND
		{
			UPDATE_KIND_GREEN_USER_REWARD,
			UPDATE_KIND_ACHIEVE_REWARD
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_FPOINT_UPDATE_NTY
		{
			public UPDATE_KIND updateKind;

			public int pointAdd;
		}

		public enum ROLL_BACK_TYPE
		{
			ROLL_BACK_SUCCESS,
			ROLL_BACK_CANCEL_OK,
			ROLL_BACK_DB_ERROR,
			ROLL_BACK_TIME_LIMIT_ERROR,
			ROLL_BACK_ABNORMAL_USER
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_ROOM_VVIP_ITEM_CHANGE
		{
			public uint dwVVIPItemCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
			public byte[] szCharacterName;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VVIP_ITEM_NUM)]
			public VVIP_ITEM[] arrVVIPItemIndex;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_ROOM_USER_CHANGE
		{
			public int m_nRoomID;
			public uint m_dwVVIPUserCount;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_KILL_DEATH_ROLL_BACK
		{
			public int m_nRollbackKill;
			public int m_nRollbackDeath;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_KILL_DEATH_ROLL_BACK_REQ
		{
			public int m_bRollback;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PROTO_VVIP_KILL_DEATH_ROLL_BACK_RESULT
		{
			public ROLL_BACK_TYPE m_Result;
			public int m_nCurrentKill;
			public int m_nCurrentDeath;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct REPORT_INFO
		{
			public uint action_code_;
			public uint average_value_;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_CM_CMD_UESI_INSERT
		{
			public uint report_count_;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
			public REPORT_INFO[] report_info_;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_NEWBIEMISSION_DATA
		{
			public int nMissionLevel;
			public int nMatchType;
			public int nMapIndex;
			public int nNewbieMissionType;
			public int nMissionValue;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_NEWBIE_FINAL_REWARD_INFO
		{
			public int m_nRewardIndex;
			public ST_MISSION_REWARD_INFO m_stInfo;
		}

		public enum eNewbieMissionLevel
		{
			NEWBIE_LEVEL_101,
			NEWBIE_LEVEL_102,
			NEWBIE_LEVEL_103,
			NEWBIE_LEVEL_104,
			NEWBIE_LEVEL_105,
			NEWBIE_MISSION_COUNT_LEVEL1,
			NEWBIE_LEVEL_201 = 5,
			NEWBIE_LEVEL_202,
			NEWBIE_LEVEL_203,
			NEWBIE_LEVEL_204,
			NEWBIE_LEVEL_205,
			NEWBIE_MISSION_COUNT_LEVEL2,
			NEWBIE_LEVEL_301 = 10,
			NEWBIE_LEVEL_302,
			NEWBIE_LEVEL_303,
			NEWBIE_LEVEL_304,
			NEWBIE_LEVEL_305,
			NEWBIE_MISSION_COUNT_LEVEL3,
			MAX_NEWBIEMISSION_COUNT = 15
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_NEWBIEMISSION_INFO
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
			public ST_NEWBIEMISSION_DATA[] stNewbieMissionData;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
			public ST_MISSION_REWARD_INFO[,] stRewardInfo;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public ST_NEWBIE_FINAL_REWARD_INFO[] stFinalRewardInfo;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_USER_NEWBIEMISSION_DATA
		{
			public int nMissionLevel;
			public int nCurrValue;
			public int nAchieveValue;
		}

		public enum E_NEWBIE_STATE
		{
			E_NB_S_EXISTED_USER,
			E_NB_S_NOT_COMPLATE_TRAINING_CAMP,
			E_NB_S_IN_PROCESS,
			E_NB_S_END
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_USER_NEWBIEMISSION_INFO
		{
			public E_NEWBIE_STATE eNewbieState;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
			public ST_USER_NEWBIEMISSION_DATA[] stNewbieMissionData;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_NEWBIEMISSION_FINALREWARD_REQ
		{
			public int rewardIndex;
		}

		public enum eResult
		{
			SUCCESS,
			FAIL_INVALID_INDEX,
			FAIL_DONOT_ACHIEVE_ALLMISSION,
			FAIL_DB_ERROR
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_NEWBIEMISSION_FINALREWARD_RES
		{
			public eResult result;
			public ST_REWARD_ITEM_INFO stFinalReward;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct ST_USER_NEWBIE_MISSION_RESULT
		{
			public int nMissionLevel;

			public int nCardRewardIndex;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public E_MISSION_RESULT[] eNewbieMissionResult;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public ST_REWARD_ITEM_INFO[] stMissionCardRewardInfo;
		}

		public enum E_RESULT_CODE
		{
			E_RC_SUCCESS,
			E_RC_ERROR_DB_UPDATE
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_USER_NEWBIE_MISSION_RESULT
		{
			public E_RESULT_CODE eResultCode;

			public int nAchieveCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public ST_USER_NEWBIE_MISSION_RESULT[] stMissionResult;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GLOBALROOM_ENTER_REQUEST
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GLOBALROOM_RECOMMEND_ENTER_REQUEST
		{
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GLOBALROOM_TUTORIAL_ENTER_REQUEST
		{
			public ROUNDTYPE mode_;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GLOBALROOM_ENTER_RESULT
		{
			public RESULT result_;

			public uint enter_key_;

			public short server_id_;

			public short challen_id_;

			public short room_id_;

			public enum RESULT : byte
			{
				ERROR_GLOBALROOM_ENTER_SUCCESS,
				ERROR_GLOBALROOM_ENTER_NOT_CONNECTED,
				ERROR_GLOBALROOM_NOT_FOUND_ROOM
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GLOBALROOM_ENTER_CMD
		{
			public ROUNDTYPE mode_;

			public ushort map_;

			public byte room_state_;

			public byte ai_level_;

			public byte weapon_type_;

			public byte room_mininum_user_;

			public byte global_room_might_selected_;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		public struct PROTO_GLOBALROOM_SAVE_CMD
		{
			public ROUNDTYPE mode_;

			public ushort map_;

			public byte room_status_;

			public byte ai_level_;

			public byte weapon_type_;

			public byte min_user_count_;

			public byte global_room_might_selected_;
		}
	}
}
