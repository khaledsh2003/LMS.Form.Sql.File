using LMS.BI;
namespace LMS.BL.Interface
{
    public interface IBookManager
    {
        public int GetBookById(string bookname);
        public List<Books> GetList();
        public Books Create(string name, string author, int copies);
        public void DecreaseCopies(string bookName);
    }
}