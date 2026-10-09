using System;
using System.Collections.Generic;

namespace Commons.Log
{
    public class CLogFormatter
    {
        private readonly Dictionary<string, string> CUSTOM_FILE_FORMATTER = new Dictionary<string, string>();
        private readonly Dictionary<string, string> CUSTOM_FORMATTER = new Dictionary<string, string>();

        private readonly Dictionary<string, ConsoleColor> CUSTOM_FORMATTER_COLOR =
            new Dictionary<string, ConsoleColor>();

        private readonly Dictionary<string, bool> CUSTOM_FORMATTER_CSV = new Dictionary<string, bool>();
        private readonly Dictionary<string, LOGLEVEL> CUSTOM_FORMATTER_LEVEL = new Dictionary<string, LOGLEVEL>();
        private readonly Dictionary<string, int> CUSTOM_FORMATTER_TIME_DIV = new Dictionary<string, int>();

        public bool Exists(string type)
        {
            return CUSTOM_FORMATTER.TryGetValue(type, out _);
        }

        public string GetFormatter(string type)
        {
            CUSTOM_FORMATTER.TryGetValue(type, out var formatter);
            return formatter;
        }

        public string GetFileFormatter(string type)
        {
            CUSTOM_FILE_FORMATTER.TryGetValue(type, out var formatter);
            return formatter;
        }

        public LOGLEVEL GetLevel(string type)
        {
            CUSTOM_FORMATTER_LEVEL.TryGetValue(type, out var level);
            return level;
        }

        public bool GetCSV(string type)
        {
            CUSTOM_FORMATTER_CSV.TryGetValue(type, out var csv);
            return csv;
        }

        public ConsoleColor GetColor(string type)
        {
            CUSTOM_FORMATTER_COLOR.TryGetValue(type, out var color);
            return color;
        }
        
        public int GetTimeDiv(string type)
        {
            CUSTOM_FORMATTER_TIME_DIV.TryGetValue(type, out var timeDiv);
            return timeDiv;
        }

        public void AddFormatter(string type, string formatter, LOGLEVEL level, bool csv,
            ConsoleColor color = ConsoleColor.Gray, bool enableTypeLog = true, string customFileFormatter = null,
            int timeDiv = 3)
        {
            CUSTOM_FORMATTER.Add(type, formatter);
            CUSTOM_FORMATTER_LEVEL.Add(type, level);
            CUSTOM_FORMATTER_CSV.Add(type, csv);
            CUSTOM_FORMATTER_COLOR.Add(type, color);
            if (customFileFormatter == null)
                customFileFormatter =
                    enableTypeLog ? "{0}/{1}/{2}/{3}_{4}_{5}_{6}_{7}.log" : "{0}/{1}/{2}/{4}_{5}_{6}_{7}.log";
            CUSTOM_FILE_FORMATTER.Add(type, customFileFormatter);
            CUSTOM_FORMATTER_TIME_DIV.Add(type, timeDiv);
        }
    }
}