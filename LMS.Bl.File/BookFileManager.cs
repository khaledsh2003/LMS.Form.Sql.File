
using LMS.BI;
using LMS.Bl.file;
using LMS.Interface;
using Microsoft.VisualBasic.FileIO;

namespace LMS.Bl.File
{
    public class BookFileManager : IBookManager
    {

        private readonly string path = @"C:\Users\kshah\source\repos\LMSapps\LMS.Bl\Books.csv";
        private string[] fields;
        private List<Books> _book;
        public BookFileManager()
        {
            _book = new List<Books>();
            _book = Utilities.Instance.ReadBookFile(path, _book);

        }

        public List<Books> GetBooksList()
        {
            return _book;
        }
        public int GetBookIdByName(string bookName)
        {
            foreach (var i in _book)
            {
                if (i.Name == bookName) { return i.Id; }
            }

            return -1;
        }

        public void CreateBook(int id, string name, string author, int copies)
        {
            Books newBook = new Books(_book.Count, name, author, copies);
            _book.Add(newBook);
            UpdateBookFile();
        }
        public bool IsBookAval(int bookId)
        {
            foreach(var i in _book)
            {
                if(i.Id==bookId && i.Copies > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void IncreaseCopies(int bookId)
        {
            _book[bookId].Copies++;
            UpdateBookFile();

        }
        public void DecreaseCopies(int bookId)
        {
            _book[bookId].Copies--;
            UpdateBookFile();

        }
        private void UpdateBookFile()
        {
            using (var w = new StreamWriter(path))
            {

                foreach (var i in _book)
                {

                    var first = i.Name;
                    var second = i.Publisher;
                    var third = i.Copies;
                    var line = string.Format("{0},{1},{2}", first, second, third);
                    w.WriteLine(line);
                    w.Flush();
                }

            }
        }
    }
}