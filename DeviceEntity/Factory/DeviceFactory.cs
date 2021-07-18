using DeviceEntities;
using DeviceEntities.Base;
using DeviceEntities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceEntity.Factory
{
    public static class DeviceFactory
    {

        public static Device Create(Guid id, string serialNumber, DeviceType type)
        {
            Device result = Create(id, serialNumber, null, 0, type);

            return (result);
        }

        public static Device Create(Guid id, string serialNumber, string ip, int port, DeviceType type)
        {
            Device result;

            switch (type)
            {
                case DeviceType.ElectricMeter:

                    result = new ElectricMeter(id, new SerialNumber(serialNumber));
                    break;
                case DeviceType.WaterMeter:

                    result = new WaterMeter(id, new SerialNumber(serialNumber));
                    break;
                case DeviceType.Gateway:

                    result = new Gateway(id, new SerialNumber(serialNumber), new DeviceAddress(ip, port));
                    break;
                default:
                    throw new DeviceValidationException(string.Format("Device type {0} not implemented", type.ToString()));
            }

            return (result);
        }
    }
}
