using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.IoC
{
    public class BasicContainer : IContainer
    {
        private Dictionary<Type, object> instances = new Dictionary<Type, object>();
        private Dictionary<Type, Type> types = new Dictionary<Type, Type>();

        public void RegisterInstance<T>(object instance)
        {
            if (instances.ContainsKey(typeof(T)) == false)
                instances.Add(typeof(T), instance);
        }



        public void RegisterType<T>(Type type)
        {
            if (types.ContainsKey(typeof(T)) == false)
                types.Add(typeof(T), type);
        }

        /// <summary>
        /// Indica si el tipo está registrado.
        /// </summary>
        public bool IsRegisterd<T>()
        {
            Type type = typeof(T);
            bool result = instances.ContainsKey(type) || types.ContainsKey(type);

            return (result);
        }

        public T Get<T>()
        {
            Type type = typeof(T);

            if (instances.ContainsKey(type))
            {
                return ((T)instances[type]);
            }
            else if (types.ContainsKey(type))
            {
                return (T)Activator.CreateInstance(types[type]);
            }
            else
            {
                Debug.Assert(false, string.Format("Unregistered [{0}] type", type.ToString()));
                return (default(T));
            }
        }
    }
}
