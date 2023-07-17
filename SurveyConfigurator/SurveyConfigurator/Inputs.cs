using ErrorLogger;
using SurveyConfigurator.UserControls;
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
using BusinessLayer;

namespace SurveyConfigurator
{
    public partial class Inputs : Form
    {
        private int Id;

        public clsQuestionSmiley QuestionSmiley = new clsQuestionSmiley();
        public clsQuestionStar QuestionStar = new clsQuestionStar();
        public  clsQuestionSlider QuestionSlider = new clsQuestionSlider();


        ExtraInputSmiley ucSmiley = new ExtraInputSmiley();
        ExtraInputStar ucStar = new ExtraInputStar();
        ExtraInputSlider ucSlider = new ExtraInputSlider();

        public Inputs()
        {
            InitializeComponent();
            buttonSave.Enabled = false;
        }

        //user control addition -> switching panel content depending on user's choice
        private void AddUserControls(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelExtraInputs.Controls.Clear();
            panelExtraInputs.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch (comboBoxType.SelectedItem.ToString())
            {
                case clsConstants.SMILEY:
                    QuestionSmiley.Type = clsConstants.SMILEY;
                    AddUserControls(ucSmiley);
                    break;
                case clsConstants.STAR:
                    QuestionStar.Type = clsConstants.STAR;
                    AddUserControls(ucStar);
                    break;
                case clsConstants.SLIDER:
                    QuestionSlider.Type = clsConstants.SLIDER;
                    AddUserControls(ucSlider);
                    break;

            }
        }


        public void Edit(int Id)
        {
            //set Id to the sent one so we can use it in the other functions
            this.Id=Id;

            this.Text = "Edit Question";
            textBoxText.Text =  Validation.GetText(Id);
            comboBoxType.SelectedItem = Validation.GetType(Id);
            comboBoxType.Enabled = false;
           
            switch (comboBoxType.SelectedItem.ToString())
            {
                case clsConstants.SMILEY:
                    this.ucSmiley.NumberOfSmileys = Validation.GetNumberOfSmileys(Id);
                    break;
                case clsConstants.STAR:
                    this.ucStar.NumberOfStars = Validation.GetNumberOfStars(Id);
                    break;
                case clsConstants.SLIDER:
                    this.ucSlider.StartValue = Validation.GetStartValue(Id);
                    this.ucSlider.EndValue = Validation.GetEndValue(Id);
                    this.ucSlider.StartCaption = Validation.GetStartCaption(Id);
                    this.ucSlider.EndCaption = Validation.GetEndCaption(Id);
                    break;

            }
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //return code from other layers
            int Code;
            //if the question already exists we need to update it not create a new one
            //when we're editing the combo box is disabled so thats our flag
            if (!(comboBoxType.Enabled))
            {
                
                switch (comboBoxType.SelectedItem.ToString())
                {
                    case clsConstants.SMILEY:
                        QuestionSmiley.Text = textBoxText.Text;
                        QuestionSmiley.NumberOfSmileys = ucSmiley.NumberOfSmileys;
                        Code = BusinessLayer.Validation.SmileyInputValidation(QuestionSmiley, Id, false);
                        ErrorMessage(Code);
                        if (Code == 1)
                            this.Close();
                        break;

                    case clsConstants.STAR:
                        QuestionStar.Text = textBoxText.Text;
                        QuestionStar.NumberOfStars = ucStar.NumberOfStars;
                        Code =BusinessLayer.Validation.StarsInputValidation(QuestionStar,Id,false);
                        ErrorMessage(Code);
                        if (Code == 1)
                            this.Close();
                        break;

                    case clsConstants.SLIDER:
                        QuestionSlider.Text = textBoxText.Text;
                        QuestionSlider.StartValue = ucSlider.StartValue;
                        QuestionSlider.EndValue = ucSlider.EndValue;
                        QuestionSlider.StartCaption = ucSlider.StartCaption;
                        QuestionSlider.EndCaption = ucSlider.EndCaption;
                        Code = BusinessLayer.Validation.SliderInputValidation(QuestionSlider,Id,false);
                        ErrorMessage(Code);
                        if (Code == 1)
                            this.Close();
                        break;

                }
            }
            else
            {
                switch (comboBoxType.SelectedItem.ToString())
                {
                    case clsConstants.SMILEY:
                        
                        QuestionSmiley.Text = textBoxText.Text;
                        QuestionSmiley.NumberOfSmileys = ucSmiley.NumberOfSmileys;
                        Code=BusinessLayer.Validation.SmileyInputValidation(QuestionSmiley);
                        ErrorMessage(Code);
                        if (Code == 1)
                            this.Close();
                        break;
                    case clsConstants.STAR:
                        
                        QuestionStar.Text = textBoxText.Text;
                        QuestionStar.NumberOfStars = ucStar.NumberOfStars;
                        Code=BusinessLayer.Validation.StarsInputValidation(QuestionStar);
                        ErrorMessage(Code);
                        if (Code == 1)
                            this.Close();
                        break;
                    case clsConstants.SLIDER:
                        QuestionSlider.Text = textBoxText.Text;
                        QuestionSlider.StartValue = ucSlider.StartValue;
                        QuestionSlider.EndValue = ucSlider.EndValue;
                        QuestionSlider.StartCaption = ucSlider.StartCaption;
                        QuestionSlider.EndCaption = ucSlider.EndCaption;
                        Code=BusinessLayer.Validation.SliderInputValidation(QuestionSlider);
                        ErrorMessage(Code);
                        if (Code == 1)
                            this.Close();
                        break;

                }
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxText_TextChanged(object sender, EventArgs e)
        {
            if (!(textBoxText.Text==null)) buttonSave.Enabled = true;
        }

        public void ErrorMessage(int ErrorCode)
        {
            MessageBox.Show(clsConstants.ErrorStrings(ErrorCode));
        }
    }
}
