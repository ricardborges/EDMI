using Core.Log;
using DeviceApplication;
using DeviceEntities.Base;
using DeviceEntities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, AddressFilterMode = AddressFilterMode.Any)]
    public class WebServiceDeviceServer : IServiceDevice
    {
        private static BootstrapWebService bootstrap = null;

        public WebServiceDeviceServer()
        {
            if (bootstrap == null)
            {
                bootstrap = new BootstrapWebService();
                bootstrap.Boot();
            }
        }

        protected ILog Log
        {
            get { return (bootstrap.Container.Get<ILog>()); }
        }

        protected IDeviceRepository DeviceRepository
        {
            get { return (bootstrap.Container.Get<IDeviceRepository>()); }
        }


        public ResultServiceDeviceClient RegisterDevice(string id, string serialNumber, int type, string ip, int port)
        {
            try
            {
                this.Log.Write(LogMessageType.Information, "", "WS starting registering new device:");
                string logDevice = string.Format("   [{0}] - Serial number: {1} - Type: {2}", id, serialNumber, ((DeviceType)type).ToString());

                if (!string.IsNullOrWhiteSpace(ip))
                    logDevice += string.Format(" ({0}:{1})", ip, port);

                this.Log.Write(LogMessageType.Information, "", logDevice);

                DeviceRegisterApplication application = new DeviceRegisterApplication(this.Log, this.DeviceRepository);
                application.Register(new Guid(id), serialNumber, ip, port, (DeviceType)type);

                this.Log.Write(LogMessageType.Information, "", "WS device registered");
                return (new ResultServiceDeviceClient(true));
            }
            catch (Exception ex)
            {
                this.Log.Write(LogMessageType.Error, "", "ERROR: " + ex.Message);
                return (new ResultServiceDeviceClient(ex));
            }
        }
    }
}
