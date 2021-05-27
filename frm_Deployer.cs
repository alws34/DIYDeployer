using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Data;

namespace Deployer
{

    public partial class Deployer : Form
    {
        /*********/
        /*Fields**/
        /*********/

        //private static string General = "";
        //private static string PCcare = "";
        //private static string Office = "";
        //private static string GamingPlatforms = "";
        //private static string Programming = "";
        //private static string Drivers = "";
        //private static string RegistyHacks = "";
        //private static string silent_install_switchesArguments = "";
        //private static string silent_install_switches = "";
        //private static string bat_path = "";
        //private static string restartAfterFinish = "";
        //private string pathsFileLocation = @"";// = @"C:\Users\alws3\Desktop\Installs\Paths.txt";

        //private string[] paths = { General, PCcare, Programming, GamingPlatforms, Office, Drivers, RegistyHacks, silent_install_switchesArguments, silent_install_switches, bat_path, restartAfterFinish };
        private string connSTR = @"Microsoft.ACE.OLEDB.12.0; Data Source =| DataDirectory |\SilentSwitchedDB.accdb";
        private string DeployerScript;
        private string installationsPath;
        private string[] programsToInstall = new string[100];
        private int programsToInstallcounter = 0;
        bool isPortAvailable = true;
        Process installBatch;

        public Deployer()
        {
            InitializeComponent();
            btnInstall.Enabled = false;
            textBoxWrdPort.Enabled = false;
        }

        /*********/
        /*Methods*/
        /*********/
        private void createcheckBoxes(string path, FlowLayoutPanel flpl)//create checkboxes
        {
            try
            {
                FileAttributes attr = File.GetAttributes(path);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    string programName = "";
                    string chckboxText = "";
                    string delimiter = path;
                    string exe = ".exe";
                    string msi = ".msi";
                    foreach (string fileName in Directory.GetFiles(path))//get all files in directory
                    {
                        programName = fileName.Replace(delimiter, "");
                        chckboxText = programName;
                        if (!(fileName.Contains(".inf") || fileName.Contains(".ini")))
                        {
                            if (programName.Contains(exe) || programName.Contains(msi))
                                chckboxText = programName.Replace(exe, "").Replace(msi, "");
                            CheckBox chckbox = new CheckBox();
                            chckbox.Name = programName;
                            chckbox.Tag = path + programName;
                            chckbox.Text = chckboxText;
                            chckbox.CheckedChanged += new EventHandler(checkBox_CheckChanged);
                            flpl.Controls.Add(chckbox);
                        }
                    }
                }
            }
            catch (IOException ioException)
            {
                reenterPaths("createcheckBoxes says:" + ioException.ToString());
            }
        }

        private void setPanels()
        {
            try
            {
                installationsPath = txtboxInstallsPath.Text;
                Regex Path_regex = new Regex(@"[A-Za-z]{1}:\\[\s\S\d]*");//accept only paths eg: c:\..\..\.. 
                Match match = Path_regex.Match(installationsPath);
                if (match.Success)
                {
                    btnInstall.Enabled = true;
                    string[] subdirectories = Directory.GetDirectories(installationsPath);
                    string[] files = Directory.GetFiles(installationsPath, "*ProfileHandler.cs");
                    if (!(File.Exists(installationsPath + @"\Deplyer.bat")))//if deployer.bat NOT exists
                    {
                        File.Create(installationsPath + @"\Deplyer.bat");
                        setDeployerPath();
                    }
                    else
                    {
                        setDeployerPath();
                    }

                    foreach (string dir in subdirectories)
                    {
                        FlowLayoutPanel flp = new FlowLayoutPanel();
                        createcheckBoxes(dir, flp);
                        panelMainPanel.Controls.Add(flp);
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                reenterPaths("FoldernotFoundException@setPanels");
            }
            catch (FileNotFoundException)
            {
                reenterPaths("FileNotFoundException@setPanels");
            }
            catch (IOException)
            {
                reenterPaths("IOException@setPanels");
            }
        }
        private DataTable getData()//get silent install switches from DB
        {
            string strSQL = "SELECT * FROM Switches";
            OleDbConnection myConn = new OleDbConnection(connSTR);
            OleDbDataAdapter myCmd = new OleDbDataAdapter(strSQL, myConn);
            myConn.Open();
            DataSet dtSet = new DataSet();
            myCmd.Fill(dtSet, "Silent Switches");
            DataTable dTable = dtSet.Tables[0];
            myConn.Close();
            return dTable;
        }
        private void install(object sender, EventArgs e)//start install 
        {
            try
            {
                DataTable switches = getData();

                using (StreamWriter w = new StreamWriter(DeployerScript))
                {
                    foreach (var item in programsToInstall)
                    {
                        int counter = 0;
                        for (int i = 0; i < silent_install_switches.Length; i++)
                        {
                            if (silent_install_switches[i] == item && counter == 0)
                            {
                                w.WriteLine(silent_install_switchesArguments[i]);
                                counter++;
                            }
                        }
                        if (counter == 0)
                            w.WriteLine(item);
                    }
                    if (checkBoxDrivers.Checked)//add open drivers folder to the installation file
                    {
                        if (paths[5].Contains("Drivers"))
                            w.WriteLine("explorer " + paths[5]);
                        else
                            showErrorMessage("no Drivers path was found! check your\nPaths.txt file\n-@install");
                    }
                }
                if (checkBoxRegistryHacks.Checked)//registry hacks
                {
                    if (paths[6].Contains("RegistyHacks.bat"))
                        System.Diagnostics.Process.Start(paths[6]);
                    else
                        showErrorMessage("no RegistyHacks.bat was found! check your\nPaths.txt file\n-@install");
                }
                if (DeployerScript)
                    using (installBatch = new Process())
                    {
                        installBatch.StartInfo.FileName = DeployerScript;
                        installBatch.Start();
                    }
                // System.Diagnostics.Process.Start(DeployerScript);//start batch file for installationsPath
                else
                    showErrorMessage("no Deployer.bat file was found!\n-@install");
                if (installBatch.HasExited)//revise
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to run the finalizing bat file?\nThe folowing things will happen:\n-power plans configuration\n-python updates and extra config\n-some websites will be opened (microsoft store use full download)\n-A restart timer for your choosing (input in ms)", "run restartAfterFinish.bat", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(paths[10]); // start restartAfterFinish.bat on finish
                    }
                    else
                        return;
                }
            }
            catch (Exception)
            {
                reenterPaths("Exception@install");
            }
        }

        private void setWRDPort()
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxWrdPort.Text) && textBoxWrdPort.Text.Length >= 4 && textBoxWrdPort.Text.Length <= 5)
                {
                    int port = Int32.Parse(textBoxWrdPort.Text);
                    if (port > 1000 && port < 65535)
                    {
                        IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                        TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
                        foreach (TcpConnectionInformation tcpConninfo in tcpConnInfoArray)
                        {
                            if (tcpConninfo.LocalEndPoint.Port == port)
                            {
                                isPortAvailable = false;
                                break;
                            }
                        }
                        if (isPortAvailable)
                        {
                            string changeWRDPort = @"reg add " + '\u0022' + @"HKLM\System\CurrentControlSet\Control\Terminal Server\WinStations\RDP - Tcp" + '\u0022' + " /v PortNumber /t REG_DWORD /d " + port;
                            DialogResult WRDportDialog = MessageBox.Show("Are you sure you want to change the following port?\n WRD RDP Port: " + port, "Port sanity check", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (WRDportDialog == DialogResult.Yes)
                            {
                                using (StreamWriter w = new StreamWriter(DeployerScript))
                                {
                                    w.WriteLine(changeWRDPort);
                                }
                            }
                            else
                            {
                                showErrorMessage("port is already taken.\nplease try again.");
                                return;
                            }
                        }
                    }
                    else
                    {
                        showErrorMessage("please enter port larger than 1000 and smaller than 65535");
                    }
                }
            }
            catch (FormatException)
            {
                if (string.IsNullOrEmpty(textBoxWrdPort.Text))
                    return;
            }
        }
        private void reenterPaths(string location)//re enter paths for invalid input
        {
            txtboxInstallsPath.Text = "";
            btnInstall.Enabled = false;
            showErrorMessage("please check your Paths for valid paths\n-" + location);
        }
        private void showErrorMessage(string message)//critical error message
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void setDeployerPath()//set the deployer.bat path
        {
            DeployerScript = installationsPath + @"\Deplyer.bat";
        }
        /*********/
        /*Events*/
        /*********/

        private void checkBox_CheckChanged(object sender, EventArgs e)//add program to the installation array
        {
            CheckBox chckbox = sender as CheckBox;
            string programName = chckbox.Name;//Tag.ToString();
            string programPath = chckbox.Tag.ToString();
            if (chckbox.Checked)//add program
            {
                programsToInstall[programsToInstallcounter] = programPath;
                programsToInstallcounter++;
            }
            else//remove program from array (later will be written to the batch file for execute
            {
                for (int i = 0; i < programsToInstall.Length; i++)
                {
                    if (programsToInstall[i] == programName)
                    {
                        programsToInstall[i] = null;
                        programsToInstall = programsToInstall.Except(new List<string> { string.Empty }).ToArray();
                    }
                }
            }
        }
        //foreach (FileInfo file in Directory.GetFiles(path))
        //                        {
        //                            if (file.Exists && file.Name.Contains("Deployer.bat"))
        //                            {
        //                                showErrorMessage("port is already taken.\nplease try again.");
        //                                return;
        //                            }
        //                            else
        //                            {
        //                                using (StreamWriter w = new StreamWriter(file))
        //                                {
        //                                    w.WriteLine(changeWRDPort);
        //                                }
        //                            }
        //                        }
        private void txtboxInstallsPath_TextChanged(object sender, EventArgs e)//paths validation and initialization
        {
            setPanels();
        }

        private void textBoxWrdPort_KeyPress(object sender, KeyPressEventArgs e)//port textbox accept only numbers
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void checkBoxChangeWRDPort_CheckStateChanged(object sender, EventArgs e)//chackbox to add port
        {
            if (checkBoxChangeWRDPort.Checked)
            {
                textBoxWrdPort.Enabled = true;
                buttonRandomWRDport.Show();
            }
            else
            {
                textBoxWrdPort.Enabled = false;
                buttonRandomWRDport.Hide();
                textBoxWrdPort.Text = "";
            }
        }

        private void textBoxWrdPort_TextChanged(object sender, EventArgs e)//port validation
        {
            setWRDPort();
        }

        private void time_Tick(object sender, EventArgs e)
        {
            textBoxDateTime.Text = DateTime.Now.ToString();
        }//date and time

        private void textBox_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox textbox = (sender as TextBox);
            toolTipdynamic.SetToolTip(textbox, textbox.Tag.ToString());
            toolTipdynamic.ToolTipTitle = textbox.Name;
        }//tooltip message

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Deployer d = new Deployer();
            d.Show();
            buttonRandomWRDport.Hide();
            this.Hide();
        }

        private void buttonRandomWRDport_Click(object sender, EventArgs e)
        {
            int randPort = new Random().Next(34568, 65535);
            textBoxWrdPort.Text = randPort.ToString();
        }

        private void checkBoxScanLan_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxScanLan.Checked)
            {
                frmLanScanner subscan = new frmLanScanner();
            }
            else
            {
                return;
            }
        }
    }
}