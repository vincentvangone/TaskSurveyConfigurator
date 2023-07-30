namespace SurveyConfigurator.UserControls
{
    partial class ExtraInputStar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtraInputStar));
            this.numericStars = new System.Windows.Forms.NumericUpDown();
            this.labelStars = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericStars)).BeginInit();
            this.SuspendLayout();
            // 
            // numericStars
            // 
            resources.ApplyResources(this.numericStars, "numericStars");
            this.numericStars.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericStars.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericStars.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericStars.Name = "numericStars";
            this.numericStars.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelStars
            // 
            resources.ApplyResources(this.labelStars, "labelStars");
            this.labelStars.Name = "labelStars";
            // 
            // ExtraInputStar
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericStars);
            this.Controls.Add(this.labelStars);
            this.Name = "ExtraInputStar";
            this.Load += new System.EventHandler(this.ExtraInputStar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericStars)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericStars;
        private System.Windows.Forms.Label labelStars;
    }
}
