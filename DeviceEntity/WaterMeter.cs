using System;
using DeviceEntities.Base;

namespace DeviceEntities
{
    public class WaterMeter : Device
    {
        public WaterMeter(Guid id, SerialNumber serialNumber)
           : base(id, serialNumber, DeviceType.WaterMeter)
        {

        }
    }
}
