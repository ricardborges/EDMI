using Core.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.GenericRepository
{
    public interface IGenericRepository<T> where T : IAggregateRoot
    {
        void Insert(T item);

        void Update(T item);

        void Delete(Guid id);

        T FindById(Guid id);

        List<T> FindAll();

    }
}
