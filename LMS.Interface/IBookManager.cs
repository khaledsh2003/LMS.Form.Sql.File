using LMS.BI;
namespace LMS.Interface
{
    public interface IBookManager
    {
        public int GetBookIdByName(string bookName);

        public List<Books> GetBooksList();
        public void CreateBook(int id,string name, string author, int copies);

        public void IncreaseCopies(int bookId);


        public void DecreaseCopies(int bookId);
       
        public bool IsBookAval(int bookId);
    }
}