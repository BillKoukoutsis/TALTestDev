using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Services
{
    public interface IOccupationRatingsService
    {
        void DeleteById(int id);
        void DeleteTestData();
        IEnumerable<OccupationRatingsModel> GetList();
        OccupationRatingsModel GetById(int id);
        decimal? Insert(OccupationRatingsModel model);
        void UpdateById(OccupationRatingsModel model);
    }
}