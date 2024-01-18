using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTBGEmulator.Classes.DTO
{
    public class DataDTO
    {
        public string Storage { get; set; }
        public string StartTimeStr { get; set; }
        public string EndTimeStr { get; set; }
        public List<string> FilePackets { get; set; }
    }
}
