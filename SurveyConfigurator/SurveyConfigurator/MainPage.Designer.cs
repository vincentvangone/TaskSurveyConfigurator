namespace SurveyConfigurator
{
    partial class formSurveyConfigurator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSurveyConfigurator));
            this.labelQuestions = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.dataGridViewQuestions = new System.Windows.Forms.DataGridView();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelDummy = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelConnectionCheck = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQuestions)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelQuestions
            // 
            resources.ApplyResources(this.labelQuestions, "labelQuestions");
            this.labelQuestions.Name = "labelQuestions";
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            resources.ApplyResources(this.buttonEdit, "buttonEdit");
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // dataGridViewQuestions
            // 
            this.dataGridViewQuestions.AllowUserToAddRows = false;
            this.dataGridViewQuestions.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridViewQuestions, "dataGridViewQuestions");
            this.dataGridViewQuestions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewQuestions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewQuestions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridViewQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewQuestions.Name = "dataGridViewQuestions";
            this.dataGridViewQuestions.ReadOnly = true;
            this.dataGridViewQuestions.RowTemplate.Height = 24;
            this.dataGridViewQuestions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewQuestions_CellClick);
            this.dataGridViewQuestions.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewQuestions_ColumnHeaderMouseClick);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonAdd);
            this.panelButtons.Controls.Add(this.panelDummy);
            this.panelButtons.Controls.Add(this.buttonEdit);
            this.panelButtons.Controls.Add(this.panel3);
            this.panelButtons.Controls.Add(this.buttonDelete);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            // 
            // panelDummy
            // 
            resources.ApplyResources(this.panelDummy, "panelDummy");
            this.panelDummy.Name = "panelDummy";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // buttonConnect
            // 
            resources.ApplyResources(this.buttonConnect, "buttonConnect");
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.pictureBox1);
            this.panelTitle.Controls.Add(this.panel2);
            this.panelTitle.Controls.Add(this.comboBoxLanguage);
            this.panelTitle.Controls.Add(this.panel1);
            this.panelTitle.Controls.Add(this.labelQuestions);
            this.panelTitle.Controls.Add(this.buttonConnect);
            resources.ApplyResources(this.panelTitle, "panelTitle");
            this.panelTitle.Name = "panelTitle";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // comboBoxLanguage
            // 
            resources.ApplyResources(this.comboBoxLanguage, "comboBoxLanguage");
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Items.AddRange(new object[] {
            resources.GetString("comboBoxLanguage.Items"),
            resources.GetString("comboBoxLanguage.Items1")});
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // labelConnectionCheck
            // 
            this.labelConnectionCheck.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.labelConnectionCheck, "labelConnectionCheck");
            this.labelConnectionCheck.Name = "labelConnectionCheck";
            // 
            // formSurveyConfigurator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelConnectionCheck);
            this.Controls.Add(this.panelTitle);
            this.Controls.Add(this.dataGridViewQuestions);
            this.Controls.Add(this.panelButtons);
            this.MaximizeBox = false;
            this.Name = "formSurveyConfigurator";
            this.Load += new System.EventHandler(this.formSurveyConfigurator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQuestions)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelQuestions;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.DataGridView dataGridViewQuestions;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Panel panelDummy;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelConnectionCheck;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
    }
}

