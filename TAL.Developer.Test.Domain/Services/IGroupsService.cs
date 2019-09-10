using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Services
{
    public interface IGroupsService
    {
        void DeleteById(int id);
        void DeleteTestData();
        IEnumerable<GroupsModel> GetList();
        GroupsModel GetById(int id);
        decimal? Insert(GroupsModel model);
        void UpdateById(GroupsModel model);
    }
}