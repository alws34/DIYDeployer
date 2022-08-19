using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using System.IO;

namespace Deployer
{
    public partial class FrmLanScanner : Form
    {
        private bool cancel = false;
        private const int MAX_IP = 252;
        private const int MIN_IP = 1;
        public FrmLanScanner()
        {
            InitializeComponent();
            InitLanScan();
            textBoxIpRange.Text = ReturnIP();
            progressBar.Maximum = 254;
            CenterToScreen();
            Show();
        }

        private string ReturnIP()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
        }

        private void InitLanScan()
        {
            int column_size = listViewLanScan.Size.Width / 3;
            listViewLanScan.View = View.Details;
            listViewLanScan.FullRowSelect = true;
            listViewLanScan.GridLines = true;
            listViewLanScan.Columns.Add("IP", column_size);
            listViewLanScan.Columns.Add("Hostname", column_size);
            listViewLanScan.Columns.Add("Description", column_size);
        }
        private void Reset()
        {
            textBoxIpRange.Text = "";
            listViewLanScan.Items.Clear();
            listBoxAvailableIps.Items.Clear();
            progressBar.Value = 0;
            labelStatusText.Text = "";
        }
        private void BtnScan_Click(object sender, EventArgs e)
        {
            cancel = false;
            string subnet = textBoxIpRange.Text; //eg 10.0.0 / 192.168.0

            progressBar.Value = 0;
            textBoxIpRange.Text = "";
            listViewLanScan.Items.Clear();
            Task.Factory.StartNew(new Action(() =>
            {
                for (int computer_ip = 1; computer_ip < MAX_IP; computer_ip++)
                {
                    if (!cancel)
                    {
                        string ip = $"{subnet}.{computer_ip}";
                        Ping ping = new Ping();
                        PingReply reply = ping.Send(ip, 100);
                        if (reply.Status == IPStatus.Success)
                        {
                            progressBar.BeginInvoke(new Action(() =>
                            {
                                try
                                {
                                    IPHostEntry host = Dns.GetHostEntry(IPAddress.Parse(ip));
                                    listViewLanScan.Items.Add(new ListViewItem(new String[] { ip, host.HostName, "UP" }));
                                }
                                catch
                                {
                                    listViewLanScan.Items.Add(new ListViewItem(new String[] { ip, "", "Up - Couldn't retrieve hostname" }));
                                }
                                progressBar.Value += 1;

                                lblStatus.BeginInvoke(new Action(() =>
                                {
                                    lblStatus.ForeColor = Color.Purple;
                                    lblStatus.Text = $"Scanning: {ip}";
                                }));

                                if (progressBar.Value == 254)
                                    lblStatus.Text = "Finished";
                            }));
                        }
                        else
                        {
                            try
                            {
                                progressBar.BeginInvoke(new Action(() =>
                                {
                                    progressBar.Value += 1;
                                    lblStatus.BeginInvoke(new Action(() =>
                                    {
                                        lblStatus.ForeColor = Color.Purple;
                                        lblStatus.Text = $"Scanning: {ip}";
                                    }));
                                    listBoxAvailableIps.Items.Add(ip);

                                    if (progressBar.Value == 254)
                                        lblStatus.Text = "Finished";
                                }));
                            }
                            catch (Exception)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        Reset();
                        return;
                    }

                }
            }));
        }
        private string GetDateTime(string format = "dd/MM/yyyy")
        {
            return DateTime.Now.ToString(format);
        }
        private void WriteToLog(string msg)
        {
            using (StreamWriter sw = new StreamWriter($"Log_{GetDateTime()}", true))
                sw.WriteLine(msg);
        }

        private void TextBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender.GetType().Name == "TextBox")
            {
                TextBox textbox = sender as TextBox;
                toolTipIPRange.SetToolTip(textbox, textbox.Tag.ToString());
                toolTipIPRange.ToolTipTitle = textbox.Name;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancel = true;
        }
    }
}
