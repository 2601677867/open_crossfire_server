namespace cf_loginsrv.Util
{
    internal static class CGlobalThreads
    {
        /*private static void __DoInitializeDefaultData()
        {
            try
            {
				//CLogger.GetLogger().debug("__DoInitializeDefaultData()");
                var dMMServers = new Dictionary<uint, CSharedVariable.Server>();

                var servers = CSharedVariable.DBMgr.ExecuteQuery(E_GDBGW_DEFS.QUERY_SELECT_SERVER_LIST);
                var eventData = CSharedVariable.DBMgr.ExecuteQuery(E_GDBGW_DEFS.QUERY_SELECT_SERVER_EVENT_INFO);
                if (servers.Success && eventData.Success)
                {
                    for (var i = 0; i < servers.row; i++)
                    {
                        var addr = servers.data[i, 5];
                        if (!CUpdateDomainInfo.Exists(addr)) CUpdateDomainInfo.AddDomain(addr);
                        var domain = CUpdateDomainInfo.GetIPAddress(addr);

                        var sServerName = servers.data[i, 1];
                        var nConnectCount = Convert.ToInt32(servers.data[i, 3]);
                        var nLimitCount = Convert.ToInt32(servers.data[i, 4]);

                        var pServerInstance = new CSharedVariable.Server
                        {
                            ServerID = Convert.ToUInt16(servers.data[i, 0]),
                            ServerName = sServerName,
                            ServerWebName = servers.data[i, 2],
                            ConnectCount = nConnectCount,
                            LimitCount = nLimitCount,

                            IPAddress = domain,
                            Port = Convert.ToUInt16(servers.data[i, 6]),
                            Event = servers.data[i, 7] == "1",
                            LocalTest = servers.data[i, 8] == "1"
                        };

                        //Log.Logger.GetLogger().info($"Added #{Convert.ToUInt16(servers.data[i, 0])} {servers.data[i, 1]}");

                        var nServerID = Convert.ToUInt32(servers.data[i, 0]);
                        if (!CSharedVariable.MMServers.ContainsKey(nServerID))
                        {
                            dMMServers.Add(nServerID, pServerInstance);
                        }
                        else
                        {
                            dMMServers[nServerID] = pServerInstance;
                        }
                    }

                    for (var i = 0; i < eventData.row; i++)
                    {
                        var nServerID = Convert.ToUInt32(eventData.data[i, 4]);
                        if (!dMMServers.TryGetValue(nServerID, out var pServerInstance))
                        {
                            CServerLog.GetLogger()
                                .warn("There is no ServerNO \"{0}\" in server list. Ignore this event info.", 
                                        eventData.data[i, 4]);
                            continue;
                        }

                        var nExpPercent = Convert.ToUInt32(eventData.data[i, 0]);
                        var nGPPercent = Convert.ToUInt32(eventData.data[i, 1]);
                        var nMapID = Convert.ToInt32(eventData.data[i, 5]);
                        var nSubMapID = Convert.ToInt32(eventData.data[i, 6]);
                        
                        if (CSocketController.GetMgmtServer() != null)
                        {
                            var socket = CSocketController.GetMgmtServer().GetSocket();
                            var connections = socket.GetClients();
                            foreach (var userTokenBase in connections)
                            {
                                var conn = (CLGUserContext) userTokenBase;
                                if (conn.nMgmtID != nServerID) continue;
                                var _packet = new CPacket(0x02, 0x00, 0x00); // MGMT_EVENT_INFO
                                _packet.WriteUInt(nExpPercent);
                                _packet.WriteUInt(nGPPercent);
                                _packet.WriteByte(Convert.ToByte(eventData.data[i, 2])); // death reset
                                _packet.WriteInt(nMapID);
                                _packet.WriteInt(nSubMapID);
                                _packet.WriteString(eventData.data[i, 3], true); // memo
                                CServer.SendMessage(conn, _packet);
                            }
                        }

                        var EventInstance = new CSharedVariable.EventInfo
                        {
                            EXP_PER = nExpPercent,
                            GP_PER = nGPPercent,
                            DEATH_RESET = eventData.data[i, 2] == "1",
                            MAPID = nMapID,
                            SUBMAPID = nSubMapID
                        };
                        pServerInstance.EventInfo = EventInstance;

                        dMMServers[nServerID] = pServerInstance;
                    }

                    var PCBEventData = CSharedVariable.DBMgr.ExecuteQuery(E_GDBGW_DEFS.QUERY_SELECT_SERVER_PCB_EVENT_INFO);
                    
					if (PCBEventData.Success)
						CSharedVariable.PCBEvent = PCBEventData.row > 0;
					else
					  CServerLog.GetLogger().error("Failed to get ServerDefaultData!!!");
                }
                else
                {
                    CServerLog.GetLogger().error("Failed to get ServerDefaultData!!!");
                }

                lock (CSharedVariable.MMServers)
                {
                    CSharedVariable.MMServers = dMMServers;
                }
            } catch (Exception e)
            {
                CServerLog.GetLogger().error("!Error in getting ServerDefaultData");
                CServerLog.GetLogger().error(e.Message);
				CServerLog.GetLogger().trace(e);
                //SharedVariable.MMServers.Clear();
            }
        }*/
        
    }
}