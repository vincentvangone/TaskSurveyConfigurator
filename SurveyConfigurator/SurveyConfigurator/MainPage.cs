
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

namespace SurveyConfigurator
{
    public partial class formSurveyConfigurator : Form
    {

        Logic LogicLayer = new Logic();
        public formSurveyConfigurator()
        {
            try
            {
                InitializeComponent();
               
                // Start a background thread or loop to check for updates
                //Task.Run(() => CheckForUpdatesLoop());
                Form Connect = new DatabaseConnection();
                Connect.ShowDialog();

            }
            catch(Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }


        }

        //public void CheckForUpdatesLoop()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            if (LogicLayer.CheckForUpdates() == 1)
        //                ViewQuestions();

        //            //delay to avoid excessive checking and resource usage
        //            Thread.Sleep(1000);
        //        }
        //    }
        //    catch(Exception E)
        //    {
        //        Logger.WriteLog(E.Message,clsConstants.ERROR);
        //    }
        //}

        private void formSurveyConfigurator_Load(object sender, EventArgs e)
        {
            try
            {

                if (!LogicLayer.CanConnect())
                {

                    buttonAdd.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    System.Windows.Forms.MessageBox.Show(clsConstants.FAILED_DATABASE_CONNECTION_STRING);
                }
                else
                {
                    ViewQuestions();
                    buttonAdd.Enabled = true;
                    buttonDelete.Enabled = false;
                    buttonEdit.Enabled = false;
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

                List<clsMergedQuestions> Questions = LogicLayer.ViewQuestions();
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
                Logger.WriteLog(E.Message,clsConstants.ERROR);
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
            catch(Exception E)
            {
                Logger.WriteLog(E.Message,clsConstants.ERROR);
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
                    ErrorMessage(LogicLayer.DeleteQuestion(Id));
                }
                ViewQuestions();
            }
             
            catch(Exception E)
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
             
            catch(Exception E)
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
                buttonDelete.Enabled = true;
                buttonEdit.Enabled = true;
            }
             
            catch(Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }


}

        private void pictureBoxConnect_Click(object sender, EventArgs e)
        {
            try
            {
                Form Connect = new DatabaseConnection();
                Connect.ShowDialog();
                formSurveyConfigurator_Load(this,null);
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
            
        }


    }
}
