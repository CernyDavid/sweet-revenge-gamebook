using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGamebook.Services
{
    public class SessionStorage<T> : ISessionStorage<T>
    {
        private readonly IHttpContextAccessor _hca;
        private ISession Session => _hca.HttpContext.Session;

        public SessionStorage(IHttpContextAccessor hca)
        {
            _hca = hca;
        }

        public T LoadOrCreate(string key)
        {
            T result = Session.Get<T>(key);
            if (typeof(T).IsClass && result == null) result = (T)Activator.CreateInstance(typeof(T));
            return result;
        }

        public void Save(string key, T data)
        {
            Session.Set(key, data);
        }
    }
}
