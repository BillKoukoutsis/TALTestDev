using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Repositories
{
    public interface IMembersRepository
    {
        void DeleteById(int id);
        void DeleteTestData();
        MembersModel GetById(int id);
        IEnumerable<MembersModel> GetList();
        decimal? Insert(MembersModel model);
        void UpdateById(MembersModel model);
    }
}