using System.Data;
using System.Data.SqlClient;

namespace Commons.Database
{
    public class CSQLParam
    {
        private readonly SqlParameter SQL;

        public CSQLParam(string parameterName, SqlDbType dbType)
        {
            SQL = new SqlParameter(parameterName, dbType) {Direction = ParameterDirection.Output};
        }

        public CSQLParam(string parameterName, SqlDbType dbType, int size)
        {
            SQL = new SqlParameter(parameterName, dbType, size) {Direction = ParameterDirection.Output};
        }

        public CSQLParam(string parameterName, object value)
        {
            SQL = new SqlParameter(parameterName, value);
        }

        public SqlParameter get()
        {
            return SQL;
        }

        public object getValue()
        {
            return SQL.Value;
        }
    }
}