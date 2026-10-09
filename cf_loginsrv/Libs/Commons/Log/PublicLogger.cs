namespace Commons.Log
{
    public static class CPublicLogger
    {
        private static CBaseLogHelper logger;

        public static void SetLogger(CBaseLogHelper _logger)
        {
            logger = _logger;
        }

        public static CBaseLogHelper GetLogger()
        {
            return logger;
        }
    }
}