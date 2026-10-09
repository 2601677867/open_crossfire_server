using System;
using System.Linq;
using System.Text;

namespace MC.Commons.Packet
{
    public class Package
    {
        private readonly byte[] data;
        private readonly int m_length;
        private int m_pos;

        public Package(byte[] data, bool removeSize = false)
        {
            this.data = data;
            m_pos = removeSize ? 3 : 0;
            m_length = data.Length;
        }

        public uint readHeader()
        {
            var result = Convert.ToUInt32(readI(3));
            return result;
        }

        public byte readB()
        {
            var result = data[m_pos];
            m_pos++;
            return result;
        }

        public uint readUD()
        {
            var result = BitConverter.ToUInt32(data, m_pos);
            m_pos += 4;
            return result;
        }

        public int readD()
        {
            var result = BitConverter.ToInt32(data, m_pos);
            m_pos += 4;
            return result;
        }

        public ushort readH()
        {
            var result = BitConverter.ToUInt16(data, m_pos);
            m_pos += 2;
            return result;
        }

        public long readI(int len)
        {
            long result = data[m_pos];
            var count = 0;
            for (var i = m_pos + 1; i < m_pos + len; i++)
            {
                count += 8;
                result = result | (data[i] << count);
            }

            m_pos += len;
            return result;
        }

        public void ignore(int len)
        {
            m_pos += len;
        }

        public string readS()
        {
            var count = 0;
            while (data[m_pos] != 0x00)
            {
                count++;
                m_pos++;
            }

            m_pos++;
            var result = Encoding.UTF8.GetString(data, m_pos - count - 1, count);
            return result;
        }

        public string readSC()
        {
            var count = 0;
            while (data[m_pos] != 0x00)
            {
                data[m_pos] = (byte) (data[m_pos] ^ 0x7F);
                count++;
                m_pos++;
            }

            m_pos++;
            var result = Encoding.UTF8.GetString(data, m_pos - count - 1, count);
            return result;
        }

        public string readS(int len, bool endChar = false)
        {
            var result = "";
            for (var i = m_pos; i < m_pos + len; i++)
                if (data[i] != 0x00)
                    result += (char) data[i];
                else
                    break;
            m_pos += endChar ? len + 1 : len;
            return result;
        }

        public byte[] getData()
        {
            return data;
        }

        public byte[] getRData()
        {
            return data.Skip(m_pos).ToArray();
        }

        public int getPos()
        {
            return m_pos;
        }

        public int getLength()
        {
            return m_length;
        }
    }
}