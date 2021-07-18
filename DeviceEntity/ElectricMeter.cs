using System;
using DeviceEntities.Base;

namespace DeviceEntities
{
    public class ElectricMeter : Device
    {
        public ElectricMeter(Guid id, SerialNumber serialNumber)
          : base(id, serialNumber, DeviceType.ElectricMeter)
        {

        }
    }
}
