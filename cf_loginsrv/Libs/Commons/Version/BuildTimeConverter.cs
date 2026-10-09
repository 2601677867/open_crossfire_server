using System;

namespace Commons.Version
{
    public static class CBuildTimeConverter
    {
        public static string Convert(System.Version version)
        {
            var now = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            return $"{now:MMM} {now.Day.ToString().PadLeft(2)} {now:yyyy HH:mm:ss}";
        }
    }
}