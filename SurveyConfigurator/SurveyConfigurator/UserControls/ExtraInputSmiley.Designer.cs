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
            this.labelSmileys = new System.Windows.Forms.Label();
            this.numericSmileys = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericSmileys)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSmileys
            // 
            this.labelSmileys.AutoSize = true;
            this.labelSmileys.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSmileys.Location = new System.Drawing.Point(16, 18);
            this.labelSmileys.Name = "labelSmileys";
            this.labelSmileys.Size = new System.Drawing.Size(183, 24);
            this.labelSmileys.TabIndex = 0;
            this.labelSmileys.Text = "Number of Smileys:";
            // 
            // numericSmileys
            // 
            this.numericSmileys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericSmileys.Font = new System.Drawing.Font("Cascadia Code", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericSmileys.Location = new System.Drawing.Point(201, 17);
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
            this.numericSmileys.Size = new System.Drawing.Size(565, 23);
            this.numericSmileys.TabIndex = 1;
            this.numericSmileys.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // ExtraInputSmiley
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericSmileys);
            this.Controls.Add(this.labelSmileys);
            this.Name = "ExtraInputSmiley";
            this.Size = new System.Drawing.Size(778, 137);
            ((System.ComponentModel.ISupportInitialize)(this.numericSmileys)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSmileys;
        private System.Windows.Forms.NumericUpDown numericSmileys;
    }
}
