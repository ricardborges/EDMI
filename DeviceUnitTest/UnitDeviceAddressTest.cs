using System;
using DeviceEntities.Base;
using DeviceEntities.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeviceUnitTest
{
    [TestClass]
    public class UnitDeviceAddressTest
    {
        [TestMethod]
        public void TestDeviceAddressValid()
        {
            string ip = "192.168.001.125";
            int port = 8080;

            DeviceAddress deviceAddress = new DeviceAddress(ip, port);
            bool isValidDeviceAddress = deviceAddress.IP == ip && deviceAddress.Port == port;

            Assert.IsTrue(isValidDeviceAddress, "Error generating a valid device address");
        }

        [TestMethod]
        public void TestDeviceAddressIPInvalid()
        {
            bool isDeviceValidationExceptionGenerated = false;

            try
            {
                string ip = "192.168.001";
                int port = DeviceAddress.MAX_PORT;

                DeviceAddress deviceAddress = new DeviceAddress(ip, port);
            }
            catch (Exception ex)
            {
                isDeviceValidationExceptionGenerated = ex is DeviceValidationException;
            }

            Assert.IsTrue(isDeviceValidationExceptionGenerated, "Error generating device addres with invalid IP");
        }

        [TestMethod]
        public void TestDeviceAddressPortInvalid()
        {
            bool isDeviceValidationExceptionGenerated = false;

            try
            {
                string ip = "192.168.001.187";
                int port = DeviceAddress.MAX_PORT + 1;

                DeviceAddress deviceAddress = new DeviceAddress(ip, port);
            }
            catch (Exception ex)
            {
                isDeviceValidationExceptionGenerated = ex is DeviceValidationException;
            }

            Assert.IsTrue(isDeviceValidationExceptionGenerated, "Error generating device addres with invalid port");
        }
    }
}
