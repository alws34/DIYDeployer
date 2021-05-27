using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace Deployer
{

    public partial class Deployer : Form
    {
        /*********/
        /*Fields**/
        /*********/
        private string DeployerScript;
        private string installationsPath;
        private List<string> programsToInstall = new List<string>();
        private List<string> files = new List<string>(); // all programs and files within the installations path
        private bool isPortAvailable = true;
        private Process installBatch;

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
                    CheckBox[] checkBoxes = new CheckBox[100];
                    int counter = 0;
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
                            chckbox.Text = chckboxText.Replace(@"\", "");
                            chckbox.CheckedChanged += new EventHandler(checkBox_CheckChanged);
                            checkBoxes[counter] = chckbox;
                            counter++;
                        }
                        //FlowLayoutPanel flp = new FlowLayoutPanel();
                        //flp.Visible = true;
                        //flp.BorderStyle = BorderStyle.Fixed3D;
                        //flp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                        flpl.Controls.AddRange(checkBoxes);
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
                    files.AddRange(Directory.GetFiles(installationsPath, ".", SearchOption.AllDirectories));// get array of all files in the folder
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
                        createcheckBoxes(dir, flp_main);
                        flp_main.Visible = true;
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

        private string GetSilentSwitches(string program)//get silent install switches from DB
        {
            string DBpath = installationsPath + @"\SilentSwitchesDB.txt";

            using (StreamReader sr = new StreamReader(DBpath))
            {
                char delimiter = '^';
                string line = sr.ReadLine();
                while (sr.ReadLine() != null)
                {
                    string program_name = line.Split(delimiter).First();
                    string silentswitch = line.Split(delimiter).Last();
                    if (program.Contains(program_name))
                        return silentswitch;// returns a path
                    return null;//if program wasnt found in db
                }
                return null;
            }
        }

        private void startInstall()
        {
            try
            {
                if (File.Exists(DeployerScript))
                {
                    using (StreamWriter w = new StreamWriter(DeployerScript))
                    {
                        List<string> nonsilents = new List<string>(); ;
                        foreach (string program in programsToInstall)
                        {
                            string silentswitch = GetSilentSwitches(program);
                            if (silentswitch != null)//if silent switch was found
                            {
                                w.WriteLine(program + " " + silentswitch); // write all silents first 
                            }
                            else
                            {
                                nonsilents.Add(program);//add to non silents array
                            }
                        }
                        foreach (string program in nonsilents)//than write all nonsilents (for order of installation)
                        {
                            w.WriteLine(program);
                        }
                        if (checkBoxDrivers.Checked)//add open drivers folder to the installation file
                        {
                            string drivers_folder = installationsPath + @"\Drivers";
                            w.WriteLine("explorer " + drivers_folder);
                        }
                        if (checkBoxRegistryHacks.Checked)//add registry hacks to installations batch
                        {
                            string reghacks = installationsPath + @"\RegistryHacks\RegistyHacks.bat";
                            w.WriteLine(reghacks);
                        }
                        w.Close();
                        using (installBatch = new Process())//run batch file
                        {
                            installBatch.StartInfo.FileName = DeployerScript;
                            installBatch.Start();
                        }
                        finishInstall();
                    }
                }
            }
            catch (Exception excep)
            {
                showErrorMessage(excep.ToString());
            }
        }

        private void finishInstall()//finalize
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to run the finalizing bat file?\nThe folowing things will happen:\n-power plans configuration\n-python updates and extra config\n-some websites will be opened (microsoft store use full download)\n-A restart timer for your choosing (input in ms)", "run restartAfterFinish.bat", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start(installationsPath + @"\restartAfterFinish.bat"); // start restartAfterFinish.bat on finish
            }
            else
                Dispose();//just exit
        }

        private void setWRDPort()// set WRD port
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
            catch (Exception e)
            {
                if (string.IsNullOrEmpty(textBoxWrdPort.Text))
                    return;
                else
                    showErrorMessage(e.ToString() + "\n-@install");
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

        private void reset()
        {
            Deployer d = new Deployer(); // start new instance
            d.Show();
            Dispose();//remove current instance
        }//reset the form


        /*********/
        /*Events*/
        /*********/
        private void install(object sender, EventArgs e)//start install 
        {
            startInstall();
        }

        private void checkBox_CheckChanged(object sender, EventArgs e)//add program to the installation array
        {
            CheckBox chckbox = sender as CheckBox;
            string programName = chckbox.Name;
            string programPath = chckbox.Tag.ToString();
            if (chckbox.Checked)//add program
            {
                programsToInstall.Add(programPath);
            }
            else//remove program from array (later will be written to the batch file for execute
            {
                for (int i = 0; i < programsToInstall.Count; i++)
                {
                    if (programsToInstall[i] == programPath)
                    {
                        //programsToInstall[i] = "";
                        programsToInstall.RemoveAt(i);
                    }
                }
            }
        }

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

        private void time_Tick(object sender, EventArgs e)//date and time
        {
            textBoxDateTime.Text = DateTime.Now.ToString();
        }

        private void textBox_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox textbox = (sender as TextBox);
            toolTipdynamic.SetToolTip(textbox, textbox.Tag.ToString());
            toolTipdynamic.ToolTipTitle = textbox.Name;
        }//tooltip message

        private void buttonReset_Click(object sender, EventArgs e)//reset form
        {
            reset();
        }

        private void buttonRandomWRDport_Click(object sender, EventArgs e)//get random WRD port
        {
            int randPort = new Random().Next(34568, 65535);
            textBoxWrdPort.Text = randPort.ToString();
        }

        private void btnLanScan_Click(object sender, EventArgs e)//get lan scanner form
        {
            frmLanScanner subscan = new frmLanScanner();
        }

    }
}
