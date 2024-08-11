using major_delivery_manager.AppDbContext;
using major_delivery_manager.Interfaces;
using major_delivery_manager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace major_delivery_manager
{
    internal class RequestRepository : IRepository<RequestModel>
    {
        private readonly ApplicationContext _DbContext;

        public RequestRepository()
        {
            _DbContext = new ApplicationContext();
        }

        public IEnumerable<RequestModel> GetAll()
        {
            _DbContext.Database.EnsureCreated();
            _DbContext.Requests.Load();
            return _DbContext.Requests.Local.ToObservableCollection();
        }

        public RequestModel? GetById(string id)
        {
            _DbContext.Database.EnsureCreated();

            if (_DbContext.Requests.Find(id) != null)
            {
                return _DbContext.Requests.Find(id);
            }
            
            return null;
        }

        public async Task Create(RequestModel entity)
        {
            await _DbContext.Database.EnsureCreatedAsync();
            await _DbContext.Requests.LoadAsync();

            if (entity != null)
            {
                await _DbContext.Requests.AddAsync(entity);
            }

            Save();
        }

        public async Task Update(RequestModel entity)
        {
            await _DbContext.Database.EnsureCreatedAsync();
            await _DbContext.Requests.LoadAsync();

            if(GetById(entity.Id) != null)
            {
                _DbContext.Requests.Update(entity);
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
