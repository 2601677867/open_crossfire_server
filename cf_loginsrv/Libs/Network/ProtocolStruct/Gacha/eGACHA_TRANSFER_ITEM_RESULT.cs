namespace Network.ProtocolStruct.Gacha
{
    public enum eGACHA_TRANSFER_ITEM_RESULT : byte
    {
        GACHA_TRANSFER_ITEM_SUCCESS,
        GACHA_TRANSFER_ITEM_FAIL,
        GACHA_TRANSFER_ITEM_INVEN_FULL,
        GACHA_TRANSFER_ITEM_DUPLICATE_ITEM,
        GACHA_TRANSFER_ITEM_CANNOT_TRANSFER_AT_ONCE 
        // Permanent character and bag extention item cannot be transferred at once.
    }
}