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
        private string cwd = Directory.GetCurrentDirectory();
        private List<string> programsToInstall = new List<string>(); //list of intended programs to Install

        public Deployer()
        {
            InitializeComponent();
            GetInitFiles();

            btnInstall.Enabled = false;
            textBoxWrdPort.Enabled = false;
            buttonRandomWRDport.Enabled = false;
            checkBoxChangeWRDPort.Enabled = false;
            checkBoxDrivers.Enabled = false;

        }

        /*********/
        /*Methods*/
        /*********/
        private void GetInitFiles()
        {
            string get_finzalizer = "curl -OL https://raw.githubusercontent.com/alws34/DIYDeployer/main/restartAfterFinish.txt " + cwd;
            string get_silenswitches = "curl -OL https://raw.githubusercontent.com/alws34/DIYDeployer/main/SilentSwitchesDB.txt " + cwd;

            RunCmd(get_finzalizer);
            RunCmd(get_silenswitches);
        }

        private void RunCmd(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();
        }

        private void CreateCheckBoxes(string path, FlowLayoutPanel flpl)
        {
            /*
             * TODO: 
             *FIX DUPLICATED CODE
             */
            try
            {
                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    string programName = "";
                    string chckboxText = "";

                    //prefixes
                    string exe = ".exe";
                    string msi = ".msi";
                    string zip = ".zip";
                    string rar = ".rar";
                    string z = ".7z";

                    List<CheckBox> checkboxlst = new List<CheckBox>();//checkboxes
                    List<FlowLayoutPanel> flplst = new List<FlowLayoutPanel>();//flow layout panels list
                    FlowLayoutPanel flp; //main flow layout panel

                    int counter = 0;
                    string[] dirs = Directory.GetDirectories(path);

                    foreach (string dir in dirs)
                    {
                        /*
                         * TODO: 
                         *Deligate
                         */
                        if (!(dir.Contains("Drivers")) && !(dir.Contains("drivers")) && !(dir.Contains("OSs")) && !(dir.Contains("operating systems")) && !(dir.Contains("RegistryHacks")) && !(dir.Contains("RPi")) && !(dir.Contains("RPI")))//skip on those directories
                        {
                            List<string> files = new List<string>(); //list all files within the folder

                            files = AddFilesToList(path);//add all wanted file types to the list

                            foreach (string fileName in files)
                            {
                                ToolTip toolTip = new ToolTip();
                                string folderPath = Path.GetDirectoryName(fileName);

                                if (fileName.Contains(exe) || fileName.Contains(msi) || fileName.Contains(zip) || fileName.Contains(z) || fileName.Contains(rar))
                                {
                                    if (counter > 0 && checkboxlst != null)
                                    {
                                        programName = fileName.Replace(folderPath, "");
                                        chckboxText = programName.Replace(exe, "").Replace(msi, "").Replace(zip, "").Replace(z, "").Replace(rar, "");
                                      
                                        CheckBox chckbox = new CheckBox
                                        {
                                            Name = programName, // name
                                            Tag = path + programName, //full path
                                            Text = chckboxText.Replace(@"\", "")
                                        };//create checkboxes

                                        chckbox.CheckedChanged += new EventHandler(CheckBox_CheckChanged);//add checkbox checked changed event

                                        toolTip.SetToolTip(chckbox, chckbox.Tag.ToString());//set custom tooltip for each textbox

                                        /*duplicants validation*/
                                        bool added = false;
                                        for (int i = 0; i < checkboxlst.Count; i++)
                                            if (checkboxlst[i] != null && checkboxlst[i].Name == chckbox.Name)
                                                added = true;

                                        if (!added)
                                        {
                                            checkboxlst.Add(chckbox);
                                            counter++;
                                        }
                                        /*duplicants validation*/
                                    }

                                    else //if (counter == 0)
                                    {
                                        programName = fileName.Replace(folderPath, "");
                                        chckboxText = programName.Replace(exe, "").Replace(msi, "").Replace(zip, "").Replace(z, "").Replace(rar, "");
                                        CheckBox chckbox = new CheckBox
                                        {
                                            Name = programName,
                                            Tag = path + programName,
                                            Text = chckboxText.Replace(@"\", "")
                                        };
                                        chckbox.CheckedChanged += new EventHandler(CheckBox_CheckChanged);
                                        toolTip.SetToolTip(chckbox, chckbox.Tag.ToString());
                                        checkboxlst.Add(chckbox);
                                        counter++;
                                    }
                                }
                            }
                            flp = new FlowLayoutPanel
                            {
                                AutoScroll = true,
                                AutoSize = true,
                                MaximumSize = new Size(400, 200), // width * height
                                BackColor = Color.White
                            };//for each directory create a flow layout panel to host its checkboxes

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
                ReenterPaths("CreateCheckBoxes says:" + e.ToString());
            }
        }
        private List<string> AddFilesToList(string path)
        {
            List<string> files_list = new List<string>();
            files_list.AddRange(Directory.GetFiles(path, "*.exe", SearchOption.AllDirectories));//add all exe files
            files_list.AddRange(Directory.GetFiles(path, "*.msi", SearchOption.AllDirectories));//add all msi files
            files_list.AddRange(Directory.GetFiles(path, "*.zip", SearchOption.AllDirectories));//add all zip files
            files_list.AddRange(Directory.GetFiles(path, "*.7z", SearchOption.AllDirectories));//add all 7z files
            files_list.AddRange(Directory.GetFiles(path, "*.rar", SearchOption.AllDirectories));//add all rar files
            return files_list;
        }

        private void SetPanels()
        {
            try
            {
                string installationsPath = txtboxInstallsPath.Text;

                Regex Path_regex = new Regex(@"[A-Za-z]{1}:\\[\s\S\d]*");//accept only paths eg: c:\..\..\.. 
                Regex Network_Path_regex = new Regex(@"(?:\\)[a-zA-Z]*");//to check if accept folder paths eg \\alws34cloud\..\..

                if (Path_regex.Match(installationsPath).Success || Network_Path_regex.Match(installationsPath).Success)
                {
                    txtboxInstallsPath.Enabled = false;//disable the textbox for changes - Reset will re-enable it 
                    btnInstall.Enabled = true;

                    string[] subdirectories = Directory.GetDirectories(installationsPath); //get a list of all sub directories

                    if (!(File.Exists(installationsPath + @"\Deplyer.bat")))//check if deployer.bat does exists, if not - create it
                        File.Create(installationsPath + @"\Deplyer.bat");

                    SetDeployerPath(installationsPath); //set deployer batch path

                    foreach (string dir in subdirectories)//create checkboxes for all valid installation files inside installations folder and its subdirectories
                        CreateCheckBoxes(dir, flp_main);
                }
            }
            catch (Exception e)
            {
                ReenterPaths(e.ToString() + " @SetPanels");
            }
        }

        private string GetSilentSwitches(string program, string[] switches_arr)//get silent Install switches from DB
        {
            char delimiter = '^';
            foreach (string line in switches_arr)
            {
                string program_name = line.Split(delimiter).First();
                string silent_switch = line.Split(delimiter).Last();
                if (program.Contains(program_name))
                    return silent_switch;// returns a path
            }
            return null; // if program not silent switches DB
        }

        private void StartInstall()//start installation
        {
            string installationsPath = txtboxInstallsPath.Text;
            try
            {
                if (File.Exists(DeployerScript))//unless user deletes the file -while running, does not suppose to crash
                {
                    using (StreamWriter w = new StreamWriter(DeployerScript)) //write to batch file
                    {
                        List<string> nonsilents = new List<string>(); ;
                        foreach (string program in programsToInstall)
                        {
                            string DBpath = cwd + @"\SilentSwitchesDB.txt";
                            string[] silentswitches = File.ReadAllLines(DBpath);//get atrray of silen switches
                            string silentswitch = GetSilentSwitches(program, silentswitches);

                            if (silentswitch != null)//if silent switch was found
                                w.WriteLine(program + " " + silentswitch); // write all silents first 
                            else
                                nonsilents.Add(program);//add to non silents array
                        }

                        foreach (string program in nonsilents)//than write all nonsilents (for order of installation)
                        {
                            if (program.Contains(".zip") || program.Contains(".7z") || program.Contains(".rar"))//copy archives to the desktop
                            {
                                string file_on_desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + Path.GetFileName(program);//set new file path to users desktop (the same file name will be created)
                                File.Copy(program, file_on_desktop, true); // creates a new file on destination folder and copy the archive from installs folder to desktop
                            }
                            else//add the program to the batch file - e.g. installation queue
                                w.WriteLine(program);
                        }

                    }//end using

                    FinishInstall(installationsPath);
                    ProcessStartInfo procInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        CreateNoWindow = false,
                        FileName = DeployerScript,
                        WorkingDirectory = installationsPath, //The working DIR.
                        Verb = "runas" //running the process as admin
                    };
                    Process.Start(procInfo);
                }
            }
            catch (Exception e)
            {
                ShowErrorMessage(e.ToString() + "\n@Install");
            }
        }

        /*
         * Finalize batch script
         */
        private void FinishInstall(string installationsPath)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Do you want to run the finalizing bat file?\nThe folowing will happen:" +
                "\n\n-power plans configuration\n\n-python updates and extra config\n\n-some websites will be opened" +
                " (microsoft store use full download)" +
                "\n\n-A restart timer for your choosing (input in ms)",
                "run restartAfterFinish.bat"
                , MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );

            if (dialogResult == DialogResult.Yes)//write to batch file
                using (StreamWriter w = new StreamWriter(DeployerScript, true))
                    foreach (string line in File.ReadAllLines(cwd + @"\restartAfterFinish.txt"))
                        w.WriteLine(line);
        }

        /*
         *set WRD port
         */
        private void SetWRDPort()//check bugs
        {
            try
            {
                bool isPortAvailable = true;
                string port_txt = textBoxWrdPort.Text;
                if (!String.IsNullOrEmpty(port_txt) && (port_txt.Length == 4 || port_txt.Length == 5))
                {
                    int port = Int32.Parse(port_txt);

                    IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                    TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

                    foreach (TcpConnectionInformation tcpConninfo in tcpConnInfoArray)//check if port is taken
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
                            using (StreamWriter w = File.AppendText(DeployerScript))//new StreamWriter
                                w.WriteLine(changeWRDPort);//write command to batch
                        else
                            return;//do nothing
                    }
                }
            }
            catch (Exception e)
            {
                if (String.IsNullOrEmpty(textBoxWrdPort.Text))
                {
                    //ShowErrorMessage("port number cant be empty!");
                    return;
                }
                else if (textBoxWrdPort.Text.Length < 4 || textBoxWrdPort.Text.Length > 5)
                {
                    ShowErrorMessage("please enter port between 1000 and 65535");
                    return;
                }
                else
                    ShowErrorMessage(e.ToString() + "\n-@Install");
            }
        }

        /*
         *re enter paths for invalid input
         */
        private void ReenterPaths(string location)
        {
            txtboxInstallsPath.Text = "";
            btnInstall.Enabled = false;
            ShowErrorMessage("please check your Paths for valid paths\n-" + location);
        }

        /*
        *display critical error message
        */
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /*
        *set the deployer.bat path
        */
        private void SetDeployerPath(string installationsPath)
        {
            DeployerScript = installationsPath + @"\Deplyer.bat";
        }

        /*
         *get random WRD port
         */
        private void GetRandomPort()
        {
            textBoxWrdPort.Text = new Random().Next(34568, 65535).ToString();
        }

        /*
         *Reset form
         */
        private void Reset()
        {
            Hide();
            txtboxInstallsPath.Enabled = true;
            Deployer d = new Deployer();
            d.Closed += (s, args) => Close();
            d.Show();

        }

        /*********/
        /*Events*/
        /*********/
        private void Install(object sender, EventArgs e)//start Install 
        {
            StartInstall();
        }

        /*
         * add program to the installation batch
         */
        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {
            CheckBox chckbox = sender as CheckBox;
            string programPath = chckbox.Tag.ToString();
            if (chckbox.Checked)//if program is selected - add
                programsToInstall.Add(programPath);
            else//remove program from array 
                for (int i = 0; i < programsToInstall.Count; i++)
                    if (programsToInstall[i] == programPath)
                        programsToInstall.RemoveAt(i);
        }

        /*
         *paths validation and initialization
         */
        private void TxtboxInstallsPath_TextChanged(object sender, EventArgs e)
        {
            checkBoxChangeWRDPort.Enabled = true;
            checkBoxDrivers.Enabled = true;
            //checkBoxRegistryHacks.Enabled = true;
            SetPanels();
        }

        /*
         *port textbox to accept only numbers
         **/
        private void TextBoxWrdPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        /*
         * enable "add port" textbox
         **/
        private void CheckBoxChangeWRDPort_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxChangeWRDPort.Checked)
            {
                textBoxWrdPort.Enabled = true;
                buttonRandomWRDport.Enabled = true;
                buttonRandomWRDport.Show();
            }
            else
            {
                textBoxWrdPort.Enabled = false;
                buttonRandomWRDport.Enabled = false;
                textBoxWrdPort.Text = "";
            }
        }

        private void TextBoxWrdPort_TextChanged(object sender, EventArgs e)//port validation
        {
            SetWRDPort();
        }

        private void Time_Tick(object sender, EventArgs e)//date and time
        {
            textBoxDateTime.Text = DateTime.Now.ToString();
        }

        private void TextBox_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox textbox = (sender as TextBox);
            toolTipdynamic.SetToolTip(textbox, textbox.Tag.ToString());
            toolTipdynamic.ToolTipTitle = textbox.Name;
        }//tooltip message

        private void ButtonReset_Click(object sender, EventArgs e)//Reset form
        {
            Reset();
        }

        private void ButtonRandomWRDport_Click(object sender, EventArgs e)//get random WRD port
        {
            GetRandomPort();
        }

        private void BtnLanScan_Click(object sender, EventArgs e)//get lan scanner form
        {
            new FrmLanScanner();
        }
    }
}
