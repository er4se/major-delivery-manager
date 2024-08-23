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
    internal class CancelRepository : IRepository<RequestCancellationModel>
    {
        private readonly ApplicationContext _DbContext;

        public CancelRepository()
        {
            _DbContext = new ApplicationContext();
        }

        public IEnumerable<RequestCancellationModel> GetAll()
        {
            _DbContext.Cancellations.Load();
            var collection = _DbContext.Cancellations.Local.ToObservableCollection();

            return collection;
        }

        public RequestCancellationModel? GetById(string id)
        {
            var entities = _DbContext.Cancellations.Where(entity => entity.RequestModelId == id);
            return entities.FirstOrDefault();
            //if (_DbContext.Cancellations.Find(id) != null)
            //{
            //    var item = _DbContext.Cancellations.Find(id);
            //
            //    return item;
            //}
            //
            //return null;
        }

        public RequestCancellationModel? GetBySubId(string subId)
        {
            var entities = _DbContext.Cancellations.Where(entity => entity.RequestModelId == subId);
            return entities.FirstOrDefault();
        }

        public async Task Create(RequestCancellationModel entity)
        {
            await _DbContext.Cancellations.LoadAsync();

            if (entity != null)
            {
                await _DbContext.Cancellations.AddAsync(entity);
            }

            Save();
        }

        public async Task Update(RequestCancellationModel entity)
        {
            await _DbContext.Cancellations.LoadAsync();

            var realEntity = GetById(entity.Id);

            if (realEntity != null)
            {
                _DbContext.Entry(realEntity).CurrentValues.SetValues(entity);
            }

            Save();
        }

        public void Delete(RequestCancellationModel entity)
        {
            _DbContext.Cancellations.Remove(entity);

            Save();
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }
    }
}
