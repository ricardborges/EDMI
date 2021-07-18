using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IoC
{
    public interface IContainer
    {
        void RegisterInstance<T>(object instance);
        void RegisterType<T>(Type type);
        bool IsRegisterd<T>();
        T Get<T>();
    }
}
