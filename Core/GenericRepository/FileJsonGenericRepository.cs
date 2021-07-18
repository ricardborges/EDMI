using Core.DDD;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GenericRepository
{
    public class FileJsonGenericRepository<T> : IGenericRepository<T> where T : IAggregateRoot
    {
        public FileJsonGenericRepository(string path)
        {
            this.Path = path;

            if (!File.Exists(path))
                File.Create(path);

            this.SerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                // TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.None
            };
        }

        protected JsonSerializerSettings SerializerSettings { get; private set; }

        public string Path { get; private set; }


        public List<T> FindAll()
        {
            List<T> result = this.ReadAll();

            return (result);
        }

        public T FindById(Guid id)
        {
            T result = this.ReadAll().Where(r => r.Id.Equals(id))
                                     .FirstOrDefault();

            return (result);
        }

        public void Insert(T item)
        {
            if(item==null || item.Id==null)
                throw new Exception(string.Format("Invalid item"));

            List<T> registers = this.ReadAll();

            if (!registers.Exists(r => r.Id.Equals(item.Id)))
            {
                registers.Add(item);
                this.WriteAll(registers);
            }
            // The exception must be changed to a GenericRepositoryException to unify the repositories.
            else
                throw new Exception(string.Format("Duplicate register identifier {0}", item.Id));
        }

        public void Update(T item)
        {
            if (item == null || item.Id == null)
                throw new Exception(string.Format("Invalid item"));

            List<T> registers = this.ReadAll();
            for (int i = 0; i < registers.Count; i++)
            {
                T register = registers[i];
                if (register.Id.Equals(item.Id))
                {
                    registers[i] = item;
                    this.WriteAll(registers);
                    break;
                }
            }
        }

        public void Delete(Guid id)
        {
            List<T> registers = this.ReadAll();

            for (int j = registers.Count - 1; j >= 0; j--)
            {
                if (registers[j].Id.Equals(id))
                {
                    registers.RemoveAt(j);
                    this.WriteAll(registers);
                    break;
                }
            }
        }


        private List<T> ReadAll()
        {
            List<T> result = new List<T>();

            foreach (string jsonRegister in File.ReadAllLines(this.Path))
            {
                T register = JsonConvert.DeserializeObject<T>(jsonRegister, this.SerializerSettings);
                result.Add(register);
            }

            return (result);
        }

        private void WriteAll(List<T> registers)
        {
            List<string> jsonRegisters = new List<string>();

            foreach (T register in registers)
            {
                string jsonRegister = JsonConvert.SerializeObject(register, this.SerializerSettings);
                jsonRegisters.Add(jsonRegister);
            }

            File.WriteAllLines(this.Path, jsonRegisters);
        }
    }
}