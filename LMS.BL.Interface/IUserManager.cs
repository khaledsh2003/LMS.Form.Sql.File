using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BI;
namespace LMS.BL.Interface
{
    public interface IUserManager
    {
        public List<UserInfo> GetList();
        public int GetUser(string name,string bookName);
        public void Delete(int id);
        public void UpdateUserFile();
        public void Create(string name, string phone, string bookName, string fromDate, string toDate);
    }
}
