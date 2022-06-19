using Microsoft.VisualBasic.FileIO;

namespace LMS.BI
{
    public class Library
    {
        private List<UserInfo> _users;
        private List<Books> _books;
        public Library()
        {
            _users = new List<UserInfo>();
            _books= new List<Books>();
        }
        public List<UserInfo> Users
        {
            get { return _users; }
        }
        public List<Books> Books
        {
            get { return _books; }
        }
        public void RentBuy(string name, int id, string bookName, string phone, string fromDate,string toDate)
        {
            _users.Add(new UserInfo(id,name, phone, bookName,fromDate,toDate,id ));
            _books[id].Copies--;
        }
        public void RemoveRenter(int id )
        {
            string bookName = _users[id].RentBoughtBook;
            _users.RemoveAt(id);
            var b = _books.FirstOrDefault(x=>x.Name==bookName);
            if (b != null) b.Copies++;
           
        }
    }
}
