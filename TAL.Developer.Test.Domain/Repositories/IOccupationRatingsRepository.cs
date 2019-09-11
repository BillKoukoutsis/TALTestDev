using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Repositories
{
    public interface IOccupationRatingsRepository
    {
        void DeleteById(int id);
        void DeleteTestData();
        OccupationRatingsModel GetById(int id);
        IEnumerable<OccupationRatingsModel> GetList();
        decimal? Insert(OccupationRatingsModel model);
        void UpdateById(OccupationRatingsModel model);
    }
}