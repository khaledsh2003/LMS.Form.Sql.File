using LMS.BI;
namespace LMS.Interface
{
    public interface IBookManager
    {
        public int GetBookIdByName(string bookName);

        public List<Books> GetBooksList();
        public void CreateBook(string name, string author, int copies);
           
        public string GetBookNameUserID(int id);

        public bool IsBookAval(int bookId);
    }
}