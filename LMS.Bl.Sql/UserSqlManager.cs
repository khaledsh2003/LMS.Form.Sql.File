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
            _users = new List<UserInfo>();
            sqlHelper = new SqlHelper();
            SqlHelper.ConnectToDatabase();
            GetAllUsers();

        }
        public List<UserInfo> GetUsersList()
        {
            return _users;
        }
        public void GetAllUsers()
        {
            string Query = "SELECT * FROM Users";
            SqlDataReader reader = sqlHelper.ExcuteQuery(Query);
            while (reader.Read())
            {
                _users.Add(new UserInfo(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), Convert.ToInt32(reader[6])));
            }
            
        }
        public void RemoveUserById(int id)
        {
            int bookId = GetBookIdByUserId(id);
            string deleteQuery = "DELETE FROM Users WHERE ID='" + id + "'";
            sqlHelper.ExcuteNoneQuery(deleteQuery);
        }

        public UserInfo CreateUser(string name, string phone, string bookName, string fromDate, string toDate, int bookId)
        {
            UserInfo user = null;
            int userId = 0;
            string insertQuery = "INSERT INTO Users VALUES('" + name + "','" + phone + "','" + bookName + "','" + fromDate + "','" + toDate + "'," + bookId + ")";
            sqlHelper.ExcuteNoneQuery(insertQuery);
            userId = GetUserIDByPhoneNum(phone);
            _users.Add(new UserInfo(userId,name,phone,bookName,fromDate,toDate, bookId));
            return user;
        }
        public int GetUserIDByPhoneNum(string phoneNumber)
        {
            int userID = 0;
            string Query = "SELECT ID FROM Users WHERE PhoneNum = '"+phoneNumber+"'";
            SqlDataReader reader = sqlHelper.ExcuteQuery(Query);
            while (reader.Read())
            {
                userID = int.Parse(reader[0].ToString());
            }
            return userID;
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
   
