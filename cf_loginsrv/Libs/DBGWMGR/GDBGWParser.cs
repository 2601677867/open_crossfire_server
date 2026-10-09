using System;
using System.Collections.Generic;
using System.IO;
using Commons.Log;
using Network.Packet;

/*
 * Author: Red_K
 */
namespace DBGWMGR
{
    public class CGDBGWParser
    {
        private string[,] m_aData;
        private long m_nResult;
        private uint m_nDataCount;
        private uint m_nCurRowIndex;
        private uint m_nColumnCount;
        private bool m_bPeeked;
            
        public long Result => m_nResult;
        public uint RowCount => m_nDataCount;
        public uint ColumnCount => m_nColumnCount;

        public bool Peek()
        {
            if (m_nCurRowIndex >= m_nDataCount) return false;

            if (m_bPeeked)
            {
                if (m_nCurRowIndex == m_nDataCount - 1) return false;
                m_nCurRowIndex++;
            }
            else
            {
                m_bPeeked = true;
            }
            
            return true;
        }
        
        public bool IsResultError() { return Result < 0; }

        public string GetString(int nColumnIndex) { return m_aData[m_nCurRowIndex, nColumnIndex - 1]; }
        public double GetDouble(int nColumnIndex) { return m_aData[m_nCurRowIndex, nColumnIndex - 1] == "" ? 0 : double.Parse(m_aData[m_nCurRowIndex, nColumnIndex - 1]); }
        public uint GetUInt(int nColumnIndex) { return m_aData[m_nCurRowIndex, nColumnIndex - 1] == "" ? 0 : uint.Parse(m_aData[m_nCurRowIndex, nColumnIndex - 1]); }
        public int GetInt(int nColumnIndex) { return m_aData[m_nCurRowIndex, nColumnIndex - 1] == "" ? 0 : int.Parse(m_aData[m_nCurRowIndex, nColumnIndex - 1]); }
        public ulong GetULong(int nColumnIndex) { return m_aData[m_nCurRowIndex, nColumnIndex - 1] == "" ? 0 : ulong.Parse(m_aData[m_nCurRowIndex, nColumnIndex - 1]); }
        public long GetLong(int nColumnIndex) { return m_aData[m_nCurRowIndex, nColumnIndex - 1] == "" ? 0 : long.Parse(m_aData[m_nCurRowIndex, nColumnIndex - 1]); }
        public short GetShort(int nColumnIndex) { return m_aData[m_nCurRowIndex, nColumnIndex - 1] == "" ? (short)0 : short.Parse(m_aData[m_nCurRowIndex, nColumnIndex - 1]); }
        public ushort GetUShort(int nColumnIndex) { return m_aData[m_nCurRowIndex, nColumnIndex - 1] == "" ? (ushort) 0 : ushort.Parse(m_aData[m_nCurRowIndex, nColumnIndex - 1]); }
        public byte GetByte(int nColumnIndex) { return m_aData[m_nCurRowIndex, nColumnIndex - 1] == "" ? (byte) 0 : byte.Parse(m_aData[m_nCurRowIndex, nColumnIndex - 1]); }

        public bool DBReadData(CBufferReader cBufferReader)
        {
            m_nDataCount = cBufferReader.ReadUShort();
            m_nColumnCount = cBufferReader.ReadUShort();
            m_nResult = cBufferReader.ReadLong();

            m_aData = new string[m_nDataCount, m_nColumnCount];

            /*var szQueryData = pPackageReader.ReadString();
            var aQueryRawData = szQueryData.Split('|');
            for (var nRowIndex = 0; nRowIndex < nDataCount; nRowIndex++)
            {
                try
                {
                    for (var nColumnIndex = 0; nColumnIndex < nColumnCount; nColumnIndex++)
                    {
                        aData[nRowIndex, nColumnIndex] = aQueryRawData[nRowIndex * nColumnIndex + nColumnCount];
                    }
                }
                catch
                {
                    m_pDatabaseUtil.PrintDBError("[CGDBGWParser::dbreaddata] Error String : S|{0}", szQueryData);
                }
            }*/
            
            for (var nRowIndex = 0; nRowIndex < m_nDataCount; nRowIndex++)
            {
                //int nCurrentRow = pPackageReader.ReadUShort();
                for (var nColumnIndex = 0; nColumnIndex < m_nColumnCount; nColumnIndex++)
                {
                    try
                    { 
                        var szData = cBufferReader.ReadString();
//                        if (string.IsNullOrEmpty(sData))
//                        {
//                            m_pDatabaseUtil.PrintDBError($"[CGDBGWParser::dbreaddata] Error String : S|{sData}");
//                        }
                        m_aData[nRowIndex, nColumnIndex] = szData;
                    }
                    catch
                    {
                        GDBGWUtils.PrintDBError(
                            "m_nDataCount ({0}), nColumnIndex ({1})",
                            m_nDataCount, nColumnIndex);
                    }
                }
            }
            
            return true;
        }
    }
}