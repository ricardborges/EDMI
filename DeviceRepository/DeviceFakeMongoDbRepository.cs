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
    public class DeviceFakeMongoDbRepository : MongoDbFakeGenericRepository<Device>, IDeviceRepository
    {
        public DeviceFakeMongoDbRepository(string host, int port, string login, string password)
            : base(host, port, login, password, "Device")
        {

        }

        public Device FindBySerialNumber(SerialNumber serialNumber)
        {
            // This method must be executed using especific MongoDb SQL.
            // We are aware that getting all devices in memory to get the device 
            // associated with the serial number is a feasible solution in case there are few records
            Device result = base.FindAll()
                                .Where(d => d.SerialNumber.isEquals(serialNumber)).FirstOrDefault();

            return (result);
        }
    }
}
