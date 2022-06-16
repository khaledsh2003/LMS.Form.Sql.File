
using LMS.BI;
using LMS.BL.Interface;
using Microsoft.VisualBasic.FileIO;

namespace LMS.Bl.file
{
    public class BookFileManager: IBookManager
    {
        private readonly string path = @"C:\Users\kshah\source\repos\LMSapps\LMS.Bl\Books.csv";
        private string[] fields;
        private List<Books> _book;
        public BookFileManager()
        {
            _book = new List<Books>();
            _book=Utilities.Instance.ReadBookFile(path,_book);

        }
        
        public List<Books> GetList()
        {
            return _book;
        }
        
        public int GetBookById(string bookname)
        {
            foreach(var i in _book)
            {
                if (i.Name == bookname) { return i.InstantId; }
            }

            return -1;
        }
        public Books Create(string name,string author,int copies)
        {
            Books newBook = new Books(name, author, copies);
            _book.Add(newBook);
            UpdateBookFile();
            return newBook;
        }
        public void DecreaseCopies(string bookName)
        {
            int index=_book.FindIndex(x => x.Name == bookName);
            if (index > -1)
            {
                
               
                _book[index].Copies--;
               
            }
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