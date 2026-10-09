using System.Runtime.InteropServices;

namespace Network.Protocol
{
    public static class MMACTION
    {
        public const int PROTOCOL_MM_ACTION = 101;
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MM_ACTION
        {
            public EMMAction eAction;
        }

        public enum EMMAction : short
        {
            ServerList_EnterServer = 1,
            ServerList_FairMatch = 2,
            ServerList_RankMatch = 3,
            ServerList_GlobalJoin = 4,
            ServerList_QuickJoinSetting = 5,
            GachaShop = 6,
            Inventory = 7,
            Shop = 8,
            ButtonMPointMall = 9,
            ButtonRefresh = 10,
            ButtonChannelEnter = 11,
            ButtonIDCard = 12,
            ButtonPrev = 13,
            ButtonNext = 14,
            ButtonMakeRoom = 15,
            ButtonRankMatch = 16,
            ButtonGlobalQuickJoin = 17,
            BT_MISSION_REFRESH = 18,
            BT_MISSION_GO = 19,
            BT_MISSIONPOPUP = 20,
            BtnRoomInfo = 21,
            ButtonOK = 23,
            ButtonIDCard_Channel = 24,
            BtnRecommendMap = 25,
            RecommendJoin = 26,
            ButtonQuickJoinSetting = 27,
            EditChatIn = 28,
            BT_MISSION_REWORD = 29,
            ButtonMissionInfo = 30,
            ButtonRoomlist = 31,
            ButtonEvent = 32,
            ButtonFairMatch = 33,
            ButtonRoomEnter = 34,
            BT_BuddyFriend = 35,
            BT_BuddyClan = 36,
            BT_BuddyAlarm = 37,
            ButtonInClan = 38,
            ButtonAutoMatchTry = 39,
            ButtonOption = 41,
            ButtonExit = 42,
            ButtonBack = 43,
            ButtonReadingGlasses = 44,
            ButtonTutorial = 45,
            ButtonBlackMarket_2 = 50,
            ButtonReferState = 51,
            ButtonWebShop = 52,
            ButtonCFPointCharge = 53,
            PCBang = 54,
            Unknown_55 = 55,
            ButtonWishList = 56,
            ButtonReCFPointView = 57,
            ButtonBlackMarket = 58,
            RepairAll = 59,
            ButtonReplay = 60, // ButtonMedia
            RadioVVIPList = 61,
            ButtonWeeklyWeapon = 62,
            ButtonUpdateMap1 = 63,
            ButtonUpdateMap_64 = 64,
            ButtonUpdateMap_65 = 65,
            ButtonUpdateMap_66 = 66,
            ButtonUpdateMap_67 = 67,
            ButtonUpdateMap_69 = 69,
            ButtonUpdateMap_70 = 70,
            ButtonUpdateMap_71 = 71,
            Unknown_72 = 72,
            Unknown_73 = 73,
            Unknown_74 = 74,
            ButtonWelfareCenter = 75, // Btn_ChWebBanner
            Unknown_1001 = 1001,
            Unknown_1002 = 1002,
            Unknown_1003 = 1003,
            Unknown_1004 = 1004,
            Unknown_1005 = 1005,
            Unknown_1006 = 1006,
            ButtonInGameRanking = 1007,
            RadioButton_IGR_FameHall = 1008,
            RadioButton_IGR_EventRanking = 1009,
            RadioButton_IGR_AbilityRanking = 1010,
            FameHall_1011 = 1011,
            Button_IGR_FramHall_TopRankerView = 1012,
            BtnMyLatestRecord = 1013
        }
    }
}