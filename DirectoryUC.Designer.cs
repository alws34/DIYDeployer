namespace Deployer
{
    partial class DirectoryUC
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
            this.flpPrograms = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDirName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flpPrograms
            // 
            this.flpPrograms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpPrograms.AutoScroll = true;
            this.flpPrograms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpPrograms.Location = new System.Drawing.Point(4, 31);
            this.flpPrograms.Name = "flpPrograms";
            this.flpPrograms.Size = new System.Drawing.Size(214, 166);
            this.flpPrograms.TabIndex = 0;
            // 
            // lblDirName
            // 
            this.lblDirName.AutoSize = true;
            this.lblDirName.BackColor = System.Drawing.Color.Orange;
            this.lblDirName.Location = new System.Drawing.Point(4, 12);
            this.lblDirName.Name = "lblDirName";
            this.lblDirName.Size = new System.Drawing.Size(48, 13);
            this.lblDirName.TabIndex = 1;
            this.lblDirName.Text = "DirName";
            // 
            // DirectoryUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblDirName);
            this.Controls.Add(this.flpPrograms);
            this.MaximumSize = new System.Drawing.Size(400, 200);
            this.Name = "DirectoryUC";
            this.Size = new System.Drawing.Size(221, 200);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpPrograms;
        private System.Windows.Forms.Label lblDirName;
    }
}
