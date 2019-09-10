using AutoMapper;
using TAL.Developer.Test.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace TAL.Developer.Test.Domain.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        TALTestEntities _context;

        private EmployeesRepository() { } // hide default constructor

        public EmployeesRepository(TALTestEntities context)
        {
            _context = context;
        }

        public void DeleteById(int id)
        {
            _context.spEmployees_DeleteById(id);
        }

        public void DeleteTestData()
        {
            _context.spEmployees_DeleteTestData();
        }

        public IEnumerable<EmployeesModel> GetList()
        {
            return Mapper.Map<IEnumerable<EmployeesModel>>(_context.spEmployees_GetList());
        }

        public EmployeesModel GetById(int id)
        {
            return Mapper.Map<EmployeesModel>(_context.spEmployees_GetById(id).FirstOrDefault());
        }

        public EmployeesModel GetByCredentials(CredentialsModel credentials)
        {
            return Mapper.Map<EmployeesModel>(_context.spEmployees_GetByCredentials(credentials.Username, credentials.Password).FirstOrDefault());
        }

        public decimal? Insert(EmployeesModel model)
        {
            return _context.spEmployees_Insert(model.Name, model.Username, model.Password, model.GroupId, model.TimezoneId, model.Rate).FirstOrDefault();
        }

        public void UpdateById(EmployeesModel model)
        {
            _context.spEmployees_UpdateById(model.Id, model.Name, model.Username, model.Password, model.GroupId, model.TimezoneId, model.Rate);
        }

    }
}