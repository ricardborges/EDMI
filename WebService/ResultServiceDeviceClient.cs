using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    [DataContract]
    public class ResultServiceDeviceClient
    {
        public ResultServiceDeviceClient()
        {
            this.ErrorMessage = "";
            this.Ok = true;
        }

        public ResultServiceDeviceClient(bool ok)
        {
            this.ErrorMessage = "";
            this.Ok = ok;
        }

        public ResultServiceDeviceClient(Exception ex)
        {
            this.ErrorMessage = ex.Message;
            this.Ok = false;
        }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public bool Ok { get; set; }
        
    }
}
