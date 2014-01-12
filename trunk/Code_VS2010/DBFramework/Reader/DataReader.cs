using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DBFramework.Reader
{
    public sealed class DataReader
    {
        private SqlConnection _SqlConnection;

        private SqlDataReader _SqlDataReader;

        public DataReader(SqlConnection sqlConnection, SqlDataReader _sqlDataReader)
        {
            _SqlConnection = sqlConnection;
            _SqlDataReader = _sqlDataReader;
        }

        public SqlDataReader Reader
        {
            get { return _SqlDataReader; }
            private set { _SqlDataReader = value; }
        }

        public void Close()
        {
            try
            {
                _SqlDataReader.Close();
                _SqlConnection.Close();
            }
            catch (System.Exception)
            {
                
            }
        }
    }
}
