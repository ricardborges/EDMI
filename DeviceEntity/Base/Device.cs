using Core.DDD;
using DeviceEntities.Exceptions;
using System;

namespace DeviceEntities.Base
{
    public abstract class Device : IAggregateRoot
    {
        public Device(Guid id, SerialNumber serialNumber, DeviceType type)
        {
            if (id == null || id.Equals(Guid.Empty))
                throw new DeviceValidationException("Invalid device identifier");

            this.Id = id;
            this.SerialNumber = serialNumber;
            this.Type = type;
        }

        public Guid Id { get; set; }

        public SerialNumber SerialNumber { get; set; }

        public string FirmwareVersion { get; set; }

        public DeviceState State { get; set; }

        public DeviceType Type { get; set; }
    }
}
