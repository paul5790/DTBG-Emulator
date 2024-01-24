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
        public string shipIPAddress { get; set; } = "192.168.0.55";
        public string controlIPAddress { get; set; }
        public string shipPort { get; set; } = "12345";
        public string controlPort { get; set; }

    }
}
