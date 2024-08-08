using major_delivery_manager.AppDbContext;
using major_delivery_manager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace major_delivery_manager
{
    internal class RequestRepository
    {
        private readonly ApplicationContext _DbContext;

        public RequestRepository(ApplicationContext context)
        {
            _DbContext = context;
        }

        public RequestModel GetById(string id)
        {
            if (_DbContext.Requests.Find(id) != null)
            {
                return _DbContext.Requests.Find(id);
            }
            
            return new RequestModel();
        }

        public void Create(RequestModel entity)
        {
            if (entity != null)
            {
                _DbContext.Requests.Add(entity);
            }

            Save();
        }

        public void Update(RequestModel entity)
        {
            if (entity != null)
            {
                if (_DbContext.Requests.Find(entity.Id) != null)
                {
                    _DbContext.Requests.Update(entity);
                }
            }
        }

        public void Delete(RequestModel entity)
        {
            _DbContext.Requests.Remove(entity);
            _DbContext.SaveChanges();
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }
    }
}
