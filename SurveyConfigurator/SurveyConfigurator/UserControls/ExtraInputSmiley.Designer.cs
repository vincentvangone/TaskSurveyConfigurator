namespace SurveyConfigurator.UserControls
{
    partial class ExtraInputSmiley
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtraInputSmiley));
            this.labelSmileys = new System.Windows.Forms.Label();
            this.numericSmileys = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericSmileys)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSmileys
            // 
            resources.ApplyResources(this.labelSmileys, "labelSmileys");
            this.labelSmileys.Name = "labelSmileys";
            // 
            // numericSmileys
            // 
            resources.ApplyResources(this.numericSmileys, "numericSmileys");
            this.numericSmileys.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericSmileys.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericSmileys.Name = "numericSmileys";
            this.numericSmileys.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // ExtraInputSmiley
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericSmileys);
            this.Controls.Add(this.labelSmileys);
            this.Name = "ExtraInputSmiley";
            this.Load += new System.EventHandler(this.ExtraInputSmiley_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericSmileys)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSmileys;
        private System.Windows.Forms.NumericUpDown numericSmileys;
    }
}
