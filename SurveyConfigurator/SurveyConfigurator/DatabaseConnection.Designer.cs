namespace SurveyConfigurator
{
    partial class DatabaseConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseConnection));
            this.labelServer = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.radioButtonIntegratedSecurity = new System.Windows.Forms.RadioButton();
            this.radioButtonSQLAuth = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxAuthentication = new System.Windows.Forms.GroupBox();
            this.panel2.SuspendLayout();
            this.groupBoxAuthentication.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelServer
            // 
            resources.ApplyResources(this.labelServer, "labelServer");
            this.labelServer.Name = "labelServer";
            // 
            // labelUsername
            // 
            resources.ApplyResources(this.labelUsername, "labelUsername");
            this.labelUsername.Name = "labelUsername";
            // 
            // labelDatabase
            // 
            resources.ApplyResources(this.labelDatabase, "labelDatabase");
            this.labelDatabase.Name = "labelDatabase";
            // 
            // textBoxServer
            // 
            resources.ApplyResources(this.textBoxServer, "textBoxServer");
            this.textBoxServer.Name = "textBoxServer";
            // 
            // textBoxDatabase
            // 
            resources.ApplyResources(this.textBoxDatabase, "textBoxDatabase");
            this.textBoxDatabase.Name = "textBoxDatabase";
            // 
            // labelPassword
            // 
            resources.ApplyResources(this.labelPassword, "labelPassword");
            this.labelPassword.Name = "labelPassword";
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.Name = "textBoxPassword";
            // 
            // textBoxUsername
            // 
            resources.ApplyResources(this.textBoxUsername, "textBoxUsername");
            this.textBoxUsername.Name = "textBoxUsername";
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonTest
            // 
            resources.ApplyResources(this.buttonTest, "buttonTest");
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // radioButtonIntegratedSecurity
            // 
            resources.ApplyResources(this.radioButtonIntegratedSecurity, "radioButtonIntegratedSecurity");
            this.radioButtonIntegratedSecurity.Name = "radioButtonIntegratedSecurity";
            this.radioButtonIntegratedSecurity.TabStop = true;
            this.radioButtonIntegratedSecurity.UseVisualStyleBackColor = true;
            this.radioButtonIntegratedSecurity.CheckedChanged += new System.EventHandler(this.radioButtonIntegratedSecurity_CheckedChanged);
            // 
            // radioButtonSQLAuth
            // 
            resources.ApplyResources(this.radioButtonSQLAuth, "radioButtonSQLAuth");
            this.radioButtonSQLAuth.Name = "radioButtonSQLAuth";
            this.radioButtonSQLAuth.TabStop = true;
            this.radioButtonSQLAuth.UseVisualStyleBackColor = true;
            this.radioButtonSQLAuth.CheckedChanged += new System.EventHandler(this.radioButtonSQLAuth_CheckedChanged);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.buttonOk);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Controls.Add(this.buttonTest);
            this.panel2.Name = "panel2";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxAuthentication
            // 
            resources.ApplyResources(this.groupBoxAuthentication, "groupBoxAuthentication");
            this.groupBoxAuthentication.Controls.Add(this.radioButtonSQLAuth);
            this.groupBoxAuthentication.Controls.Add(this.radioButtonIntegratedSecurity);
            this.groupBoxAuthentication.Controls.Add(this.textBoxUsername);
            this.groupBoxAuthentication.Controls.Add(this.textBoxPassword);
            this.groupBoxAuthentication.Controls.Add(this.labelPassword);
            this.groupBoxAuthentication.Controls.Add(this.labelUsername);
            this.groupBoxAuthentication.Name = "groupBoxAuthentication";
            this.groupBoxAuthentication.TabStop = false;
            // 
            // DatabaseConnection
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxAuthentication);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBoxDatabase);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.labelDatabase);
            this.Controls.Add(this.labelServer);
            this.MaximizeBox = false;
            this.Name = "DatabaseConnection";
            this.Load += new System.EventHandler(this.DatabaseConnection_Load);
            this.panel2.ResumeLayout(false);
            this.groupBoxAuthentication.ResumeLayout(false);
            this.groupBoxAuthentication.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.RadioButton radioButtonIntegratedSecurity;
        private System.Windows.Forms.RadioButton radioButtonSQLAuth;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxAuthentication;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
    }
}