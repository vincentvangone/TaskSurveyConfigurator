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
    public partial class ExtraInputSlider : UserControl
    {
        public ExtraInputSlider()
        {
            InitializeComponent();
        }

        public int StartValue
        {
            get { return Convert.ToInt32(numericStartValue.Value); }
            set { numericStartValue.Value = value; }
        }

        public int EndValue
        {
            get { return Convert.ToInt32(numericEndValue.Value); }
            set { numericEndValue.Value = value; }
        }

        public string StartCaption
        {
            get { return textBoxStartCaption.Text;}
            set { textBoxStartCaption.Text = value;}
        }

        public string EndCaption
        {
            get { return textBoxEndCaption.Text; }
            set { textBoxEndCaption.Text = value;}
        }
        private void textBoxEndCaption_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericStars_ValueChanged(object sender, EventArgs e)
        {

        }

        private void labelStartValue_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void labelEndValue_Click(object sender, EventArgs e)
        {

        }

        private void labelStartCaption_Click(object sender, EventArgs e)
        {

        }

        private void labelEndCaption_Click(object sender, EventArgs e)
        {

        }

        private void textBoxStartCaption_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExtraInputSlider_Load(object sender, EventArgs e)
        {

        }
    }
}
