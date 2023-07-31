using BusinessLayer;
using ErrorLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;
using Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SurveyConfigurator
{
    public partial class DatabaseConnection : Form
    {
        Logic LogicLayer = new Logic();

        //reload main to set connection
        public event EventHandler<EventArgs> E_ResetConnection;

        public DatabaseConnection()
        {
            try
            { 
                InitializeComponent();

            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }


        private void DatabaseConnection_Load(object sender, EventArgs e)
        {
            try
            {
                 Controls.Clear();
                InitializeComponent();
                if (Thread.CurrentThread.CurrentUICulture.Name == clsConstants.AR)
                { 
                    this.RightToLeft = RightToLeft.Yes;
                }
                else
                    this.RightToLeft = RightToLeft.No;
               // ConfigurationManager.RefreshSection("connectionStrings");
                if (ConfigurationManager.AppSettings["IntegratedSecurity"] == "True")
                {
                    textBoxServer.Text = ConfigurationManager.AppSettings["Server"];
                    textBoxDatabase.Text = ConfigurationManager.AppSettings["Database"];
                    radioButtonIntegratedSecurity.Checked = true;
                    textBoxUsername.Enabled = false;
                    textBoxPassword.Enabled = false;
                }
                else
                {
                    radioButtonSQLAuth.Checked = true;
                    textBoxUsername.Enabled = true;
                    textBoxPassword.Enabled = true;
                    textBoxServer.Text = ConfigurationManager.AppSettings["Server"];
                    textBoxDatabase.Text = ConfigurationManager.AppSettings["Database"];
                    textBoxUsername.Text = ConfigurationManager.AppSettings["Username"];
                    textBoxPassword.Text = ConfigurationManager.AppSettings["Password"];


                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        protected virtual void OnResetConnection()
        {
            try
            {
                // Raise the event to request a UI update in the UI layer
                E_ResetConnection?.Invoke(this, EventArgs.Empty);
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
                if (textBoxServer.Text == "")
                    MessageBox.Show(clsConstants.EMPTY_SERVER_STRING, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                else if (textBoxDatabase.Text == "")
                    MessageBox.Show(clsConstants.EMPTY_DATABASE_STRING, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (!radioButtonIntegratedSecurity.Checked && ((textBoxUsername.Text == "")||(textBoxPassword.Text == "")))
                    MessageBox.Show(clsConstants.EMPTY_USERNAME_OR_PASSWORD, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                   
                    bool IntegratedSecurity =radioButtonIntegratedSecurity.Checked;
                    Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    AppConfig.AppSettings.Settings["Server"].Value = textBoxServer.Text;
                    AppConfig.AppSettings.Settings["Database"].Value = textBoxDatabase.Text;
                    AppConfig.AppSettings.Settings["IntegratedSecurity"].Value = "True";


                    if (!IntegratedSecurity)
                    {
                        AppConfig.AppSettings.Settings["IntegratedSecurity"].Value = "False";
                        AppConfig.AppSettings.Settings["Username"].Value = textBoxUsername.Text;
                        AppConfig.AppSettings.Settings["Password"].Value = textBoxPassword.Text;
                    }


                    AppConfig.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    LogicLayer.SetConnectionString();
                    OnResetConnection();
                    
                }

            }
            catch (Exception E)
            {

                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }


        private void buttonTest_Click(object sender, EventArgs e)
        {
            try
            {
                string TestConnectionString;
                if ((textBoxServer.Text == "") || (textBoxDatabase.Text == "")|| (!(radioButtonIntegratedSecurity.Checked) &&( (textBoxUsername.Text == "") || (textBoxPassword.Text == ""))))
                    MessageBox.Show(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    
                    Cursor.Current = Cursors.WaitCursor;
                    if (radioButtonIntegratedSecurity.Checked)
                    {
                        //Constructing connection string from the inputs
                        TestConnectionString = string.Format(clsConstants.SET_CONNECTION_WINDOWS_AUTH, textBoxServer.Text, textBoxDatabase.Text, radioButtonIntegratedSecurity.Checked);
                    }
                    else
                    {
                        
                        TestConnectionString = string.Format(clsConstants.SET_CONNECTION_SQL_AUTH, textBoxServer.Text, textBoxDatabase.Text, textBoxUsername.Text, textBoxPassword.Text);
                    }
                    
                    if (LogicLayer.CanConnect(TestConnectionString))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        MessageBox.Show(clsConstants.SUCCESS_STRING, clsConstants.SUCCESS_STRING, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        MessageBox.Show(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception E)
            {
                Cursor.Current = Cursors.WaitCursor;
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void labelIntegratedSecurity_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (radioButtonSQLAuth.Checked) radioButtonSQLAuth.Checked = false;
                //radioButtonIntegratedSecurity.Checked = true;

                textBoxPassword.Clear();
                textBoxUsername.Clear();
                textBoxUsername.Enabled = false;
                textBoxPassword.Enabled = false;

            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);

            }
        }

        private void radioButtonSQLAuth_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButtonIntegratedSecurity.Checked) radioButtonIntegratedSecurity.Checked = false;
            //radioButtonSQLAuth.Checked = true;

            try
            {
                textBoxUsername.Enabled = true;
                textBoxPassword.Enabled = true;
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try { 
            this.Close();
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        
    }
}
