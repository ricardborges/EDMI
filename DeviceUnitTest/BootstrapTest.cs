using Core.Bootstrap;
using Core.IoC;
using Core.Log;
using DeviceEntities.Repository;
using DeviceRepository;

namespace DeviceUnitTest
{
    public class BootstrapTest : Bootstrap
    {

        public BootstrapTest(IContainer container) 
            : base(container)
        {

        }

        public override void Boot()
        {
            Container.RegisterInstance<ILog>(new LogMock());
            Container.RegisterInstance<IDeviceRepository>(new MockDeviceRepository());
        }
    }
}
