using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using TAL.Developer.Test.Domain.Logging;
using TAL.Developer.Test.Domain.Models;
using TAL.Developer.Test.Domain.Repositories;

namespace TAL.Developer.Test.Domain.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly ILogger _logger;
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesService(ILogger logger, IEmployeesRepository contactsRepository)
        {
            _logger = logger;
            _employeesRepository = contactsRepository;
        }

        public void DeleteById(int id)
        {
            try
            {
                _employeesRepository.DeleteById(id);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public void DeleteTestData()
        {
            try
            {
                _employeesRepository.DeleteTestData();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public IEnumerable<EmployeesModel> GetList()
        {
            try
            {
                return _employeesRepository.GetList();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
           
        }

        public EmployeesModel GetById(int id)
        {
            try
            {
                return _employeesRepository.GetById(id);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public EmployeesModel GetByCredentials(CredentialsModel credentials)
        {
            try
            {
                return _employeesRepository.GetByCredentials(credentials);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }

        }

        public decimal? Insert(EmployeesModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException("Cannot insert an empty string for employee name.");

                if (string.IsNullOrEmpty(model.Username))
                    throw new ApplicationException("Cannot insert an empty string for employee username.");

                if (string.IsNullOrEmpty(model.Password))
                    throw new ApplicationException("Cannot insert an empty string for employee password.");

                if (model.GroupId == null || model.GroupId <= 0)
                    throw new ApplicationException("Invalid group selected.");

                if (model.TimezoneId == null || model.TimezoneId <= 0)
                    throw new ApplicationException("Invalid timezone selected.");

                return _employeesRepository.Insert(model);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
        }

        public void UpdateById(EmployeesModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException($"Cannot update employee name with an empty string. Employee Id was {model.Id}.");

                if (string.IsNullOrEmpty(model.Username))
                    throw new ApplicationException($"Cannot update employee username with an empty string. Employee Id was {model.Id}.");

                if (string.IsNullOrEmpty(model.Password))
                    throw new ApplicationException($"Cannot update employee password with an empty string. Employee Id was {model.Id}.");

                if (model.GroupId == null || model.GroupId <= 0)
                    throw new ApplicationException($"Invalid group selected. Employee Id was {model.Id}.");

                if (model.TimezoneId == null || model.TimezoneId <= 0)
                    throw new ApplicationException($"Invalid timezone selected. Employee Id was {model.Id}.");

                _employeesRepository.UpdateById(model);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
        }
    }
}