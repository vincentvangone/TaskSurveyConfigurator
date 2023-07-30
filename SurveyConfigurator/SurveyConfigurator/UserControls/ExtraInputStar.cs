using ErrorLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace SurveyConfigurator.UserControls
{
    public partial class ExtraInputStar : UserControl
    {
        public ExtraInputStar()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        public int NumberOfStars
        {
            get { return Convert.ToInt32(numericStars.Value); }
            set { numericStars.Value = value; }
        }

        public void InitializeStar()
        {
            this.Controls.Clear();
            InitializeComponent();
        }
        private void ExtraInputStar_Load(object sender, EventArgs e)
        {
            //numericStars.Value = 10;
        }
    }
}
