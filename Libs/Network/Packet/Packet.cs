using System;
using System.Runtime.InteropServices;

using static Network.Protocol.P_SZ;

namespace Network.Packet
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct T_PACKET
    {
        public byte tStartByte;
        public ushort wSize;
        public byte tFirst;
        public byte tSecond;
        public byte tThird;
        public byte tExtCount;
        public byte tCurExt;
        public fixed byte UserDataArea[LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE];
    }
    
    public unsafe class CPacket
    {
        protected int m_nRecordedSize;
        protected readonly IntPtr m_pSocketBuffer;

        public CPacket()
        {
            m_pSocketBuffer = Marshal.AllocHGlobal(LENGTH_SOCKET_BUFFER);
            if (m_pSocketBuffer == null) throw new OutOfMemoryException();
            m_nRecordedSize = 0;
            
            var pSocketBuffer = (byte*) m_pSocketBuffer;
            for (var i = 0; i < LENGTH_SOCKET_BUFFER / 8; i++)
            { 
                *(long*) pSocketBuffer = 0;
                pSocketBuffer += sizeof(long);
            }
        }

        public CPacket(byte[] buff) : this()
        {
            // Wire framing for normal packets is 7 bytes of overhead
            // (F1 + wSize + 3 class bytes + data + [F2]); the two extension bytes
            // exist only in the in-memory T_PACKET layout (data at offset 8).
            // The encrypted login packet ends with a non-F2 trailer byte, so only
            // the start byte and the exact length are checked here.
            if (IsWireFramed(buff))
            {
                var p = (byte*) m_pSocketBuffer;
                for (var i = 0; i < 6; i++) p[i] = buff[i];
                p[6] = 0;
                p[7] = 0;
                var wSize = BitConverter.ToUInt16(buff, 1);
                for (var i = 0; i < wSize; i++) p[8 + i] = buff[6 + i];
                p[8 + wSize] = PROTOCOL_END_BYTE;
                m_nRecordedSize = wSize + PROTOCOL_NON_USER_AREA_SIZE;
            }
            else
            {
                Marshal.Copy(buff, 0, m_pSocketBuffer, buff.Length);
                m_nRecordedSize = buff.Length;
            }
        }

        private static bool IsWireFramed(byte[] buff)
        {
            // F2 at the end must NOT be required: the encrypted login frame ends
            // with a different trailer byte. Server.cs already hands us exactly
            // one complete frame (wSize + 7 bytes), so length is the reliable test.
            if (buff.Length < 7 || buff[0] != PROTOCOL_START_BYTE)
                return false;
            return BitConverter.ToUInt16(buff, 1) == buff.Length - 7;
        }

        ~CPacket()
        {
            Marshal.FreeHGlobal(m_pSocketBuffer);
        }
        
        public void Assemble(byte chFirstClass, byte chSecondClass, byte chThirdClass)
        {
            SetFirstClass(chFirstClass);
            SetSecondClass(chSecondClass);
            SetThirdClass(chThirdClass);
            CopyToUserDataArea(null);
        }

        public void Assemble<TObject>(byte chFirstClass, byte chSecondClass, byte chThirdClass, TObject tObject)
        {
            SetFirstClass(chFirstClass);
            SetSecondClass(chSecondClass);
            SetThirdClass(chThirdClass);
            CopyToUserDataArea(tObject);
        }
        
        public byte[] GetPacketData()
        {
            var wSize = ((T_PACKET*) m_pSocketBuffer)->wSize;

            if (((T_PACKET*) m_pSocketBuffer)->tExtCount == 0)
            {
                // 7-byte wire overhead: F1, wSize(2), c1, c2, c3, data, F2.
                var dest = new byte[wSize + 7];
                var p = (byte*) m_pSocketBuffer;
                for (var i = 0; i < 6; i++) dest[i] = p[i];
                for (var i = 0; i < wSize; i++) dest[6 + i] = p[8 + i];
                dest[dest.Length - 1] = PROTOCOL_END_BYTE;
                return dest;
            }

            var extDest = new byte[wSize + PROTOCOL_NON_USER_AREA_SIZE];
            Marshal.Copy(m_pSocketBuffer, extDest, 0, extDest.Length);
            return extDest;
        }
        
        public int GetRecordedSize()
        {
            return m_nRecordedSize;
        }

        public ushort GetReceivedSize()
        {
            return ((T_PACKET*) m_pSocketBuffer)->wSize;
        }

        public byte GetFirstClass()
        {
            return ((T_PACKET*) m_pSocketBuffer)->tFirst;
        }
        
        public byte GetSecondClass()
        {
            return ((T_PACKET*) m_pSocketBuffer)->tSecond;
        }
        
        public short GetBuddySecondClass()
        {
            return (short) (((T_PACKET*) m_pSocketBuffer)->tSecond << 8 | ((T_PACKET*) m_pSocketBuffer)->tThird);
        }
        
        public byte GetThirdClass()
        {
            return ((T_PACKET*) m_pSocketBuffer)->tThird;
        }
        
        public byte GetExtensionPacketCount()
        {
            return ((T_PACKET*) m_pSocketBuffer)->tExtCount;
        }
        
         public byte GetExtensionPacketCurCount()
         {
             return ((T_PACKET*) m_pSocketBuffer)->tCurExt;
         }
        
        public void SetFirstClass(byte chFirstClass)
        {
            ((T_PACKET*) m_pSocketBuffer)->tFirst = chFirstClass;
        }
        
        public void SetSecondClass(byte chSecondClass)
        { 
            ((T_PACKET*) m_pSocketBuffer)->tSecond = chSecondClass;
        }
        
        public void SetSecondClass(short wSecondClass)
        { 
            ((T_PACKET*) m_pSocketBuffer)->tSecond = (byte) (wSecondClass >> 8);
            ((T_PACKET*) m_pSocketBuffer)->tThird = (byte) (wSecondClass & 0xff);
        }
        
        public void SetThirdClass(byte chThirdClass)
        {
            ((T_PACKET*) m_pSocketBuffer)->tThird = chThirdClass;
        }
        
        public void SetExtensionPacketCount(byte chExtPktCnt)
        {
            ((T_PACKET*) m_pSocketBuffer)->tExtCount = chExtPktCnt;
        }
        
        public void SetExtensionPacketCurCount(byte chExtCurCnt)
        {
            ((T_PACKET*) m_pSocketBuffer)->tCurExt = chExtCurCnt;
        }

        public bool CopyFromUserDataArea<TObject>(ref TObject tObject)
        {
            return CopyFromUserDataArea(ref tObject, (ushort) Marshal.SizeOf(typeof(TObject)));
        }
        
        public bool CopyFromUserDataArea<TObject>(ref TObject tObject, ushort wSize)
        {
            if (((T_PACKET*) m_pSocketBuffer)->wSize != wSize) return false;
            //if (tObject == null) return false;
            if (wSize > LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE) return false;
            if (wSize > m_nRecordedSize - PROTOCOL_NON_USER_AREA_SIZE) return false;
            
            tObject = (TObject) Marshal.PtrToStructure(
                (IntPtr) ((T_PACKET*) m_pSocketBuffer)->UserDataArea, 
                typeof(TObject));
            return true;
        }
        
        public int CopyFromUserDataAreaRanged<TObject>(ref TObject tObject)
        {
            return CopyFromUserDataAreaRanged(ref tObject, (ushort) Marshal.SizeOf(typeof(TObject)));
        }
        
        public int CopyFromUserDataAreaRanged<TObject>(ref TObject tObject, ushort wSize)
        {
            //if (tObject == null) return -1;
            var wRecordedSize = m_nRecordedSize - PROTOCOL_NON_USER_AREA_SIZE;

            var iSize = Marshal.SizeOf(typeof(TObject));
            var pBuffer = Marshal.AllocHGlobal(iSize);
            if (pBuffer == IntPtr.Zero) return -1;

            if (wSize < wRecordedSize)
            {
                Buffer.MemoryCopy(
                    ((T_PACKET*) m_pSocketBuffer)->UserDataArea, 
                    pBuffer.ToPointer(), 
                    iSize, wSize);
                tObject = (TObject) Marshal.PtrToStructure(
                    pBuffer, 
                    typeof(TObject));
                Marshal.FreeHGlobal(pBuffer);
                return wSize;
            }

            Buffer.MemoryCopy(
                ((T_PACKET*) m_pSocketBuffer)->UserDataArea, 
                pBuffer.ToPointer(), 
                iSize, wRecordedSize);
            tObject = (TObject) Marshal.PtrToStructure(
                pBuffer, 
                typeof(TObject));
            Marshal.FreeHGlobal(pBuffer);
            return wRecordedSize;
        }
        
        public bool CopyToUserDataArea<TClass>(TClass objData)
        {
            return CopyToUserDataArea(objData, (ushort) Marshal.SizeOf(typeof(TClass)));
        }

        public bool CopyToUserDataArea<TClass>(TClass objData, ushort wSize)
        {
            if (wSize > LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE) return false;

            ((T_PACKET*) m_pSocketBuffer)->tStartByte = PROTOCOL_START_BYTE;

            if (objData == null)
                wSize = 0;
            else
            {
                var buffer = new byte[Marshal.SizeOf(objData)];
                var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(objData, handle.AddrOfPinnedObject(), false);
                handle.Free();
                Marshal.Copy(buffer, 0, (IntPtr) ((T_PACKET*) m_pSocketBuffer)->UserDataArea, wSize);
            }
            
            ((T_PACKET*) m_pSocketBuffer)->wSize = wSize;
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize = wSize + PROTOCOL_NON_USER_AREA_SIZE;
            
            return true;
        }
        
        public bool CopyToUserDataArea(object objData)
        {
            return CopyToUserDataArea(objData, objData == null ? (ushort) 0 : (ushort) Marshal.SizeOf(objData));
        }
        
        public bool CopyToUserDataArea(object objData, ushort wSize)
        {
            if (wSize > LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE) return false;

            ((T_PACKET*) m_pSocketBuffer)->tStartByte = PROTOCOL_START_BYTE;
            if (objData == null)
                wSize = 0;
            else
            {
                var buffer = new byte[Marshal.SizeOf(objData)];
                var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(objData, handle.AddrOfPinnedObject(), false);
                handle.Free();
                Marshal.Copy(buffer, 0, (IntPtr) ((T_PACKET*) m_pSocketBuffer)->UserDataArea, wSize);
            }

            ((T_PACKET*) m_pSocketBuffer)->wSize = wSize;
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize = wSize + PROTOCOL_NON_USER_AREA_SIZE;

            return true;
        }
        
        public bool CopyToUserDataArea(IntPtr ptr, ushort wSize)
        {
            if (wSize > LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE) return false;

            ((T_PACKET*) m_pSocketBuffer)->tStartByte = PROTOCOL_START_BYTE;
            if (ptr == IntPtr.Zero)
                wSize = 0;
            else
                Buffer.MemoryCopy(ptr.ToPointer(), ((T_PACKET*) m_pSocketBuffer)->UserDataArea, 
                    LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE, wSize);
            ((T_PACKET*) m_pSocketBuffer)->wSize = wSize;
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize = wSize + PROTOCOL_NON_USER_AREA_SIZE;

            return true;
        }
        
        public bool CheckValidPacket()
        {
            return CheckValidPacket(out _);
        }

        public bool CheckValidPacket(out int wSize)
        {
            if (((T_PACKET*) m_pSocketBuffer)->tStartByte != PROTOCOL_START_BYTE)
            {
                wSize = -1;
                return false;
            }
            
            if (m_nRecordedSize < 3)
            {
                wSize = 0;
                return false;
            }

            wSize = ((T_PACKET*) m_pSocketBuffer)->wSize;
            
            if (wSize > LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE)
            {
                wSize = -1;
                return false;
            }
            
            if (m_nRecordedSize < PROTOCOL_NON_USER_AREA_SIZE)
            {
                wSize = 0;
                return false;
            }

            if (wSize + PROTOCOL_NON_USER_AREA_SIZE > m_nRecordedSize)
            {
                wSize = 0;
                return false;
            }
            
            if (*(((T_PACKET*) m_pSocketBuffer)->UserDataArea + wSize) != PROTOCOL_END_BYTE)
            {
                wSize = -1;
                return false;
            }
            
            return true;
        }
    }
}