using Core.GenericRepository;
using DeviceEntities;
using DeviceEntities.Base;
using DeviceEntities.Repository;
using System;

namespace DeviceRepository
{
    public class MockDeviceRepository : DeviceMemoryRepository, IDeviceRepository
    {
        public static Guid GUID_WATER_METER = new Guid("128074C5-ADA9-4448-BAB7-704D9B27F029");
        public static string SERIAL_NUMBER_WATER_METER = "WATER-2859-W";

        public static Guid GUID_ELECTRIC_METER = new Guid("942B8703-C50E-493D-A570-2E3D4928E55B");
        public static string SERIAL_NUMBER_ELECTRIC_METER = "ELECTRIC-5587-E";

        public static Guid GUID_GATEWAY = new Guid("A32D35BC-D25F-4D2A-9A8D-1629873915B4");
        public static string SERIAL_NUMBER_GATEWAY = "GATEWAY-8899-G";

        public MockDeviceRepository()
        {
            WaterMeter waterMeter = new WaterMeter(GUID_WATER_METER, new SerialNumber(SERIAL_NUMBER_WATER_METER));
            base.Insert(waterMeter);

            ElectricMeter electricMeter = new ElectricMeter(GUID_ELECTRIC_METER, new SerialNumber(SERIAL_NUMBER_ELECTRIC_METER));
            base.Insert(electricMeter);

            Gateway gateway = new Gateway(GUID_GATEWAY, new SerialNumber(SERIAL_NUMBER_GATEWAY), new DeviceAddress("217.147.152.001", 8080));
            base.Insert(gateway);
        }
    }
}
