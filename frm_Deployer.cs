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
             * TODO: 
             *FIX DUPLICATED CODE
             */
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
                            flp = new FlowLayoutPanel();//for each directory create a flow layout panel to host its checkboxes
                            flp.AutoScroll = true;
                            flp.AutoSize = true;
                            flp.MaximumSize = new Size(400, 200); // width * height
                            flp.BackColor = Color.White;

                            foreach (CheckBox c in checkboxlst) // add check boxes to flow layout panel
                                flp.Controls.Add(c);

                            if (flp.Controls.Count >= 1) //dont add empty panels
                                flpl.Controls.Add(flp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                reenterPaths("createCheckBoxes says:" + e.ToString());
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
                    string[] subdirectories = Directory.GetDirectories(installationsPath); //list of all sub directories

                    if (!(File.Exists(installationsPath + @"\Deplyer.bat")))//if deployer.bat does NOT exist, create it
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
            catch (Exception e)
            {
                reenterPaths(e.ToString() + " @setPanels");
            }
        }

        private string GetSilentSwitches(string program, string[] switches_arr)//get silent install switches from DB
        {
            char delimiter = '^';
            foreach (string line in switches_arr)
            {
                string program_name = line.Split(delimiter).First();
                string silent_switch = line.Split(delimiter).Last();
                if (program.Contains(program_name))
                    return silent_switch;// returns a path
            }
            return null; // if program wasnt found in DB
        }

        private void startInstall(string path)//start installation
        {
            //Process installBatch;
            try
            {
                if (File.Exists(DeployerScript))
                {
                    using (StreamWriter w = new StreamWriter(DeployerScript)) //write to batch file
                    {
                        //string init = "@Echo On\nColor 1A\n";
                        //init += @"cd %systemroot%\system32";
                        //init += "\ncall :IsAdmin";
                        //init += "\n:IsAdmin";
                        //init += "\nReg.exe query " + '\u0022' + @"HKU\S-1-5-19\Environment" + '\u0022';
                        //init += "\nIf Not %ERRORLEVEL% EQU 0 (";
                        //init += "\n Cls & Echo You must have administrator rights to continue ...";
                        //init += "\n Pause & Exit";
                        //init += "\n)";
                        //init += "\nCls";
                        //// init += "\ngoto:eof";
                        //init += "\n\n\n";
                        //w.WriteLine(init);

                        List<string> nonsilents = new List<string>(); ;
                        foreach (string program in programsToInstall)
                        {
                            string DBpath = installationsPath + @"\SilentSwitchesDB.txt";
                            string[] silentswitches = File.ReadAllLines(DBpath);//get atrray of silen switches
                            string silentswitch = GetSilentSwitches(program, silentswitches);
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

                        finishInstall(DeployerScript);
                        ProcessStartInfo procInfo = new ProcessStartInfo();
                        procInfo.UseShellExecute = true;
                        procInfo.CreateNoWindow = false;
                        procInfo.FileName = DeployerScript;
                        procInfo.WorkingDirectory = path; //The working DIR.
                        procInfo.Verb = "runas"; //running the process as admin
                        Process.Start(procInfo);
                    }
                }
            }
            catch (Exception e)
            {
                showErrorMessage(e.ToString() + "\n@install");
            }
        }

        /*
         * Finalize batch script
         */
        private void finishInstall(string script)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Do you want to run the finalizing bat file?\nThe folowing things will happen:" +
                "\n-power plans configuration\n-python updates and extra config\n-some websites will be opened " +
                "(microsoft store use full download)\n-A restart timer for your choosing (input in ms)",
                "run restartAfterFinish.bat"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                string[] finalize = File.ReadAllLines(installationsPath + @"\restartAfterFinish.txt"); // read the entire file consequently
                using (StreamWriter w = new StreamWriter(DeployerScript, true))
                {
                    foreach (string line in finalize)
                    {
                        w.WriteLine(line);
                    }
                    //for (int i = 0; i < finalize.Length; i++)
                    //    w.WriteLine(finalize[i]);
                }
            }
        }

        /*
         *set WRD port
         */
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
            catch (Exception e)
            {
                if (string.IsNullOrEmpty(textBoxWrdPort.Text))
                    return;
                else
                    showErrorMessage(e.ToString() + "\n-@install");
            }
        }

        /*
         *re enter paths for invalid input
         */
        private void reenterPaths(string location)
        {
            txtboxInstallsPath.Text = "";
            btnInstall.Enabled = false;
            showErrorMessage("please check your Paths for valid paths\n-" + location);
        }

        /*
        *display critical error message
        */
        private void showErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /*
        *set the deployer.bat path
        */
        private void setDeployerPath()
        {
            DeployerScript = installationsPath + @"\Deplyer.bat";
        }

        /*
         *get random WRD port
         */
        private void getRandomPort()
        {
            // string randPort = new Random().Next(34568, 65535).ToString();
            // textBoxWrdPort.Text = randPort.ToString();
            textBoxWrdPort.Text = new Random().Next(34568, 65535).ToString();
        }
        /*********/
        /*Events*/
        /*********/
        private void install(object sender, EventArgs e)//start install 
        {
            startInstall(txtboxInstallsPath.Text);
        }

        private void reset()//reset the form
        {
            Deployer d = new Deployer(); //start new instance
            d.Show();
            Dispose();//remove current instance
        }

        /*
         * add program to the installation batch
         */
        private void checkBox_CheckChanged(object sender, EventArgs e)
        {
            CheckBox chckbox = sender as CheckBox;
            string programPath = chckbox.Tag.ToString();
            if (chckbox.Checked)//if program is selected - add
            {
                programsToInstall.Add(programPath);
            }
            else//remove program from array 
            {
                for (int i = 0; i < programsToInstall.Count; i++)
                {
                    if (programsToInstall[i] == programPath)
                    {
                        programsToInstall.RemoveAt(i);
                    }
                }
            }
        }

        /*
         *paths validation and initialization
         */
        private void txtboxInstallsPath_TextChanged(object sender, EventArgs e)
        {
            setPanels();
        }

        /*
         *port textbox to accept only numbers
         **/
        private void textBoxWrdPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        /*
         * enable "add port" textbox
         **/
        private void checkBoxChangeWRDPort_CheckStateChanged(object sender, EventArgs e)
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
