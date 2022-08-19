
namespace Deployer
{
    partial class FrmLanScanner
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
            this.components = new System.ComponentModel.Container();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelipRange = new System.Windows.Forms.Label();
            this.textBoxIpRange = new System.Windows.Forms.TextBox();
            this.labelStatusText = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.listViewLanScan = new System.Windows.Forms.ListView();
            this.toolTipIPRange = new System.Windows.Forms.ToolTip(this.components);
            this.listBoxAvailableIps = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(11, 283);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(259, 15);
            this.progressBar.TabIndex = 1;
            // 
            // labelipRange
            // 
            this.labelipRange.AutoSize = true;
            this.labelipRange.Location = new System.Drawing.Point(13, 332);
            this.labelipRange.Name = "labelipRange";
            this.labelipRange.Size = new System.Drawing.Size(51, 13);
            this.labelipRange.TabIndex = 2;
            this.labelipRange.Text = "Ip Range";
            // 
            // textBoxIpRange
            // 
            this.textBoxIpRange.Location = new System.Drawing.Point(70, 329);
            this.textBoxIpRange.Name = "textBoxIpRange";
            this.textBoxIpRange.Size = new System.Drawing.Size(100, 20);
            this.textBoxIpRange.TabIndex = 3;
            this.textBoxIpRange.Tag = "This is the Ip range of this computer. is that the desired Ip range?    if not, c" +
    "hange it before clicking the \"start scan\" button.  add only the first 3 octates " +
    "of your IP address";
            this.textBoxIpRange.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseMove);
            // 
            // labelStatusText
            // 
            this.labelStatusText.AutoSize = true;
            this.labelStatusText.Location = new System.Drawing.Point(196, 332);
            this.labelStatusText.Name = "labelStatusText";
            this.labelStatusText.Size = new System.Drawing.Size(37, 13);
            this.labelStatusText.TabIndex = 4;
            this.labelStatusText.Text = "Status";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(250, 332);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 5;
            // 
            // btnScan
            // 
            this.btnScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnScan.Location = new System.Drawing.Point(136, 369);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(114, 64);
            this.btnScan.TabIndex = 6;
            this.btnScan.Text = "Start Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.BtnScan_Click);
            // 
            // listViewLanScan
            // 
            this.listViewLanScan.HideSelection = false;
            this.listViewLanScan.Location = new System.Drawing.Point(12, 12);
            this.listViewLanScan.Name = "listViewLanScan";
            this.listViewLanScan.Size = new System.Drawing.Size(258, 265);
            this.listViewLanScan.TabIndex = 7;
            this.listViewLanScan.UseCompatibleStateImageBehavior = false;
            // 
            // toolTipIPRange
            // 
            this.toolTipIPRange.IsBalloon = true;
            this.toolTipIPRange.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // listBoxAvailableIps
            // 
            this.listBoxAvailableIps.FormattingEnabled = true;
            this.listBoxAvailableIps.Location = new System.Drawing.Point(276, 12);
            this.listBoxAvailableIps.Name = "listBoxAvailableIps";
            this.listBoxAvailableIps.Size = new System.Drawing.Size(114, 264);
            this.listBoxAvailableIps.TabIndex = 10;
            // 
            // button1
            // 
            this.btnCancel.Location = new System.Drawing.Point(305, 369);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmLanScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 445);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.listBoxAvailableIps);
            this.Controls.Add(this.listViewLanScan);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.labelStatusText);
            this.Controls.Add(this.textBoxIpRange);
            this.Controls.Add(this.labelipRange);
            this.Controls.Add(this.progressBar);
            this.Name = "FrmLanScanner";
            this.Text = "Lan Scanner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelipRange;
        private System.Windows.Forms.TextBox textBoxIpRange;
        private System.Windows.Forms.Label labelStatusText;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListView listViewLanScan;
        private System.Windows.Forms.ToolTip toolTipIPRange;
        private System.Windows.Forms.ListBox listBoxAvailableIps;
        private System.Windows.Forms.Button btnCancel;
    }
}