using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BI;
using LMS.BL.Interface;
using Microsoft.VisualBasic.FileIO;
namespace LMS.Bl.file
{
    public class UserFileManager:IUserManager
    {
        private readonly string path = @"C:\Users\kshah\source\repos\LMSapps\LMS.Bl\renter.csv";

        private List<UserInfo> _users;
        public UserFileManager()
        {
            _users = new List<UserInfo>();
            _users = Utilities.Instance.ReadUsersFile(path, _users);
            
        }
        public List<UserInfo> GetList()
        {
            return _users;
        }
        public int GetUserById(string name,string bookname)
        {
            int id=_users.FindIndex(x=>x.Name==name && x.RentBoughtBook==bookname);
            return id;
        }
        public void Delete(int id)
        {
            _users.RemoveAt(id);
            UpdateUserFile();
        }
        public void UpdateUserFile()
        {
            var path = @"C:\Users\kshah\source\repos\LMSapps\LMS.Bl\renter.csv";

            using (var w = new StreamWriter(path))
            {
                foreach (var i in _users)
                {
                    var first = i.Name;
                    var second = i.PhoneNum;
                    var third = i.RentBoughtBook;
                    var fourth = i.FromDate;
                    var fifth = i.ToDate;
                    var line = string.Format("{0},{1},{2},{3},{4},{5}", _users.IndexOf(i), first, second, third, fourth, fifth);
                    w.WriteLine(line);
                    w.Flush();
                }

            }
        }
        public UserInfo Create(string name, string phone, string bookName,string fromDate,string toDate)
        {
            UserInfo newUser = new UserInfo(name, phone, bookName, fromDate, toDate);
            _users.Add(newUser);
            UpdateUserFile();
            return newUser;

        }

    }
}
