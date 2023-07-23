using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class clsQuestionStar : clsQuestion
    {
        private int numberOfStars;

        public int NumberOfStars
        {
            get { return numberOfStars; }
            set { numberOfStars = value; }
        }
    }
}
