using AutoMapper;
using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace TAL.Developer.Test.Domain.Repositories
{
    public class OccupationsRepository : IOccupationsRepository
    {
        TALTestEntities _context;

        private OccupationsRepository() { } // hide default constructor

        public OccupationsRepository(TALTestEntities context)
        {
            _context = context;
        }

        public void DeleteById(int id)
        {
            _context.spOccupations_DeleteById(id);
        }

        public void DeleteTestData()
        {
            _context.spOccupations_DeleteTestData();
        }

        public IEnumerable<OccupationsModel> GetList()
        {
            return Mapper.Map<IEnumerable<OccupationsModel>>(_context.spOccupations_GetList());
        }

        public OccupationsModel GetById(int id)
        {
            return Mapper.Map<OccupationsModel>(_context.spOccupations_GetById(id).FirstOrDefault());
        }

        public decimal? Insert(OccupationsModel model)
        {
            return _context.spOccupations_Insert(model.Name, model.OccupationRating.Id).FirstOrDefault();
        }

        public void UpdateById(OccupationsModel model)
        {
            _context.spOccupations_UpdateById(model.Id, model.Name, model.OccupationRating.Id);
        }

    }
}