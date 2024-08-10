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

        public RequestRepository()
        {
            _DbContext = new ApplicationContext();
        }

        public RequestModel GetById(string id)
        {
            _DbContext.Database.EnsureCreated();

            if (_DbContext.Requests.Find(id) != null)
            {
                return _DbContext.Requests.Find(id);
            }
            
            return new RequestModel();
        }

        public void Create(RequestModel entity)
        {
            _DbContext.Database.EnsureCreated();
            _DbContext.Requests.Load();

            if (entity != null)
            {
                _DbContext.Requests.Add(entity);
            }

            Save();
        }

        public void Update(RequestModel entity)
        {
            _DbContext.Database.EnsureCreated();

            if (entity != null)
            {
                if (_DbContext.Requests.Find(entity.Id) != null)
                {
                    _DbContext.Requests.Update(entity);
                }
            }

            Save();
        }

        public void Delete(RequestModel entity)
        {
            _DbContext.Database.EnsureCreated();
            _DbContext.Requests.Remove(entity);
            
            Save();
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }
    }
}
