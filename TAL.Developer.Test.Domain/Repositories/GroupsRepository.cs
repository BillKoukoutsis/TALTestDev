using AutoMapper;
using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace TAL.Developer.Test.Domain.Repositories
{
    public class GroupsRepository : IGroupsRepository
    {
        TALTestEntities _context;

        private GroupsRepository() { } // hide default constructor

        public GroupsRepository(TALTestEntities context)
        {
            _context = context;
        }

        public void DeleteById(int id)
        {
            _context.spGroups_DeleteById(id);
        }

        public void DeleteTestData()
        {
            _context.spGroups_DeleteTestData();
        }

        public IEnumerable<GroupsModel> GetList()
        {
            return Mapper.Map<IEnumerable<GroupsModel>>(_context.spGroups_GetList());
        }

        public GroupsModel GetById(int id)
        {
            return Mapper.Map<GroupsModel>(_context.spGroups_GetById(id).FirstOrDefault());
        }

        public decimal? Insert(GroupsModel model)
        {
            return _context.spGroups_Insert(model.Name).FirstOrDefault();
        }

        public void UpdateById(GroupsModel model)
        {
            _context.spGroups_UpdateById(model.Id, model.Name);
        }

    }
}