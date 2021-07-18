using Core.Log;
using DeviceEntities.Base;
using DeviceEntities.Exceptions;
using DeviceEntities.Repository;
using DeviceEntity.Factory;
using System;

namespace DeviceApplication
{
    public class DeviceRegisterApplication
    {

        #region Constructor

        public DeviceRegisterApplication(ILog log, IDeviceRepository deviceRepository)
        {
            this.Log = log;
            this.DeviceRepository = deviceRepository;
        }

        #endregion


        #region Properties

        private ILog Log { get; }

        private IDeviceRepository DeviceRepository { get; }

        #endregion


        public void Register(Guid id, string serialNumber, DeviceType type)
        {
            this.Register(id, serialNumber, null, 0, type);
        }

        public void Register(Guid id, string serialNumber, string ip, int port, DeviceType type)
        {
            Device device;

            try
            {
                device = DeviceFactory.Create(id, serialNumber, ip, port, type);
            }
            catch (Exception ex)
            {
                this.Log.Write(LogMessageType.Error, "", ex.Message);
                if (!(ex is DeviceValidationException))
                {
                    throw new DeviceValidationException(ex.Message);
                }
                else
                    throw ex;
            }

            this.Register(device);
        }

        public void Register(Device device)
        {
            try
            {
                if (device == null)
                    throw new DeviceRegisterException("Cannot register device. The device argument is null.");

                if (this.DeviceRepository.FindById(device.Id) == null)
                {
                    Device duplicatedDeviceBySerialNumber = this.DeviceRepository.FindBySerialNumber(device.SerialNumber);

                    if (duplicatedDeviceBySerialNumber == null)
                    {
                        this.DeviceRepository.Insert(device);
                    }
                    else
                        throw new DeviceRegisterException(string.Format("Cannot register device. Duplicated [{0}] serial number device.", device.SerialNumber.Value));
                }
                else
                    this.DeviceRepository.Update(device);

                /* 
                 * At this point, a domain event could be sent notifying the registration of a device 
                 * in order to be able to perform other decoupled operations. For example, sending mail to the associated client.
                 *
                 * The reference to the domain event service must be passed in the Application (constructor) 
                 * and used from here (interface like IEventDomainService using a function like void Notify(IAggregateRoot)). 
                 * Remember that an IAggregateRoot can be serialized to Json. 
                 */
                 
                this.Log.Write(LogMessageType.Information, "", string.Format("A device type {0} with id={1} is registered correctly", device.Type, device.Id));
            }
            catch (Exception ex)
            {
                this.Log.Write(LogMessageType.Error, "", ex.Message);
                throw new DeviceRegisterException(ex);
            }
        }
    }
}
