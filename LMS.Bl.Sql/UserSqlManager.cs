using LMS.BI;
using LMS.Interface;
using System.Data.SqlClient;

namespace LMS.Bl.Sql
{
    public class UserSqlManager : IUserManager
    {
        private List<UserInfo> _users;
        private SqlHelper sqlHelper;
        public UserSqlManager()
        {
            sqlHelper = new SqlHelper();
            SqlHelper.ConnectToDatabase();
        }
        public List<UserInfo> GetUsersList()
        {
            string query = "SELECT * FROM Users";
            SqlDataReader usersData = sqlHelper.ExcuteQuery(query);
            while (usersData.Read())
            {
                _users.Add(new UserInfo(Convert.ToInt32(usersData[0]), usersData[1].ToString(), usersData[2].ToString(), usersData[3].ToString(), usersData[4].ToString(), usersData[5].ToString(), Convert.ToInt32(usersData[6])));
            }
            usersData.Close();
            return _users;
        }
        public void RemoveUserById(int id)
        {
            int bookId = GetBookIdByUserId(id);
            string updatequery = "UPDATE books SET Copies = Copies + 1 WHERE BookID = " + bookId + "";
            string deleteQuery = "DELETE FROM Users WHERE ID='" + id + "'";
            sqlHelper.ExcuteNoneQuery(updatequery);
            sqlHelper.ExcuteNoneQuery(deleteQuery);
        }

        public void CreateUser(string name, string phone, string bookName, string fromDate, string toDate, int bookId)
        {
            string insertQuery = "INSERT INTO Users VALUES('" + name + "','" + phone + "','" + bookName + "','" + fromDate + "','" + toDate + "'," + bookId + ")";
            string updatequery = "UPDATE books SET Copies = Copies - 1 WHERE BookID = " + bookId + "";
            sqlHelper.ExcuteNoneQuery(insertQuery);
            sqlHelper.ExcuteNoneQuery(updatequery);
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
       
        
       
    }
}
   
