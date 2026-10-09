using System.Runtime.CompilerServices;

namespace Network.Assert
{
    public static class ProtocolAssert
    {
        public static void PROTOCOL_ASSERT(
            bool bCondition,
            [CallerFilePath] string szCallerPath = "",
            [CallerMemberName] string szCallerName = "",
            [CallerLineNumber] int iCallerLineNum = 0)
        {
            if (!bCondition)
                throw new ProtocolSizeException($"[PROTOCOL_ASSERT] Protocol Size Error!! {szCallerPath}::{szCallerName}:{iCallerLineNum}");
        }
    }
}