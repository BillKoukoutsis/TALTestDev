using System;
using System.Collections.Generic;
using TAL.Developer.Test.Domain.Logging;
using TAL.Developer.Test.Domain.Models;
using TAL.Developer.Test.Domain.Repositories;

namespace TAL.Developer.Test.Domain.Services
{
    public class OccupationRatingsService : IOccupationRatingsService
    {
        private readonly ILogger _logger;
        private readonly IOccupationRatingsRepository _OccupationRatingsRepository;

        public OccupationRatingsService(ILogger logger, IOccupationRatingsRepository contactsRepository)
        {
            _logger = logger;
            _OccupationRatingsRepository = contactsRepository;
        }

        public void DeleteById(int id)
        {
            try
            {
                _OccupationRatingsRepository.DeleteById(id);
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
                _OccupationRatingsRepository.DeleteTestData();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public IEnumerable<OccupationRatingsModel> GetList()
        {
            try
            {
                return _OccupationRatingsRepository.GetList();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
           
        }

        public OccupationRatingsModel GetById(int id)
        {
            try
            {
                return _OccupationRatingsRepository.GetById(id);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public decimal? Insert(OccupationRatingsModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException("Occupation ratings name cannot be an empty string.");

                return _OccupationRatingsRepository.Insert(model);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
        }

        public void UpdateById(OccupationRatingsModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException($"Occupation ratings name cannot be an empty string. Occupational Rating Id was {model.Id}.");

                _OccupationRatingsRepository.UpdateById(model);
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