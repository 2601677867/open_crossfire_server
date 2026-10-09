using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_HASHDATA
    {
        public int iHashData;

        public bool Calculate(byte[] aPacket, string szKey)
        {
            if (aPacket == null || szKey == null) return false;
            var nTempHashData = 0;

            for (var i = 0; i < aPacket.Length; i++)
                nTempHashData += aPacket[i];
            for (var i = 0; i < szKey.Length; i++)
                nTempHashData += szKey[i];

            iHashData = nTempHashData;
            return true;
        }
        
        public bool CheckHashData(byte[] aPacket, string szKey)
        {
            if (aPacket == null || szKey == null) return false;
            var nTempHashData = 0;
            var nHashDataBackup = iHashData;
            iHashData = 0;
            
            for (var i = 0; i < aPacket.Length; i++)
                nTempHashData += aPacket[i];
            for (var i = 0; i < szKey.Length; i++)
                nTempHashData += szKey[i];
            iHashData = nHashDataBackup;

            return nHashDataBackup == nTempHashData;
        }
    }
}