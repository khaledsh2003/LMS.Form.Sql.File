
using LMS.BI;
using LMS.BL.Interface;
using Microsoft.VisualBasic.FileIO;

namespace LMS.Bl.file
{
    public class Bookfile: IBookManager
    {
        private List<Books> _book;
        public Bookfile()
        {
            _book = new List<Books>();
            var path = @"C:\Users\kshah\OneDrive\Desktop\AMS\LMS.Bl\Books.csv";
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                int index = 1;
                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    if (fields.Length != 3) { Console.WriteLine("Error occured while reading line " + index + ", the rest of the data after is read"); continue; }

                    _book.Add(new Books(fields[0], fields[1], int.Parse(fields[2])));
                    if (fields[2] == "0") { continue; };
                    index++;
                }
            }
        }
        public List<Books> GetList()
        {
            return _book;
        }
        
        public int GetBook(string bookname)
        {
            foreach(var i in _book)
            {
                if (i.Name == bookname) { return i.InstantId; }
            }

            return -1;
        }
        public void Create(string name,string author,int copies)
        {
            _book.Add(new Books(name, author, copies));
        }
        public void UpdateBookFile()
        {
            var path = @"C:\Users\kshah\OneDrive\Desktop\AMS\LMS.Bl\Books.csv";

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