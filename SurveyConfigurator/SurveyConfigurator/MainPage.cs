
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

namespace SurveyConfigurator
{
    public partial class formSurveyConfigurator : Form
    {
        public formSurveyConfigurator()
        {

            InitializeComponent();


        }
        private void formSurveyConfigurator_Load(object sender, EventArgs e)
        {

            if (!Validation.CanConnect())
            {
                buttonAdd.Enabled = false;
                buttonEdit.Enabled = false;
                buttonDelete.Enabled = false;
            }
            
            ListQuestions();
            
            buttonDelete.Enabled = false;
            buttonEdit.Enabled = false;
        }


    
        public void ListQuestions()
        {
            var DataTable = Validation.ListQuestions();

            //if no questions in the database
            if (DataTable.Rows.Count==0)
            {
                buttonDelete.Enabled = false;
                buttonEdit.Enabled = false;
            }
            else
            {
                buttonDelete.Enabled = true;
                buttonEdit.Enabled = true;
            }
            dataGridViewQuestions.DataSource = DataTable;
            dataGridViewQuestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewQuestions.MultiSelect = false;
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {

            Inputs AddQuestion = new Inputs();
            AddQuestion.ShowDialog();
            ListQuestions();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int Id = int.Parse(this.dataGridViewQuestions.SelectedRows[0].Cells[0].Value.ToString());

            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete question " + Id + " ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.OK)
            {
               Validation.DeleteQuestion(Id);
            }
            ListQuestions();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

            Inputs Edit = new Inputs();
            Edit.Edit(int.Parse(this.dataGridViewQuestions.SelectedRows[0].Cells[0].Value.ToString()));
            Edit.ShowDialog();

            ListQuestions();
        }
        public void ErrorMessage(int ErrorCode)
        {
            System.Windows.Forms.MessageBox.Show(clsConstants.ErrorStrings(ErrorCode));
        }

        private void dataGridViewQuestions_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            buttonDelete.Enabled = true;
            buttonEdit.Enabled = true;


        }
    }
}
