using Commons.Log;

namespace iggm_proxy
{
    public class CGameProxy
    {
        public CGameProxy()
        {
            
        }

        public void Init()
        {
            
        }
        
        public void OnUserPositionChange()
        {
            
        }
        
        public void OnUserEnterGame(string sDBName, string sCallName, byte nTeamIndex, byte nSlotIndex,
            short nChannelNumber, short nRoomNumber)
        {
            CPublicLogger.GetLogger().info($"::(对讲机) 玩家 [ {sCallName} ] 进入语音房间 [{nChannelNumber:00}-{nRoomNumber:00}]");
        }

        public void OnUserQuitGame()
        {
            
        }
    }
}