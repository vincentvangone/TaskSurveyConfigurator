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
            this.numericStars = new System.Windows.Forms.NumericUpDown();
            this.labelStars = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericStars)).BeginInit();
            this.SuspendLayout();
            // 
            // numericStars
            // 
            this.numericStars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericStars.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericStars.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericStars.Location = new System.Drawing.Point(202, 18);
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
            this.numericStars.Size = new System.Drawing.Size(440, 27);
            this.numericStars.TabIndex = 3;
            this.numericStars.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelStars
            // 
            this.labelStars.AutoSize = true;
            this.labelStars.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStars.Location = new System.Drawing.Point(17, 19);
            this.labelStars.Name = "labelStars";
            this.labelStars.Size = new System.Drawing.Size(159, 24);
            this.labelStars.TabIndex = 2;
            this.labelStars.Text = "Number of Stars:";
            // 
            // ExtraInputStar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericStars);
            this.Controls.Add(this.labelStars);
            this.Name = "ExtraInputStar";
            this.Size = new System.Drawing.Size(659, 135);
            ((System.ComponentModel.ISupportInitialize)(this.numericStars)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericStars;
        private System.Windows.Forms.Label labelStars;
    }
}
