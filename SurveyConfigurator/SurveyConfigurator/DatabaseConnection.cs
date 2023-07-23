using BusinessLayer;
using ErrorLogger;
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
using Entities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SurveyConfigurator
{
    public partial class DatabaseConnection : Form
    {
        public DatabaseConnection()
        {
            try
            {
                InitializeComponent();
                checkBoxSecurity.Checked = true;
                textBoxUsername.Enabled = false;
                textBoxPassword.Enabled = false;
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        private void checkBoxSecurity_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxSecurity.Checked == true)
                {
                    checkBoxSecurity.Text = "True";
                    textBoxPassword.Clear();
                    textBoxUsername.Clear();
                    textBoxUsername.Enabled = false;
                    textBoxPassword.Enabled = false;
                }
                else
                {
                    checkBoxSecurity.Text = "False";
                    textBoxUsername.Enabled = true;
                    textBoxPassword.Enabled = true;
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
                if (textBoxServer.Text == "")
                    System.Windows.Forms.MessageBox.Show(clsConstants.EMPTY_SERVER_STRING, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                else if (textBoxDatabase.Text == "")
                    System.Windows.Forms.MessageBox.Show(clsConstants.EMPTY_DATABASE_STRING, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Logic.SetConnectionString(textBoxServer.Text, textBoxDatabase.Text, textBoxUsername.Text, textBoxPassword.Text, checkBoxSecurity.Checked);
                    if (Logic.CanConnect())
                    {
                        this.Close();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                if (textBoxServer.Text == ""|| textBoxDatabase.Text == "")
                    System.Windows.Forms.MessageBox.Show(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Logic.SetConnectionString(textBoxServer.Text, textBoxDatabase.Text, textBoxUsername.Text, textBoxPassword.Text, checkBoxSecurity.Checked);
                    if (Logic.CanConnect())
                    {
                        System.Windows.Forms.MessageBox.Show(clsConstants.SUCCESS_STRING, clsConstants.SUCCESS_STRING, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception E)
            {

                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }
    }
}
