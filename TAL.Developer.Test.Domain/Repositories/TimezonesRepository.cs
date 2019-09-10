using AutoMapper;
using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace TAL.Developer.Test.Domain.Repositories
{
    public class TimezonesRepository : ITimezonesRepository
    {
        TALTestEntities _context;

        private TimezonesRepository() { } // hide default constructor

        public TimezonesRepository(TALTestEntities context)
        {
            _context = context;
        }

        public void DeleteById(int id)
        {
            _context.spTimezones_DeleteById(id);
        }

        public void DeleteTestData()
        {
            _context.spTimezones_DeleteTestData();
        }

        public IEnumerable<TimezonesModel> GetList()
        {
            return Mapper.Map<IEnumerable<TimezonesModel>>(_context.spTimezones_GetList());
        }

        public TimezonesModel GetById(int id)
        {
            return Mapper.Map<TimezonesModel>(_context.spTimezones_GetById(id).FirstOrDefault());
        }

        public decimal? Insert(TimezonesModel model)
        {
            return _context.spTimezones_Insert(model.Name).FirstOrDefault();
        }

        public void UpdateById(TimezonesModel model)
        {
            _context.spTimezones_UpdateById(model.Id, model.Name);
        }

    }
}