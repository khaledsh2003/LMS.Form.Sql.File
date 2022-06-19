using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace LMS.Bl.Sql
{
    public class SqlHelper
    {
        private static SqlConnection _sqlConnection;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private static string _connectionString = @"Data Source=DESKTOP-1LF25MG;Initial Catalog=library;Integrated Security=True";
        public static void ConnectToDatabase()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlConnection.Open();
        }
        public static void CloseDatabaseConnection()
        {
            _sqlConnection.Close();
        }
        public void ExcuteNoneQuery(string Query)
        {
            ConnectToDatabase();
            _command = new SqlCommand(Query, _sqlConnection);
            _command.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        public SqlDataReader ExcuteQuery(string Query)
        {
            ConnectToDatabase();
            if (_reader != null) _reader.Close();
            _command = new SqlCommand(Query, _sqlConnection);
            _reader = _command.ExecuteReader();
            return _reader;
        }
        public SqlDataAdapter ExcuteAllDataQuery(string Query)
        {
            ConnectToDatabase();
            _command = new SqlCommand(Query, _sqlConnection);
           // SqlDataAdapter _adapter = new SqlDataAdapter(_command);
            //return _adapter;
        }
    }
}
