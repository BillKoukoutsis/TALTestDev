using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Services
{
    public interface IOccupationsService
    {
        void DeleteById(int id);
        void DeleteTestData();
        IEnumerable<OccupationsModel> GetList();
        OccupationsModel GetById(int id);
        decimal? Insert(OccupationsModel model);
        void UpdateById(OccupationsModel model);
    }
}