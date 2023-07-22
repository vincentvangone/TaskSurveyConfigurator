
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

namespace SurveyConfigurator
{
    public partial class formSurveyConfigurator : Form
    {
        public formSurveyConfigurator()
        {
            try
            {
                InitializeComponent();

               
            }
            catch(Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }


        }
        private void formSurveyConfigurator_Load(object sender, EventArgs e)
        {
            try
            {
                Form Connect = new DatabaseConnection();
                Connect.ShowDialog();
                if (!Logic.CanConnect())
                {

                    buttonAdd.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    System.Windows.Forms.MessageBox.Show("Cant connect");
                }

                ViewQuestions();

                buttonDelete.Enabled = false;
                buttonEdit.Enabled = false;
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

                List<clsMergedQuestions> Questions = Logic.ViewQuestions();
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

                    //rearrange columns
                    dataGridViewQuestions.Columns["Id"].DisplayIndex = 0;
                    dataGridViewQuestions.Columns["Type"].DisplayIndex = 1;
                    dataGridViewQuestions.Columns["Text"].DisplayIndex = 2;
                    dataGridViewQuestions.Columns["Properties"].DisplayIndex = 3;
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

                Inputs AddQuestion = new Inputs();
                AddQuestion.ShowDialog();
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

                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete question " + Id + " ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK)
                {
                    ErrorMessage(Logic.DeleteQuestion(Id));
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
                Edit.Edit(int.Parse(this.dataGridViewQuestions.SelectedRows[0].Cells[1].Value.ToString()));
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
            try { 
            Form Connect = new DatabaseConnection();
            Connect.ShowDialog();
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }
    }
}
