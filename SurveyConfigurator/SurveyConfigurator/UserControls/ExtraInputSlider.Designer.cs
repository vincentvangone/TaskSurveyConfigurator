namespace SurveyConfigurator.UserControls
{
    partial class ExtraInputSlider
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtraInputSlider));
            this.labelEndValue = new System.Windows.Forms.Label();
            this.labelStartCaption = new System.Windows.Forms.Label();
            this.numericStartValue = new System.Windows.Forms.NumericUpDown();
            this.numericEndValue = new System.Windows.Forms.NumericUpDown();
            this.labelStartValue = new System.Windows.Forms.Label();
            this.labelEndCaption = new System.Windows.Forms.Label();
            this.textBoxStartCaption = new System.Windows.Forms.TextBox();
            this.textBoxEndCaption = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericStartValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEndValue)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelEndValue
            // 
            resources.ApplyResources(this.labelEndValue, "labelEndValue");
            this.labelEndValue.Name = "labelEndValue";
            // 
            // labelStartCaption
            // 
            resources.ApplyResources(this.labelStartCaption, "labelStartCaption");
            this.labelStartCaption.Name = "labelStartCaption";
            // 
            // numericStartValue
            // 
            resources.ApplyResources(this.numericStartValue, "numericStartValue");
            this.numericStartValue.Name = "numericStartValue";
            // 
            // numericEndValue
            // 
            resources.ApplyResources(this.numericEndValue, "numericEndValue");
            this.numericEndValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericEndValue.Name = "numericEndValue";
            this.numericEndValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelStartValue
            // 
            resources.ApplyResources(this.labelStartValue, "labelStartValue");
            this.labelStartValue.Name = "labelStartValue";
            // 
            // labelEndCaption
            // 
            resources.ApplyResources(this.labelEndCaption, "labelEndCaption");
            this.labelEndCaption.Name = "labelEndCaption";
            // 
            // textBoxStartCaption
            // 
            resources.ApplyResources(this.textBoxStartCaption, "textBoxStartCaption");
            this.textBoxStartCaption.Name = "textBoxStartCaption";
            // 
            // textBoxEndCaption
            // 
            resources.ApplyResources(this.textBoxEndCaption, "textBoxEndCaption");
            this.textBoxEndCaption.Name = "textBoxEndCaption";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.textBoxStartCaption);
            this.panel1.Controls.Add(this.labelStartValue);
            this.panel1.Controls.Add(this.labelStartCaption);
            this.panel1.Controls.Add(this.numericStartValue);
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.textBoxEndCaption);
            this.panel2.Controls.Add(this.labelEndCaption);
            this.panel2.Controls.Add(this.labelEndValue);
            this.panel2.Controls.Add(this.numericEndValue);
            this.panel2.Name = "panel2";
            // 
            // ExtraInputSlider
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ExtraInputSlider";
            this.Load += new System.EventHandler(this.ExtraInputSlider_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericStartValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEndValue)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelEndValue;
        private System.Windows.Forms.Label labelStartCaption;
        private System.Windows.Forms.NumericUpDown numericStartValue;
        private System.Windows.Forms.NumericUpDown numericEndValue;
        private System.Windows.Forms.Label labelStartValue;
        private System.Windows.Forms.Label labelEndCaption;
        private System.Windows.Forms.TextBox textBoxStartCaption;
        private System.Windows.Forms.TextBox textBoxEndCaption;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
