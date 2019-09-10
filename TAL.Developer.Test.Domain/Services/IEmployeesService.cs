using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Services
{
    public interface IEmployeesService
    {
        void DeleteById(int id);
        void DeleteTestData();
        IEnumerable<EmployeesModel> GetList();
        EmployeesModel GetById(int id);
        EmployeesModel GetByCredentials(CredentialsModel credentials);
        decimal? Insert(EmployeesModel model);
        void UpdateById(EmployeesModel model);
    }
}