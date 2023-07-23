using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class clsMergedQuestions:clsQuestion
    {
        private string properties;

        public string Properties
        {
            get { return properties; }
            set { properties = value; }
        }
    }
}
