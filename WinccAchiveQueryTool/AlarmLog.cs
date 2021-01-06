using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruifei.Models
{
    public class AlarmLog
    {
        public DateTime localDateTime { get; set; }
        public string MsgNr { get; set; }
        /// <summary>
        /// 1 Cam 2 Went 3 Ack 16 Ack
        /// </summary>
        public short State { get; set; }
        public string Text1 { get; set; }
        public string Text3 { get; set; }
        public string TxtCame { get; set; }
        public string TxtWent { get; set; }
        public string TxtAck { get; set; }
    }
}
