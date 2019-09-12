using AutoMapper;
using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace TAL.Developer.Test.Domain.Repositories
{
    public class MembersRepository : IMembersRepository
    {
        TALTestEntities _context;

        private MembersRepository() { } // hide default constructor

        public MembersRepository(TALTestEntities context)
        {
            _context = context;
        }

        public void DeleteById(int id)
        {
            _context.spMembers_DeleteById(id);
        }

        public void DeleteTestData()
        {
            _context.spMembers_DeleteTestData();
        }

        public IEnumerable<MembersModel> GetList()
        {
            return Mapper.Map<IEnumerable<MembersModel>>(_context.spMembers_GetList());
        }

        public MembersModel GetById(int id)
        {
            return Mapper.Map<MembersModel>(_context.spMembers_GetById(id).FirstOrDefault());
        }

        public decimal? Insert(MembersModel model)
        {
            return _context.spMembers_Insert(model.Name, model.DOB, model.Occupation.Id, model.SumInsured).FirstOrDefault();
        }

        public void UpdateById(MembersModel model)
        {
            _context.spMembers_UpdateById(model.Id, model.Name, model.DOB, model.Occupation.Id, model.SumInsured);
        }

    }
}