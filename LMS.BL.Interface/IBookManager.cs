using LMS.BI;
namespace LMS.BL.Interface
{
    public interface IBookManager
    {
        public int GetBook(string bookname);
        public List<Books> GetList();
        public void Create(string name, string author, int copies);
        public void UpdateBookFile();
    }
}