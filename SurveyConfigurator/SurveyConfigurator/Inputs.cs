using BusinessLayer;
using ErrorLogger;
using SurveyConfigurator.Properties;
using SurveyConfigurator.UserControls;
using System;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Threading;
//using System.Windows.Controls;
using System.Windows.Forms;
using Utilities;


namespace SurveyConfigurator
{
    public partial class Inputs : Form
    {
        private int Id;

        Logic LogicLayer = new Logic();

        public clsQuestionSmiley QuestionSmiley = new clsQuestionSmiley();
        public clsQuestionStar QuestionStar = new clsQuestionStar();
        public clsQuestionSlider QuestionSlider = new clsQuestionSlider();


        ExtraInputSmiley ucSmiley = new ExtraInputSmiley();
        ExtraInputStar ucStar = new ExtraInputStar();
        ExtraInputSlider ucSlider = new ExtraInputSlider();



        // Define an event to reload the main form when saved
        public event EventHandler<EventArgs> E_RequestMainFormUpdate;

        public Inputs()
        {
            try
            {
                
                InitializeComponent();
                LoadComboBoxItems();


            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void Inputs_Load(object sender, EventArgs e)
        {
            try { 
                if (comboBoxType.Enabled)
                {
                    comboBoxType.SelectedIndex = 0;
                    numericUpDownOrder.Value = 1;
                    textBoxText.Text = string.Empty;
                }
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


        public void OnLanguageChanged()
        {
            try
            {

                if (Thread.CurrentThread.CurrentUICulture.Name == clsConstants.AR)
                {
                    RightToLeftLayout = true;
                    this.RightToLeft = RightToLeft.Yes;
                    buttonCancel.Dock = DockStyle.Left;
                    panelDummy.Dock = DockStyle.Left;
                    buttonSave.Dock = DockStyle.Left;
                }
                if (Thread.CurrentThread.CurrentUICulture.Name == clsConstants.ENG)
                {
                    RightToLeftLayout = false;
                    this.RightToLeft = RightToLeft.No;
                }
                this.Controls.Clear();
                ucSlider.InitializeSlider();
                ucSmiley.InitializeSmiley();
                ucStar.InitializeStar();
                InitializeComponent();
                LoadComboBoxItems();
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
                switch (Resources.ResourceManager.GetString(comboBoxType.SelectedItem.ToString(), new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name) ))
                {
                    case clsConstants.SMILEY:
                        QuestionSmiley.Type = clsConstants.SMILEY;
                        AddUserControls(ucSmiley);
                        //resetting form
                        ucSmiley.NumberOfSmileys = 5;
                        break;
                    case clsConstants.STAR:
                        QuestionStar.Type = clsConstants.STAR;
                        AddUserControls(ucStar);
                        //resetting form
                        ucStar.NumberOfStars = 10;
                        break;
                    case clsConstants.SLIDER:
                        QuestionSlider.Type = clsConstants.SLIDER;
                        AddUserControls(ucSlider);
                        //resetting form
                        ucSlider.EndCaption = "";
                        ucSlider.StartCaption = "";
                        ucSlider.StartValue = 0;
                        ucSlider.EndValue = 100;
                        break;

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }


        public void Edit(int Id,string Type)
        {
            try
            {
               
                //set Id to the sent one so we can use it in the other functions
                this.Id = Id;

                this.Text = "Edit Question";
                //textBoxText.Text = LogicLayer.GetText(Id);
                comboBoxType.SelectedItem = Type; 
                comboBoxType.Enabled = false;

                switch (Resources.ResourceManager.GetString(Type, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name)))
                {
                    case clsConstants.SMILEY:
                        QuestionSmiley.Id= Id;
                        QuestionSmiley.Type = clsConstants.SMILEY;
                        LogicLayer.GetSmileyQuestion(QuestionSmiley);
                        textBoxText.Text = QuestionSmiley.Text;
                        numericUpDownOrder.Value = QuestionSmiley.Order;
                        ucSmiley.NumberOfSmileys = QuestionSmiley.NumberOfSmileys;
                        break;
                    case clsConstants.STAR:
                        QuestionStar.Id = Id;
                        QuestionStar.Type = clsConstants.STAR;
                        LogicLayer.GetStarsQuestion(QuestionStar);
                        textBoxText.Text = QuestionStar.Text;
                        numericUpDownOrder.Value = QuestionStar.Order;
                        ucStar.NumberOfStars = QuestionStar.NumberOfStars;
                        break;
                    case clsConstants.SLIDER:
                        QuestionSlider.Id = Id;
                        this.QuestionSlider.Type = clsConstants.SLIDER;
                        LogicLayer.GetSliderQuestion(QuestionSlider);
                        numericUpDownOrder.Value = QuestionSlider.Order;
                        textBoxText.Text = QuestionSlider.Text;
                        ucSlider.StartValue = QuestionSlider.StartValue;
                        ucSlider.EndValue = QuestionSlider.EndValue;
                        ucSlider.StartCaption = QuestionSlider.StartCaption;
                        ucSlider.EndCaption = QuestionSlider.EndCaption;
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

                    switch (Resources.ResourceManager.GetString(comboBoxType.SelectedItem.ToString(), new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name)))
                    {
                        case clsConstants.SMILEY:
                            QuestionSmiley.Order = (int)numericUpDownOrder.Value;
                            QuestionSmiley.Text = textBoxText.Text;
                            QuestionSmiley.NumberOfSmileys = ucSmiley.NumberOfSmileys;
                            Result = LogicLayer.SmileyInputValidation(QuestionSmiley, Id);
                            ErrorMessage(Result);
                            if (Result == 1)
                            {
                                this.Close();
                            }
                            
                            break;

                        case clsConstants.STAR:
                            QuestionStar.Order = (int)numericUpDownOrder.Value;
                            QuestionStar.Text = textBoxText.Text;
                            QuestionStar.NumberOfStars = ucStar.NumberOfStars;
                            Result =LogicLayer.StarsInputValidation(QuestionStar, Id);
                            ErrorMessage(Result);
                            if (Result == 1)
                            {
                                this.Close();
                            }
                            break;

                        case clsConstants.SLIDER:
                            QuestionSlider.Order = (int)numericUpDownOrder.Value;
                            QuestionSlider.Text = textBoxText.Text;
                            QuestionSlider.StartValue = ucSlider.StartValue;
                            QuestionSlider.EndValue = ucSlider.EndValue;
                            QuestionSlider.StartCaption = ucSlider.StartCaption;
                            QuestionSlider.EndCaption = ucSlider.EndCaption;
                            Result = LogicLayer.SliderInputValidation(QuestionSlider, Id);
                            ErrorMessage(Result);
                            if (Result == 1) {
                                this.Close();
                            }
                            break;

                    }

                }
                //if the question is new send id=-1
                else
                {
                    switch (Resources.ResourceManager.GetString(comboBoxType.SelectedItem.ToString(), new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name)))
                    {
                        case clsConstants.SMILEY:
                            QuestionSmiley.Order = (int)numericUpDownOrder.Value;
                            QuestionSmiley.Text = textBoxText.Text;
                            QuestionSmiley.NumberOfSmileys = ucSmiley.NumberOfSmileys;
                            Result = LogicLayer.SmileyInputValidation(QuestionSmiley, -1);
                            ErrorMessage(Result);
                            if (Result == 1)
                            {
                                this.Close();
                            }
                            break;
                        case clsConstants.STAR:
                            QuestionStar.Order = (int)numericUpDownOrder.Value;
                            QuestionStar.Text = textBoxText.Text;
                            QuestionStar.NumberOfStars = ucStar.NumberOfStars;
                            Result = LogicLayer.StarsInputValidation(QuestionStar, -1);
                            ErrorMessage(Result);
                            if (Result == 1)
                            {
                                this.Close();
                            }
                            break;
                        case clsConstants.SLIDER:
                            QuestionSlider.Order = (int)numericUpDownOrder.Value;
                            QuestionSlider.Text = textBoxText.Text;
                            QuestionSlider.StartValue = ucSlider.StartValue;
                            QuestionSlider.EndValue = ucSlider.EndValue;
                            QuestionSlider.StartCaption = ucSlider.StartCaption;
                            QuestionSlider.EndCaption = ucSlider.EndCaption;
                            Result = LogicLayer.SliderInputValidation(QuestionSlider, -1);
                            ErrorMessage(Result);
                            if (Result == 1)
                            {
                                this.Close();
                            }
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
            try
            {
                string Result = ErrorStrings(ErrorCode);
                if (Result != "")
                    MessageBox.Show(Result, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void LoadComboBoxItems()
        {
            // Get the current culture from the selected language
            CultureInfo Culture = new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name);

            
            // Retrieve the translated values for the ComboBox from the resources file
            string ComboBoxItemSmiley = Resources.ResourceManager.GetString(clsConstants.SMILEY, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
            string ComboBoxItemStar = Resources.ResourceManager.GetString(clsConstants.STAR, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
            string ComboBoxItemSlider = Resources.ResourceManager.GetString(clsConstants.SLIDER, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
            

            // Set the DataSource for the ComboBox
            comboBoxType.Items.Clear();
            comboBoxType.Items.AddRange(new string[] { ComboBoxItemSmiley, ComboBoxItemStar, ComboBoxItemSlider });

            comboBoxType.SelectedIndex = 0;
            // Add more items as needed
        }


        public static string ErrorStrings(int Result)
        {
            try
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
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                return clsConstants.ERROR;
            }
        }


    }
}

