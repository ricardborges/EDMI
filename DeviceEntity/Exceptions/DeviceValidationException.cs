using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceEntities.Exceptions
{
    public class DeviceValidationException : Exception
    {
        public DeviceValidationException(string message) :
            base(message)
        {

        }
    }
}
