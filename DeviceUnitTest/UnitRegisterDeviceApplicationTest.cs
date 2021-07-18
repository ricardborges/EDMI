using Core.IoC;
using Core.Log;
using DeviceApplication;
using DeviceEntities.Base;
using DeviceEntities.Exceptions;
using DeviceEntities.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DeviceUnitTest
{
    [TestClass]
    public class UnitRegisterDeviceApplicationTest
    {
        private static Guid id = new Guid("58685143-0651-452C-88FF-10F43F56E16E");
        private static string serialNumber = "AA-28";

        private static string ip = "192.168.001.125";
        private static int port = 8080;

        public UnitRegisterDeviceApplicationTest()
        {
            BootstrapTest bootstrap = new BootstrapTest(new BasicContainer());
            bootstrap.Boot();

            this.Container = bootstrap.Container;
        }

        protected IContainer Container { get; private set; }

        [TestMethod]
        public void TestRegisterValidElectricMeterDevice()
        {
            DeviceRegisterApplication application =
                new DeviceRegisterApplication(this.Container.Get<ILog>(), this.Container.Get<IDeviceRepository>());

            application.Register(id, serialNumber, DeviceType.ElectricMeter);
            Assert.IsTrue(true, "Error registering a valid electric meter device");

        }

        [TestMethod]
        public void TestRegisterValidWaterMeterDevice()
        {
            DeviceRegisterApplication application =
                new DeviceRegisterApplication(this.Container.Get<ILog>(), this.Container.Get<IDeviceRepository>());

            application.Register(id, serialNumber, DeviceType.WaterMeter);
            Assert.IsTrue(true, "Error registering a valid water meter device");

        }

        [TestMethod]
        public void TestRegisterValidGatewayDevice()
        {
            DeviceRegisterApplication application =
                new DeviceRegisterApplication(this.Container.Get<ILog>(), this.Container.Get<IDeviceRepository>());

            application.Register(id, serialNumber, ip, port, DeviceType.Gateway);
            Assert.IsTrue(true, "Error registering a valid gateway device");
        }


        [TestMethod]
        public void TestRegisterDuplicateSerialNumberDevice()
        {
            bool isDeviceRegisterExceptionGenerated = false;

            DeviceRegisterApplication application =
                new DeviceRegisterApplication(this.Container.Get<ILog>(), this.Container.Get<IDeviceRepository>());

            application.Register(id, serialNumber, ip, port, DeviceType.Gateway);
            try
            {
                application.Register(Guid.NewGuid(), serialNumber, ip, port, DeviceType.Gateway);
            }
            catch (Exception ex)
            {
                isDeviceRegisterExceptionGenerated = ex is DeviceRegisterException;
            }

            Assert.IsTrue(isDeviceRegisterExceptionGenerated, "Error registering a duplicated serial number");
        }
    }
}