using Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bootstrap
{
    public abstract class Bootstrap
    {
        public Bootstrap(IContainer container)
        {
            this.Container = container;
        }

        public IContainer Container { get; private set; }

        public abstract void Boot();
    }
}
