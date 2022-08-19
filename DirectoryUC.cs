using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deployer
{
    public partial class DirectoryUC : UserControl
    {
        int controls_count = 0; //this is faster than accessing the flpPrograms.Controls[]
        ToolTip tooltip = new ToolTip();
        string dirname = "";

        public int Controls_count { get => controls_count; set => controls_count = value; }
        public string DirName { get => dirname; set => dirname = value; }

        public DirectoryUC()
        {
            InitDir();
        }

        public DirectoryUC(string dir_name)
        {
            InitDir();
            DirName = dir_name;
        }

        private void InitDir()
        {
            InitializeComponent();
            Width = 300;
        }
        
        public void SetLabel(string text)
        {
            lblDirName.Text = text;
            tooltip.SetToolTip(lblDirName, text);
        }

        public void AddControlToFLP(Control control)
        {
            flpPrograms.Controls.Add(control);
            Controls_count++;
        }

        public void RemoveControlFromFLP(Control control)
        {
            if (flpPrograms.Controls.Contains(control))
                flpPrograms.Controls.Remove(control);
            Controls_count--;
        }

        public int GetControlsCount()
        {
            return Controls_count;
        }
    }
}
