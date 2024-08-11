using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager.Interfaces
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(string id);
        Task Create(T entity);
        Task Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
