using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    [ServiceContract]
    public interface IServiceDevice
    {
        [OperationContract]
        ResultServiceDeviceClient RegisterDevice(String id, string serialNumber, int type, string ip, int port);

    }
}
