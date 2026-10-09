using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Commons.Log;

namespace Commons.Database
{
    public class CDBHelper
    {
        private readonly SqlDataAdapter m_adapter;
        private readonly SqlCommand m_comm;
        private readonly SqlConnection m_conn;
        
        // Use for program-wide - save memory & performance improvement
        private static readonly Dictionary<string, SqlCommand> m_dicSqlParamsCache = 
            new Dictionary<string, SqlCommand>();

        public CDBHelper(string _connString)
        {
            m_conn = new SqlConnection(_connString);
            m_adapter = new SqlDataAdapter();
            m_comm = new SqlCommand {Connection = m_conn};
        }

        public SqlConnection GetConn()
        {
            return m_conn;
        }

        public SqlDataAdapter GetAdapter()
        {
            return m_adapter;
        }

        public int TryOpen(bool bDisplayError = true)
        {
            try
            {
                m_conn.Open();
                return 0;
            }
            catch (Exception ex)
            {
                if (bDisplayError) CPublicLogger.GetLogger().error("CDBHelper::TryOpen Fail : {0}", ex.Message);
                return ex.HResult;
            }
        }

        public SqlCommand ExecuteSP(string _sql, params SqlParameter[] _paras)
        {
            return InternalExecuteSP(_sql, _paras);
        }

        public DataTable ExecuteQuery(string _sql, params SqlParameter[] _paras)
        {
            return InternalExecuteQuery(_sql, _paras);
        }
        
        public int ExecuteNonQuery(string _sql, params SqlParameter[] _paras)
        {
            return InternalExecuteNonQuery(_sql, _paras);
        }
        
        private SqlCommand InternalExecuteSP(string _sql, params SqlParameter[] _paras)
        {
            SqlCommand pCommand;
            if (!m_dicSqlParamsCache.ContainsKey(_sql))
            {
                pCommand = new SqlCommand
                {
                    Connection = m_conn,
                    CommandText = _sql,
                    CommandType = CommandType.StoredProcedure
                };
                SqlCommandBuilder.DeriveParameters(pCommand);
                
                m_dicSqlParamsCache.Add(_sql, pCommand);
                
                CPublicLogger.GetLogger().trace("CDBHelper::InternalExecuteSP - {0} cache success", _sql);
            }
            else
            {
                pCommand = m_dicSqlParamsCache[_sql];
            }
            
            if (_paras != null)
            {
                if (_paras.Length != pCommand.Parameters.Count - 1)
                {
                    throw new Exception("Parameter length is not enough!");
                }
                
                // 0: @RETURN_VALUE
                for (var i = 1; i < pCommand.Parameters.Count; i++)
                {
                    pCommand.Parameters[i].Direction = _paras[i - 1].Direction;
                    pCommand.Parameters[i].Value = _paras[i - 1].Value;
                }
            }

            try
            {
                pCommand.ExecuteNonQuery();
                return pCommand;
            }
            catch (SqlException e)
            {
                //PublicLogger.GetLogger().error("DBHelper::InternalExecuteSP() - comm.ExecuteNonQuery() SqlException");
                throw new Exception(e.Message);
            }
        }

        private DataTable InternalExecuteQuery(string _sql, params SqlParameter[] _paras)
        {
            var dset = new DataTable();
            m_comm.Parameters.Clear();
            if (_paras != null)
                m_comm.Parameters.AddRange(_paras);
            m_comm.CommandText = _sql;
            m_comm.CommandType = CommandType.Text;
            m_adapter.SelectCommand = m_comm;

            try
            {
                m_adapter.Fill(dset);
            }
            catch (SqlException e)
            {
                //PublicLogger.GetLogger().error("DBHelper::ExecuteQuery() - adapter.Fill() SqlException");
                throw new Exception(e.Message);
            }
            return dset;
        }

        private int InternalExecuteNonQuery(string _sql, params SqlParameter[] _paras)
        {
            m_comm.Parameters.Clear();
            if (_paras != null)
                m_comm.Parameters.AddRange(_paras);
            m_comm.CommandText = _sql;
            m_comm.CommandType = CommandType.Text;

            try
            {
                return m_comm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                //PublicLogger.GetLogger().error("DBHelper::ExecuteNonQuery() - comm.ExecuteNonQuery() SqlException");
                throw new Exception(e.Message);
            }
        }
    }
}