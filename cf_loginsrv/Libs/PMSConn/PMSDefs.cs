namespace PMSConn
{
    public enum E_USE_ERROR
    {
        PMS_USE_CONNECT,
        PMS_USE_ARGUMENT,
        PMS_NO_CONNECT_OR_ARGUMENT,
        PMS_FAIL,
    }

    public enum E_LAST_ERROR_EVENT
    {
        NO_ERROR,
        CONNECT_FAILED,
        WRONG_CONNECT_PASSWORD,
        CONNECT_UNKNOWN_ERROR,
        PMS_HEARTBEAT_SKIPPED,
        PMS_FORCE_DISCONNECTED
    }
}