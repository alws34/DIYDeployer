
namespace Deployer
{
    partial class Deployer
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
        [System.Obsolete]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Deployer));
            this.time = new System.Windows.Forms.Timer(this.components);
            this.toolTipdynamic = new System.Windows.Forms.ToolTip(this.components);
            this.flp_main = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLanScan = new System.Windows.Forms.Button();
            this.textBoxDateTime = new System.Windows.Forms.TextBox();
            this.buttonRandomWRDport = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.textBoxWrdPort = new System.Windows.Forms.TextBox();
            this.checkBoxChangeWRDPort = new System.Windows.Forms.CheckBox();
            this.checkBoxRegistryHacks = new System.Windows.Forms.CheckBox();
            this.checkBoxDrivers = new System.Windows.Forms.CheckBox();
            this.btnInstall = new System.Windows.Forms.Button();
            this.labelPathFile = new System.Windows.Forms.Label();
            this.txtboxInstallsPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // time
            // 
            this.time.Enabled = true;
            this.time.Interval = 1000;
            this.time.Tick += new System.EventHandler(this.time_Tick);
            // 
            // toolTipdynamic
            // 
            this.toolTipdynamic.IsBalloon = true;
            this.toolTipdynamic.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // flp_main
            // 
            this.flp_main.Location = new System.Drawing.Point(0, 0);
            this.flp_main.Name = "flp_main";
            this.flp_main.Size = new System.Drawing.Size(1386, 663);
            this.flp_main.TabIndex = 0;
            // 
            // btnLanScan
            // 
            this.btnLanScan.Location = new System.Drawing.Point(551, 722);
            this.btnLanScan.Name = "btnLanScan";
            this.btnLanScan.Size = new System.Drawing.Size(95, 25);
            this.btnLanScan.TabIndex = 71;
            this.btnLanScan.Text = "Lan scanner";
            this.btnLanScan.UseVisualStyleBackColor = true;
            this.btnLanScan.Click += new System.EventHandler(this.btnLanScan_Click);
            // 
            // textBoxDateTime
            // 
            this.textBoxDateTime.Enabled = false;
            this.textBoxDateTime.Location = new System.Drawing.Point(4, 807);
            this.textBoxDateTime.MaxLength = 50;
            this.textBoxDateTime.Name = "textBoxDateTime";
            this.textBoxDateTime.ReadOnly = true;
            this.textBoxDateTime.Size = new System.Drawing.Size(166, 20);
            this.textBoxDateTime.TabIndex = 70;
            // 
            // buttonRandomWRDport
            // 
            this.buttonRandomWRDport.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonRandomWRDport.Location = new System.Drawing.Point(551, 693);
            this.buttonRandomWRDport.Name = "buttonRandomWRDport";
            this.buttonRandomWRDport.Size = new System.Drawing.Size(75, 23);
            this.buttonRandomWRDport.TabIndex = 67;
            this.buttonRandomWRDport.Text = "Random Port";
            this.buttonRandomWRDport.UseVisualStyleBackColor = true;
            this.buttonRandomWRDport.Click += new System.EventHandler(this.buttonRandomWRDport_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(5, 674);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(95, 25);
            this.buttonReset.TabIndex = 66;
            this.buttonReset.Text = "Reset Form";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // textBoxWrdPort
            // 
            this.textBoxWrdPort.BackColor = System.Drawing.Color.White;
            this.textBoxWrdPort.Location = new System.Drawing.Point(551, 669);
            this.textBoxWrdPort.Name = "textBoxWrdPort";
            this.textBoxWrdPort.Size = new System.Drawing.Size(93, 20);
            this.textBoxWrdPort.TabIndex = 65;
            this.textBoxWrdPort.Tag = "Port number between 34568 and 65535";
            this.textBoxWrdPort.TextChanged += new System.EventHandler(this.textBoxWrdPort_TextChanged);
            this.textBoxWrdPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWrdPort_KeyPress);
            // 
            // checkBoxChangeWRDPort
            // 
            this.checkBoxChangeWRDPort.AutoSize = true;
            this.checkBoxChangeWRDPort.Location = new System.Drawing.Point(415, 672);
            this.checkBoxChangeWRDPort.Name = "checkBoxChangeWRDPort";
            this.checkBoxChangeWRDPort.Size = new System.Drawing.Size(130, 17);
            this.checkBoxChangeWRDPort.TabIndex = 64;
            this.checkBoxChangeWRDPort.Text = "Change WRD Port";
            this.checkBoxChangeWRDPort.UseVisualStyleBackColor = true;
            this.checkBoxChangeWRDPort.CheckStateChanged += new System.EventHandler(this.checkBoxChangeWRDPort_CheckStateChanged);
            // 
            // checkBoxRegistryHacks
            // 
            this.checkBoxRegistryHacks.AutoSize = true;
            this.checkBoxRegistryHacks.Location = new System.Drawing.Point(270, 709);
            this.checkBoxRegistryHacks.Name = "checkBoxRegistryHacks";
            this.checkBoxRegistryHacks.Size = new System.Drawing.Size(108, 17);
            this.checkBoxRegistryHacks.TabIndex = 63;
            this.checkBoxRegistryHacks.Text = "RegistryHacks";
            this.checkBoxRegistryHacks.UseVisualStyleBackColor = true;
            // 
            // checkBoxDrivers
            // 
            this.checkBoxDrivers.AutoSize = true;
            this.checkBoxDrivers.Location = new System.Drawing.Point(270, 672);
            this.checkBoxDrivers.Name = "checkBoxDrivers";
            this.checkBoxDrivers.Size = new System.Drawing.Size(139, 17);
            this.checkBoxDrivers.TabIndex = 62;
            this.checkBoxDrivers.Text = "Open Drivers Folder";
            this.checkBoxDrivers.UseVisualStyleBackColor = true;
            // 
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.Color.Red;
            this.btnInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnInstall.Location = new System.Drawing.Point(106, 674);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(147, 54);
            this.btnInstall.TabIndex = 61;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.Click += new System.EventHandler(this.install);
            // 
            // labelPathFile
            // 
            this.labelPathFile.AutoSize = true;
            this.labelPathFile.Location = new System.Drawing.Point(10, 753);
            this.labelPathFile.Name = "labelPathFile";
            this.labelPathFile.Size = new System.Drawing.Size(120, 13);
            this.labelPathFile.TabIndex = 69;
            this.labelPathFile.Text = "Paths  File Location";
            // 
            // txtboxInstallsPath
            // 
            this.txtboxInstallsPath.Location = new System.Drawing.Point(168, 750);
            this.txtboxInstallsPath.Name = "txtboxInstallsPath";
            this.txtboxInstallsPath.Size = new System.Drawing.Size(515, 20);
            this.txtboxInstallsPath.TabIndex = 68;
            this.txtboxInstallsPath.Tag = "Enter Paths.txt *path* full here";
            this.txtboxInstallsPath.TextChanged += new System.EventHandler(this.txtboxInstallsPath_TextChanged);
            // 
            // Deployer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(1384, 831);
            this.Controls.Add(this.btnLanScan);
            this.Controls.Add(this.textBoxDateTime);
            this.Controls.Add(this.buttonRandomWRDport);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.textBoxWrdPort);
            this.Controls.Add(this.checkBoxChangeWRDPort);
            this.Controls.Add(this.checkBoxRegistryHacks);
            this.Controls.Add(this.checkBoxDrivers);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.labelPathFile);
            this.Controls.Add(this.txtboxInstallsPath);
            this.Controls.Add(this.flp_main);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Deployer";
            this.Text = "Deployer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer time;
        private System.Windows.Forms.ToolTip toolTipdynamic;
        private System.Windows.Forms.FlowLayoutPanel flp_main;
        private System.Windows.Forms.Button btnLanScan;
        private System.Windows.Forms.TextBox textBoxDateTime;
        private System.Windows.Forms.Button buttonRandomWRDport;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.TextBox textBoxWrdPort;
        private System.Windows.Forms.CheckBox checkBoxChangeWRDPort;
        private System.Windows.Forms.CheckBox checkBoxRegistryHacks;
        private System.Windows.Forms.CheckBox checkBoxDrivers;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label labelPathFile;
        private System.Windows.Forms.TextBox txtboxInstallsPath;
    }
}

