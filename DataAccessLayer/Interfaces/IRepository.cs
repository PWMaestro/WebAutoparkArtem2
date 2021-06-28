using System;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T instance);
        void Delete(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Update(T instance);
    }
}
