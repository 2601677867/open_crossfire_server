using System.Collections.Generic;
using System.Runtime.InteropServices;
using cf_loginsrv.Log;
using Commons.Native;
using DBGWMGR;
using Network.Packet;
using static Network.Protocol.P_SZ;
using static DBGWMGR.E_GDBGW_DEFS;

namespace cf_loginsrv.Util
{
    public static class CSharedMethod
    {
        public static int LoadDefaultCharacterData()
        {
            var bSuccess = CServerDataManager.ExecuteGameQuery(QUERY_SELECT_ABUSENAME, out var cParser);
            if (!bSuccess) return -3;
            if (cParser.RowCount == 0) return -3;
            CSharedVariable.iRealAbuseCount = 0;
            CSharedVariable.aszRealAbuseNames = new List<string>();
            
            while (cParser.Peek())
            {
                var szAbuseName = cParser.GetString(1);
                CSharedVariable.aszRealAbuseNames.Add(szAbuseName);
                CServerLog.GetLogger().debug($"RealAbuseCount[{CSharedVariable.iRealAbuseCount}] : {szAbuseName}");
                CSharedVariable.iRealAbuseCount++;
            }

            CServerLog.GetLogger().info($"------------RealAbuseCount tot[{CSharedVariable.iRealAbuseCount}]");
            
            bSuccess = CServerDataManager.ExecuteGameQuery(QUERY_SELECT_LEVELEPS, out cParser);
            if (!bSuccess) return -3;
            if (cParser.RowCount == 0) return -3;

            for (var i = 0; i < MAX_LEVEL; i++)
            {
                //if (!cParser.Peek()) return -4;//need edit
                
                CSharedVariable.aLevelLimits[i] = 1;
            }
            
            return 1;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int IsDBCSLeadByteEx(uint dwCodePage, byte bTestChar);
        
        public static int CheckNickName(byte[] aszNickName)
        {
            var iLen = NativeUtil.strlen(aszNickName);
            if (iLen > SF_MAX_CHARACTER_NAME_LENGTH - 1) return -1;

            var nResult = 0;
            for (var i = 0; i < SF_MAX_CHARACTER_NAME_LENGTH - 1; i++)
            {
                if (aszNickName[i] != ' ') nResult = 1;

            }
            if (nResult == 0) return -2; // CHARACTERNAMECHECK_NULL

            if (NativeUtil.BArrToStr(aszNickName).Contains(" ")) return -3; // CHARACTERNAMECHECK_BLANK

            var bContainDBCSByte = false;

            for (var i = 0; i < iLen; i++)
            {
                if (
                    NativeUtil.IsUnix() || 
                    IsDBCSLeadByteEx(936, aszNickName[i]) != 0
                    )
                {
                    var byPreChar = aszNickName[i];
                    var byPostChar = aszNickName[i + 1];
                    
                    //CServerLog.GetLogger().error("dbcsleadbyte - {0}, {1}", byPreChar, byPostChar);
                    /*if ( byPreChar < 161 || byPreChar > 162 || byPostChar < 161 || byPostChar > 254 )
                    {
                        if ( byPreChar < 163 || byPreChar > 163 || byPostChar < 161 || byPostChar > 254 )
                        {
                            if ( byPreChar < 165 || byPreChar > 169 || byPostChar < 161 || byPostChar > 254 )
                            {
                                if (byPreChar < 170 || byPreChar > 171 || byPostChar < 161 || byPostChar > 254 )
                                {
                                    if ( byPreChar < 172 || byPreChar > 172 || byPostChar < 161 || byPostChar > 254 )
                                    {
                                        if ( byPreChar < 202 || byPreChar > 253 || byPostChar < 161 || byPostChar > 254 )
                                            bContainDBCSByte = true;
                                        else 
                                            nResult = -4;
                                    }
                                    else
                                    {
                                        nResult = -4;
                                    }
                                }
                                else
                                {
                                    nResult = -4;
                                }
                            }
                            else
                            {
                                nResult = -4;
                            }
                        }
                        else
                        {
                            nResult = -4;
                        }
                    }
                    else
                    {
                        nResult = -4;
                    }*/
                    
                    bContainDBCSByte = true;
                    
                    i++;
                }
                else
                {
                    switch ((char)aszNickName[i])
                    {
                        case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7': case '8': case '9':
                    
                        case 'A': case 'B': case 'C': case 'D': case 'E': case 'F': case 'G': case 'H': case 'I': case 'J': 
                        case 'K': case 'L': case 'M': case 'N': case 'O': case 'P': case 'Q': case 'R': case 'S': case 'T':
                        case 'U': case 'V': case 'W': case 'X': case 'Y': case 'Z':
                        
                        case 'a': case 'b': case 'c': case 'd': case 'e': case 'f': case 'g': case 'h': case 'i': case 'j': 
                        case 'k': case 'l': case 'm': case 'n': case 'o': case 'p': case 'q': case 'r': case 's': case 't':
                        case 'u': case 'v': case 'w': case 'x': case 'y': case 'z':
                        
                        case '*': case '-': case '.': case '[': case ']': case '_':
                            break;
                        default:
                            nResult = -4;
                            break;
                    }
                }

                if (nResult < 0) break;
            }

            if (nResult >= 0)
            {
                if (bContainDBCSByte)
                {
                    if (iLen < 4) nResult = -5;
                }
                else if (iLen < 3) nResult = -5;
            }
                
            return nResult;
        }
    }
}