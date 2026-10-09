namespace Network.Protocol
{
    public static class GDBGW_PK
    {
        public const int PROTOCOL_GDBGW = 127;

        public const int PROTOCOL_REQUEST_GS_CONNECT = 0;
        public const int PROTOCOL_REQUEST_GS_CONNECT_RESULT = 1;
        public const int PROTOCOL_GS_QUERY_LIST = 2;
        public const int PROTOCOL_GS_QUERY_LIST_RESULT = 3;
        public const int PROTOCOL_HEARTBEAT = 4;
        public const int PROTOCOL_EXECUTE_QUERY = 5;
        public const int PROTOCOL_EXECUTE_QUERY_RESULT = 6;
        
        public const int PROTOCOL_EXECUTE_QUERY_RESULT_HEADER = 0;
        public const int PROTOCOL_EXECUTE_QUERY_RESULT_DATA = 1;
        public const int PROTOCOL_EXECUTE_QUERY_RESULT_DATA_EXT = 2;
        public const int PROTOCOL_EXECUTE_QUERY_RESULT_DATA_END = 3;

        public enum QUERYTYPE
        {
            Text,
            StoredProcedure
        }

        public enum PRIORITY : byte
        {
            Low,
            Normal,
            High,
        }
        
        public struct PROTO_REQUEST_GS_CONNECT
        {
            public byte byPacketCount;
        }
        
        public struct PROTO_REQUEST_GS_CONNECT_RESULT
        {
            public enum RESULT
            {
                SUCCESS,
                FAIL,
                ERROR
            }
            
            public RESULT eResult;
        }

        public struct PROTO_GS_QUERY_LIST_RESULT
        {
            public enum RESULT
            {
                RECEIVED_ALL_ACK,
                ERROR
            }
            
            public RESULT eResult;
        }
        
        public struct EXECUTE_QUERY_DATA
        {
            
        }
        
        public class GDBGWQUERY
        {
            public string szQuery;
            
            public bool bParsed;
            public bool bSelectQuery;
            public string szParsedQuery;
            public string[] aQuerySplitted;
        }
    }
}