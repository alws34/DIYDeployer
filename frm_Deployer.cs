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

        private string cwd = Directory.GetCurrentDirectory();
        private List<string> programsToInstall = new List<string>(); //list of intended programs to Install
        string[] postfixes = { ".exe", ".msi", ".zip", ".7z", ".rar" };
        string[] dirs_to_ignore = { "Drivers", "drivers", "OSs", "operating systems", "RegistryHacks", "RPi", "RPI" };

        public Deployer()
        {
            InitializeComponent();
            // GetFilesFromGitHub();
            SetFormComponentsMode(false);
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
            RunCmd($"curl -OL https://raw.githubusercontent.com/alws34/DIYDeployer/main/restartAfterFinish.txt {cwd}");
            RunCmd($"curl -OL https://raw.githubusercontent.com/alws34/DIYDeployer/main/SilentSwitchesDB.txt {cwd}");
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
                    int counter = 0;
                    string programName = "";
                    string chckboxText = "";
                    string[] subdirs = GetDirectories(path);
                    Control[] checkboxlst = { };
                    List<string> programnames = new List<string>();
                    List<FlowLayoutPanel> flplst = new List<FlowLayoutPanel>();

                    DirectoryUC duc = new DirectoryUC();

                    foreach (string dir in subdirs)
                    {
                        if (!dirs_to_ignore.Contains(dir))
                        {
                            List<string> dir_files = GetDirFiles(path);
                            duc.SetLabel(Path.GetDirectoryName(dir));

                            foreach (string fileName in dir_files)
                            {
                                foreach (string postfix in postfixes)
                                {
                                    if (fileName.Contains(postfix))
                                    {
                                        programName = fileName.Replace(Path.GetDirectoryName(fileName), "");
                                        chckboxText = programName.Replace(postfix, "").Replace(@"\", "");
                                        CheckBox chckbox = CreateCheckBox(programName, chckboxText, $"{fileName}");

                                        if (counter > 0 && checkboxlst != null)
                                        {
                                            if (!programnames.Contains(chckbox.Name))
                                            {
                                                programnames.Add(chckbox.Name);
                                                duc.AddControlToFLP(chckbox);
                                            }
                                        }
                                        else
                                            duc.AddControlToFLP(chckbox);

                                        counter++;
                                    }
                                }
                            }

                            if (duc.GetControlsCount() >= 1)
                                flpl.Controls.Add(duc);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReenterPaths("CreateCheckBoxes says:" + e.ToString());
            }
        }

        private CheckBox CreateCheckBox(string name, string text, object tag)
        {
            CheckBox chckbox = new CheckBox
            {
                Name = name,
                Tag = tag,
                Text = text
            };
            chckbox.CheckedChanged += new EventHandler(CheckBox_CheckChanged);

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(chckbox, chckbox.Tag.ToString());

            return chckbox;
        }

        private List<string> GetDirFiles(string path)
        {
            List<string> files_list = new List<string>();
            try
            {
                foreach (string postfix in postfixes)
                    files_list.AddRange(GetFilesFromDir(path, $"*{postfix}", SearchOption.AllDirectories));
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
            }

            return files_list;
        }

        private void SetPanels()
        {
            try
            {
                string installationsPath = txtboxInstallsPath.Text;

                Regex Path_regex = new Regex(@"[A-Za-z]{1}:\\[\s\S\d]*");
                Regex Network_Path_regex = new Regex(@"(?:\\)[a-zA-Z]*");

                if (Path_regex.Match(installationsPath).Success || Network_Path_regex.Match(installationsPath).Success)
                {
                    txtboxInstallsPath.Enabled = false;//disable the textbox for changes - Reset will re-enable it 
                    btnInstall.Enabled = true;

                    string[] dirs = GetDirectories(installationsPath);
                    foreach (string dir in dirs)
                        CreateCheckBoxes(dir, flp_main);
                }
            }
            catch (Exception e)
            {
                ReenterPaths(e.ToString() + " @SetPanels");
            }
        }

        private string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        private string[] GetFilesFromDir(string path, string search_pattern, SearchOption search_option)
        {
            return Directory.GetFiles(path, search_pattern, search_option);
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

            foreach (string program in programsToInstall)
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

        private void ReenterPaths(string location)
        {
            txtboxInstallsPath.Text = "";
            btnInstall.Enabled = false;
            ShowMessageBox("please check your Paths for valid paths\n-" + location);
        }

        private void ShowMessageBox(string message, string caption = "Error", MessageBoxButtons btns = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(message, caption, btns, icon);
        }

        private void Reset()
        {
            txtboxInstallsPath.Enabled = true;
            Deployer d = new Deployer();
            d.Closed += (s, args) => Close();
            Hide();
            d.Show();
        }

        private void Install(object sender, EventArgs e)//start Install 
        {
            StartInstall();
        }

        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {
            CheckBox chckbox = sender as CheckBox;
            string programPath = chckbox.Tag.ToString();

            if (chckbox.Checked)
                programsToInstall.Add(programPath);
            else
                programsToInstall.Remove(programPath);
        }

        private void TxtboxInstallsPath_TextChanged(object sender, EventArgs e)
        {
            checkBoxChangeWRDPort.Enabled = true;
            checkBoxDrivers.Enabled = true;
            SetPanels();
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
                if (String.IsNullOrEmpty(textBoxWrdPort.Text))
                    return;
                else if (textBoxWrdPort.Text.Length < 4 || textBoxWrdPort.Text.Length > 5)
                {
                    ShowMessageBox("please enter port number between 34568 and 65535");
                    return;
                }
                else
                    ShowMessageBox(ex.ToString() + "\n-@Install");
            }
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            textBoxDateTime.Text = DateTime.Now.ToString();
        }

        private void TextBox_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox textbox = (sender as TextBox);
            toolTipdynamic.SetToolTip(textbox, textbox.Tag.ToString());
            toolTipdynamic.ToolTipTitle = textbox.Name;
        }

        private void ButtonReset_Click(object sender, EventArgs e)//Reset form
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
    }
}
