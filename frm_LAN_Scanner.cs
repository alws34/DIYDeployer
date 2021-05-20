using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;

namespace Deployer
{
    public partial class frmLanScanner : Form
    {
        [Obsolete]
        public frmLanScanner()
        {
            InitializeComponent();
            listViewLanScan.View = View.Details;
            listViewLanScan.FullRowSelect = true;
            listViewLanScan.GridLines = true;
            listViewLanScan.Columns.Add("IP", listViewLanScan.Size.Width / 3);
            listViewLanScan.Columns.Add("Hostname", listViewLanScan.Size.Width / 3);
            listViewLanScan.Columns.Add("Description", listViewLanScan.Size.Width / 3);
            textBoxIpRange.Text = ReturnIP();
            Show();
        }

        [Obsolete]
        private string ReturnIP()
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            myIP = myIP.Substring(0, myIP.LastIndexOf("."));
            return myIP;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            string subnet = textBoxIpRange.Text;
            progressBar.Maximum = 254;
            progressBar.Value = 0;
            listViewLanScan.Items.Clear();
            string[] availables = new string[] { };
            Task.Factory.StartNew(new Action(() =>
            {
                for (int i = 1; i < 255; i++)
                {
                    //Thread.Sleep(1000);//sleep for 1 second between each iteration
                    string ip = $"{subnet}.{i}";
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(ip, 100);
                    if (reply.Status == IPStatus.Success)
                    {
                        progressBar.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                IPHostEntry host = Dns.GetHostEntry(IPAddress.Parse(ip));
                                listViewLanScan.Items.Add(new ListViewItem(new String[] { ip, host.HostName, "Up" }));
                            }
                            catch
                            {
                                listViewLanScan.Items.Add(new ListViewItem(new String[] { ip, "", "Up - Couldn't retrieve hostname" }));
                                //listBoxAvailableIps.Items.Add(ip);
                                // MessageBox.Show($"Couldn't retrieve hostname from {ip}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            progressBar.Value += 1;
                            lblStatus.ForeColor = Color.Blue;
                            lblStatus.Text = $"Scanning: {ip}";
                            if (progressBar.Value == 253)
                                lblStatus.Text = "Finished";
                        }));
                    }
                    else
                    {
                        progressBar.BeginInvoke(new Action(() =>
                        {
                            progressBar.Value += 1;
                            lblStatus.ForeColor = Color.DarkGray;
                            lblStatus.Text = $"Scanning: {ip}";
                            //listViewLanScan.Items.Add(new ListViewItem(new String[] { ip, "", "Down" }));
                            listBoxAvailableIps.Items.Add(ip);
                            if (progressBar.Value == 253)
                                lblStatus.Text = "Finished";
                        }));
                    }
                }
            }));
        }
        private void textBox_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox textbox = (sender as TextBox);
            toolTipIPRange.SetToolTip(textbox, textbox.Tag.ToString());
            toolTipIPRange.ToolTipTitle = textbox.Name;
        }//tooltip message
    }
}
