using LMS.BI;
using LMS.Interface;
using System.Data.SqlClient;
namespace LMS.Bl.Sql
{
    public class BookSqlManager : IBookManager
    {
        private List<Books> _books;
        private SqlHelper sqlHelper;
        public BookSqlManager()
        {
            sqlHelper = new SqlHelper();
            SqlHelper.ConnectToDatabase();
        }
        public int GetBookIdByName(string bookName)
        {
            int bookId = -2;
            string query = "SELECT BookID FROM Books WHERE Name = '" + bookName + "'";
            using (SqlDataReader reader = sqlHelper.ExcuteQuery(query))
            {
                while (reader.Read())
                {
                    bookId = Convert.ToInt32(reader[0]);
                }
            }

            return bookId;
        }

        public List<Books> GetBooksList()
        { 
            return _books;
        }
        public void CreateBook(string name, string author, int copies)
        {
            copies = 0;
            string query = "SELECT BookID,Name,Copies FROM Books";
            SqlDataReader sqlReader = sqlHelper.ExcuteQuery(query);

            while (sqlReader.Read())
            {
                copies = int.Parse(sqlReader[2].ToString());
                if (copies > 0)
                {
                    _books.Add(new Books(Convert.ToInt32(sqlReader[0]), sqlReader[1].ToString(), sqlReader[2].ToString(), copies));
                }
            }
            sqlReader.Close();
        }

        public string GetBookNameUserID(int id)
        {
            string bookName = "";
            string query = "SELECT Bookname FROM Users WHERE ID = " + id + "";
            SqlDataReader reader = sqlHelper.ExcuteQuery(query);
            while (reader.Read())
            {
                bookName = reader[0].ToString();
            }
            return bookName;
        }

        public bool IsBookAval(int bookId)
        {
            int copies = 0;
            string query = "SELECT Copies FROM Books WHERE BookID=" + bookId + "";
            SqlDataReader sqlReader = sqlHelper.ExcuteQuery(query);
            while (sqlReader.Read())
            {
                copies = int.Parse(sqlReader[0].ToString());
            }
            if (copies > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }













        

       
        public string GetBookNameUserID(int id)
        {
            string bookName="";
            string query = "SELECT Bookname FROM Users WHERE ID = " + id + "";
            SqlDataReader reader = sqlHelper.ExcuteQuery(query);
            while (reader.Read())
            {
                bookName = reader[0].ToString();
            }
            return bookName;
        }

        public bool IsBookAval(int bookId)
        {
            int copies = 0;
            string query = "SELECT Copies FROM Books WHERE BookID="+bookId+"";
            SqlDataReader sqlReader = sqlHelper.ExcuteQuery(query);
            while (sqlReader.Read())
            {
                copies = int.Parse(sqlReader[0].ToString());
            }
            if (copies > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}