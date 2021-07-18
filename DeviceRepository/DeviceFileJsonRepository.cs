using Core.GenericRepository;
using DeviceEntities.Base;
using DeviceEntities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceRepository
{
    public class DeviceFileJsonRepository : FileJsonGenericRepository<Device>, IDeviceRepository
    {
        public DeviceFileJsonRepository(string path)
            : base(path)
        {

        }

        public Device FindBySerialNumber(SerialNumber serialNumber)
        {
            Device result = base.FindAll().Where(r => r.SerialNumber.isEquals(serialNumber))
                                          .FirstOrDefault();

            return (result);
        }
    }
}
