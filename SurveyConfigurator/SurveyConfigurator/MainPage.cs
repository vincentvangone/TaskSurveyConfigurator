
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
using System.Windows;
using System.Windows.Forms;
using BusinessLayer;
using System.Data.SqlClient;
using System.Configuration;
using Utilities;
using System.Security.Cryptography;
using System.Threading;
using System.IO;
using System.Globalization;

namespace SurveyConfigurator
{
    public partial class formSurveyConfigurator : Form
    {

        private Logic LogicLayer = new Logic();



        public formSurveyConfigurator()
        {
            try
            {
                InitializeComponent();

                if (ConfigurationManager.ConnectionStrings["CONNECTION"].ToString()=="") {
                    Form Connect = new DatabaseConnection();
                    Connect.ShowDialog(); }


                //subscribe to event 
                LogicLayer.RequestUIUpdate += Logic_RequestUIUpdate;
                ViewQuestions();
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }


        }

        private void Logic_RequestUIUpdate(object sender, EventArgs e)
        {
                ViewQuestions();
        }

        private void formSurveyConfigurator_Load(object sender, EventArgs e)
        {
            try
            {

                if (!this.LogicLayer.CanConnect())
                {
                    dataGridViewQuestions.DataSource = null;
                    dataGridViewQuestions.Rows.Clear();
                    buttonAdd.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    System.Windows.Forms.MessageBox.Show(clsConstants.FAILED_DATABASE_CONNECTION_STRING);
                }
                else
                {
                    ViewQuestions();
                    buttonAdd.Enabled = true;
                    
                }
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
                Logic.LastUpdateTime = DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture("en-us"));
                List<clsMergedQuestions> Questions = this.LogicLayer.ViewQuestions();
                
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

                }
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

                Inputs NewQuestion = new Inputs();
                NewQuestion.ShowDialog();
                ViewQuestions();
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

                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(clsConstants.DELETE_QUESTION_CONFIRM_STRING + Id + " ?", clsConstants.WARNING, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK)
                {
                    ErrorMessage(this.LogicLayer.DeleteQuestion(Id));
                    ViewQuestions();
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

                Inputs Edit = new Inputs();
                Edit.Edit(int.Parse(this.dataGridViewQuestions.SelectedRows[0].Cells[1].Value.ToString()), this.dataGridViewQuestions.SelectedRows[0].Cells[2].Value.ToString());
                Edit.ShowDialog();

                ViewQuestions();
            }

            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }
        public void ErrorMessage(int ErrorCode)
        {
            //System.Windows.Forms.MessageBox.Show(clsConstants.ErrorStrings(ErrorCode));
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


        private void buttonConnect_Click(object sender, EventArgs e)
        {

            try
            {
                Form Connect = new DatabaseConnection();
                Connect.ShowDialog();
                formSurveyConfigurator_Load(this, null);
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }
    }
}
