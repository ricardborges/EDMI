using DeviceEntities.Exceptions;
using System.Text.RegularExpressions;

namespace DeviceEntities.Base
{

    public class DeviceAddress
    {
        private const string IP_REGEX_PATTERN = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
        public const int MAX_PORT = 65536;

        public DeviceAddress(string ip, int port)
        {
            if (string.IsNullOrEmpty(ip) || !new Regex(IP_REGEX_PATTERN).IsMatch(ip))
                throw new DeviceValidationException(string.Format("Invalid IP format [{0}]", ip ?? "null"));

            if (port < 1 || port > MAX_PORT)
                throw new DeviceValidationException(string.Format("Invalid port [{0}]", port));

            this.IP = ip;
            this.Port = port;
        }

        public string IP { get; set; }

        public int Port { get; set; }


        public bool Equals(DeviceAddress deviceAddress)
        {
            bool result = deviceAddress != null &&
                this.IP == deviceAddress.IP &&
                this.Port == deviceAddress.Port;

            return (result);
        }
    }
}
