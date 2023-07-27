﻿using System;
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

        private void ExtraInputSlider_Load(object sender, EventArgs e)
        {
           
           
        }
    }
}
