using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Repositories
{
    public interface IEmployeesRepository
    {
        void DeleteById(int id);
        void DeleteTestData();
        EmployeesModel GetById(int id);
        EmployeesModel GetByCredentials(CredentialsModel id);
        IEnumerable<EmployeesModel> GetList();
        decimal? Insert(EmployeesModel model);
        void UpdateById(EmployeesModel model);
    }
}