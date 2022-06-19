using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BI;
namespace LMS.Interface
{
    public interface IUserManager
    {
        public List<UserInfo> GetUsersList();
        public void RemoveUserById(int id);

        public UserInfo CreateUser(string name, string phone, string bookName, string fromDate, string toDate, int bookId);


    }
}
