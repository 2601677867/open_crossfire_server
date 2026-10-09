using System;
using System.Collections.Generic;

namespace Commons.Log
{
    /// <summary>
    ///     Registry of console channels: every log type maps to a 【TAG】, a verbosity and a color.
    /// </summary>
    public class CLogFormatter
    {
        private class SChannel
        {
            public string m_sTag;
            public string m_sFormatter;
            public LOGLEVEL m_eLevel;
            public ConsoleColor m_pColor;
        }

        private readonly Dictionary<string, SChannel> m_pChannels = new Dictionary<string, SChannel>();

        public bool Exists(string type)
        {
            return m_pChannels.ContainsKey(type);
        }

        public string GetFormatter(string type)
        {
            SChannel pChannel;
            return m_pChannels.TryGetValue(type, out pChannel) ? pChannel.m_sFormatter : null;
        }

        public LOGLEVEL GetLevel(string type)
        {
            SChannel pChannel;
            return m_pChannels.TryGetValue(type, out pChannel) ? pChannel.m_eLevel : LOGLEVEL.LEVEL_INFO;
        }

        public ConsoleColor GetColor(string type)
        {
            SChannel pChannel;
            return m_pChannels.TryGetValue(type, out pChannel) ? pChannel.m_pColor : ConsoleColor.Gray;
        }

        public string GetTag(string type)
        {
            SChannel pChannel;
            if (!m_pChannels.TryGetValue(type, out pChannel)) return TagOfLevel(LOGLEVEL.LEVEL_INFO);
            return pChannel.m_sTag ?? TagOfLevel(pChannel.m_eLevel);
        }

        /// <summary>
        ///     Register a console channel; the log line is the formatted message itself.
        /// </summary>
        public void AddChannel(string type, string tag, LOGLEVEL level, ConsoleColor color)
        {
            m_pChannels[type] = new SChannel
            {
                m_sTag = tag,
                m_sFormatter = null,
                m_eLevel = level,
                m_pColor = color
            };
        }

        /// <summary>
        ///     Register a channel whose line is built from a positional template filled with the caller's
        ///     context objects. Used by <see cref="CBaseLogHelper.print"/> calls that still pass extra fields.
        /// </summary>
        public void AddFormatter(string type, string formatter, LOGLEVEL level, ConsoleColor color, string tag)
        {
            m_pChannels[type] = new SChannel
            {
                m_sTag = tag,
                m_sFormatter = formatter,
                m_eLevel = level,
                m_pColor = color
            };
        }

        /// <summary>
        ///     Legacy signature kept because pre-built DLLs (PMSConn, gDBGW gateway) call it with the
        ///     file target and time-slicing arguments; those are ignored now that nothing is written to disk.
        /// </summary>
        public void AddFormatter(string type, string formatter, LOGLEVEL level, bool csv,
            ConsoleColor color = ConsoleColor.Gray, bool enableTypeLog = true, string customFileFormatter = null,
            int timeDiv = 3)
        {
            AddFormatter(type, formatter, level, color, null);
        }

        private static string TagOfLevel(LOGLEVEL level)
        {
            switch (level)
            {
                case LOGLEVEL.LEVEL_FATAL: return "FATAL";
                case LOGLEVEL.LEVEL_ERROR: return "ERROR";
                case LOGLEVEL.LEVEL_WARN: return "WARN";
                case LOGLEVEL.LEVEL_INFO: return "INFO";
                case LOGLEVEL.LEVEL_DEBUG: return "DEBUG";
                case LOGLEVEL.LEVEL_TRACE: return "TRACE";
                default: return "LOG";
            }
        }
    }
}
