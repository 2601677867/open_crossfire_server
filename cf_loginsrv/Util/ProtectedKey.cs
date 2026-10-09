using System.Collections.Generic;
using Commons;

namespace cf_loginsrv.Util
{
    public class CProtectedKey : CSingleton<CProtectedKey>
    {
        #region Properties
        
        public const int MAX_CLIENT_SECURE_CODE_COUNT = 100;
        public const int MAX_SERVER_SECURE_CODE_COUNT = 100;

        public enum ESecureCodeType
        {
            TYPE_DEFAULT,
            TYPE_SERVER,
            TYPE_CLIENT
        }

        public struct SecureCode_tag
        {
            public int m_iKey { get; set; }
            public ESecureCodeType m_eType { get; set; }
        }
        
        private readonly List<SecureCode_tag> m_lstSecureCodes = new List<SecureCode_tag>();
        
        #endregion

        public int GetProtectedKeyCount() { return m_lstSecureCodes.Count; }

        public SecureCode_tag GetProtectedKey(int nIndex) { return m_lstSecureCodes[nIndex]; }

        public void AddProtectedKey(int iKey, ESecureCodeType eType)
        {
            m_lstSecureCodes.Add(new SecureCode_tag
            {
                m_iKey = iKey,
                m_eType = eType
            });
        }
    }
}