using System;

namespace DeviceEntities.Exceptions
{
    public class DeviceRegisterException : Exception
    {
        public DeviceRegisterException(string message) :
           base(message)
        {

        }

        public DeviceRegisterException(Exception ex)
            : base(ex.Message, ex.InnerException)
        {

        }
    }
}