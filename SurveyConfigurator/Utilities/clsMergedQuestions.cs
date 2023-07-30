using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilities
{
    public class clsMergedQuestions : clsQuestion
    {
        private string properties;

        public string Properties
        {
            get { return properties; }
            set { properties = value; }
        }

    }
}
