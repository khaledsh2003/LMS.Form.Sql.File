using LMS.BL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BI;
using Microsoft.VisualBasic.FileIO;

namespace LMS.Bl.file
{
    public class UserFile:IUserManager
    {
        private List<UserInfo> _users;
        public UserFile()
        {
            _users = new List<UserInfo>();
            var path = @"C:\Users\kshah\OneDrive\Desktop\AMS\LMS.Bl\renter.csv";
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;


                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    if (fields.Length != 6) { Console.WriteLine("Error occured while reading users info, the rest of the data after is read"); continue; }
                    _users.Add(new UserInfo(fields[1], fields[2], fields[3], fields[4], fields[5]));

                }

            }
        }
        public List<UserInfo> GetList()
        {
            return _users;
        }
        public int GetUser(string name,string bookname)
        {
            int id=_users.FindIndex(x=>x.Name==name && x.RentBoughtBook==bookname);
            return id;
        }
        public void Delete(int id)
        {
            string bookName = _users[id].RentBoughtBook;
            _users.RemoveAt(id);
           // var b = bookfile.GetList().FirstOrDefault(x => x.Name == bookName);
            //if (b != null) b.Copies++;
        }
        public void UpdateUserFile()
        {
            var path = @"C:\Users\kshah\OneDrive\Desktop\AMS\LMS.Bl\renter.csv";

            using (var w = new StreamWriter(path))
            {
                foreach (var i in _users)
                {
                    var firstId = i.InstantId;
                    var first = i.Name;
                    var second = i.PhoneNum;
                    var third = i.RentBoughtBook;
                    var fourth = i.FromDate;
                    var fifth = i.ToDate;
                    var line = string.Format("{0},{1},{2},{3},{4},{5}", firstId, first, second, third, fourth, fifth);
                    w.WriteLine(_users.IndexOf(i));
                    w.Flush();
                }

            }
        }


        public void Create(string name, string phone, string bookName,string fromDate,string toDate)
        {
            _users.Add(new UserInfo(name,phone,bookName,fromDate,toDate));
            //var b = bookfile.GetList().FindIndex(x => x.Name == bookName);
            //if(b!=-1) book[b].Copies--;
        }
       
    }
}
