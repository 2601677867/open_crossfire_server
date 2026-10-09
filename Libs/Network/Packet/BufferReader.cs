using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Commons;

namespace Network.Packet
{
    public class CBufferReader
    {
        private readonly byte[] m_gData;
        private readonly int m_nLength;
        private int m_nPos;

        public CBufferReader(byte[] data)
        {
            m_gData = data;
            m_nPos = 0;
            m_nLength = data.Length;
        }
        
        public void ResetPos()
        {
            m_nPos = 0;
        }

        public byte[] ReadBytes(int len)
        {
            var result = new byte[len];
            for (var i = 0; i < len; i++) result[i] = m_gData[m_nPos + i];
            m_nPos += len;

            return result;
        }
        
        public byte ReadByte()
        {
            var result = m_gData[m_nPos];
            m_nPos++;
            return result;
        }
        
        public bool ReadBool()
        {
            var result = m_gData[m_nPos] != 0;
            m_nPos++;
            return result;
        }

        public ushort ReadUShort()
        {
            var result = BitConverter.ToUInt16(m_gData, m_nPos);
            m_nPos += 2;
            return result;
        }

        public short ReadShort()
        {
            var result = BitConverter.ToInt16(m_gData, m_nPos);
            m_nPos += 2;
            return result;
        }

        public uint ReadUInt()
        {
            var result = BitConverter.ToUInt32(m_gData, m_nPos);
            m_nPos += 4;
            return result;
        }

        public int ReadInt()
        {
            var result = BitConverter.ToInt32(m_gData, m_nPos);
            m_nPos += 4;
            return result;
        }
        
        public float ReadFloat()
        {
            var result = BitConverter.ToSingle(m_gData, m_nPos);
            m_nPos += 4;
            return result;
        }
        
        public ulong ReadULong()
        {
            var result = BitConverter.ToUInt64(m_gData, m_nPos);
            m_nPos += 8;
            return result;
        }
        
        public long ReadLong()
        {
            var result = BitConverter.ToInt64(m_gData, m_nPos);
            m_nPos += 8;
            return result;
        }
        
        public double ReadDouble()
        {
            var result = BitConverter.ToDouble(m_gData, m_nPos);
            m_nPos += 8;
            return result;
        }
        
        public unsafe TStruct CopyDataFromUserArea<TStruct>()
        {
            fixed (byte* ptr = &m_gData[m_nPos])
            {
                m_nPos += Marshal.SizeOf(typeof(TStruct));
                return (TStruct)Marshal.PtrToStructure((IntPtr)ptr, typeof(TStruct));
            }
        }

        public void Skip(int len)
        {
            m_nPos += len;
        }

        public string ReadString()
        {
            var count = 0;
            while (m_nPos < m_nLength && m_gData[m_nPos] != 0x00)
            {
                count++;
                m_nPos++;
            }

            m_nPos++;
            var result = Global.Encoding.GetString(m_gData, m_nPos - count - 1, count);
            return result;
        }

        public string ReadStringCrypted()
        {
            var count = 0;
            while (m_gData[m_nPos] != 0x00)
            {
                m_gData[m_nPos] = (byte) (m_gData[m_nPos] ^ 0x7F);
                count++;
                m_nPos++;
            }

            m_nPos++;
            var result = Global.Encoding.GetString(m_gData, m_nPos - count - 1, count);
            return result;
        }

        public string ReadString(int len, bool endChar = false)
        {
            var gBytes = new List<byte>();
            for (var i = m_nPos; i < m_nPos + len; i++)
                if (m_gData[i] != 0x00)
                    gBytes.Add(m_gData[i]);
                else
                    break;
            m_nPos += endChar ? len + 1 : len;
            
            var result = Global.Encoding.GetString(gBytes.ToArray());
            return result;
        }

        public byte[] GetData()
        {
            return m_gData;
        }

        public byte[] GetRData()
        {
            return m_gData.Skip(m_nPos).ToArray();
        }

        public int GetPos()
        {
            return m_nPos;
        }

        public int GetLength()
        {
            return m_nLength;
        }
    }
}