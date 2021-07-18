using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebService
{
    public class ServiceDevicePublisher
    {
        private ServiceHost serviceHost;

        public const  string HOST_IP = "http://localhost:16000/ServiceDevice";

        public void Open()
        {
            // ThreadPool.QueueUserWorkItem(new WaitCallback(OpenAsync), null);
            OpenAsync(null);
        }

        protected void OpenAsync(object state)
        {
            
            this.serviceHost = new ServiceHost(typeof(WebServiceDeviceServer), new Uri(HOST_IP));
            this.serviceHost.Open();
            //Console.WriteLine("Openned!");
        }

        public void Stop()
        {
            try
            {
                if (this.serviceHost != null && this.serviceHost.State != CommunicationState.Closed)
                    this.serviceHost.Close();
            }
            catch { }
        }
    }
}
