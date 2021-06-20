using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Deployer
{

    public partial class Deployer : Form
    {
        /*********/
        /*Fields**/
        /*********/
        private string DeployerScript;
        private string installationsPath;
        private bool isPortAvailable = true;
        private List<string> programsToInstall = new List<string>(); //list of intended programs to install
        ToolTip toolTip = new ToolTip();

        public Deployer()
        {
            InitializeComponent();
            btnInstall.Enabled = false;
            textBoxWrdPort.Enabled = false;
        }

        /*********/
        /*Methods*/
        /*********/
        private void createCheckBoxes(string path, FlowLayoutPanel flpl)
        {
            /*
             * TODO 
             *FIX DUPLICATED CODE!!!
            **/
            try
            {
                FileAttributes attr = File.GetAttributes(path);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    string programName = "";
                    string chckboxText = "";

                    string exe = ".exe";//prefix
                    string msi = ".msi";//prefix
                    List<CheckBox> checkboxlst = new List<CheckBox>();//checkboxes
                    List<FlowLayoutPanel> flplst = new List<FlowLayoutPanel>();//flow layout panels list
                    FlowLayoutPanel flp;
                    int counter = 0;
                    string[] dirs = Directory.GetDirectories(path);
                    foreach (string dir in Directory.GetDirectories(path))
                    {
                        if (!(dir.Contains("Drivers")) && !(dir.Contains("drivers")) && !(dir.Contains("OSs")) && !(dir.Contains("operating systems")) && !(dir.Contains("RegistryHacks")) && !(dir.Contains("RPi")) && !(dir.Contains("RPI")))
                        {
                            List<string> files = new List<string>(); // list all files within the folder
                            files.AddRange(Directory.GetFiles(path, "*.exe", SearchOption.AllDirectories));//add all exe files
                            files.AddRange(Directory.GetFiles(path, "*.msi", SearchOption.AllDirectories));//add all msi files

                            foreach (string fileName in files)
                            {
                                string delimiter = Path.GetDirectoryName(fileName); //delimiter will be the folder path
                                if (fileName.Contains(exe) || fileName.Contains(msi))
                                {
                                    if (counter > 0 && checkboxlst != null)
                                    {
                                        programName = fileName.Replace(delimiter, "");
                                        chckboxText = programName.Replace(exe, "").Replace(msi, "");
                                        CheckBox chckbox = new CheckBox();
                                        chckbox.Name = programName;
                                        chckbox.Tag = path + programName;
                                        chckbox.Text = chckboxText.Replace(@"\", "");
                                        chckbox.CheckedChanged += new EventHandler(checkBox_CheckChanged);
                                        toolTip.SetToolTip(chckbox, chckbox.Tag.ToString());

                                        /*duplicants validation*/
                                        int added = 0;
                                        for (int i = 0; i < checkboxlst.Count; i++)
                                        {
                                            if (checkboxlst[i] != null && checkboxlst[i].Name == chckbox.Name)
                                            {
                                                added = 1;
                                            }
                                        }
                                        if (added == 0)
                                        {
                                            checkboxlst.Add(chckbox);
                                            counter++;
                                        }
                                        /*duplicants validation*/
                                    }
                                    else if (counter == 0)
                                    {
                                        programName = fileName.Replace(delimiter, "");
                                        chckboxText = programName.Replace(exe, "").Replace(msi, "");
                                        CheckBox chckbox = new CheckBox();
                                        chckbox.Name = programName;
                                        chckbox.Tag = path + programName;
                                        chckbox.Text = chckboxText.Replace(@"\", "");
                                        chckbox.CheckedChanged += new EventHandler(checkBox_CheckChanged);
                                        toolTip.SetToolTip(chckbox, chckbox.Tag.ToString());
                                        checkboxlst.Add(chckbox);
                                        counter++;
                                    }
                                }
                            }

                            flp = new FlowLayoutPanel();//foreach directory create a flow layout panel to host its checkboxes
                            flp.AutoScroll = true;
                            flp.AutoSize = true;
                            flp.BackColor = Color.White;

                            foreach (CheckBox c in checkboxlst)
                                flp.Controls.Add(c);

                           if(flp.Controls.Count>=1) //dont add empty panels
                                flpl.Controls.Add(flp);
                        }

                    }
                    //foreach (CheckBox c in checkboxlst)
                    //    flpl.Controls.Add(c);
                }
            }
            catch (Exception Exception)
            {
                reenterPaths("createCheckBoxes says:" + Exception.ToString());
            }
        }

        private void setPanels()
        {
            try
            {
                installationsPath = txtboxInstallsPath.Text;
                Regex Path_regex = new Regex(@"[A-Za-z]{1}:\\[\s\S\d]*");//accept only paths eg: c:\..\..\.. 
                Regex Network_Path_regex = new Regex(@"(?:\\)[a-zA-Z]*");//to check if accept folder paths eg \\alws34cloud\..\..

                Match matchpath = Path_regex.Match(installationsPath);
                Match matchnetwork = Network_Path_regex.Match(installationsPath);

                if (matchpath.Success || matchnetwork.Success)
                {
                    btnInstall.Enabled = true;
                    string[] subdirectories = Directory.GetDirectories(installationsPath);
                    //files.AddRange(Directory.GetFiles(installationsPath, ".", SearchOption.AllDirectories));// get array of all files in the folder
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
                        createCheckBoxes(dir, flp_main);
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

        private void startInstall()//start installation
        {
            Process installBatch;
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
                        // if (!(installBatch.HasExited))
                        finishInstall();
                    }
                }
            }
            catch (System.ComponentModel.Win32Exception w32)
            {
                finishInstall();
                showErrorMessage(w32.ToString() + "\n@install");
            }
            catch (ArgumentNullException ane)
            {
                showErrorMessage(ane.ToString() + "\n@install");
            }
            catch (IOException ioe)
            {
                reenterPaths(ioe.ToString() + "\n@install");
            }
            catch (ArgumentException ae)
            {
                showErrorMessage(ae.ToString() + "\n@install");
            }
            catch (System.Security.SecurityException se)
            {
                showErrorMessage(se.ToString() + "\n@install");
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

        private void reset()//reset the form
        {
            Deployer d = new Deployer(); // start new instance
            d.Show();
            Dispose();//remove current instance
        }

        private void getRandomPort()//get random WRD port
        {
            int randPort = new Random().Next(34568, 65535);
            textBoxWrdPort.Text = randPort.ToString();
        }
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
            getRandomPort();
        }

        private void btnLanScan_Click(object sender, EventArgs e)//get lan scanner form
        {
            frmLanScanner subscan = new frmLanScanner();
        }
    }
}
