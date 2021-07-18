using DeviceEntities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMI.ConsoleService
{
    public class DeviceDTO
    {
        public int Type { get; set; }
        public string DeviceType { get; set; }

        public string Id { get; set; }
        public string SerialNumber { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }

        public new string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("[{0}] {1} with serial {2}", this.Id, this.DeviceType, this.SerialNumber));
            if (!string.IsNullOrWhiteSpace(this.IP))
                sb.Append(string.Format(" ({0}:{1})", this.IP, this.Port));

            return (sb.ToString());
        }
    }
}