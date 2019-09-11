using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Repositories
{
    public interface IOccupationsRepository
    {
        void DeleteById(int id);
        void DeleteTestData();
        OccupationsModel GetById(int id);
        IEnumerable<OccupationsModel> GetList();
        decimal? Insert(OccupationsModel model);
        void UpdateById(OccupationsModel model);
    }
}