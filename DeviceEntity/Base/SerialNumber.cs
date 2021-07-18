using DeviceEntities.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace DeviceEntities.Base
{
    public class SerialNumber
    {
        private const string SERIAL_REGEX_PATTERN = @"^[a-zA-Z0-9\-]+$";
        private string value;

        public SerialNumber(String serialNumber)
        {
            if (String.IsNullOrWhiteSpace(serialNumber))
                throw new DeviceValidationException("Serial number cannot be null or empty");
            
            if (!new Regex(SERIAL_REGEX_PATTERN).IsMatch(serialNumber))
                throw new DeviceValidationException(string.Format("Invalid format serial number [{0}]", serialNumber));

            this.value = serialNumber;
        }

        public string Value
        {
            get { return (this.value); }
        }

        public SerialNumber Clone()
        {
            return (new SerialNumber(this.value));
        }

        public bool isEquals(SerialNumber serialNumber)
        {
            bool result = this.value == serialNumber.value;

            return (result);
        }
    }
}
