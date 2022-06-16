using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BI
{
    public class Books
    {
        public static int InstantNum = 0;
        public int InstantId;
        public string Name;
        public string Publisher;
        public int Copies;
        public Books(string name, string publisher, int copy)
        {
            InstantId=InstantNum;
            InstantNum++;
            Name = name;
            Publisher = publisher;
            Copies = copy;
        }

    }
}
