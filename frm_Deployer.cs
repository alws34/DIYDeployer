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
        string deployer_appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Deployer";
        string available_paths_txt = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Deployer" + "\\AvailablePaths.txt";
        //private string cwd = Directory.GetCurrentDirectory();
        private List<string> programsToInstall = new List<string>(); //list of intended programs to Install
        List<string> avaialble_Paths = new List<string>();

        public Deployer()
        {
            InitializeComponent();
            GetInitFiles();

            CheckFolder(deployer_appdata);
            GetAvailablePaths(available_paths_txt);

            DisableFeatures();
        }

        /*********/
        /*Methods*/
        /*********/

        private void DisableFeatures()
        {
            btnInstall.Enabled = false;
            textBoxWrdPort.Enabled = false;
            buttonRandomWRDport.Enabled = false;
            checkBoxChangeWRDPort.Enabled = false;
            checkBoxDrivers.Enabled = false;
        }
        private void CheckFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private void GetAvailablePaths(string path)
        {
            CreateFile(path);
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    avaialble_Paths.Add(line);
                    comboBoxAvailablePaths.Items.Add(line);
                }
            }
                
                    
        }

        private void GetInitFiles()
        {
            string get_finzalizer = "curl -OL https://raw.githubusercontent.com/alws34/DIYDeployer/main/restartAfterFinish.txt";
            string get_silenswitches = "curl -OL https://raw.githubusercontent.com/alws34/DIYDeployer/main/SilentSwitchesDB.txt";

            RunCmd(get_finzalizer, true);
            RunCmd(get_silenswitches, true);
        }

        private void RunCmd(string command, bool createNoWindow)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = createNoWindow;
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
                    string bat = ".bat";


                    List<CheckBox> checkboxlst = new List<CheckBox>();//checkboxes
                    List<FlowLayoutPanel> flplst = new List<FlowLayoutPanel>();//flow layout panels list
                    FlowLayoutPanel flp; //main flow layout panel
                    List<string> files = new List<string>(); //list all files within the folder

                    //int counter = 0;
                    //try
                    //{
                    //    string[] dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);

                    //}
                    //catch (Exception)
                    //{
                    //    files = AddFilesToList(path);//add all wanted file types to the list
                    //}

                    //foreach (string dir in dirs)
                    //{
                    //    /*
                    //     * TODO: 
                    //     *Deligate
                    //     */
                    //    if (!(dir.Contains("Drivers")) && !(dir.Contains("drivers")) && !(dir.Contains("OSs")) && !(dir.Contains("operating systems")) && !(dir.Contains("RegistryHacks")) && !(dir.Contains("RPi")) && !(dir.Contains("RPI")))//skip on those directories
                    //{
                    //List<string> files = new List<string>(); //list all files within the folder

                    files = AddFilesToList(path);//add all wanted file types to the list

                    foreach (string fileName in files)
                    {
                        ToolTip toolTip = new ToolTip();
                        string folderPath = Path.GetDirectoryName(fileName);

                        if (fileName.Contains(exe) || fileName.Contains(msi) || fileName.Contains(zip) || fileName.Contains(z) || fileName.Contains(rar) || fileName.Contains(bat))
                        {
                            programName = fileName.Replace(folderPath, "");
                            chckboxText = programName.Replace(exe, "").Replace(msi, "").Replace(zip, "").Replace(z, "").Replace(rar, "");

                            //if (counter > 0 && checkboxlst != null)
                            //{
                            CheckBox chckbox = CheckBoxCreator(programName, path + programName, chckboxText);

                            AddCheckBoxEvent(chckbox);

                            SetTooltip(toolTip, chckbox);

                            if (!checkboxlst.Contains(chckbox))
                                checkboxlst.Add(chckbox);

                            /*duplicants validation*/
                            //bool added = false;

                            //for (int i = 0; i < checkboxlst.Count; i++)
                            //    if (checkboxlst[i] != null && checkboxlst[i].Name == chckbox.Name)
                            //        added = true;

                            //if (!added)
                            //{
                            //    checkboxlst.Add(chckbox);
                            //    counter++;
                            //}
                            /*duplicants validation*/
                            //}

                            //else //if (counter == 0)
                            //{
                            //    CheckBox chckbox = CheckBoxCreator(programName, path + programName, chckboxText);

                            //    AddCheckBoxEvent(chckbox);
                            //    toolTip.SetToolTip(chckbox, chckbox.Tag.ToString());
                            //    SetTooltip(toolTip, chckbox);

                            //    checkboxlst.Add(chckbox);
                            //    counter++;
                            //}
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
                    //}
                    //}
                }
            }
            catch (Exception e)
            {
                ReenterPaths("CreateCheckBoxes says:" + e.ToString());
            }
        }

        private CheckBox CheckBoxCreator(string name, string tag, string text)
        {
            CheckBox c = new CheckBox
            {
                Name = name,
                Tag = tag,
                Text = text.Replace(@"\", "")
            };
            return c;
        }

        private void AddCheckBoxEvent(CheckBox c)
        {
            c.CheckedChanged += new EventHandler(CheckBox_CheckChanged);
        }

        private void SetTooltip(ToolTip t, CheckBox c)
        {
            t.SetToolTip(c, c.Tag.ToString());
        }

        private List<string> AddFilesToList(string path)
        {
            List<string> files_list = new List<string>();
            files_list.AddRange(Directory.GetFiles(path, "*.exe", SearchOption.AllDirectories));//add all exe files
            files_list.AddRange(Directory.GetFiles(path, "*.msi", SearchOption.AllDirectories));//add all msi files
            files_list.AddRange(Directory.GetFiles(path, "*.zip", SearchOption.AllDirectories));//add all zip files
            files_list.AddRange(Directory.GetFiles(path, "*.7z", SearchOption.AllDirectories));//add all 7z files
            files_list.AddRange(Directory.GetFiles(path, "*.rar", SearchOption.AllDirectories));//add all rar files
            files_list.AddRange(Directory.GetFiles(path, "*.bat", SearchOption.AllDirectories));//add all bat files
            return files_list;
        }

        private void FileWriter(string filepath, List<string> list)
        {
            using (StreamWriter sw = new StreamWriter(filepath))
                foreach (string item in list)
                    sw.WriteLine(item);
        }

        private void FileWriter(string filepath, string str)
        {
            using (StreamWriter sw = new StreamWriter(filepath))
                sw.WriteLine(str);
        }

        private void AppendToFile(string filepath, string[] list)
        {
            using (StreamWriter sw = new StreamWriter(filepath, true))
                foreach (string item in list)
                    sw.WriteLine(item);
        }

        private void SetPanels()
        {
            try
            {
                string installationsPath = txtboxInstallsPath.Text;

               
                string[] subdirectories = { };

                Regex Path_regex = new Regex(@"[A-Za-z]{1}:\\\D*");//[A-Za-z]{1}:\\[\s\S\d]*");//accept only paths eg: c:\..\..\.. 
                Regex Network_Path_regex = new Regex(@"\\[\S]*");//to check if accept folder paths eg \\networkfolder\..\..

                if (Path_regex.Match(installationsPath).Success || Network_Path_regex.Match(installationsPath).Success)
                {
                    txtboxInstallsPath.Enabled = false;//disable the textbox for changes - Reset will re-enable it 
                    btnInstall.Enabled = true;
                    try
                    {
                        subdirectories = Directory.GetDirectories(installationsPath, "*", SearchOption.AllDirectories); //get a list of all sub directories

                    }
                    catch (Exception)
                    {
                        ReenterPaths("@setpanels");
                        return;
                    }
                    //string deployerpath = installationsPath + @"\Deplyer.bat";

                    CreateFile(installationsPath + @"\Deplyer.bat");

                    SetDeployerPath(); //set deployer batch path

                    foreach (string dir in subdirectories)//create checkboxes for all valid installation files inside installations folder and its subdirectories
                        if (dir.Contains("Installs") || dir.Contains("scripts"))
                            CreateCheckBoxes(dir, flp_main);
                }
            }
            catch (Exception e)
            {
                ReenterPaths(e.ToString() + " @SetPanels");
            }
        }

        private void CreateFile(string path)
        {
            if (!File.Exists(path))
                File.Create(path).Close();
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
            if (!avaialble_Paths.Contains(installationsPath))
                avaialble_Paths.Add(installationsPath);

            FileWriter(available_paths_txt, avaialble_Paths);

            try
            {
                if (File.Exists(DeployerScript))//unless user deletes the file -while running, does not suppose to crash
                {
                    using (StreamWriter w = new StreamWriter(DeployerScript)) //write to batch file
                    {
                        List<string> nonsilents = new List<string>(); ;
                        foreach (string program in programsToInstall)
                        {
                            string DBpath = "SilentSwitchesDB.txt";
                            string[] silentswitches = File.ReadAllLines(DBpath);//get atrray of silen switches
                            string silentswitch = GetSilentSwitches(program, silentswitches);

                            if (silentswitch != null)//if silent switch was found
                                w.WriteLine(program + " " + silentswitch); // write all silents first 
                            else
                                nonsilents.Add(program);//add to non silents array
                        }

                        foreach (string program in nonsilents)//than write all nonsilents (for order of installation)
                        {
                            if (program.Contains(".zip") || program.Contains(".7z") || program.Contains(".rar") || program.Contains(".bat"))//copy archives to the desktop
                            {
                                string file_on_desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + Path.GetFileName(program);//set new file path to users desktop (the same file name will be created)
                                File.Copy(program, file_on_desktop, true); // creates a new file on destination folder and copy the archive from installs folder to desktop
                            }
                            else//add the program to the batch file - e.g. installation queue
                                w.WriteLine(program);
                        }

                    }//end using

                    FinishInstall();

                    StartInstaller(DeployerScript, installationsPath);    
                }
            }
            catch (Exception e)
            {
                ShowErrorMessage(e.ToString() + "\n@Install");
            }
        }

        private void StartInstaller(string DeployerScript, string installationsPath)
        {
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

        /*
         * Finalize batch script
         */
        private void FinishInstall()
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
                AppendToFile(DeployerScript, File.ReadAllLines("restartAfterFinish.txt"));
                //using (StreamWriter w = new StreamWriter(DeployerScript, true))
                //    foreach (string line in File.ReadAllLines("restartAfterFinish.txt"))
                //        w.WriteLine(line);
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
                            FileWriter(DeployerScript, changeWRDPort);
                        //using (StreamWriter w = File.AppendText(DeployerScript))//new StreamWriter
                        //    w.WriteLine(changeWRDPort);//write command to batch
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
                else if (Int32.Parse(textBoxWrdPort.Text) < 1000 || Int32.Parse(textBoxWrdPort.Text) > 65535)
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
            txtboxInstallsPath.Enabled = true;
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
        private void SetDeployerPath()
        {
            DeployerScript = deployer_appdata + "\\Deplyer.bat";
            CreateFile(DeployerScript);
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
                programsToInstall.Remove(programPath);
            //for (int i = 0; i < programsToInstall.Count; i++)
            //    if (programsToInstall[i] == programPath)
            //        programsToInstall.RemoveAt(i);
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

        private void comboBoxAvailablePaths_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtboxInstallsPath.Text = comboBoxAvailablePaths.SelectedItem.ToString();
        }

        private void btnInstallCosmos_Click(object sender, EventArgs e)
        {
            string cosmos_auto_install = @"\\ad.ast-science.com\share\EngTLV\RnD\users\Alon\Cosmos_Israel\CosmosClown.exe";
            RunCmd(cosmos_auto_install, true);
        }

        private void btnLabComputer_Click(object sender, EventArgs e)
        {
            string cosmos_auto_install = @"\\ad.ast-science.com\share\EngTLV\RnD\users\Alon\scripts\auto_New_Lab_PC.bat";
            RunCmd(cosmos_auto_install, true);
        }
    }
}
