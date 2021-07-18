using Core.Bootstrap;
using Core.IoC;
using Core.Log;
using DeviceEntities.Repository;
using DeviceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    public class BootstrapWebService : Bootstrap
    {

        public BootstrapWebService()
            : this(new BasicContainer())
        {

        }

        public BootstrapWebService(IContainer container)
          : base(container)
        {

        }

        public override void Boot()
        {
            Container.RegisterInstance<ILog>(new LogConsole());
            Container.RegisterInstance<IDeviceRepository>(new DeviceMemoryRepository());
        }
    }
}
