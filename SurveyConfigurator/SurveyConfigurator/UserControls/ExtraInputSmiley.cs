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
    public partial class ExtraInputSmiley : UserControl
    {
        public ExtraInputSmiley()
        {
            InitializeComponent();
        }

        public int NumberOfSmileys
        {
            get { return Convert.ToInt32(numericSmileys.Value); }
            set { numericSmileys.Value = value;}
        }
    }
}
