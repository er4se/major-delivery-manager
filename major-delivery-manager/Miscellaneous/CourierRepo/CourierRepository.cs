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
    internal class CourierRepository : IRepository<CourierModel>
    {
        private readonly ApplicationContext _DbContext;

        public CourierRepository()
        {
            _DbContext = new ApplicationContext();
        }

        public IEnumerable<CourierModel> GetAll()
        {
            _DbContext.Couriers.Load();
            return _DbContext.Couriers.Local.ToObservableCollection();
        }

        public CourierModel? GetById(string id)
        {
            if (_DbContext.Couriers.Find(id) != null)
            {
                return _DbContext.Couriers.Find(id);
            }

            return null;
        }

        public async Task Create(CourierModel entity)
        {
            await _DbContext.Couriers.LoadAsync();

            if (entity != null)
            {
                await _DbContext.Couriers.AddAsync(entity);
            }

            Save();
        }

        public async Task Update(CourierModel entity)
        {
            await _DbContext.Couriers.LoadAsync();

            var realEntity = GetById(entity.Id);

            if (realEntity != null)
            {
                _DbContext.Entry(realEntity).CurrentValues.SetValues(entity);
            }

            Save();
        }

        public void Delete(CourierModel entity)
        {
            _DbContext.Couriers.Remove(entity);

            Save();
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }
    }
}
