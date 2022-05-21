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

        public int Controls_count { get => controls_count; set => controls_count = value; }

        public DirectoryUC()
        {
            InitializeComponent();
        }

        public void SetLabel(string text)
        {
            this.lblDirName.Text = text;
            tooltip.SetToolTip(this.lblDirName, text); 
        }
        public void AddControlToFLP(Control control)
        {
            this.flpPrograms.Controls.Add(control);
            Controls_count++;
        }
        public void RemoveControlFromFLP(Control control)
        {
            if (this.flpPrograms.Controls.Contains(control))
                this.flpPrograms.Controls.Remove(control);
            Controls_count--;
        }

        public int GetControlsCount()
        {
            //return this.flpPrograms.Controls.Count;
            return this.Controls_count;
        }
    }
}
