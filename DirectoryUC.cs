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
        int controls_count = 0; //this seems to be faster than accessing the flpPrograms.Controls
        ToolTip tt = new ToolTip();
        public DirectoryUC()
        {
            InitializeComponent();
        }

        public void SetLabel(string text)
        {
            this.lblDirName.Text = text;
            tt.SetToolTip(this.lblDirName, text); 
        }
        public void AddControlToFLP(Control c)
        {
            this.flpPrograms.Controls.Add(c);
            controls_count++;
        }
        public void RemoveControlFromFLP(Control c)
        {
            if (this.flpPrograms.Controls.Contains(c))
                this.flpPrograms.Controls.Remove(c);
            controls_count--;
        }

        public int GetControlsCount()
        {
            return this.controls_count;
        }
    }
}
