using System.Data.SqlClient;
using LMS.BL.Interface;

namespace Lms.Managers.SQL.database
{
    public class UserSqlManager : IUserManagerSql
    {
        private SqlHelper sqlHelper;
        public UserSqlManager()
        {
            sqlHelper = new SqlHelper();
            SqlHelper.ConnectToDatabase();
        }
        public int GetBookIdByUserId(int id)
        {
            int bookId = -1;
            string selectQuery = "SELECT BookID FROM Users WHERE ID = " + id + "";
            SqlDataReader reader = sqlHelper.ExcuteQuery(selectQuery);
            while (reader.Read())
            {
                bookId = int.Parse(reader[0].ToString());
            }
            return bookId;
        }
        public void CreateUser(string name, string phone, string bookName, string fromDate, string toDate, int bookId)
        {
            string insertQuery = "INSERT INTO Users VALUES('" + name + "','" + phone + "','" + bookName + "','" + fromDate + "','" + toDate + "'," + bookId + ")";
            string updatequery = "UPDATE books SET Copies = Copies - 1 WHERE BookID = " + bookId + "";

            sqlHelper.ExcuteNoneQuery(insertQuery);
            sqlHelper.ExcuteNoneQuery(updatequery);
        }
        public void RemoveUserById(int id)
        {
            int bookId = GetBookIdByUserId(id);
            string updatequery = "UPDATE books SET Copies = Copies + 1 WHERE BookID = " + bookId + "";
            string deleteQuery = "DELETE FROM Users WHERE ID='" + id + "'";
            sqlHelper.ExcuteNoneQuery(updatequery);
            sqlHelper.ExcuteNoneQuery(deleteQuery);
        }
        public SqlDataAdapter GetUsersReader()
        {
            string query = "SELECT * FROM Users";
            SqlDataAdapter usersData = sqlHelper.ExcuteAllDataQuery(query);
            return usersData;
        }
    }
}
   
