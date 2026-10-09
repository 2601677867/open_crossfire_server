namespace Network.SharedFolder
{
    public enum SERVERHIGHPROPERTY : short
    {
        SERVERHIGHPROPERTY_NONE,
        NORMAL_SERVER,
        CLAN_SERVER,
        PCB_SERVER,
        UNDEFINED, // 匹配服务器
        TOURNAMENT_SERVER, // 预备服务器
        SERVERHIGHPROPERTY_MAX
    }
        
    public enum SERVERLOWPROPERTY : short
    {
        SERVERLOWPROPERTY_NONE,
        KD_LIMIT,
        LEVEL_LIMIT,
        AGE_LIMIT,
        TESTER_LIMIT,
        WAVE_LEVEL_LIMIT,
        SERVERLOWPROPERTY_MAX
    }
}