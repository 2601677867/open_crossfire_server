using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using cf_loginsrv.Config;
using cf_loginsrv.Log;
using Commons.Log;
using Network.Packet;
using Network.ProtocolStruct.Login;
using Network.Server;
using Network.SharedFolder;

using static Network.Protocol.P_SZ;

namespace cf_loginsrv.Packet
{
    public class CBaseNetworkHandler
    {
        #region Shared Methods
        
        protected void GetServerUserCountLimit(short sServerNo, out int iConnectCount, out int iLimitCount)
        {
            iConnectCount = 0;
            iLimitCount = 0;
            if (sServerNo < 0 || sServerNo >= MAX_GAMESERVER_COUNT) return;
            
            lock (CMainServer.m_aGameServers)
            {
                if (CMainServer.m_aGameServers[sServerNo].m_nServerID != -1)
                {
                    if (CMainServer.m_aGameServers[sServerNo].m_nServerConnectCount >= 0)
                    {
                        if (CMainServer.m_aGameServers[sServerNo].m_bGameServerConnected)
                            iConnectCount = CMainServer.m_aGameServers[sServerNo].m_nServerConnectCount;
                        else
                            iConnectCount = -1;
                    }
                    else if (CMainServer.m_aGameServers[sServerNo].m_bGameServerConnected)
                    {
                        iConnectCount = 0;
                    }
                    else
                    {
                        iConnectCount = -1;
                    }

                    iLimitCount = CMainServer.m_aGameServers[sServerNo].m_nServerLimitCount;
                }
            }
        }
        
        protected short CopyUpdateServerList(ref PROTO_GAMESERVER_UPDATE[] tServers)
        {
            short iServerCount = 0;
            
            lock (CMainServer.m_aGameServers)
            {
                for (var i = 0; i < MAX_GAMESERVER_COUNT; i++)
                {
                    if (CMainServer.m_aGameServers[i].m_nServerID != -1)
                    {
                        tServers[iServerCount].m_nServerID = CMainServer.m_aGameServers[i].m_nServerID;
                        
                        if (tServers[iServerCount].m_nServerID < 0 
                            || tServers[iServerCount].m_nServerID > MAX_GAMESERVER_COUNT)
                            tServers[iServerCount].m_nEvent = 0;
                        else if (CMainServer.m_aGameServers[i].m_bEvent)
                            tServers[iServerCount].m_nEvent = 1;
                        else
                            tServers[iServerCount].m_nEvent = 0;

                        if (CMainServer.m_aGameServers[i].m_nServerConnectCount >= 0)
                            if (CMainServer.m_aGameServers[i].m_bGameServerConnected)
                                tServers[iServerCount].m_nServerConnectCount = CalculateServerConnectPercentage(
                                    CMainServer.m_aGameServers[i].m_nServerConnectCount,
                                    CMainServer.m_aGameServers[i].m_nServerLimitCount);
                            else
                                tServers[iServerCount].m_nServerConnectCount = -1;
                        else if (CMainServer.m_aGameServers[i].m_bGameServerConnected)
                            tServers[iServerCount].m_nServerConnectCount = 0;
                        else
                            tServers[iServerCount].m_nServerConnectCount = -1;

                        iServerCount++;
                    }
                }
            }
            
            return iServerCount;
        }

        private int CalculateServerConnectPercentage(int nConnectCount, int nLimitCount)
        {
            if (nConnectCount == 0) return 0;
            if (nConnectCount == nLimitCount) return 100;

            var nPercentage = nConnectCount / (nLimitCount / 100);
            if (nConnectCount % (nLimitCount / 100) != 0) nPercentage++;

            if (nPercentage >= 1 && nPercentage <= 3) return 3;
            if (nPercentage >= 4 && nPercentage <= 6) return 6;
            if (nPercentage >= 7 && nPercentage <= 10) return 10;
            if (nPercentage >= 11 && nPercentage <= 20) return 20;
            if (nPercentage >= 21 && nPercentage <= 30) return 30;
            if (nPercentage >= 31 && nPercentage <= 40) return 40;
            if (nPercentage >= 41 && nPercentage <= 50) return 50;
            if (nPercentage >= 51 && nPercentage <= 60) return 60;
            if (nPercentage >= 61 && nPercentage <= 70) return 70;
            if (nPercentage >= 71 && nPercentage <= 80) return 80;
            if (nPercentage >= 81 && nPercentage <= 90) return 90;
            if (nPercentage >= 91 && nPercentage <= 93) return 93;
            if (nPercentage >= 94 && nPercentage <= 96) return 96;
            
            return 99;
        }

        protected short CopyServerList(ref PROTO_GAMESERVER[] tServers, out byte byAutoSelectID)
        {
            short iCurrentId = 0;
            float fLowestPercentage = 100;
            byAutoSelectID = 0;

            Console.WriteLine($"[DIAG] CopyServerList: m_aGameServers null={CMainServer.m_aGameServers == null}, len={CMainServer.m_aGameServers?.Length}");
            if (CMainServer.m_aGameServers != null)
            {
                for (var dbg = 0; dbg < System.Math.Min(5, CMainServer.m_aGameServers.Length); dbg++)
                    Console.WriteLine($"[DIAG] m_aGameServers[{dbg}]: ServerID={CMainServer.m_aGameServers[dbg].m_nServerID}, Connected={CMainServer.m_aGameServers[dbg].m_bGameServerConnected}, Port={CMainServer.m_aGameServers[dbg].m_nServerPort}");
            }

            lock (CMainServer.m_aGameServers)
            {
                for (var i = 0; i < MAX_GAMESERVER_COUNT; i++)
                {
                    if (CMainServer.m_aGameServers[i].m_nServerID != -1)
                    {
                        tServers[iCurrentId].m_nServerID = CMainServer.m_aGameServers[i].m_nServerID;
                        tServers[iCurrentId].m_nServerHighProperty = CMainServer.m_aGameServers[i].m_nServerHighProperty;
                        tServers[iCurrentId].m_nServerLowProperty = CMainServer.m_aGameServers[i].m_nServerLowProperty;
                        tServers[iCurrentId].m_nServerHighLimit = CMainServer.m_aGameServers[i].m_nServerHighLimit;
                        tServers[iCurrentId].m_nServerLowLimit = CMainServer.m_aGameServers[i].m_nServerLowLimit;
                        tServers[iCurrentId].m_dServerHighKD = CMainServer.m_aGameServers[i].m_dServerHighKD;
                        tServers[iCurrentId].m_dServerLowKD = CMainServer.m_aGameServers[i].m_dServerLowKD;
                        tServers[iCurrentId].m_nEvent = CMainServer.m_aGameServers[i].m_bEvent ? 1 : 0;

                        // The real server sends these literals on every entry
                        // (verified against the captured login-result packet).
                        tServers[iCurrentId].m_nDummy12008 = 12008;
                        tServers[iCurrentId].m_nDummy28004 = 28004;

                        if (CMainServer.m_aGameServers[i].m_nServerConnectCount >= 0)
                            if (CMainServer.m_aGameServers[i].m_bGameServerConnected)
                            {
                                tServers[iCurrentId].m_nServerConnectCount = CalculateServerConnectPercentage(
                                    CMainServer.m_aGameServers[i].m_nServerConnectCount,
                                    CMainServer.m_aGameServers[i].m_nServerLimitCount);
                                tServers[iCurrentId].m_nServerLimitCount = 100;
                            }
                            else
                                tServers[iCurrentId].m_nServerConnectCount = -1;
                        else if (CMainServer.m_aGameServers[i].m_bGameServerConnected)
                            tServers[iCurrentId].m_nServerConnectCount = 0;
                        else
                            tServers[iCurrentId].m_nServerConnectCount = -1;

                        if (tServers[iCurrentId].m_nServerHighProperty == (int)SERVERHIGHPROPERTY.NORMAL_SERVER
                        && tServers[iCurrentId].m_nServerHighLimit == 100 && tServers[iCurrentId].m_nServerLowLimit == 0
                        && tServers[iCurrentId].m_dServerHighKD == 0.0 && tServers[iCurrentId].m_dServerLowKD == 0.0)
                        {
                            if (tServers[iCurrentId].m_nServerConnectCount != -1
                                && (float) tServers[iCurrentId].m_nServerConnectCount / tServers[iCurrentId].m_nServerLimitCount < fLowestPercentage)
                            {
                                fLowestPercentage = 
                                    (float) tServers[iCurrentId].m_nServerConnectCount / tServers[iCurrentId].m_nServerLimitCount;
                                byAutoSelectID = (byte) tServers[iCurrentId].m_nServerID;
                            }
                        }
                        
                        tServers[iCurrentId].m_dwServerAddr = CMainServer.m_aGameServers[i].m_dwServerAddr;
                        tServers[iCurrentId].m_nServerPort = CMainServer.m_aGameServers[i].m_nServerPort;
                        tServers[iCurrentId].m_szServerName = CMainServer.m_aGameServers[i].m_szServerName;

                        iCurrentId++;
                    }
                }
            }

            if (CServerConfig.GetUseAutoServerSelect() == 1)
            {
                CServerLog.GetLogger().info("[CBaseNetworkHandler::CopyServerList] AutoSelectServerId = {0}", byAutoSelectID);
            }
            else
            {
                byAutoSelectID = 0;
            }
            
            return iCurrentId;
        }

        #endregion

        #region Base Methods

        protected byte[] StructureToByteArr<TClass>(TClass objData) where TClass : class
        {
            var structSize = Marshal.SizeOf(typeof(TClass));
            var buffer = new byte[structSize];
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(objData, handle.AddrOfPinnedObject(), false);
            handle.Free();

            return buffer;
        }

        protected unsafe T ByteArrayToClass<T>(byte[] bytes) where T : class
        {
            fixed (byte* ptr = &bytes[0])
            {
                return (T) Marshal.PtrToStructure((IntPtr) ptr, typeof(T));
            }
        }

        #endregion
    }
}