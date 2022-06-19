using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Interface
{
    public interface IUserManagerSql
    {
        public void CreateUser(string name, string phone,string bookName,string fromDate, string toDate, int bookId);
        public SqlDataAdapter GetUsersReader();

        public int GetBookIdByUserId(int id);
        public void RemoveUserById(int id);
    }
}
