using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LMS.BI
{
    public class UserInfo
    {
        public int id;
        public int bookID;
        public string Name;
        public string PhoneNum;
        public string RentBoughtBook;
        public string FromDate;
        public string ToDate;
        public UserInfo(int Id, string name, string phone, string book, string fromDate, string toDate, int bookID)
        {
            id = Id;
            Name = name;
            PhoneNum = phone;
            RentBoughtBook = book;
            FromDate = fromDate;
            ToDate = toDate;
            this.bookID = bookID;
            
        }

    }
}
