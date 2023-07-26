
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
using System.Globalization;
using System.Security.AccessControl;

namespace SurveyConfigurator
{
    public partial class formSurveyConfigurator : Form
    {
        private DatabaseConnection Connectionform=new DatabaseConnection();
        private Inputs InputsForm = new Inputs();
        private Inputs InputsEditForm = new Inputs();
        private Logic LogicLayer = new Logic();
        private List<clsMergedQuestions> Questions;
        private static bool FirstTimeRun = true;
        public formSurveyConfigurator()
        {
            try
            {
                InitializeComponent();
                
                
                //subscribe to events
                Connectionform.E_ResetConnection += DatabaseConnection_ResetConnection;
                InputsForm.E_RequestMainFormUpdate += Inputs_RequestMainFormUpdate;
                InputsEditForm.E_RequestMainFormUpdate += Inputs_RequestMainFormUpdate;
                LogicLayer.E_RequestUIUpdate += Logic_RequestUIUpdate;


                if (ConfigurationManager.ConnectionStrings["CONNECTION"].ToString() == "")
                {
                    Form Connect = new DatabaseConnection();
                    Connect.ShowDialog();
                }




               // ViewQuestions();
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
                MessageBox.Show(clsConstants.DELAY_MESSAGE);
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

        private void Inputs_RequestMainFormUpdate(object sender, EventArgs e)
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

                if (!this.LogicLayer.CanConnect())
                {
                    Cursor.Current = Cursors.Default;
                    dataGridViewQuestions.DataSource = null;
                    dataGridViewQuestions.Rows.Clear();
                    buttonAdd.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    label1.Visible = true;
                    if (FirstTimeRun == false)
                        MessageBox.Show(clsConstants.FAILED_DATABASE_CONNECTION_STRING);
                }
                else
                {
                    label1.Visible = false;
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
                this.LogicLayer.LastUpdateTime = DateTime.Now.ToString("M/d/yyyy HH:mm:ss");
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

                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(clsConstants.DELETE_QUESTION_CONFIRM_STRING , clsConstants.WARNING, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
                    if (e.ColumnIndex == 0)
                    {
                        Questions = Questions.OrderBy(obj => obj.Properties).ToList();
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        Questions = Questions.OrderBy(obj => obj.Type).ToList();
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        Questions = Questions.OrderBy(obj => obj.Text).ToList();
                    }
                    else if (e.ColumnIndex == 4)
                    {
                        Questions = Questions.OrderBy(obj => obj.Order).ToList();
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
    }
}
