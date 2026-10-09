using System;
using Commons;

namespace cf_loginsrv.Util
{
    public class CLoginHash : CSingleton<CLoginHash>
    {
        public int GetLoginCryptedDataKey()
        {
            return 18468;
        }

        private byte[] GetKeyByteArr(ulong nKey)
        {
            var lKey = nKey | 0x78D645BC00000000;
            return BitConverter.GetBytes(lKey);
            //Array.Reverse(aKey);
        }
        
        public unsafe void DecryptConnectHashData(ref byte[] aData)
        {
            //var aKey = GetKeyByteArr(*(uint*)aData[0]);
            var aKey = GetKeyByteArr(18468);
            
            var byLast = aData[4];
            aData[4] ^= aKey[0];
            for (var i = 5; i < aData.Length; i ++)
            {
                var byTemp = aData[i];
                aData[i] ^= (byte) (aKey[(i - 4) & 7] ^ byLast);
                byLast = byTemp;
            }
        }
        
        private void EncryptConnectHashData(ref byte[] aData, byte[] aKey)
        {
            aData[4] = (byte) (aData[4] ^ aKey[0]);
            for (var i = 5; i < aData.Length; i++)
            {
                aData[i] = (byte) (aKey[(i - 4) & 7] ^ aData[i - 1] ^ aData[i]);
            }
        }
        
        public unsafe long CalculateConnectHash(long lUSN, uint dwLowClock, uint dwHighClock)
        {
            long lHash = 0;

            var dwUSNHash = (uint) 
                (((short) ( *((short*)&lUSN + 1) ^ *((short*)&lUSN + 2) ^ *((short*)&lUSN + 3) ^ *((short*)&lUSN)) % 30000 << 16) | (short) dwHighClock);

            *((uint*)&lHash) = dwLowClock;
            *((uint*)&lHash + 1) = dwUSNHash;
            return lHash;
        }
    }
}