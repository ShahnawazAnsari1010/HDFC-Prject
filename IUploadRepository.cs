using System;
using System.Collections.Generic;
using System.Text;

namespace GDMS.Repository.Interfaces
{
    public interface  IUploadRepository<T> where T : class
    {
        string Add(T entity);
    }
}
