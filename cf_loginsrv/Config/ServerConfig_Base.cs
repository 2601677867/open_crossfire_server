using System;
using System.Collections.Generic;
using System.Globalization;
using cf_loginsrv.Log;
using Commons.Config;

namespace cf_loginsrv.Config
{
    internal partial class CServerConfig
    {
        #region Base Functions

        private readonly List<string> m_gWarnList = new List<string>();
        
        private string GetIniValueString(string sPath, string sSection, string sKey, string sDefaultValue, bool bWarn = true)
        {
            var sValue = CIniFile.GetIniString(sSection, sKey, sPath);

            if (sValue == "")
            {
                if (bWarn)
                    m_gWarnList.Add(
                        $"Not Found Key \"{sKey}\" In [{sSection}] Section. Using Default Value \"{sDefaultValue}\"");
                sValue = sDefaultValue;
            }

            return sValue;
        }
        
        private int GetIniValueInt(string sPath, string sSection, string sKey, int nDefaultValue, bool bWarn = true)
        {
            return Convert.ToInt32(GetIniValueString(sPath, sSection, sKey, nDefaultValue.ToString(), bWarn));
        }
        
        private ushort GetIniValueUShort(string sPath, string sSection, string sKey, int nDefaultValue, bool bWarn = true)
        {
            return Convert.ToUInt16(GetIniValueString(sPath, sSection, sKey, nDefaultValue.ToString(), bWarn));
        }
        
        private byte GetIniValueByte(string sPath, string sSection, string sKey, int nDefaultValue, bool bWarn = true)
        {
            return Convert.ToByte(GetIniValueString(sPath, sSection, sKey, nDefaultValue.ToString(), bWarn));
        }
        
        private double GetIniValueDouble(string sPath, string sSection, string sKey, double dDefaultValue, bool bWarn = true)
        {
            return Convert.ToDouble(GetIniValueString(sPath, sSection, sKey, dDefaultValue.ToString(CultureInfo.InvariantCulture), bWarn));
        }
        
        public void FinishInit()
        {
            foreach (var szWarnMsg in m_gWarnList) CServerLog.GetLogger().warn(szWarnMsg);
        }

        #endregion
    }
}