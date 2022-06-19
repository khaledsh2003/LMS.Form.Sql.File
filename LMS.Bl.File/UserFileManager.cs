using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BI;
using LMS.Interface;
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
        public List<UserInfo> GetUsersList()
        {
            return _users;
        }
        public void RemoveUserById(int id)
        {
            _users.RemoveAt(id);
            UpdateUserFile();
        }
        public UserInfo CreateUser(string name, string phone, string bookName, string fromDate, string toDate, int bookId)
        {
            UserInfo user= new UserInfo(_users.Count, name, phone, bookName, fromDate, toDate, bookId);
            _users.Add(user);
            UpdateUserFile();
            return user;
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

    }
}
