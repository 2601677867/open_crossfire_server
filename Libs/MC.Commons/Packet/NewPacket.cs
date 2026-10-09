using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Commons.Packet
{
    public class NewPacket
    {
        private readonly List<byte> data;
        private readonly uint header;

        public NewPacket(uint header)
        {
            data = new List<byte>();
            this.header = header;
        }

        public bool writeCh(byte ch)
        {
            data.Add(ch);
            return true;
        }

        public byte[] HexStr2Bytes(string src)
        {
            //int m = 0, n = 0;
            var l = src.Length / 2;
            var ret = new byte[l];
            for (var i = 0; i < l; i++) ret[i] = Convert.ToByte(src.Substring(i * 2, 2), 16);
            return ret;
        }

        public bool writeRaw(string data)
        {
            var bytes = HexStr2Bytes(data);
            foreach (var c in bytes)
                writeCh(c);
            return true;
        }

        public bool writeHex(byte[] data)
        {
            foreach (var c in data)
                writeCh(c);
            return true;
        }

        public bool writeS8(string data, bool t = false)
        {
            var b = Encoding.UTF8.GetBytes(data);
            foreach (var x in b)
                writeCh(x);
            if (t) writeCh(0x00); // End Char
            return true;
        }

        public bool writeS(string data, bool t = false)
        {
            var b = Encoding.Default.GetBytes(data);
            foreach (var x in b)
                writeCh(x);
            if (t) writeCh(0x00); // End Char
            return true;
        }

        public bool writeSC(string data, bool t = false)
        {
            var b = Encoding.Default.GetBytes(data);
            foreach (var x in b)
                writeCh((byte) (x ^ 0x7F));
            if (t) writeCh(0x00); // End Char
            return true;
        }

        public bool writeS(string data, int len)
        {
            var b = Encoding.Default.GetBytes(data);
            foreach (var x in b)
                writeCh(x);
            for (var i = 0; i < len - b.Length; i++) writeCh(0x00);
            return true;
        }

        public bool writeD(int data)
        {
            writeCh((byte) data);
            writeCh((byte) (data >> 8));
            writeCh((byte) (data >> 16));
            writeCh((byte) (data >> 24));
            return true;
        }

        public bool writeD(uint data)
        {
            writeCh((byte) data);
            writeCh((byte) (data >> 8));
            writeCh((byte) (data >> 16));
            writeCh((byte) (data >> 24));
            return true;
        }

        public bool writeF(int data)
        {
            writeCh((byte) data);
            writeCh((byte) (data >> 8));
            writeCh((byte) (data >> 16));
            return true;
        }

        public bool writeH(int data)
        {
            writeCh((byte) data);
            writeCh((byte) (data >> 8));
            return true;
        }

        public bool writeI(byte data)
        {
            writeCh(data);
            return true;
        }

        public bool writeC(byte data, int c)
        {
            for (var i = 0; i < c; i++)
                writeCh(data);
            return true;
        }

        public byte[] getData()
        {
            return data.ToArray();
        }

        public uint getHeader()
        {
            return header;
        }
    }
}