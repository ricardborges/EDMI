using Core.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GenericRepository
{
    public class MongoDbFakeGenericRepository<T> : IGenericRepository<T> where T : IAggregateRoot 
    {

        public MongoDbFakeGenericRepository(string host, int port, string login, string password, string collection)
        {
            this.Collection = collection;
            this.Host = host;
            this.Port = port;

            this.Login = login;
            this.Password = password;
        }

        protected string Host { get; }
        protected int Port { get; }

        protected string Login { get; }
        protected string Password { get; }

        public string Collection { get; }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public T FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T item)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
