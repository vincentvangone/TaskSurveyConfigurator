using BusinessLayer;
using ErrorLogger;
using SurveyConfigurator.UserControls;
using System;
using System.Windows.Forms;
using Utilities;

namespace SurveyConfigurator
{
    public partial class Inputs : Form
    {
        private int Id;

        public clsQuestionSmiley QuestionSmiley = new clsQuestionSmiley();
        public clsQuestionStar QuestionStar = new clsQuestionStar();
        public clsQuestionSlider QuestionSlider = new clsQuestionSlider();


        ExtraInputSmiley ucSmiley = new ExtraInputSmiley();
        ExtraInputStar ucStar = new ExtraInputStar();
        ExtraInputSlider ucSlider = new ExtraInputSlider();

        public Inputs()
        {
            try
            {
                InitializeComponent();
                comboBoxType.SelectedIndex = 0;
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        //user control addition -> switching panel content depending on user's choice
        private void AddUserControls(UserControl userControl)
        {
            try
            {
                userControl.Dock = DockStyle.Fill;
                panelExtraInputs.Controls.Clear();
                panelExtraInputs.Controls.Add(userControl);
                userControl.BringToFront();
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }


        public void Edit(int Id)
        {
            try
            {
                //set Id to the sent one so we can use it in the other functions
                this.Id = Id;

                this.Text = "Edit Question";
                textBoxText.Text = Logic.GetText(Id);
                comboBoxType.SelectedItem = Logic.GetType(Id);
                comboBoxType.Enabled = false;

                switch (comboBoxType.SelectedItem.ToString())
                {
                    case clsConstants.SMILEY:
                        this.ucSmiley.NumberOfSmileys = Logic.GetNumberOfSmileys(Id);
                        break;
                    case clsConstants.STAR:
                        this.ucStar.NumberOfStars = Logic.GetNumberOfStars(Id);
                        break;
                    case clsConstants.SLIDER:
                        this.ucSlider.StartValue = Logic.GetStartValue(Id);
                        this.ucSlider.EndValue = Logic.GetEndValue(Id);
                        this.ucSlider.StartCaption = Logic.GetStartCaption(Id);
                        this.ucSlider.EndCaption = Logic.GetEndCaption(Id);
                        break;

                }
            }

            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //return code from other layers
                int Result;
                //if the question already exists we need to update it not create a new one
                //when we're editing the combo box is disabled so thats our flag
                if (!(comboBoxType.Enabled))
                {

                    switch (comboBoxType.SelectedItem.ToString())
                    {
                        case clsConstants.SMILEY:
                            QuestionSmiley.Text = textBoxText.Text;
                            QuestionSmiley.NumberOfSmileys = ucSmiley.NumberOfSmileys;
                            Result = BusinessLayer.Logic.SmileyInputValidation(QuestionSmiley, Id);
                            ErrorMessage(Result);
                            if (Result == 1)
                                this.Close();
                            break;

                        case clsConstants.STAR:
                            QuestionStar.Text = textBoxText.Text;
                            QuestionStar.NumberOfStars = ucStar.NumberOfStars;
                            Result = BusinessLayer.Logic.StarsInputValidation(QuestionStar, Id);
                            ErrorMessage(Result);
                            if (Result == 1)
                                this.Close();
                            break;

                        case clsConstants.SLIDER:
                            QuestionSlider.Text = textBoxText.Text;
                            QuestionSlider.StartValue = ucSlider.StartValue;
                            QuestionSlider.EndValue = ucSlider.EndValue;
                            QuestionSlider.StartCaption = ucSlider.StartCaption;
                            QuestionSlider.EndCaption = ucSlider.EndCaption;
                            Result = BusinessLayer.Logic.SliderInputValidation(QuestionSlider, Id);
                            ErrorMessage(Result);
                            if (Result == 1)
                                this.Close();
                            break;

                    }

                }
                //if the question is new send id=-1
                else
                {
                    switch (comboBoxType.SelectedItem.ToString())
                    {
                        case clsConstants.SMILEY:

                            QuestionSmiley.Text = textBoxText.Text;
                            QuestionSmiley.NumberOfSmileys = ucSmiley.NumberOfSmileys;
                            Result = BusinessLayer.Logic.SmileyInputValidation(QuestionSmiley, -1);
                            ErrorMessage(Result);
                            if (Result == 1)
                                this.Close();
                            break;
                        case clsConstants.STAR:

                            QuestionStar.Text = textBoxText.Text;
                            QuestionStar.NumberOfStars = ucStar.NumberOfStars;
                            Result = BusinessLayer.Logic.StarsInputValidation(QuestionStar, -1);
                            ErrorMessage(Result);
                            if (Result == 1)
                                this.Close();
                            break;
                        case clsConstants.SLIDER:
                            QuestionSlider.Text = textBoxText.Text;
                            QuestionSlider.StartValue = ucSlider.StartValue;
                            QuestionSlider.EndValue = ucSlider.EndValue;
                            QuestionSlider.StartCaption = ucSlider.StartCaption;
                            QuestionSlider.EndCaption = ucSlider.EndCaption;
                            Result = BusinessLayer.Logic.SliderInputValidation(QuestionSlider, -1);
                            ErrorMessage(Result);
                            if (Result == 1)
                                this.Close();
                            break;

                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try 
            {
                this.Close();
            }  
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            } 
        }


        public void ErrorMessage(int ErrorCode)
        {
            string Result= ErrorStrings(ErrorCode);
            if (Result != "") 
                MessageBox.Show(Result,clsConstants.ERROR,MessageBoxButtons.OK,MessageBoxIcon.Error);
        }


        public static string ErrorStrings(int Result)
        {
            switch (Result)
            {
                case clsConstants.SUCCESS:
                    return "";
                case clsConstants.TYPE_NOT_SELECTED:
                    return clsConstants.TYPE_NOT_SELECTED_STRING;
                case clsConstants.FAILED_DATABASE_CONNECTION:
                    return clsConstants.FAILED_DATABASE_CONNECTION_STRING;
                case clsConstants.TEXT_NOT_SPECIFIED:
                    return clsConstants.TEXT_NOT_SPECIFIED_STRING;
                case clsConstants.FAILED_NEW_QUESTION:
                    return clsConstants.FAILED_NEW_QUESTION_STRING;
                case clsConstants.FAILED_DELETE_QUESTION:
                    return clsConstants.FAILED_DELETE_QUESTION_STRING;
                case clsConstants.FAILED_EDIT_QUESTION:
                    return clsConstants.FAILED_EDIT_QUESTION_STRING;
                case clsConstants.INVALID_NUMBER_OF_SMILEYS:
                    return clsConstants.INVALID_NUMBER_OF_SMILEYS_STRING;
                case clsConstants.INVALID_NUMBER_OF_STARS:
                    return clsConstants.INVALID_NUMBER_OF_STARS_STRING;
                case clsConstants.INVALID_START_VALUE:
                    return clsConstants.INVALID_START_VALUE_STRING;
                case clsConstants.INVALID_END_VALUE:
                    return clsConstants.INVALID_END_VALUE_STRING;
                case clsConstants.INVALID_END_LESS_THAN_START_VALUE:
                    return clsConstants.INVALID_END_LESS_THAN_START_VALUE_STRING;
                case clsConstants.INVALID_START_CAPTION:
                    return clsConstants.INVALID_START_CAPTION_STRING;
                case clsConstants.INVALID_END_CAPTION:
                    return clsConstants.INVALID_END_CAPTION_STRING;
                default:
                    return "Not Found.";
            }
        }
    }
}

