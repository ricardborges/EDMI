using Core.GenericRepository;
using DeviceEntities.Base;
using DeviceEntities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeviceRepository
{
    public class DeviceMemoryRepository : MemoryGenericRepository<Device>, IDeviceRepository
    {
        public DeviceMemoryRepository()
            : base()
        {

        }

        public Device FindBySerialNumber(SerialNumber serialNumber)
        {
            Device result = this.registers.Values
                                .FirstOrDefault(d => d.SerialNumber.isEquals(serialNumber));

            return (result);
        }
    }
}
