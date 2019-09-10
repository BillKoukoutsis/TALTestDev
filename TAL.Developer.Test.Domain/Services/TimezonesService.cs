using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using TAL.Developer.Test.Domain.Logging;
using TAL.Developer.Test.Domain.Models;
using TAL.Developer.Test.Domain.Repositories;

namespace TAL.Developer.Test.Domain.Services
{
    public class TimezonesService : ITimezonesService
    {
        private readonly ILogger _logger;
        private readonly ITimezonesRepository _timezonesRepository;

        public TimezonesService(ILogger logger, ITimezonesRepository contactsRepository)
        {
            _logger = logger;
            _timezonesRepository = contactsRepository;
        }

        public void DeleteById(int id)
        {
            try
            {
                _timezonesRepository.DeleteById(id);
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
                _timezonesRepository.DeleteTestData();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public IEnumerable<TimezonesModel> GetList()
        {
            try
            {
                return _timezonesRepository.GetList();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
           
        }

        public TimezonesModel GetById(int id)
        {
            try
            {
                return _timezonesRepository.GetById(id);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public decimal? Insert(TimezonesModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException("Cannot insert an empty string timezone name.");

                return _timezonesRepository.Insert(model);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
        }

        public void UpdateById(TimezonesModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException($"Cannot update timezone name with an empty string. Timezone Id was {model.Id}.");

                _timezonesRepository.UpdateById(model);
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