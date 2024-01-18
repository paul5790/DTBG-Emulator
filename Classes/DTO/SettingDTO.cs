using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTBGEmulator.Classes
{
    public class SettingDTO
    {
        // IP,Port Setting
        public string shipIPAddress { get; set; }
        public string controlIPAddress { get; set; }
        public string shipPort { get; set; }
        public string controlPort { get; set; }

    }
}
