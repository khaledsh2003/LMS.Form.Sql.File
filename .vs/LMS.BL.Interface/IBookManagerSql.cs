using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace LMS.BL.Interface
{
    public interface IBookManagerSql
    {
        public int GetBookIdByName(string bookName);
        public string GetBookNameUserID(int id);

        public SqlDataReader GetBooksReader();
        public bool IsBookAval(int bookId);

    }
}
