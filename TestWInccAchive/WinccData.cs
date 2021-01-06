using System;
using System.Collections.Generic;
using System.Text;

namespace TestWInccAchive
{
    class WinccData
    {
        public DateTime TimeStamp { get; set; }
        public int ValueID { get; set; }
        public double RealValue { get; set; }
        public int Quality { get; set; }
        public int Flags { get; set; }
    }
}
