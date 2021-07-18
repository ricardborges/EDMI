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
using WebService;

namespace EDMI.ConsoleService
{
    public class BootstrapConsoleService : Bootstrap
    {

        private ConsoleServiceMode consoleServiceMode;

        public BootstrapConsoleService(ConsoleServiceMode consoleServiceMode)
            : this(new BasicContainer(), consoleServiceMode)
        {

        }

        public BootstrapConsoleService(IContainer container, ConsoleServiceMode consoleServiceMode)
          : base(container)
        {
            this.consoleServiceMode = consoleServiceMode;
        }

        public override void Boot()
        {
            switch (this.consoleServiceMode)
            {
                case ConsoleServiceMode.Both:
                    Container.RegisterInstance<ILog>(new LogMock());
                    Container.RegisterInstance<IDeviceRepository>(new DeviceMemoryRepository());
                    break;
                case ConsoleServiceMode.Client:
                
                    Container.RegisterInstance<IDeviceRepository>(new DeviceMemoryRepository());
                    Container.RegisterInstance<ILog>(new LogConsole());
                    Container.RegisterInstance<IServiceDevice>(new WebServiceDeviceClient());

                    break;
                case ConsoleServiceMode.Server:
                default:
                    Container.RegisterInstance<IDeviceRepository>(new DeviceMemoryRepository());
                    Container.RegisterInstance<ILog>(new LogConsole());
                    break;
            }

            
        }
    }
}
