using DeviceEntities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceEntities
{
    public class Gateway : Device
    {
        public Gateway(Guid id, SerialNumber serialNumber, DeviceAddress address)
            : base(id, serialNumber, DeviceType.Gateway)
        {
            this.Address = address;
        }

        public DeviceAddress Address { get; set; }
    }
}
