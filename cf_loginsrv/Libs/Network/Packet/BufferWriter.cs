using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Commons;
using Commons.Log;
using Commons.Native;

namespace Network.Packet
{
    public class CBufferWriter
    {
        public const int MAX_PACKET_SIZE = 17408;
        public const int HEADER_SIZE = 5;
        public const int MIN_PACKET_SIZE = HEADER_SIZE + 4;
        
        public const int USERDATA_START_POS = HEADER_SIZE + 3;
        public const int MAX_USERDATA_SIZE = MAX_PACKET_SIZE - HEADER_SIZE - 4;

        private byte[] m_aRecvedData;
        private readonly byte[] m_aPacketData;
        private int m_nOffset;
        private int m_nCopyPos;

        public CBufferWriter()
        {
            m_aPacketData = new byte[MAX_PACKET_SIZE];
            m_aPacketData[0] = 0xF1;
            m_aPacketData[MIN_PACKET_SIZE - 1] = 0xF2;
            m_nOffset = HEADER_SIZE + 3;
            m_nCopyPos = HEADER_SIZE + 3;
        }

        public CBufferWriter(byte chFirstClass, byte chSecondClass, byte chThirdClass,
            byte chExtPktCnt = 0, byte chExtCurCnt = 0) : this()
        {
            SetFirstClass(chFirstClass);
            SetSecondClass(chSecondClass);
            SetThirdClass(chThirdClass);
            SetExtensionPacketCount(chExtPktCnt);
            SetExtensionPacketCurCount(chExtCurCnt);
        }
        
        public CBufferWriter(byte[] buff) : this(buff[3], buff[4], buff[5], buff[6], buff[7])
        {
            m_aRecvedData = buff;
            //Array.Copy(buff, m_aPacketData, buff.Length);
        }
        
        public byte[] GetPacketData()
        {
            return m_aPacketData;
        }
        
        public byte[] GetReceivedData()
        {
            return m_aRecvedData;
        }

        public short GetRecordedSize()
        {
            return (short) (m_aPacketData[1] | m_aPacketData[2] << 8);
        }
        
        public short GetReceivedSize()
        {
            return (short) (m_aRecvedData[1] | m_aRecvedData[2] << 8);
        }
        
        private void SetRecordedSize(int nSize)
        {
            m_aPacketData[1] = (byte) nSize;
            m_aPacketData[2] = (byte) (nSize >> 8);
        }
        
        private void AddRecordedSize(int nAddSize)
        {
            SetRecordedSize(GetRecordedSize() + nAddSize);
        }

        public byte GetFirstClass()
        {
            return m_aPacketData[3];
        }
        
        public byte GetSecondClass()
        {
            return m_aPacketData[4];
        }
        
        public short GetBuddySecondClass()
        {
            return (short) (GetSecondClass() << 8 | GetThirdClass());
        }
        
        public byte GetThirdClass()
        {
            return m_aPacketData[5];
        }
        
        public byte GetExtensionPacketCount()
        {
            return m_aPacketData[6];
        }
        
         public byte GetExtensionPacketCurCount()
        {
            return m_aPacketData[7];
        }
        
        public void SetFirstClass(byte chFirstClass)
        {
            m_aPacketData[3] = chFirstClass;
        }
        
        public void SetSecondClass(byte chSecondClass)
        { 
            m_aPacketData[4] = chSecondClass;
        }
        
        public void SetSecondClass(short wSecondClass)
        { 
            m_aPacketData[4] = (byte) (wSecondClass >> 8);
            m_aPacketData[5] = (byte) (wSecondClass & 0xff);
        }
        
        public void SetThirdClass(byte chThirdClass)
        {
            m_aPacketData[5] = chThirdClass;
        }
        
        public void SetExtensionPacketCount(byte chExtPktCnt)
        {
            m_aPacketData[6] = chExtPktCnt;
        }
        
        public void SetExtensionPacketCurCount(byte chExtCurCnt)
        {
            m_aPacketData[7] = chExtCurCnt;
        }
        
        public int GetRemainingReceivedLength()
        {
            return GetReceivedSize() - (m_nCopyPos - HEADER_SIZE - 3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InternalWriteByte(byte dataToWrite)
        {
            m_aPacketData[m_nOffset++] = dataToWrite;
        }
        
        public void WriteByte(byte dataToWrite)
        {
            m_aPacketData[m_nOffset++] = dataToWrite;
            AddRecordedSize(sizeof(byte));
            m_aPacketData[m_nOffset] = 0xF2;
        }

        public bool CheckValidPacket(out int nRecordedSize)
        {
            nRecordedSize = GetRecordedSize();
            return m_aPacketData[0] == 0xF1 && 
                   nRecordedSize <= MAX_USERDATA_SIZE &&
                   m_aPacketData[nRecordedSize + HEADER_SIZE + 3] == 0xF2;
        }
        
        public bool CheckValidPacket()
        {
            var nRecordedSize = GetRecordedSize();
            return m_aPacketData[0] == 0xF1 && 
                   nRecordedSize <= MAX_USERDATA_SIZE &&
                   m_aPacketData[nRecordedSize + HEADER_SIZE + 3] == 0xF2;
        }

        public void Clear()
        {
            SetRecordedSize(0);
            m_aPacketData[0] = 0xF1;
            m_aPacketData[MIN_PACKET_SIZE - 1] = 0xF2;
            m_nOffset = HEADER_SIZE + 3;
        }
        
        public void WriteLong(long dataToWrite)
        {
            InternalWriteByte((byte) dataToWrite);
            InternalWriteByte((byte) (dataToWrite >> 8));
            InternalWriteByte((byte) (dataToWrite >> 16));
            InternalWriteByte((byte) (dataToWrite >> 24));
            InternalWriteByte((byte) (dataToWrite >> 32));
            InternalWriteByte((byte) (dataToWrite >> 40));
            InternalWriteByte((byte) (dataToWrite >> 48));
            InternalWriteByte((byte) (dataToWrite >> 56));
            
            AddRecordedSize(sizeof(long));
            m_aPacketData[m_nOffset] = 0xF2;
        }

        public void WriteULong(ulong dataToWrite)
        {
            InternalWriteByte((byte) dataToWrite);
            InternalWriteByte((byte) (dataToWrite >> 8));
            InternalWriteByte((byte) (dataToWrite >> 16));
            InternalWriteByte((byte) (dataToWrite >> 24));
            InternalWriteByte((byte) (dataToWrite >> 32));
            InternalWriteByte((byte) (dataToWrite >> 40));
            InternalWriteByte((byte) (dataToWrite >> 48));
            InternalWriteByte((byte) (dataToWrite >> 56));
            
            AddRecordedSize(sizeof(ulong));
            m_aPacketData[m_nOffset] = 0xF2;
        }

        public void WriteInt(int dataToWrite)
        {
            InternalWriteByte((byte) dataToWrite);
            InternalWriteByte((byte) (dataToWrite >> 8));
            InternalWriteByte((byte) (dataToWrite >> 16));
            InternalWriteByte((byte) (dataToWrite >> 24));
            
            AddRecordedSize(sizeof(int));
            m_aPacketData[m_nOffset] = 0xF2;
        }

        public void WriteUInt(uint dataToWrite)
        {
            InternalWriteByte((byte) dataToWrite);
            InternalWriteByte((byte) (dataToWrite >> 8));
            InternalWriteByte((byte) (dataToWrite >> 16));
            InternalWriteByte((byte) (dataToWrite >> 24));
            
            AddRecordedSize(sizeof(uint));
            m_aPacketData[m_nOffset] = 0xF2;
        }

        public void WriteShort(short dataToWrite)
        {
            InternalWriteByte((byte) dataToWrite);
            InternalWriteByte((byte) (dataToWrite >> 8));
            
            AddRecordedSize(sizeof(short));
            m_aPacketData[m_nOffset] = 0xF2;
        }

        public void WriteUShort(ushort dataToWrite)
        {
            InternalWriteByte((byte) dataToWrite);
            InternalWriteByte((byte) (dataToWrite >> 8));
            
            AddRecordedSize(sizeof(ushort));
            m_aPacketData[m_nOffset] = 0xF2;
        }
        
        public void WriteFloat(float dataToWrite)
        {
            unsafe
            {
                var pData = (byte*) &dataToWrite;
                
                for (var i = 0; i < sizeof(float); ++i)
                {
                    InternalWriteByte(*pData++);
                }
            }
            
            AddRecordedSize(sizeof(float));
            m_aPacketData[m_nOffset] = 0xF2;
        }
        
        public void WriteDouble(double dataToWrite)
        {
            unsafe
            {
                var pData = (byte*) &dataToWrite;
                
                for (var i = 0; i < sizeof(double); ++i)
                {
                    InternalWriteByte(*pData++);
                }
            }
            
            AddRecordedSize(sizeof(double));
            m_aPacketData[m_nOffset] = 0xF2;
        }
        
        public void WriteBool(bool dataToWrite)
        {
            InternalWriteByte(dataToWrite ? (byte) 1 : (byte) 0);
            
            AddRecordedSize(sizeof(byte));
            m_aPacketData[m_nOffset] = 0xF2;
        }

        public void WriteBytes(byte dataToWrite, int c)
        {
            for (var i = 0; i < c; i++)
                InternalWriteByte(dataToWrite);
            
            AddRecordedSize(c);
            m_aPacketData[m_nOffset] = 0xF2;
        }
        
        public void WriteString(string szString)
        {
            var nActualLen = Global.Encoding.GetBytes(
                szString, 0, szString.Length, m_aPacketData, m_nOffset);
            m_aPacketData[m_nOffset + nActualLen] = 0;
            m_nOffset += nActualLen + 1;
            AddRecordedSize(nActualLen + 1);
            m_aPacketData[m_nOffset] = 0xF2;
        }

        public void WriteString(string szString, int len)
        {
            var nActualLen = Global.Encoding.GetBytes(
                szString, 0, szString.Length >= len ? len - 1 : szString.Length, m_aPacketData, m_nOffset);
            m_aPacketData[m_nOffset + nActualLen] = 0;
            m_nOffset += len;
            AddRecordedSize(len);
            m_aPacketData[m_nOffset] = 0xF2;
        }

        public void WriteRaw(string dataToWrite)
        {
            var l = dataToWrite.Length / 2;
            for (var i = 0; i < l; i++) 
                InternalWriteByte(Convert.ToByte(dataToWrite.Substring(i * 2, 2), 16));
            
            AddRecordedSize(l);
            m_aPacketData[m_nOffset] = 0xF2;
        }

        public void WriteHex(byte[] dataToWrite)
        {
            foreach (var c in dataToWrite)
                InternalWriteByte(c);
            
            AddRecordedSize(dataToWrite.Length);
            m_aPacketData[m_nOffset] = 0xF2;
        }
        
        public void WriteByteArray(byte[] dataToWrite, int nStartIndex, int nCount)
        {
            for (var i = 0; i < nCount; i++)
                InternalWriteByte(dataToWrite[nStartIndex + i]);
            
            AddRecordedSize(nCount);
            m_aPacketData[m_nOffset] = 0xF2;
        }
    }
}