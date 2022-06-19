using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BI
{
    public class Books
    {
        
        public string Name;
        public string Publisher;
        public int Copies;
        public int Id;
        public Books(int id,string name, string publisher, int copy)
        {
            Id = id;
            Name = name;
            Publisher = publisher;
            Copies = copy;
        }

    }
}
