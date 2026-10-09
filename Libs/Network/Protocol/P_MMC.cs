namespace Network.Protocol
{
    public class P_MMC
    {
        public const int PROTOCOL_MM_CLAN = 100;
		
        public const int PROTOCOL_MM_CLAN_BASE = 1;
        public const int PROTOCOL_MM_CLAN_MANAGE = 2;
        public const int PROTOCOL_MM_CLAN_COMMUNITY = 3;
        public const int PROTOCOL_MM_CLAN_SELF = 4;
        
        public const int MM_CLAN_CONNECT_REQ = 1;
        public const int MM_CLAN_CONNECT_REP = 2;
        public const int MM_CLAN_USER_LOGIN = 3;
        public const int MM_CLAN_USER_LOGOUT = 4;
        public const int MM_CLAN_BASE_CONFIG_INFO_REQ = 5;
        public const int MM_CLAN_BASE_CONFIG_INFO_REP = 6;
        public const int MM_CLAN_MANAGE_GET_MYCLAN_REP = 8;
        public const int MM_CLAN_MANAGE_SHOW_CLAN_REP = 11;
        public const int MM_CLAN_GET_ALL_CLAN_COUNT_REP = 13;
        public const int MM_CLAN_MANAGE_CHECK_NAME_REP = 15;
        public const int MM_CLAN_MANAGE_CHECK_DOMAIN_REP = 17;
        public const int MM_CLAN_MANAGE_CREATE_CLAN_REP = 19;
        public const int MM_CLAN_MANAGE_JOIN_CLAN_REP = 21;
        public const int MM_CLAN_CONTRIBUTE_BY_EXPERIENCE_POINT_REP = 23;
        public const int MM_CLAN_CONTRIBUTE_BY_CF_POINT_REP = 25;
        public const int MM_CLAN_MANAGE_CONFIRM_REP = 28;
        public const int MM_CLAN_MANAGE_CONFIRM_NOTI = 29;
        public const int MM_CLAN_MANAGE_CANCLE_CONFIRM_REP = 31;
        public const int MM_CLAN_CANCLE_CONFIRM_NOTI = 32;
        public const int MM_CLAN_MANAGE_JOIN_CANCLE_CLAN_REP  = 34;
        public const int MM_CLAN_MANAGE_QUIT_CLAN_REP = 36;
        public const int MM_CLAN_MANAGE_BANISH_REP = 38;
        public const int MM_CLAN_MANAGE_BANISH_NOTI = 39;
        public const int MM_CLAN_MANAGE_SEARCH_NAME_REP = 41;
        public const int MM_CLAN_SEARCH_MASTER_NAME_REP = 43;
        public const int MM_CLAN_MANAGE_CHANGE_GRADE_REP = 45;
        public const int MM_CLAN_MANAGE_CHANGE_GRADE_NOTI = 46;
        public const int MM_CLAN_MANAGE_DELETE_CLAN_REP = 48;
        public const int MM_CLAN_MANAGE_SEARCH_MEMBER_NAME_REP = 50;
        public const int MM_CLAN_MANAGE_GET_MYCLAN_MEMBER_REP = 52;
        public const int MM_CLAN_MANAGE_MOVE_MEMBER_REP = 54;
        public const int MM_CLAN_MANAGE_MOVE_MEMBER_NOTI = 55;
        public const int MM_CLAN_MANAGE_CHANGE_LIMIT_LEVEL_REP = 57;
        public const int MM_CLAN_MANAGE_CHANGE_APPROVAL_REP = 59;
        public const int MM_CLAN_MANAGE_CHANGE_STRING_REP = 61;
        public const int MM_CLAN_MANAGE_CHANGE_REGION_REP = 63;
        public const int MM_CLAN_MANAGE_GET_WAIT_MEMBER_REP = 65;
        public const int MM_CLAN_MANAGE_CANCEL_DELETE_MEMBER_REP = 67;
        public const int MM_CLAN_MANAGE_CHECK_MARK_REP = 69;
        public const int MM_CLAN_MANAGE_CHANGE_MARK_REP = 71;
        public const int MM_CLAN_MANAGE_CHANGE_CLAN_NAME_REP = 73;
        public const int MM_CLAN_MANAGE_REQUEST_TRANSFER_MASTER_REP = 75;
        public const int MM_CLAN_MANAGE_CANCLE_TRANSFER_MASTER_REP = 77;
        public const int MM_CLAN_MANAGE_CONFIRM_NEXT_MASTER_REP = 79;
        public const int MM_CLAN_MANAGE_CONFIRM_NEXT_MASTER_NOTIFY = 80;
        public const int MM_CLAN_MANAGE_GET_DELETE_MEMBER_REP = 82;
        public const int MM_CLAN_MANAGE_SHOW_POP_UP_REP = 84;
        public const int MM_CLAN_MANAGE_GET_TOTAL_UNIT_INFO_REP = 86;
        public const int MM_CLAN_MANAGE_GET_STAFF_INFO_REP = 88;
        public const int MM_CLAN_MANAGE_GET_CLAN_LOG_REP = 90;
        public const int MM_CLAN_MANAGE_INCREASE_PERSON_REP = 92;
        public const int MM_CLAN_MANAGE_IS_ABLE_TO_CONFIRM_REP = 94;
        public const int MM_CLAN_MANAGE_DIFF_7DAYS_REP = 96;
        public const int MM_CLAN_MANAGE_GET_RECORD_COUNT_REP = 98;
        public const int MM_CLAN_MANAGE_GET_COMMANDER_COUNT_REP = 100;
        public const int MM_CLAN_MANAGE_GET_STAFF_COUNT_REP = 102;
        public const int MM_CLAN_MANAGE_GET_CLAN_BASE_INFO_REP = 104;
        public const int MM_CLAN_MANAGE_PW_INFO_REP = 107;
        public const int MM_CLAN_MANAGE_PW_CHECK_REP = 109;
        public const int MM_CLAN_MANAGE_PW_TIME_CHECK_REP = 111;
        public const int MM_CLAN_MANAGE_NEW_PW_REP = 113;
        public const int MM_CLAN_MANAGE_PW_CHANGE_REP = 115;
        public const int MM_CLAN_MANAGE_PW_Q_AND_A_CHANGE_REP = 117;
        public const int MM_CLAN_MANAGE_PW_GET_BACK_REP = 119;
        public const int MM_CLAN_MANAGE_CLAN_MARK_LIST_REP = 127;
        public const int MM_CLAN_MANAGE_SEARCH_CLANKEY_REP = 129;
        public const int MM_CLAN_MANAGE_SEARCH_MEMBER_PART_NAME_REP = 132;
        public const int MM_CLAN_COMMUNITY_CHATTING_SEND_REP = 135;
        public const int MM_CLAN_COMMUNITY_CHATTING_RECV_CMD = 136;
        public const int MM_CLAN_SELF_CONTRIBUTE_BY_EXP_REQ = 138;
        public const int MM_CLAN_SELF_CONTRIBUTE_BY_CASH_REQ = 139;
    }
}