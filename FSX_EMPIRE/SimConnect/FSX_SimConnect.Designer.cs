namespace FSX_EMPIRE
{
    partial class FSX_SimConnect
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.picSimConnect = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSimConnect)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(15, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(64, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SimConnect";
            // 
            // picSimConnect
            // 
            this.picSimConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSimConnect.Image = global::FSX_EMPIRE.Properties.Resources.Red_Light;
            this.picSimConnect.Location = new System.Drawing.Point(0, 0);
            this.picSimConnect.Name = "picSimConnect";
            this.picSimConnect.Size = new System.Drawing.Size(16, 16);
            this.picSimConnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSimConnect.TabIndex = 33;
            this.picSimConnect.TabStop = false;
            // 
            // FSX_SimConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picSimConnect);
            this.Controls.Add(this.lblTitle);
            this.Name = "FSX_SimConnect";
            this.Size = new System.Drawing.Size(76, 17);
            ((System.ComponentModel.ISupportInitialize)(this.picSimConnect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picSimConnect;
    }
}
