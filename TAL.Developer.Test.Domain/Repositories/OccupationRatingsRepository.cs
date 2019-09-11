using AutoMapper;
using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace TAL.Developer.Test.Domain.Repositories
{
    public class OccupationRatingsRepository : IOccupationRatingsRepository
    {
        TALTestEntities _context;

        private OccupationRatingsRepository() { } // hide default constructor

        public OccupationRatingsRepository(TALTestEntities context)
        {
            _context = context;
        }

        public void DeleteById(int id)
        {
            _context.spOccupationRatings_DeleteById(id);
        }

        public void DeleteTestData()
        {
            _context.spOccupationRatings_DeleteTestData();
        }

        public IEnumerable<OccupationRatingsModel> GetList()
        {
            return Mapper.Map<IEnumerable<OccupationRatingsModel>>(_context.spOccupationRatings_GetList());
        }

        public OccupationRatingsModel GetById(int id)
        {
            return Mapper.Map<OccupationRatingsModel>(_context.spOccupationRatings_GetById(id).FirstOrDefault());
        }

        public decimal? Insert(OccupationRatingsModel model)
        {
            return _context.spOccupationRatings_Insert(model.Name, model.Factor).FirstOrDefault();
        }

        public void UpdateById(OccupationRatingsModel model)
        {
            _context.spOccupationRatings_UpdateById(model.Id, model.Name, model.Factor);
        }

    }
}