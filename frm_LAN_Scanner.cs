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
        bool cancel = false;
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
            progressBar.Maximum = 254;
            Show();
        }

        private string ReturnIP()
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).ToString();
            myIP = myIP.Substring(0, myIP.LastIndexOf("."));
            return myIP;
        }
        private void reset()
        {
            textBoxIpRange.Text = "";
            listViewLanScan.Items.Clear();
            listBoxAvailableIps.Items.Clear();
            progressBar.Value = 0;
            labelStatusText.Text = "";
        }
        private void btnScan_Click(object sender, EventArgs e)
        {
            cancel = false;
            int maximum_ip = 255;
            string network_name = textBoxIpRange.Text; //eg 10.0.0 / 192.168.0

            progressBar.Value = 0;
            textBoxIpRange.Text = "";
            listViewLanScan.Items.Clear();
            Task.Factory.StartNew(new Action(() =>
            {
                for (int computer_name = 1; computer_name < maximum_ip; computer_name++)
                {
                    string ip = $"{network_name}.{computer_name}";
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(ip, 100);
                    if (reply.Status == IPStatus.Success)
                    {
                        if(cancel == true)
                        {
                            reset();
                            return;
                        }
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
                            lblStatus.ForeColor = Color.Purple;
                            lblStatus.Text = $"Scanning: {ip}";
                            if (progressBar.Value == 254)
                                lblStatus.Text = "Finished";
                        }));
                    }
                    else
                    {
                        progressBar.BeginInvoke(new Action(() =>
                        {
                            progressBar.Value += 1;
                            lblStatus.ForeColor = Color.Purple;
                            lblStatus.Text = $"Scanning: {ip}";
                            listBoxAvailableIps.Items.Add(ip);
                            if (progressBar.Value == 254)
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

        private void button1_Click(object sender, EventArgs e)
        {
            cancel = true;
        }
    }
}
