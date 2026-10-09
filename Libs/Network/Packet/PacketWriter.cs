using System;
using System.Runtime.InteropServices;
using Commons;
using static Network.Protocol.P_SZ;

namespace Network.Packet
{
    public unsafe class CPacketWriter : CPacket
    {
        public CPacketWriter()
        {
            ((T_PACKET*) m_pSocketBuffer)->tStartByte = PROTOCOL_START_BYTE;
            ((T_PACKET*) m_pSocketBuffer)->wSize = 0;
            *((T_PACKET*) m_pSocketBuffer)->UserDataArea = PROTOCOL_END_BYTE;
            m_nRecordedSize = PROTOCOL_NON_USER_AREA_SIZE;
        }

        public CPacketWriter(byte[] buff) : base(buff)
        {
        }

        public CPacketWriter(byte first, byte second, byte third) : this()
        {
            SetFirstClass(first);
            SetSecondClass(second);
            SetThirdClass(third);
        }

        public bool CopyStruct<TClass>(TClass objData)
        {
            return CopyStruct(objData, (ushort) Marshal.SizeOf(typeof(TClass)));
        }

        public bool CopyStruct<TClass>(TClass objData, ushort wSize)
        {
            Marshal.StructureToPtr(objData, (IntPtr) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
                                                      ((T_PACKET*) m_pSocketBuffer)->wSize), false);
            ((T_PACKET*) m_pSocketBuffer)->wSize += wSize;
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += wSize;

            return true;
        }

        public void WriteLong(long dataToWrite)
        {
            *(long*) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
                      ((T_PACKET*) m_pSocketBuffer)->wSize) = dataToWrite;
            ((T_PACKET*) m_pSocketBuffer)->wSize += sizeof(long);
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += sizeof(long);
        }
        
        public void WriteULong(ulong dataToWrite)
        {
            *(ulong*) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
                       ((T_PACKET*) m_pSocketBuffer)->wSize) = dataToWrite;
            ((T_PACKET*) m_pSocketBuffer)->wSize += sizeof(ulong);
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += sizeof(ulong);
        }
        
        public void WriteInt(int dataToWrite)
        {
            *(int*) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
                     ((T_PACKET*) m_pSocketBuffer)->wSize) = dataToWrite;
            ((T_PACKET*) m_pSocketBuffer)->wSize += sizeof(int);
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += sizeof(int);
        }
        
        public void WriteUInt(uint dataToWrite)
        {
            *(uint*) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
                     ((T_PACKET*) m_pSocketBuffer)->wSize) = dataToWrite;
            ((T_PACKET*) m_pSocketBuffer)->wSize += sizeof(uint);
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += sizeof(uint);
        }
        
        public void WriteShort(short dataToWrite)
        {
            *(short*) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
                     ((T_PACKET*) m_pSocketBuffer)->wSize) = dataToWrite;
            ((T_PACKET*) m_pSocketBuffer)->wSize += sizeof(short);
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += sizeof(short);
        }
        
        public void WriteUShort(ushort dataToWrite)
        {
            *(ushort*) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
                       ((T_PACKET*) m_pSocketBuffer)->wSize) = dataToWrite;
            ((T_PACKET*) m_pSocketBuffer)->wSize += sizeof(ushort);
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += sizeof(ushort);
        }

        public void WriteByte(byte dataToWrite)
        {
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = dataToWrite;
            ((T_PACKET*) m_pSocketBuffer)->wSize += sizeof(byte);
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += sizeof(byte);
        }
        
        public void WriteFloat(float dataToWrite)
        {
            *(float*) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
                       ((T_PACKET*) m_pSocketBuffer)->wSize) = dataToWrite;
            ((T_PACKET*) m_pSocketBuffer)->wSize += sizeof(float);
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += sizeof(float);
        }
        
        public void WriteDouble(double dataToWrite)
        {
            *(double*) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
                        ((T_PACKET*) m_pSocketBuffer)->wSize) = dataToWrite;
            ((T_PACKET*) m_pSocketBuffer)->wSize += sizeof(double);
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += sizeof(double);
        }
        
        public void WriteString(string dataToWrite)
        {
            var aData = new byte[dataToWrite.Length + 1];
            Global.Encoding.GetBytes(dataToWrite, 0, dataToWrite.Length, aData, 0);
            WriteByteArray(aData, 0, (ushort) aData.Length);
        }

        public void WriteString(string dataToWrite, ushort len)
        {
            var aData = new byte[len];
            Global.Encoding.GetBytes(
                dataToWrite, 0, 
                dataToWrite.Length >= len ? len - 1 : dataToWrite.Length,
                aData, 0);
            WriteByteArray(aData, 0, (ushort) aData.Length);
        }
        
        public void WriteByteArray(byte[] dataToWrite, int startIndex, int count)
        {
            Marshal.Copy(dataToWrite, startIndex,
                (IntPtr) (((T_PACKET*) m_pSocketBuffer)->UserDataArea + ((T_PACKET*) m_pSocketBuffer)->wSize), count);
            ((T_PACKET*) m_pSocketBuffer)->wSize += (ushort) count;
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += count;
        }

        public void WriteBool(bool b)
        {
            WriteByte(b ? (byte) 1 : (byte) 0);
        }
        
        public void WriteBytes(byte c, ushort count)
        {
            for (var i = 0; i < count; i ++)
                *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
                  ((T_PACKET*) m_pSocketBuffer)->wSize + i) = c;
            ((T_PACKET*) m_pSocketBuffer)->wSize += count;
            *(((T_PACKET*) m_pSocketBuffer)->UserDataArea + 
              ((T_PACKET*) m_pSocketBuffer)->wSize) = PROTOCOL_END_BYTE;
            m_nRecordedSize += count;
        }
        
        public void WriteRaw(string dataToWrite)
        {
            var l = (ushort) (dataToWrite.Length / 2);
            for (var i = 0; i < l; i++) 
                WriteByte(Convert.ToByte(dataToWrite.Substring(i * 2, 2), 16));
        }
        
        public void WriteHex(byte[] dataToWrite)
        {
            foreach (var c in dataToWrite)
                WriteByte(c);
        }

        public void Clear()
        {
            ((T_PACKET*) m_pSocketBuffer)->wSize = 0;
            *((T_PACKET*) m_pSocketBuffer)->UserDataArea = PROTOCOL_END_BYTE;
            m_nRecordedSize = PROTOCOL_NON_USER_AREA_SIZE;
        }
    }
}