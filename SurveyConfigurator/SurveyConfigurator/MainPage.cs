
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
        }


        //temporary because this isnt working on thee database layer
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
            //System.Windows.MessageBox.Show(this.dataGridViewQuestions.SelectedRows[0].Cells[1].Value.ToString());
            Validation.DeleteQuestion(int.Parse(this.dataGridViewQuestions.SelectedRows[0].Cells[0].Value.ToString()), this.dataGridViewQuestions.SelectedRows[0].Cells[2].Value.ToString());
            ListQuestions();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

            Inputs Edit = new Inputs();
            Edit.Edit(int.Parse(this.dataGridViewQuestions.SelectedRows[0].Cells[0].Value.ToString()));
            Edit.ShowDialog();

            ListQuestions();
        }
    }
}
