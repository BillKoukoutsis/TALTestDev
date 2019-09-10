using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Repositories
{
    public interface IGroupsRepository
    {
        void DeleteById(int id);
        void DeleteTestData();
        GroupsModel GetById(int id);
        IEnumerable<GroupsModel> GetList();
        decimal? Insert(GroupsModel model);
        void UpdateById(GroupsModel model);
    }
}