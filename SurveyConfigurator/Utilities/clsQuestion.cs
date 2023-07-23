using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class clsQuestion
    {
        private string type;
        private string text;
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

    }
}
