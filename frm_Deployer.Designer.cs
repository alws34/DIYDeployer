
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
            this.btnInstall = new System.Windows.Forms.Button();
            this.checkBoxDrivers = new System.Windows.Forms.CheckBox();
            this.checkBoxRegistryHacks = new System.Windows.Forms.CheckBox();
            this.flowLayoutGeneralPrograms = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelOffice = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelPcCare = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelGamingPlatforms = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelProgramming = new System.Windows.Forms.FlowLayoutPanel();
            this.labelgeneral = new System.Windows.Forms.Label();
            this.labelpcCare = new System.Windows.Forms.Label();
            this.labelGamingPlatforms = new System.Windows.Forms.Label();
            this.labelProgramming = new System.Windows.Forms.Label();
            this.labeloffice = new System.Windows.Forms.Label();
            this.checkBoxChangeWRDPort = new System.Windows.Forms.CheckBox();
            this.textBoxWrdPort = new System.Windows.Forms.TextBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonRandomWRDport = new System.Windows.Forms.Button();
            this.checkBoxScanLan = new System.Windows.Forms.CheckBox();
            this.panelMainPanel = new System.Windows.Forms.Panel();
            this.txtboxPathFileLocation = new System.Windows.Forms.TextBox();
            this.labelPathFile = new System.Windows.Forms.Label();
            this.panelPathsPanel = new System.Windows.Forms.Panel();
            this.textBoxDateTime = new System.Windows.Forms.TextBox();
            this.panelMainPanel.SuspendLayout();
            this.panelPathsPanel.SuspendLayout();
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
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.Color.Red;
            this.btnInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnInstall.Location = new System.Drawing.Point(408, 644);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(147, 54);
            this.btnInstall.TabIndex = 41;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.Click += new System.EventHandler(this.install);
            // 
            // checkBoxDrivers
            // 
            this.checkBoxDrivers.AutoSize = true;
            this.checkBoxDrivers.Location = new System.Drawing.Point(572, 642);
            this.checkBoxDrivers.Name = "checkBoxDrivers";
            this.checkBoxDrivers.Size = new System.Drawing.Size(139, 17);
            this.checkBoxDrivers.TabIndex = 42;
            this.checkBoxDrivers.Text = "Open Drivers Folder";
            this.checkBoxDrivers.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegistryHacks
            // 
            this.checkBoxRegistryHacks.AutoSize = true;
            this.checkBoxRegistryHacks.Location = new System.Drawing.Point(572, 679);
            this.checkBoxRegistryHacks.Name = "checkBoxRegistryHacks";
            this.checkBoxRegistryHacks.Size = new System.Drawing.Size(108, 17);
            this.checkBoxRegistryHacks.TabIndex = 43;
            this.checkBoxRegistryHacks.Text = "RegistryHacks";
            this.checkBoxRegistryHacks.UseVisualStyleBackColor = true;
            // 
            // flowLayoutGeneralPrograms
            // 
            this.flowLayoutGeneralPrograms.AutoScroll = true;
            this.flowLayoutGeneralPrograms.AutoSize = true;
            this.flowLayoutGeneralPrograms.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutGeneralPrograms.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutGeneralPrograms.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutGeneralPrograms.Location = new System.Drawing.Point(40, 44);
            this.flowLayoutGeneralPrograms.MaximumSize = new System.Drawing.Size(420, 600);
            this.flowLayoutGeneralPrograms.MinimumSize = new System.Drawing.Size(58, 50);
            this.flowLayoutGeneralPrograms.Name = "flowLayoutGeneralPrograms";
            this.flowLayoutGeneralPrograms.Size = new System.Drawing.Size(58, 50);
            this.flowLayoutGeneralPrograms.TabIndex = 44;
            // 
            // flowLayoutPanelOffice
            // 
            this.flowLayoutPanelOffice.AutoSize = true;
            this.flowLayoutPanelOffice.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelOffice.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutPanelOffice.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelOffice.Location = new System.Drawing.Point(408, 468);
            this.flowLayoutPanelOffice.MaximumSize = new System.Drawing.Size(0, 150);
            this.flowLayoutPanelOffice.MinimumSize = new System.Drawing.Size(58, 50);
            this.flowLayoutPanelOffice.Name = "flowLayoutPanelOffice";
            this.flowLayoutPanelOffice.Size = new System.Drawing.Size(58, 50);
            this.flowLayoutPanelOffice.TabIndex = 45;
            // 
            // flowLayoutPanelPcCare
            // 
            this.flowLayoutPanelPcCare.AutoSize = true;
            this.flowLayoutPanelPcCare.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelPcCare.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelPcCare.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelPcCare.Location = new System.Drawing.Point(408, 44);
            this.flowLayoutPanelPcCare.MaximumSize = new System.Drawing.Size(0, 150);
            this.flowLayoutPanelPcCare.MinimumSize = new System.Drawing.Size(58, 50);
            this.flowLayoutPanelPcCare.Name = "flowLayoutPanelPcCare";
            this.flowLayoutPanelPcCare.Size = new System.Drawing.Size(58, 50);
            this.flowLayoutPanelPcCare.TabIndex = 46;
            // 
            // flowLayoutPanelGamingPlatforms
            // 
            this.flowLayoutPanelGamingPlatforms.AutoScroll = true;
            this.flowLayoutPanelGamingPlatforms.AutoSize = true;
            this.flowLayoutPanelGamingPlatforms.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelGamingPlatforms.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutPanelGamingPlatforms.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelGamingPlatforms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.flowLayoutPanelGamingPlatforms.Location = new System.Drawing.Point(673, 44);
            this.flowLayoutPanelGamingPlatforms.MaximumSize = new System.Drawing.Size(0, 150);
            this.flowLayoutPanelGamingPlatforms.MinimumSize = new System.Drawing.Size(58, 50);
            this.flowLayoutPanelGamingPlatforms.Name = "flowLayoutPanelGamingPlatforms";
            this.flowLayoutPanelGamingPlatforms.Size = new System.Drawing.Size(58, 50);
            this.flowLayoutPanelGamingPlatforms.TabIndex = 48;
            // 
            // flowLayoutPanelProgramming
            // 
            this.flowLayoutPanelProgramming.AutoScroll = true;
            this.flowLayoutPanelProgramming.AutoSize = true;
            this.flowLayoutPanelProgramming.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelProgramming.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutPanelProgramming.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelProgramming.Location = new System.Drawing.Point(408, 256);
            this.flowLayoutPanelProgramming.MaximumSize = new System.Drawing.Size(0, 180);
            this.flowLayoutPanelProgramming.MinimumSize = new System.Drawing.Size(58, 50);
            this.flowLayoutPanelProgramming.Name = "flowLayoutPanelProgramming";
            this.flowLayoutPanelProgramming.Size = new System.Drawing.Size(58, 50);
            this.flowLayoutPanelProgramming.TabIndex = 47;
            // 
            // labelgeneral
            // 
            this.labelgeneral.AutoSize = true;
            this.labelgeneral.Location = new System.Drawing.Point(34, 15);
            this.labelgeneral.Name = "labelgeneral";
            this.labelgeneral.Size = new System.Drawing.Size(107, 13);
            this.labelgeneral.TabIndex = 49;
            this.labelgeneral.Text = "General Programs";
            // 
            // labelpcCare
            // 
            this.labelpcCare.AutoSize = true;
            this.labelpcCare.Location = new System.Drawing.Point(405, 13);
            this.labelpcCare.Name = "labelpcCare";
            this.labelpcCare.Size = new System.Drawing.Size(47, 13);
            this.labelpcCare.TabIndex = 50;
            this.labelpcCare.Text = "pcCare";
            // 
            // labelGamingPlatforms
            // 
            this.labelGamingPlatforms.AutoSize = true;
            this.labelGamingPlatforms.Location = new System.Drawing.Point(673, 12);
            this.labelGamingPlatforms.Name = "labelGamingPlatforms";
            this.labelGamingPlatforms.Size = new System.Drawing.Size(101, 13);
            this.labelGamingPlatforms.TabIndex = 51;
            this.labelGamingPlatforms.Text = "GamingPlatforms";
            // 
            // labelProgramming
            // 
            this.labelProgramming.AutoSize = true;
            this.labelProgramming.Location = new System.Drawing.Point(405, 225);
            this.labelProgramming.Name = "labelProgramming";
            this.labelProgramming.Size = new System.Drawing.Size(79, 13);
            this.labelProgramming.TabIndex = 52;
            this.labelProgramming.Text = "Programming";
            // 
            // labeloffice
            // 
            this.labeloffice.AutoSize = true;
            this.labeloffice.Location = new System.Drawing.Point(405, 440);
            this.labeloffice.Name = "labeloffice";
            this.labeloffice.Size = new System.Drawing.Size(39, 13);
            this.labeloffice.TabIndex = 53;
            this.labeloffice.Text = "office";
            // 
            // checkBoxChangeWRDPort
            // 
            this.checkBoxChangeWRDPort.AutoSize = true;
            this.checkBoxChangeWRDPort.Location = new System.Drawing.Point(717, 642);
            this.checkBoxChangeWRDPort.Name = "checkBoxChangeWRDPort";
            this.checkBoxChangeWRDPort.Size = new System.Drawing.Size(130, 17);
            this.checkBoxChangeWRDPort.TabIndex = 54;
            this.checkBoxChangeWRDPort.Text = "Change WRD Port";
            this.checkBoxChangeWRDPort.UseVisualStyleBackColor = true;
            this.checkBoxChangeWRDPort.CheckStateChanged += new System.EventHandler(this.checkBoxChangeWRDPort_CheckStateChanged);
            // 
            // textBoxWrdPort
            // 
            this.textBoxWrdPort.BackColor = System.Drawing.Color.White;
            this.textBoxWrdPort.Location = new System.Drawing.Point(853, 639);
            this.textBoxWrdPort.Name = "textBoxWrdPort";
            this.textBoxWrdPort.Size = new System.Drawing.Size(93, 20);
            this.textBoxWrdPort.TabIndex = 55;
            this.textBoxWrdPort.Tag = "Port number between 34568 and 65535";
            this.textBoxWrdPort.TextChanged += new System.EventHandler(this.textBoxWrdPort_TextChanged);
            this.textBoxWrdPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWrdPort_KeyPress);
            this.textBoxWrdPort.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseMove);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(4, 675);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(95, 25);
            this.buttonReset.TabIndex = 56;
            this.buttonReset.Text = "Reset Form";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonRandomWRDport
            // 
            this.buttonRandomWRDport.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonRandomWRDport.Location = new System.Drawing.Point(853, 663);
            this.buttonRandomWRDport.Name = "buttonRandomWRDport";
            this.buttonRandomWRDport.Size = new System.Drawing.Size(75, 23);
            this.buttonRandomWRDport.TabIndex = 57;
            this.buttonRandomWRDport.Text = "Random Port";
            this.buttonRandomWRDport.UseVisualStyleBackColor = true;
            this.buttonRandomWRDport.Click += new System.EventHandler(this.buttonRandomWRDport_Click);
            // 
            // checkBoxScanLan
            // 
            this.checkBoxScanLan.AutoSize = true;
            this.checkBoxScanLan.Location = new System.Drawing.Point(717, 680);
            this.checkBoxScanLan.Name = "checkBoxScanLan";
            this.checkBoxScanLan.Size = new System.Drawing.Size(80, 17);
            this.checkBoxScanLan.TabIndex = 58;
            this.checkBoxScanLan.Text = "Scan Lan";
            this.checkBoxScanLan.UseVisualStyleBackColor = true;
            this.checkBoxScanLan.CheckStateChanged += new System.EventHandler(this.checkBoxScanLan_CheckStateChanged);
            // 
            // panelMainPanel
            // 
            this.panelMainPanel.AutoSize = true;
            this.panelMainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMainPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.panelMainPanel.Controls.Add(this.checkBoxScanLan);
            this.panelMainPanel.Controls.Add(this.buttonRandomWRDport);
            this.panelMainPanel.Controls.Add(this.buttonReset);
            this.panelMainPanel.Controls.Add(this.textBoxWrdPort);
            this.panelMainPanel.Controls.Add(this.checkBoxChangeWRDPort);
            this.panelMainPanel.Controls.Add(this.labeloffice);
            this.panelMainPanel.Controls.Add(this.labelProgramming);
            this.panelMainPanel.Controls.Add(this.labelGamingPlatforms);
            this.panelMainPanel.Controls.Add(this.labelpcCare);
            this.panelMainPanel.Controls.Add(this.labelgeneral);
            this.panelMainPanel.Controls.Add(this.flowLayoutPanelProgramming);
            this.panelMainPanel.Controls.Add(this.flowLayoutPanelGamingPlatforms);
            this.panelMainPanel.Controls.Add(this.flowLayoutPanelPcCare);
            this.panelMainPanel.Controls.Add(this.flowLayoutPanelOffice);
            this.panelMainPanel.Controls.Add(this.flowLayoutGeneralPrograms);
            this.panelMainPanel.Controls.Add(this.checkBoxRegistryHacks);
            this.panelMainPanel.Controls.Add(this.checkBoxDrivers);
            this.panelMainPanel.Controls.Add(this.btnInstall);
            this.panelMainPanel.Location = new System.Drawing.Point(12, 12);
            this.panelMainPanel.MinimumSize = new System.Drawing.Size(10, 10);
            this.panelMainPanel.Name = "panelMainPanel";
            this.panelMainPanel.Size = new System.Drawing.Size(949, 703);
            this.panelMainPanel.TabIndex = 21;
            // 
            // txtboxPathFileLocation
            // 
            this.txtboxPathFileLocation.Location = new System.Drawing.Point(165, 11);
            this.txtboxPathFileLocation.Name = "txtboxPathFileLocation";
            this.txtboxPathFileLocation.Size = new System.Drawing.Size(515, 20);
            this.txtboxPathFileLocation.TabIndex = 33;
            this.txtboxPathFileLocation.Tag = "Enter Paths.txt *path* full here";
            this.txtboxPathFileLocation.TextChanged += new System.EventHandler(this.txtboxPathFileLocation_TextChanged);
            this.txtboxPathFileLocation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseMove);
            // 
            // labelPathFile
            // 
            this.labelPathFile.AutoSize = true;
            this.labelPathFile.Location = new System.Drawing.Point(15, 14);
            this.labelPathFile.Name = "labelPathFile";
            this.labelPathFile.Size = new System.Drawing.Size(120, 13);
            this.labelPathFile.TabIndex = 39;
            this.labelPathFile.Text = "Paths  File Location";
            // 
            // panelPathsPanel
            // 
            this.panelPathsPanel.Controls.Add(this.labelPathFile);
            this.panelPathsPanel.Controls.Add(this.txtboxPathFileLocation);
            this.panelPathsPanel.Location = new System.Drawing.Point(12, 13);
            this.panelPathsPanel.Name = "panelPathsPanel";
            this.panelPathsPanel.Size = new System.Drawing.Size(692, 41);
            this.panelPathsPanel.TabIndex = 22;
            // 
            // textBoxDateTime
            // 
            this.textBoxDateTime.Enabled = false;
            this.textBoxDateTime.Location = new System.Drawing.Point(12, 4);
            this.textBoxDateTime.MaxLength = 50;
            this.textBoxDateTime.Name = "textBoxDateTime";
            this.textBoxDateTime.ReadOnly = true;
            this.textBoxDateTime.Size = new System.Drawing.Size(166, 20);
            this.textBoxDateTime.TabIndex = 56;
            // 
            // Deployer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(970, 720);
            this.Controls.Add(this.textBoxDateTime);
            this.Controls.Add(this.panelPathsPanel);
            this.Controls.Add(this.panelMainPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Deployer";
            this.Text = "Deployer";
            this.panelMainPanel.ResumeLayout(false);
            this.panelMainPanel.PerformLayout();
            this.panelPathsPanel.ResumeLayout(false);
            this.panelPathsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer time;
        private System.Windows.Forms.ToolTip toolTipdynamic;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.CheckBox checkBoxDrivers;
        private System.Windows.Forms.CheckBox checkBoxRegistryHacks;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutGeneralPrograms;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOffice;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPcCare;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGamingPlatforms;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProgramming;
        private System.Windows.Forms.Label labelgeneral;
        private System.Windows.Forms.Label labelpcCare;
        private System.Windows.Forms.Label labelGamingPlatforms;
        private System.Windows.Forms.Label labelProgramming;
        private System.Windows.Forms.Label labeloffice;
        private System.Windows.Forms.CheckBox checkBoxChangeWRDPort;
        private System.Windows.Forms.TextBox textBoxWrdPort;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonRandomWRDport;
        private System.Windows.Forms.CheckBox checkBoxScanLan;
        private System.Windows.Forms.Panel panelMainPanel;
        private System.Windows.Forms.TextBox txtboxPathFileLocation;
        private System.Windows.Forms.Label labelPathFile;
        private System.Windows.Forms.Panel panelPathsPanel;
        private System.Windows.Forms.TextBox textBoxDateTime;
    }
}

