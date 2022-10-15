using System;
using System.Collections.Generic;
using System.Text;

namespace GDMS.Repository.Interfaces
{
    public interface ICourierRepository<T> where T : class
    {
        IEnumerable<T> Get();
        string Add(T entity);
        T Get(int id);
        string Update(T entity);
        bool Delete(int id);
    }
}
