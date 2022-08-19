using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deployer
{
    public partial class Deployer : Form
    {
        private List<string> programs_To_Install = new List<string>();
        private List<ProgramDetails> programs_detailes_lst = new List<ProgramDetails>();
        private string cwd = Directory.GetCurrentDirectory();
        private string[] postfixes = { ".exe", ".msi", ".zip", ".7z", ".rar" };
        private int total_loaded_directories = 0;
        private int total_loaded_programs = 0;

        public Deployer()
        {
            InitializeComponent();
            GetFilesFromGitHub();
            SetFormComponentsMode(false);
            CenterToScreen();
        }

        private void SetFormComponentsMode(bool mode)
        {
            btnInstall.Enabled = mode;
            textBoxWrdPort.Enabled = mode;
            buttonRandomWRDport.Enabled = mode;
            checkBoxChangeWRDPort.Enabled = mode;
            checkBoxDrivers.Enabled = mode;
        }

        private void GetFilesFromGitHub()
        {
            if (!(CheckForInternetConnection()))
            {
                SendMessageToConsole($"No internet connection.\nAborting.");
                ExitApp(5000);
            }
            if (!File.Exists($"{cwd}\\restartAfterFinish.txt") && !File.Exists($"{cwd}\\SilentSwitchesDB.txt"))
            {
                RunCmd($"curl -OL https://raw.githubusercontent.com/alws34/DIYDeployer/main/restartAfterFinish.txt {cwd}");
                SendMessageToConsole(("Downloaded File restartAfterFinish.txt"));
                RunCmd($"curl -OL https://raw.githubusercontent.com/alws34/DIYDeployer/main/SilentSwitchesDB.txt {cwd}");
                SendMessageToConsole(("Downloaded File SilentSwitchesDB.txt"));
            }
        }

        private void ExitApp(int ms)
        {
            Thread.Sleep(ms);
            Application.Exit();
        }

        private bool CheckForInternetConnection()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        private void RunCmd(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", $"/c {command}")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            SendMessageToConsole(($"Starting command {command}"));
            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                SendMessageToConsole(("output>>" + e.Data));
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                SendMessageToConsole(("error>>" + e.Data));
            process.BeginErrorReadLine();

            process.WaitForExit();

            SendMessageToConsole(($"ExitCode: {process.ExitCode}"));
            process.Close();
        }

        delegate void SetDriversCheckBoxCallBack(string path);
        private void SetDriversCheckBox(string path)
        {
            if (checkBoxDrivers.InvokeRequired)
            {
                SetDriversCheckBoxCallBack callback = new SetDriversCheckBoxCallBack(SetDriversCheckBox);
                checkBoxDrivers.Invoke(callback, path);
            }
            else
            {
                Invoker(checkBoxDrivers, new Action(() =>
                {
                    checkBoxDrivers.Tag = path;
                    checkBoxDrivers.Visible = true;
                    checkBoxDrivers.Checked = false;
                }));
            }
        }

        public static void Invoker(Control control, Action action)
        {
            control.Invoke(action);
        }

        private void CreateCheckBoxes(string path)
        {
            try
            {
                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    Control[] checkboxlst = { };
                    DirectoryUC duc = new DirectoryUC(path);
                    List<string> duplicate_avoidance_lst = new List<string>();
                    Application.UseWaitCursor = true;
                    string drivers_dir = "DRIVERS";

                    string[] dirs = GetDirsFromPath(path);
                    foreach (string subdir in dirs)
                    {
                        string parent_dir_name = Directory.GetParent(subdir).Name.ToUpper();

                        if (parent_dir_name == drivers_dir)
                            SetDriversCheckBox(path);

                        SendMessageToConsole(($"Loading Controls for directory: {subdir}"));
                        List<string> programs_in_dir = GetProgramsFromSubDir(path);

                        if (programs_in_dir == null || programs_in_dir.Count == 0)
                        {
                            SendMessageToConsole(($"No Programs inside Dir!"));
                            continue;
                        }

                        foreach (string program_full_path in programs_in_dir)
                        {
                            foreach (string postfix in postfixes)
                            {
                                if (program_full_path.EndsWith(postfix))
                                {
                                    string program_Name = program_full_path.Replace(Path.GetDirectoryName(program_full_path), "").Replace(postfix, "").Replace(@"\", "");

                                    if (!duplicate_avoidance_lst.Contains(program_Name))
                                    {
                                        ProgramDetails current_program = new ProgramDetails(program_Name, program_full_path);
                                        duplicate_avoidance_lst.Add(program_Name);
                                        programs_detailes_lst.Add(current_program);
                                        duc.AddControlToFLP(CreateCheckBox(current_program));
                                        total_loaded_programs++;
                                    }
                                }
                            }
                        }
                        SendMessageToConsole(($"Finished Loading {total_loaded_programs} Controls for directory: {subdir}"));
                        total_loaded_programs = 0;
                        if (duc.GetControlsCount() >= 1)
                        {
                            Invoker(flp_main, new Action(() => flp_main.Controls.Add(duc)));
                            Invoker(duc, new Action(() => duc.SetLabel(new DirectoryInfo(subdir).Name)));
                            total_loaded_directories++;
                        }
                        SendMessageToConsole(($"Total controls Loaded for {duc.DirName}: {duc.GetControlsCount()}"));
                    }
                    SendMessageToConsole(($"Path: {path}:\n\tLoaded Total of {total_loaded_directories} Directorie Panels,\n\tand Total of {total_loaded_programs} Programs loaded!"));
                    total_loaded_directories = 0;
                    Application.UseWaitCursor = false;
                }
            }
            catch (Exception e)
            {
                ReenterPaths("CreateCheckBoxes says:" + e.ToString());
            }
        }

        private CheckBox CreateCheckBox(ProgramDetails program)
        {
            CheckBox chckbox = new CheckBox
            {
                Name = program.Name,
                Text = program.Name,
                Tag = program.Path
            };
            chckbox.CheckedChanged += new EventHandler(CheckBox_CheckChanged);
            new ToolTip().SetToolTip(chckbox, chckbox.Tag.ToString());

            return chckbox;
        }

        private List<string> GetProgramsFromSubDir(string path)
        {
            List<string> files_list = new List<string>();
            try
            {
                SendMessageToConsole(($"Getting Programs from Dir {path}"));
                foreach (string postfix in postfixes)
                    files_list.AddRange(GetFilesFromDir(path, $"*{postfix}", SearchOption.AllDirectories));
            }
            catch (Exception e)
            {
                SendMessageToConsole(($"Error @path:\t{path}.\nSays:\t{e}"));
                return null;
            }

            return files_list;
        }

        private async void SetPanelsAsync()
        {
            try
            {
                string installationsPath = txtboxInstallsPath.Text;
                Regex Path_regex = new Regex(@"[A-Za-z]{1}:\\[\s\S\d]*");
                Regex Network_Path_regex = new Regex(@"(?:\\)[a-zA-Z]*");

                if (Directory.Exists(installationsPath) && (Path_regex.Match(installationsPath).Success || Network_Path_regex.Match(installationsPath).Success))
                {
                    Invoker(txtboxInstallsPath, new Action(() => txtboxInstallsPath.Enabled = false));
                    Invoker(btnInstall, new Action(() => btnInstall.Enabled = true));
                    string[] dirs = GetDirsFromPath(installationsPath);

                    foreach (string dir in dirs)
                        await Task.Run(() => CreateCheckBoxes(dir));
                }
                else
                {
                    SendMessageToConsole(($"Invalid Directory: {installationsPath}"));
                    Invoker(txtboxInstallsPath, new Action(() => txtboxInstallsPath.Clear()));
                }

            }
            catch (Exception e)
            {
                ReenterPaths(e.ToString() + " @SetPanels");
            }
        }

        private string[] GetDirsFromPath(string path)
        {
            try
            {
                return Directory.GetDirectories(path);
            }
            catch (Exception e)
            {
                SendMessageToConsole($"{e}");
                return null;
            }

        }

        private string[] GetFilesFromDir(string path, string search_pattern, SearchOption search_option)
        {
            try
            {
                return Directory.GetFiles(path, search_pattern, search_option);
            }
            catch (Exception e)
            {
                SendMessageToConsole($"{e}");
                return null;
            }

        }

        private string GetSilentSwitches(string program, string[] switches_arr)
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

        private void StartInstall()
        {
            string installationsPath = txtboxInstallsPath.Text;

            foreach (string program in programs_To_Install)
                RunInstallation(program);

            FinishInstall(installationsPath);
        }

        private void RunInstallation(string program_path)
        {
            Process p = Process.Start(program_path);
            p.WaitForExit();
        }

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

            if (dialogResult == DialogResult.Yes)
                foreach (string command in File.ReadAllLines(cwd + @"\restartAfterFinish.txt"))
                    RunCmd(command);
            else
                return;
        }

        delegate void ReenterPathsCallback(string txt);

        private void ReenterPaths(string txt)
        {
            if (txtboxInstallsPath.InvokeRequired || btnInstall.InvokeRequired)
            {
                ReenterPathsCallback callback = new ReenterPathsCallback(ReenterPaths);
                Invoke(callback, txt);
            }
            else
            {
                Invoker(txtboxInstallsPath, new Action(() => txtboxInstallsPath.Text = ""));
                Invoker(btnInstall, new Action(() => btnInstall.Enabled = false));
                SendMessageToConsole(($"please check your Paths for valid paths\n - {txt}"));
            }
        }

        private void ShowMessageBox(string message, string caption = "Error", MessageBoxButtons btns = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(message, caption, btns, icon);
        }

        private void Reset()
        {
            Invoker(flp_main, new Action(() => flp_main.Controls.Clear()));
            Invoker(txtboxInstallsPath, new Action(() => txtboxInstallsPath.Clear()));
            Invoker(txtboxInstallsPath, new Action(() => txtboxInstallsPath.Enabled = true));
            //Deployer d = new Deployer();
            //d.Closed += (s, args) => Close();
            //Hide();
            //d.Show();
        }

        private void Install(object sender, EventArgs e)
        {
            StartInstall();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {
            if (sender.GetType().Name == "CheckBox")
            {
                CheckBox chckbox = sender as CheckBox;
                string programPath = chckbox.Tag.ToString();

                if (chckbox.Checked)
                    programs_To_Install.Add(programPath);
                else
                    programs_To_Install.Remove(programPath);
            }
        }

        private void TxtboxInstallsPath_TextChanged(object sender, EventArgs e)
        {
            checkBoxChangeWRDPort.Enabled = true;
            checkBoxDrivers.Enabled = true;
            SetPanelsAsync();
        }

        private void TextBoxWrdPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

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

        private void TextBoxWrdPort_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string port_txt = textBoxWrdPort.Text;
                if (!String.IsNullOrEmpty(port_txt) && (port_txt.Length == 4 || port_txt.Length == 5))
                {
                    int port = Int32.Parse(port_txt);

                    foreach (TcpConnectionInformation tcpConninfo in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections())
                    {
                        if (tcpConninfo.LocalEndPoint.Port == port)
                        {
                            ShowMessageBox("Port is already taken");
                            return;
                        }
                    }
                    DialogResult WRDportDialog = MessageBox.Show($"Are you sure you want to change the following port?\n WRD RDP Port: {port}", "Port sanity check", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (WRDportDialog == DialogResult.Yes)
                    {
                        RunCmd($"reg add {'\u0022'}HKLM\\System\\CurrentControlSet\\Control\\Terminal Server\\WinStations\\RDP - Tcp{'\u0022'} /v PortNumber /t REG_DWORD /d {port}");
                        ShowMessageBox($"RDP port changed successfully!\nNew Port {port}\n\t*Please Validate in settings", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        return;

                }
            }
            catch (Exception ex)
            {
                if (String.IsNullOrWhiteSpace(textBoxWrdPort.Text))
                    return;
                else if (textBoxWrdPort.Text.Length < 4 || textBoxWrdPort.Text.Length > 5)
                {
                    SendMessageToConsole(("please enter port number between 34568 and 65535"));
                    return;
                }
                else
                    SendMessageToConsole((ex.ToString() + "\n-@Install"));
            }
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            textBoxDateTime.Text = DateTime.Now.ToString();
        }

        private void MouseMoveEventHandler(object sender, MouseEventArgs e)
        {
            string sender_type = sender.GetType().Name;

            switch (sender_type)
            {
                case "TextBox":
                    System.Windows.Forms.TextBox tb = (sender as System.Windows.Forms.TextBox);
                    toolTipdynamic.SetToolTip(tb, tb.Tag.ToString());
                    toolTipdynamic.ToolTipTitle = tb.Name;
                    break;
                case "RadioButton":
                    RadioButton rb = (sender as RadioButton);
                    toolTipdynamic.SetToolTip(rb, rb.Tag.ToString());
                    toolTipdynamic.ToolTipTitle = rb.Name;
                    break;
            }
            
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ButtonRandomWRDport_Click(object sender, EventArgs e)
        {
            textBoxWrdPort.Text = new Random().Next(34568, 65535).ToString();
        }

        private void BtnLanScan_Click(object sender, EventArgs e)
        {
            new FrmLanScanner();
        }

        private void checkBoxDrivers_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c != null && !string.IsNullOrWhiteSpace(c.Tag.ToString()))
                Process.Start(c.Tag.ToString());
        }

        private void btnBrowsePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog fbd = new OpenFileDialog()
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection."
            };

            if (fbd.ShowDialog() == DialogResult.OK)
                txtboxInstallsPath.Text = Path.GetDirectoryName(fbd.FileName);
        }

        public void SendMessageToConsole(string msg)
        {
            Invoker(textBoxConsole, new Action(() => textBoxConsole.AppendText($"{new SendMessageToConsoleEventArgs(msg).Message}{Environment.NewLine}{Environment.NewLine}")));
        }
    }
}
