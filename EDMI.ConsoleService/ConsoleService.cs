using Core.IoC;
using Core.Log;
using DeviceApplication;
using DeviceEntities;
using DeviceEntities.Base;
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
    class ConsoleService
    {
        private static string strConsoleServiceMode = "";

        static void Main(string[] args)
        {
            StartService();
        }

        private static void StartService()
        {

            strConsoleServiceMode = "?";

            ConsoleServiceMode consoleServiceMode = ReadConsoleServiceMode();
            strConsoleServiceMode = consoleServiceMode.ToString();

            switch (consoleServiceMode)
            {
                case ConsoleServiceMode.Client:
                    StartServiceClientMode();
                    break;
                case ConsoleServiceMode.Server:
                    StartServiceServerMode();
                    break;
                case ConsoleServiceMode.Both:
                    StartServiceBothMode();
                    break;
            }
        }

        static ConsoleServiceMode ReadConsoleServiceMode()
        {
            do
            {
                ConsoleWriteTitle();

                Console.WriteLine("1. Server (Webservice)");
                Console.WriteLine("2. Client (Webservice)");
                Console.WriteLine("3. Both (without Webservice)");

                ConsoleWriteEmptyLines(2);
                Console.Write("Select a console mode (1,2,3) and pulse enter: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        return (ConsoleServiceMode.Server);
                    case "2":
                        return (ConsoleServiceMode.Client);
                    case "3":
                        return (ConsoleServiceMode.Both);
                }
            } while (true);
        }


        private static void StartServiceServerMode()
        {
            ServiceDevicePublisher publisher = null;

            BootstrapConsoleService bootstrap = new BootstrapConsoleService(new BasicContainer(), ConsoleServiceMode.Server);
            bootstrap.Boot();
            ILog log = bootstrap.Container.Get<ILog>();
            IDeviceRepository repository = bootstrap.Container.Get<IDeviceRepository>();

            try
            {
                ConsoleWriteTitle();
                

                publisher = new ServiceDevicePublisher();

                log.Write(LogMessageType.Information, "", "WebService server opening at " + ServiceDevicePublisher.HOST_IP + " ...");

                publisher.Open();

                log.Write(LogMessageType.Information, "", "WebService server opened successfully");
                Console.WriteLine(">> Press ESC to exit");

                ConsoleKeyInfo pressedKey;

                do { pressedKey = Console.ReadKey(); }
                while (pressedKey.Key != ConsoleKey.Escape);
            }
            catch (Exception ex)
            {
                log.Write(LogMessageType.Error, "", ex.Message);
            }
            finally
            {
                try
                {
                    if (publisher != null)
                        publisher.Stop();
                }
                catch { }
            }
        }

        private static void StartServiceClientMode()
        {
            BootstrapConsoleService bootstrap = new BootstrapConsoleService(new BasicContainer(), ConsoleServiceMode.Client);
            bootstrap.Boot();

            ILog log = bootstrap.Container.Get<ILog>();
            IDeviceRepository repository = bootstrap.Container.Get<IDeviceRepository>();
            IServiceDevice client = bootstrap.Container.Get<IServiceDevice>();

            ConsoleKeyInfo pressedKey;

            do
            {
                try
                {
                    DeviceType deviceType = ReadDeviceType();
                    DeviceDTO deviceDto = ReadDeviceInfo(deviceType);
                    Console.WriteLine("");

                    log.Write(LogMessageType.Information, "", "Connecting to WS to register device: " + deviceDto.ToString());
                    
                    ResultServiceDeviceClient resultService =
                        client.RegisterDevice(deviceDto.Id, deviceDto.SerialNumber, deviceDto.Type, deviceDto.IP, deviceDto.Port);

                    if (!resultService.Ok)
                    {
                        log.Write(LogMessageType.Error, "", string.Format("Error registering Device {0}", deviceDto.ToString()));
                        log.Write(LogMessageType.Error, "", "Message: " + resultService.ErrorMessage);
                    }
                    else
                        log.Write(LogMessageType.Information, "", string.Format("Device {0} registered correctly.", deviceDto.ToString()));
                }
                catch (Exception ex)
                {
                    log.Write(LogMessageType.Error, "", "ERROR: " + ex.Message);
                }

                ConsoleWriteEmptyLines(2);
                Console.WriteLine(">> Press any key to register new device or ESC to exit");
                pressedKey = Console.ReadKey();

            } while (pressedKey.Key != ConsoleKey.Escape);
        }


        private static void StartServiceBothMode()
        {
            BootstrapConsoleService bootstrap = new BootstrapConsoleService(new BasicContainer(), ConsoleServiceMode.Both);
            bootstrap.Boot();

            ILog log = bootstrap.Container.Get<ILog>();
            IDeviceRepository repository = bootstrap.Container.Get<IDeviceRepository>();

            ConsoleKeyInfo pressedKey;

            do
            {
                try
                {
                    DeviceType deviceType = ReadDeviceType();
                    DeviceDTO deviceDto = ReadDeviceInfo(deviceType);
                    Console.WriteLine("");

                    DeviceRegisterApplication application = new DeviceRegisterApplication(log, repository);

                    switch (deviceType)
                    {
                        case DeviceType.ElectricMeter:
                        case DeviceType.WaterMeter:

                            application.Register(new Guid(deviceDto.Id), deviceDto.SerialNumber, deviceType);
                            break;
                        case DeviceType.Gateway:
                        default:

                            application.Register(new Guid(deviceDto.Id), deviceDto.SerialNumber, deviceDto.IP, deviceDto.Port, deviceType);
                            break;
                    }

                    Console.WriteLine(string.Format("Device {0} registered correctly.", deviceDto.ToString()));
                }
                catch (Exception ex)
                {
                    Console.Write("ERROR: " + ex.Message);
                }

                ConsoleWriteEmptyLines(2);
                Console.WriteLine(">> Press any key to register new device or ESC to exit");
                pressedKey = Console.ReadKey();

            } while (pressedKey.Key != ConsoleKey.Escape);
            
        }

        static DeviceDTO ReadDeviceInfo(DeviceType type)
        {
            DeviceDTO result = new DeviceDTO
            {
                Id = Guid.NewGuid().ToString(),
                DeviceType = type.ToString(),
                Type = (int)type
            };

            ConsoleWriteTitle();

            Console.WriteLine(string.Format("Enter the information for register the [{0}] device type:", type.ToString()));
            ConsoleWriteEmptyLines(2);

            Console.Write("     Enter serial number: ");
            result.SerialNumber = Console.ReadLine();

            if (type == DeviceType.Gateway)
            {
                Console.Write("     Enter IP: ");
                result.IP = Console.ReadLine();

                Console.Write("     Enter port (default 8080): ");
                string strPort = Console.ReadLine();
                if (Int32.TryParse(strPort, out int port))
                {
                    result.Port = port;
                }
                else
                    result.Port = 8080; // Default value
            }
            
            return (result);
        }


        static DeviceType ReadDeviceType()
        {
            do
            {
                ConsoleWriteTitle();

                Console.WriteLine("Enter the information for register a device type:");
                ConsoleWriteEmptyLines(2);

                Console.WriteLine("1. Electric meter");
                Console.WriteLine("2. Water meter");
                Console.WriteLine("3. Gateway");

                ConsoleWriteEmptyLines(2);
                Console.Write("Select a device type (1,2,3) and pulse enter: ");

                switch (Console.ReadLine())
                {
                    case "1":

                        return (DeviceType.ElectricMeter);
                    case "2":

                        return (DeviceType.WaterMeter);
                    case "3":
                        return (DeviceType.Gateway);
                }
            } while (true);
        }


        static void ConsoleWriteTitle()
        {
            Console.Clear();
            

            Console.WriteLine("**************************************************************************");
            Console.WriteLine(string.Format("*    EDMI TEST v.1.00 (18/07/2021) - Mode: {0}                    *", strConsoleServiceMode.PadRight(10, ' ')));
            Console.WriteLine("**************************************************************************");

            ConsoleWriteEmptyLines(2);
        }

        static void ConsoleWriteEmptyLines(int lines)
        {
            for (int line = 0; line < lines; line++)
                Console.WriteLine("");

        }
    }
}
