using Core.DDD;
using System;
using System.Collections.Generic;

namespace Core.GenericRepository
{
    public class MemoryGenericRepository<T> : IGenericRepository<T> where T : IAggregateRoot
    {
        protected Dictionary<Guid, T> registers;

        public MemoryGenericRepository()
        {
            this.registers = new Dictionary<Guid, T>();
        }

        public void Insert(T item)
        {
            if (!this.registers.ContainsKey(item.Id))
            {
                this.registers.Add(item.Id, item);
            }
            else
                throw new Exception("Duplicate item: " + item.Id);
        }

        public void Update(T item)
        {
            if (this.registers.ContainsKey(item.Id))
                this.registers[item.Id] = item;
        }

        public void Delete(Guid id)
        {
            if (this.registers.ContainsKey(id))
                this.registers.Remove(id);
        }

        public List<T> FindAll()
        {
            List<T> result = new List<T>(this.registers.Values);

            return (result);
        }

        public T FindById(Guid id)
        { 
            T result = this.registers.Get(id, (default(T)));
           
            return (result);
        }
    }
}
