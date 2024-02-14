﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTBGEmulator.Classes.DTO
{
    public class DataDTO
    {
        public Dictionary<string, List<List<string>>> FilePacketsData { get; set; }
        public Dictionary<string, List<string>> FilePackets { get; set; }
        public int FileCount { get; set; }
        public int PacketCount { get; set; }
        public string FirstFileName { get; set; }
        public string LastFileName { get; set; }
        public int takenTime { get; set; }
    }
}
