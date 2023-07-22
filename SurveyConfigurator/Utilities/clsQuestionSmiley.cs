using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class clsQuestionSmiley :clsQuestion
    {
        private int numberOfSmileys;
        public int NumberOfSmileys
        {
            get { return numberOfSmileys; } 
            set { numberOfSmileys = value; }
        }

    }
}
