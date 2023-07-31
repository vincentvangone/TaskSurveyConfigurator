namespace SurveyConfigurator
{
    partial class Inputs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inputs));
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.labelText = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.panelExtraInputs = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelDummy = new System.Windows.Forms.Panel();
            this.labelOrder = new System.Windows.Forms.Label();
            this.numericUpDownOrder = new System.Windows.Forms.NumericUpDown();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxText
            // 
            this.textBoxText.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxText, "textBoxText");
            this.textBoxText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxText.Name = "textBoxText";
            // 
            // labelText
            // 
            resources.ApplyResources(this.labelText, "labelText");
            this.labelText.Name = "labelText";
            // 
            // labelType
            // 
            resources.ApplyResources(this.labelType, "labelType");
            this.labelType.Name = "labelType";
            // 
            // comboBoxType
            // 
            resources.ApplyResources(this.comboBoxType, "comboBoxType");
            this.comboBoxType.DisplayMember = "Smiley";
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // panelExtraInputs
            // 
            resources.ApplyResources(this.panelExtraInputs, "panelExtraInputs");
            this.panelExtraInputs.Name = "panelExtraInputs";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Controls.Add(this.panelDummy);
            this.panelButtons.Controls.Add(this.buttonCancel);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            // 
            // panelDummy
            // 
            resources.ApplyResources(this.panelDummy, "panelDummy");
            this.panelDummy.Name = "panelDummy";
            // 
            // labelOrder
            // 
            resources.ApplyResources(this.labelOrder, "labelOrder");
            this.labelOrder.Name = "labelOrder";
            // 
            // numericUpDownOrder
            // 
            resources.ApplyResources(this.numericUpDownOrder, "numericUpDownOrder");
            this.numericUpDownOrder.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownOrder.Name = "numericUpDownOrder";
            this.numericUpDownOrder.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Inputs
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDownOrder);
            this.Controls.Add(this.labelOrder);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.panelExtraInputs);
            this.Controls.Add(this.panelButtons);
            this.MaximizeBox = false;
            this.Name = "Inputs";
            this.Load += new System.EventHandler(this.Inputs_Load);
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Panel panelExtraInputs;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelDummy;
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.NumericUpDown numericUpDownOrder;
    }
}