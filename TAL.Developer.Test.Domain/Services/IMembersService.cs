using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Services
{
    public interface IMembersService
    {
        void DeleteById(int id);
        void DeleteTestData();
        IEnumerable<MembersModel> GetList();
        MembersModel GetById(int id);
        decimal? Insert(MembersModel model);
        void UpdateById(MembersModel model);
    }
}