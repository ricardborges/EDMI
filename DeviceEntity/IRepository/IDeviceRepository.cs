using Core.GenericRepository;
using DeviceEntities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceEntities.Repository
{
    public interface IDeviceRepository  : IGenericRepository<Device>
    {
        Device FindBySerialNumber(SerialNumber serialNumber);

    }
}
