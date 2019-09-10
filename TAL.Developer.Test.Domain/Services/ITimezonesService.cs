using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Services
{
    public interface ITimezonesService
    {
        void DeleteById(int id);
        void DeleteTestData();
        IEnumerable<TimezonesModel> GetList();
        TimezonesModel GetById(int id);
        decimal? Insert(TimezonesModel model);
        void UpdateById(TimezonesModel model);
    }
}