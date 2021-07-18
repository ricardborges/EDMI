using System;
using System.ServiceModel;

namespace WebService
{
    public class WebServiceDeviceClient : IServiceDevice
    {
        
        public ResultServiceDeviceClient RegisterDevice(string id, string serialNumber, int type, string ip, int port)
        {
            IServiceDevice service = this.GetService(15);
            ResultServiceDeviceClient result = service.RegisterDevice(id, serialNumber, type, ip, port);

            return (result);
        }

        protected IServiceDevice GetService(int seconds)
        {

            string serverURL = ServiceDevicePublisher.HOST_IP;

            EndpointAddress address = new EndpointAddress(serverURL);

            BasicHttpBinding httpBinding = new BasicHttpBinding();

            httpBinding.MessageEncoding = WSMessageEncoding.Text;
            httpBinding.TransferMode = TransferMode.Buffered;
            httpBinding.MaxReceivedMessageSize = 10000000;       // 10 Megas como máximo por mensaje
            httpBinding.MaxBufferSize = 10000000;
            httpBinding.MaxReceivedMessageSize = 10000000;
            httpBinding.ReaderQuotas.MaxDepth = 10000000;
            httpBinding.ReaderQuotas.MaxStringContentLength = 10000000;
            httpBinding.ReaderQuotas.MaxArrayLength = 10000000;
            httpBinding.ReceiveTimeout = new TimeSpan(0, 0, 0, seconds);  // 'seconds' per una petició

            //T result = new ChannelFactory<T>(httpBinding, address).CreateChannel();
            IServiceDevice result = new ChannelFactory<IServiceDevice>(httpBinding, address).CreateChannel();

            return (result);
        }

    }
}
