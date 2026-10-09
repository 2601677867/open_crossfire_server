namespace Network.Packet
{
    public class CClanServerPacket : CSirPacket
    {
        public CClanServerPacket(byte bySecond, byte byThird)
        {
            SetSecondClass(bySecond);
            SetThirdClass(byThird);
        }
    }
}