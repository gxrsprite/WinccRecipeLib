using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruifei.Models
{
    [DebuggerDisplay("ValueID = {ValueID}, ValueName = {ValueName}")]
    public class Archive
    {
        public long ValueID { get; set; }
        public string ValueName { get; set; }
        public int LocaleID { get; set; }
        public float CompPrecision { get; set; }
        public int CompressionMode { get; set; }
        public int VarType { get; set; }
        public int Flags { get; set; }
        public int CtrF { get; set; }
        public int CtrS { get; set; }
        public int ID { get; set; }
        public DateTime LastModification { get; set; }
    }
}
