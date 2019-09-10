using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Repositories
{
    public interface ITimezonesRepository
    {
        void DeleteById(int id);
        void DeleteTestData();
        TimezonesModel GetById(int id);
        IEnumerable<TimezonesModel> GetList();
        decimal? Insert(TimezonesModel model);
        void UpdateById(TimezonesModel model);
    }
}