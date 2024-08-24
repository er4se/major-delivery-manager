using major_delivery_manager.AppDbContext;
using major_delivery_manager.Interfaces;
using major_delivery_manager.Miscellaneous;
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
            _DbContext.Requests.Load();
            var collection = _DbContext.Requests
                .Include(r => r.CancellationModel)
                .Include(r => r.CourierModel)
                .ToList();

            foreach ( var item in collection )
            {
                RequestStateConverter.FromString(item);
            }

            return new ObservableCollection<RequestModel>(collection);
        }

        public RequestModel? GetById(string id)
        {
            if (_DbContext.Requests.Find(id) != null)
            {
                var item = _DbContext.Requests.Include(r => r.CancellationModel)
                    .Include(r => r.CourierModel)
                    .FirstOrDefault(x => x.Id == id);

                RequestStateConverter.FromString(item);

                return item;
            }
            
            return null;
        }

        public async Task Create(RequestModel entity)
        {
            await _DbContext.Requests.LoadAsync();

            if (entity != null)
            {
                await _DbContext.Requests.AddAsync(entity);
            }

            Save();
        }

        public async Task Update(RequestModel entity)
        {
            await _DbContext.Requests.LoadAsync();

            var realEntity = GetById(entity.Id);

            if(realEntity != null)
            {
                _DbContext.Entry(realEntity).CurrentValues.SetValues(entity);
            }

            Save();
        }

        public void Delete(RequestModel entity)
        {
            _DbContext.Requests.Remove(entity);
            
            Save();
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }
    }
}
