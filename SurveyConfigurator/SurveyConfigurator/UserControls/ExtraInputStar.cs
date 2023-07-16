using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurveyConfigurator.UserControls
{
    public partial class ExtraInputStar : UserControl
    {
        public ExtraInputStar()
        {
            InitializeComponent();
        }

        public int NumberOfStars
        {
            get { return Convert.ToInt32(numericStars.Value); }
            set { numericStars.Value = value; }
        }
    }
}
