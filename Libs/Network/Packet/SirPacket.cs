using System;
using System.Runtime.InteropServices;
using Commons.Log;
using static Network.Protocol.BUDDY_PK;
using static Network.Protocol.P_SZ;

namespace Network.Packet
{
    public unsafe class CSirPacket : CPacket
    {
        public CSirPacket()
        {
        }
        
        public CSirPacket(byte[] buff) : base(buff) { }

        public void MakeRoutingPacket<TObject>(byte byFirstClass, short wSecondClass, 
            TObject tObject, int iSize, long lTargetUSN)
        {
            SetFirstClass(byFirstClass);
            SetSecondClass(wSecondClass);
            
            if (iSize == -1)
                CopyToUserDataAreaWithRoutingParam(tObject, lTargetUSN);
            else
                CopyToUserDataAreaWithRoutingParam(tObject, (ushort) iSize, lTargetUSN);
        }
        
        public bool CopyToUserDataAreaWithRoutingParam<TClass>(TClass objData, long lTargetUSN)
        {
            return CopyToUserDataAreaWithRoutingParam(objData, (ushort) Marshal.SizeOf(typeof(TClass)), lTargetUSN);
        }

        public bool CopyToUserDataAreaWithRoutingParam<TClass>(TClass objData, ushort wSize, long lTargetUSN)
        {
            wSize += sizeof(long);
            
            if (wSize > LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE) return false;
            
            ((T_PACKET*) m_pSocketBuffer)->tStartByte = PROTOCOL_START_BYTE;
            *(long*) ((T_PACKET*) m_pSocketBuffer)->UserDataArea = lTargetUSN;
            if (objData == null)
                wSize = 0;
            else
                Marshal.StructureToPtr(objData, (IntPtr) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + sizeof(long)), false);
            ((T_PACKET*) m_pSocketBuffer)->wSize = wSize;
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize = wSize + PROTOCOL_NON_USER_AREA_SIZE;
            
            return true;
        }

        public bool GetRoutingParam(out long nRoutingParam)
        {
            nRoutingParam = 0;
            if (GetReceivedSize() < sizeof(long)) return false;
            nRoutingParam = *(long*) ((T_PACKET*) m_pSocketBuffer)->UserDataArea;
            return true;
        }

        public ushort GetRoutingSize()
        {
            var wSize = ((T_PACKET*) m_pSocketBuffer)->wSize - sizeof(long);
            if (wSize < 0)
            {
                CPublicLogger.GetLogger().error("packet size {0} below zero!", wSize);
                return 0;
            }

            return (ushort) wSize;
        }
        
        public bool CopyFromUserDataAreaWithRoutingParam<TObject>(ref TObject tObject, ushort wSize, out long nRoutingParam)
        {
            nRoutingParam = 0;
            
            if (((T_PACKET*) m_pSocketBuffer)->wSize - sizeof(long) != wSize) return false;
            if (wSize > LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE) return false;
            if (wSize > m_nRecordedSize - PROTOCOL_NON_USER_AREA_SIZE) return false;
            
            nRoutingParam = *(long*) ((T_PACKET*) m_pSocketBuffer)->UserDataArea;
            tObject = (TObject) Marshal.PtrToStructure(
                (IntPtr) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + sizeof(long)), 
                typeof(TObject));
            
            return true;
        }
        
        public bool CopyFromUserDataAreaWithRoutingParam(IntPtr dest, ushort wSize, out long nRoutingParam)
        {
            nRoutingParam = 0;
            
            if (((T_PACKET*) m_pSocketBuffer)->wSize - sizeof(long) != wSize) return false;
            if (wSize > LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE) return false;
            if (wSize > m_nRecordedSize - PROTOCOL_NON_USER_AREA_SIZE) return false;
            
            nRoutingParam = *(long*) ((T_PACKET*) m_pSocketBuffer)->UserDataArea;
            Buffer.MemoryCopy(
                ((T_PACKET*) m_pSocketBuffer)->UserDataArea + sizeof(long), 
                dest.ToPointer(), 
                LENGTH_SOCKET_BUFFER, wSize);

            return true;
        }
    }
}