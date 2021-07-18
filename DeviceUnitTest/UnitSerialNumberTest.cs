using System;
using DeviceEntities.Base;
using DeviceEntities.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeviceUnitTest
{
    [TestClass]
    public class UnitSerialNumberTest
    {
        [TestMethod]
        public void TestSerialNumberValid()
        {
            string validSerialNumber = "AA-989";
            SerialNumber serialNumber = new SerialNumber(validSerialNumber);

            Assert.IsTrue(serialNumber.Value == validSerialNumber, "Error generating a valid serial number");
        }

        [TestMethod]
        public void TestSerialNumberEmptyInvalid()
        {
            bool isDeviceValidationExceptionGenerated = false;

            try
            {
                string emptyInvalidSerialNumber = "";
                SerialNumber serialNumber = new SerialNumber(emptyInvalidSerialNumber);
            }
            catch (Exception ex)
            {
                isDeviceValidationExceptionGenerated = ex is DeviceValidationException;
            }

            Assert.IsTrue(isDeviceValidationExceptionGenerated, "Error generating serial number with empty value");
        }

        [TestMethod]
        public void TestSerialNumberValueInvalid()
        {
            bool isDeviceValidationExceptionGenerated = false;

            try
            {
                string invalidSerialNumber = "AA*90";
                SerialNumber serialNumber = new SerialNumber(invalidSerialNumber);
            }
            catch (Exception ex)
            {
                isDeviceValidationExceptionGenerated = ex is DeviceValidationException;
            }

            Assert.IsTrue(isDeviceValidationExceptionGenerated, "Error generating serial number with invalid value");
        }
    }
}
