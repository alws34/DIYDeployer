using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer
{
    internal class ProgramDetails
    {
        private string name = "";
        private string path = "";
        private string silent_switch = "";

        public ProgramDetails(string name, string path, string silent_switch = "")
        {
            this.name = name;
            this.path = path;
            this.silent_switch = silent_switch;
        }

        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
        public string Silent_switch { get => silent_switch; set => silent_switch = value; }
    }
}
