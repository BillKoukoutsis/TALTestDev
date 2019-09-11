using System;
using System.Collections.Generic;
using TAL.Developer.Test.Domain.Logging;
using TAL.Developer.Test.Domain.Models;
using TAL.Developer.Test.Domain.Repositories;

namespace TAL.Developer.Test.Domain.Services
{
    public class OccupationsService : IOccupationsService
    {
        private readonly ILogger _logger;
        private readonly IOccupationsRepository _OccupationsRepository;

        public OccupationsService(ILogger logger, IOccupationsRepository contactsRepository)
        {
            _logger = logger;
            _OccupationsRepository = contactsRepository;
        }

        public void DeleteById(int id)
        {
            try
            {
                _OccupationsRepository.DeleteById(id);
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
                _OccupationsRepository.DeleteTestData();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public IEnumerable<OccupationsModel> GetList()
        {
            try
            {
                return _OccupationsRepository.GetList();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
           
        }

        public OccupationsModel GetById(int id)
        {
            try
            {
                return _OccupationsRepository.GetById(id);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public decimal? Insert(OccupationsModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException("Occupation name cannot be an empty string.");

                if (model.OccupationRatings == null || model.OccupationRatings.Id <= 0)
                    throw new ApplicationException("Invalid occupation rating selected.");

                return _OccupationsRepository.Insert(model);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
        }

        public void UpdateById(OccupationsModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException($"Occupation name cannot be an empty string. Occupational Id was {model.Id}.");

                if (model.OccupationRatings == null || model.OccupationRatings.Id <= 0)
                    throw new ApplicationException($"Invalid occupation rating selected. Occupational Id was {model.Id}.");

                _OccupationsRepository.UpdateById(model);
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