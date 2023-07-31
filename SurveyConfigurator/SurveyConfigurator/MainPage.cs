
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
using BusinessLayer;
using System.Data.SqlClient;
using System.Configuration;
using Utilities;
using System.Security.Cryptography;
using System.Threading;
using System.IO;
using System.Security.AccessControl;
using System.Globalization;
using SurveyConfigurator.Properties;
using System.Resources;
using System.Windows.Controls.Primitives;

namespace SurveyConfigurator
{
    public partial class formSurveyConfigurator : Form
    {
        private DatabaseConnection Connectionform = new DatabaseConnection();
        private Inputs InputsForm = new Inputs();
        private Inputs InputsEditForm = new Inputs();
        private Logic LogicLayer = new Logic();
        private List<clsMergedQuestions> Questions;
        private static bool FirstTimeRun = true;
        public event EventHandler LanguageChanged;


        // Boolean flag to keep track of the sorting order
        private bool OrderAscending = true;

        public formSurveyConfigurator()
        {
            try
            {
                InitializeComponent();
                //subscribe to events
                Connectionform.E_ResetConnection += DatabaseConnection_ResetConnection; 
                LogicLayer.E_RequestUIUpdate += Logic_RequestUIUpdate;


                // ViewQuestions();
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }


        }


        protected virtual void OnLanguageChanged(EventArgs e)
        {
            try { 
            // Raise the LanguageChanged event in other forms
            LanguageChanged?.Invoke(this, e);
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }


        private void DatabaseConnection_ResetConnection(object sender, EventArgs e)
        {
            try
            {
                Connectionform.Close();
                Cursor.Current = Cursors.WaitCursor;
                formSurveyConfigurator_Load(this, null);
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void Logic_RequestUIUpdate(object sender, EventArgs e)
        {
            try
            {
                ViewQuestions();
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }
         

        private void formSurveyConfigurator_Load(object sender, EventArgs e)
        {
            try
            {
                //temporarily disabling the event
                comboBoxLanguage.SelectedIndexChanged -= comboBoxLanguage_SelectedIndexChanged;
                if(Thread.CurrentThread.CurrentUICulture.Name==clsConstants.AR)
                    comboBoxLanguage.SelectedItem = clsConstants.ARABIC;
                else
                {
                    comboBoxLanguage.SelectedItem = clsConstants.ENGLISH;
                }
                //turning the evnt back on
                comboBoxLanguage.SelectedIndexChanged += comboBoxLanguage_SelectedIndexChanged;

                if (ConfigurationManager.ConnectionStrings["CONNECTION"].ToString() == "")
                {

                    Connectionform.ShowDialog();
                }

                if (!this.LogicLayer.CanConnect())
                {
                    Cursor.Current = Cursors.Default;
                    dataGridViewQuestions.DataSource = null;
                    dataGridViewQuestions.Rows.Clear();
                    buttonAdd.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    labelConnectionCheck.Visible = true;
                    if (FirstTimeRun == false)
                        MessageBox.Show(clsConstants.FAILED_DATABASE_CONNECTION_STRING);
                }
                else
                {
                    labelConnectionCheck.Visible = false;
                    Cursor.Current = Cursors.Default;
                    ViewQuestions();
                    buttonAdd.Enabled = true;
                }

                FirstTimeRun = false;
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }


        }



        public void ViewQuestions()
        {
            try
            {
                Questions = this.LogicLayer.ViewQuestions();

                //if no questions in the database
                if (!Questions.Any())
                {
                    buttonDelete.Enabled = false;
                    buttonEdit.Enabled = false;
                }
                else
                {

                    buttonDelete.Enabled = true;
                    buttonEdit.Enabled = true;

                    dataGridViewQuestions.DataSource = Questions;
                    dataGridViewQuestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewQuestions.MultiSelect = false;


                    //rearrange columns and hide id
                    dataGridViewQuestions.Columns[clsConstants.ID].Visible = false;
                    dataGridViewQuestions.Columns[clsConstants.ORDER].DisplayIndex = 0;
                    dataGridViewQuestions.Columns[clsConstants.TYPE].DisplayIndex = 1;
                    dataGridViewQuestions.Columns[clsConstants.TEXT].DisplayIndex = 2;
                    dataGridViewQuestions.Columns[clsConstants.PROPERTIES].DisplayIndex = 3;

                    TranslateData();



                    

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void TranslateData()
        {
            try
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == clsConstants.AR)
                {
                   

                    foreach (DataGridViewRow Row in dataGridViewQuestions.Rows)
                    {
                        Row.Cells[clsConstants.TYPE].Value = Resources.ResourceManager.GetString(Row.Cells[clsConstants.TYPE].Value.ToString(), new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
                        string[] SplitProperties = Row.Cells[clsConstants.PROPERTIES].Value.ToString().Split(' ');
                        if (Resources.ResourceManager.GetString(Row.Cells[clsConstants.TYPE].Value.ToString()) == clsConstants.SLIDER)
                        {
                            Row.Cells[clsConstants.PROPERTIES].Value = String.Format(Resources.ResourceManager.GetString(clsConstants.SLIDER_PROPERTIES, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name)), SplitProperties[clsConstants.START_VALUE_INDEX], SplitProperties[clsConstants.START_CAPTION_INDEX], SplitProperties[clsConstants.END_VALUE_INDEX], SplitProperties[clsConstants.END_CAPTION_INDEX]);

                        }
                        else if (Resources.ResourceManager.GetString(Row.Cells[clsConstants.TYPE].Value.ToString()) == clsConstants.SMILEY)
                        {
                            Row.Cells[clsConstants.PROPERTIES].Value = String.Format(Resources.ResourceManager.GetString(clsConstants.NUMBER_OF_SMILEYS, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name)), SplitProperties[clsConstants.SMILEYS_STARS_INDEX]);

                        }
                        else if (Resources.ResourceManager.GetString(Row.Cells[clsConstants.TYPE].Value.ToString()) == clsConstants.STAR)
                        {
                            Row.Cells[clsConstants.PROPERTIES].Value = String.Format(Resources.ResourceManager.GetString(clsConstants.NUMBER_OF_STARS, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name)), SplitProperties[clsConstants.SMILEYS_STARS_INDEX]);

                        }

                       
                    }
                    

                }

                dataGridViewQuestions.Columns[clsConstants.ORDER_COLUMN_INDEX].HeaderText = Resources.ResourceManager.GetString(clsConstants.ORDER, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
                dataGridViewQuestions.Columns[clsConstants.TYPE].HeaderText = Resources.ResourceManager.GetString(clsConstants.TYPE, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
                dataGridViewQuestions.Columns[clsConstants.TEXT].HeaderText = Resources.ResourceManager.GetString(clsConstants.TEXT, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
                dataGridViewQuestions.Columns[clsConstants.PROPERTIES].HeaderText = Resources.ResourceManager.GetString(clsConstants.PROPERTIES, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));

            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }
            
        
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                InputsForm.ShowDialog();
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {

                int Id = int.Parse(this.dataGridViewQuestions.SelectedRows[0].Cells[1].Value.ToString());

                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(clsConstants.DELETE_QUESTION_CONFIRM_STRING, clsConstants.WARNING, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK)
                {
                    this.LogicLayer.DeleteQuestion(Id);
                }

            }

            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                InputsEditForm.Edit(int.Parse(this.dataGridViewQuestions.SelectedRows[0].Cells[1].Value.ToString()), this.dataGridViewQuestions.SelectedRows[0].Cells[2].Value.ToString());
                InputsEditForm.ShowDialog();
            }

            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void dataGridViewQuestions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    buttonDelete.Enabled = true;
                    buttonEdit.Enabled = true;
                }
            }

            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }


        }

        private void dataGridViewQuestions_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (Questions != null)
                {

                    // Sort the DataGridView by the clicked column
                    if (OrderAscending)
                    {
                        if (e.ColumnIndex == clsConstants.PROPERTIES_COLUMN_INDEX)
                        {
                            Questions = Questions.OrderBy(obj => obj.Properties).ToList();
                        }
                        else if (e.ColumnIndex == clsConstants.TYPE_COLUMN_INDEX)
                        {
                            Questions = Questions.OrderBy(obj => obj.Type).ToList();
                        }
                        else if (e.ColumnIndex == clsConstants.TEXT_COLUMN_INDEX)
                        {
                            Questions = Questions.OrderBy(obj => obj.Text).ToList();
                        }
                        else if (e.ColumnIndex == clsConstants.ORDER_COLUMN_INDEX)
                        {
                            Questions = Questions.OrderBy(obj => obj.Order).ToList();
                        }
                        OrderAscending = false;
                    }
                    else
                    {
                        if (e.ColumnIndex == clsConstants.PROPERTIES_COLUMN_INDEX)
                        {
                            Questions = Questions.OrderByDescending(obj => obj.Properties).ToList();
                        }
                        else if (e.ColumnIndex == clsConstants.TYPE_COLUMN_INDEX)
                        {
                            Questions = Questions.OrderByDescending(obj => obj.Type).ToList();
                        }
                        else if (e.ColumnIndex == clsConstants.TEXT_COLUMN_INDEX)
                        {
                            Questions = Questions.OrderByDescending(obj => obj.Text).ToList();
                        }
                        else if (e.ColumnIndex == clsConstants.ORDER_COLUMN_INDEX)
                        {
                            Questions = Questions.OrderByDescending(obj => obj.Order).ToList();
                        }
                        OrderAscending = true; // Set the flag for ascending order
                    }
                   
                    dataGridViewQuestions.DataSource = null;
                    dataGridViewQuestions.DataSource = Questions;
                    dataGridViewQuestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewQuestions.MultiSelect = false;


                    //rearrange columns and hide id
                    dataGridViewQuestions.Columns[clsConstants.ID].Visible = false;
                    dataGridViewQuestions.Columns[clsConstants.ORDER].DisplayIndex = 0;
                    dataGridViewQuestions.Columns[clsConstants.TYPE].DisplayIndex = 1;
                    dataGridViewQuestions.Columns[clsConstants.TEXT].DisplayIndex = 2;
                    dataGridViewQuestions.Columns[clsConstants.PROPERTIES].DisplayIndex = 3;

                    dataGridViewQuestions.Columns[clsConstants.ORDER_COLUMN_INDEX].HeaderText = Resources.ResourceManager.GetString(clsConstants.ORDER, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
                    dataGridViewQuestions.Columns[clsConstants.TYPE].HeaderText = Resources.ResourceManager.GetString(clsConstants.TYPE, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
                    dataGridViewQuestions.Columns[clsConstants.TEXT].HeaderText = Resources.ResourceManager.GetString(clsConstants.TEXT, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
                    dataGridViewQuestions.Columns[clsConstants.PROPERTIES].HeaderText = Resources.ResourceManager.GetString(clsConstants.PROPERTIES, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));



                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {

            try
            {
                Connectionform.ShowDialog();

            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Controls.Clear();
                
                
                switch (comboBoxLanguage.SelectedItem.ToString())
                {

                    case clsConstants.ENGLISH:

                        RightToLeftLayout = false;
                        this.RightToLeft = RightToLeft.No;
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(clsConstants.ENG);
                        break;
                    case clsConstants.ARABIC:


                        //MessageBox.Show(Resources.ResourceManager.GetString("Smiley",new CultureInfo(clsConstants.AR)));
                       
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(clsConstants.AR);

                        RightToLeftLayout = true;
                        this.RightToLeft = RightToLeft.Yes;
                        break;
                }
                InputsForm.OnLanguageChanged();
                InputsEditForm.OnLanguageChanged();
                InitializeComponent();
                formSurveyConfigurator_Load(this, null);

            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }
    }
}

