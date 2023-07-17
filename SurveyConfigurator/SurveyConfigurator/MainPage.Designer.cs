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
            this.panelTitle = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQuestions)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelQuestions
            // 
            this.labelQuestions.AutoSize = true;
            this.labelQuestions.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelQuestions.Font = new System.Drawing.Font("Microsoft JhengHei Light", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuestions.Location = new System.Drawing.Point(15, 15);
            this.labelQuestions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelQuestions.Name = "labelQuestions";
            this.labelQuestions.Size = new System.Drawing.Size(193, 29);
            this.labelQuestions.TabIndex = 0;
            this.labelQuestions.Text = "Survey Questions";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.Location = new System.Drawing.Point(656, 5);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(86, 34);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "New";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEdit.Location = new System.Drawing.Point(752, 5);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(86, 34);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.Location = new System.Drawing.Point(848, 5);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(86, 34);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // dataGridViewQuestions
            // 
            this.dataGridViewQuestions.AllowUserToAddRows = false;
            this.dataGridViewQuestions.AllowUserToDeleteRows = false;
            this.dataGridViewQuestions.AllowUserToOrderColumns = true;
            this.dataGridViewQuestions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewQuestions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewQuestions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewQuestions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridViewQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewQuestions.Location = new System.Drawing.Point(12, 56);
            this.dataGridViewQuestions.Name = "dataGridViewQuestions";
            this.dataGridViewQuestions.ReadOnly = true;
            this.dataGridViewQuestions.RowHeadersWidth = 51;
            this.dataGridViewQuestions.RowTemplate.Height = 24;
            this.dataGridViewQuestions.Size = new System.Drawing.Size(925, 425);
            this.dataGridViewQuestions.TabIndex = 4;
            this.dataGridViewQuestions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewQuestions_CellClick);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonAdd);
            this.panelButtons.Controls.Add(this.panelDummy);
            this.panelButtons.Controls.Add(this.buttonEdit);
            this.panelButtons.Controls.Add(this.panel3);
            this.panelButtons.Controls.Add(this.buttonDelete);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 485);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(15, 5, 15, 15);
            this.panelButtons.Size = new System.Drawing.Size(949, 54);
            this.panelButtons.TabIndex = 5;
            // 
            // panelDummy
            // 
            this.panelDummy.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDummy.Location = new System.Drawing.Point(742, 5);
            this.panelDummy.Name = "panelDummy";
            this.panelDummy.Size = new System.Drawing.Size(10, 34);
            this.panelDummy.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(838, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 34);
            this.panel3.TabIndex = 5;
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.labelQuestions);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Padding = new System.Windows.Forms.Padding(15);
            this.panelTitle.Size = new System.Drawing.Size(949, 53);
            this.panelTitle.TabIndex = 6;
            // 
            // formSurveyConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 539);
            this.Controls.Add(this.panelTitle);
            this.Controls.Add(this.dataGridViewQuestions);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.MinimumSize = new System.Drawing.Size(887, 544);
            this.Name = "formSurveyConfigurator";
            this.Text = "Survey Configurator";
            this.Load += new System.EventHandler(this.formSurveyConfigurator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQuestions)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
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
    }
}

