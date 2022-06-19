using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BI;
using Microsoft.VisualBasic.FileIO;


namespace LMS.Bl.file
{

    public sealed class Utilities
    {
        private Utilities() { }
        private static Utilities instance = null;
        public static Utilities Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Utilities();
                }
                return instance;
            }
        }
        public List<UserInfo> ReadUsersFile(string paths, List<UserInfo> _users)
        {
            using (TextFieldParser csvParser = new TextFieldParser(paths))
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
                return _users;

            }

        }
        public List<Books> ReadBookFile(string paths,List<Books> _book)
        {
            using (TextFieldParser csvParser = new TextFieldParser(paths))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;
                string[] fields;
                while (!csvParser.EndOfData)
                {
                     fields = csvParser.ReadFields();
                    _book.Add(new Books(fields[0], fields[1], int.Parse(fields[2])));
                    if (fields[2] == "0") { continue; };
                    
                }
                return _book;
                
            }
        }

    }
    
}
