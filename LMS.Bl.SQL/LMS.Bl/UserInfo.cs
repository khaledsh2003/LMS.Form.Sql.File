using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LMS.BI
{
    public class UserInfo
    {
        public static int InstanNum = 0;
        public int InstantId;
        public string Name;
        public string PhoneNum;
        public string RentBoughtBook;
        public string FromDate;
        public string ToDate;
        public UserInfo(string name, string phone, string book, string fromDate, string toDate)
        {
            InstantId = InstanNum;
            InstanNum++;
            Name = name;
            PhoneNum = phone;
            RentBoughtBook = book;
            FromDate = fromDate;
            ToDate = toDate;
            
        }

    }
}
