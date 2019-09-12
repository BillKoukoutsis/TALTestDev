using System;
using System.Collections.Generic;
using TAL.Developer.Test.Domain.Logging;
using TAL.Developer.Test.Domain.Models;
using TAL.Developer.Test.Domain.Repositories;

namespace TAL.Developer.Test.Domain.Services
{
    public class MembersService : IMembersService
    {
        private readonly ILogger _logger;
        private readonly IMembersRepository _MembersRepository;

        public MembersService(ILogger logger, IMembersRepository contactsRepository)
        {
            _logger = logger;
            _MembersRepository = contactsRepository;
        }

        public void DeleteById(int id)
        {
            try
            {
                _MembersRepository.DeleteById(id);
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
                _MembersRepository.DeleteTestData();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public IEnumerable<MembersModel> GetList()
        {
            try
            {
                return _MembersRepository.GetList();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
           
        }

        public MembersModel GetById(int id)
        {
            try
            {
                return _MembersRepository.GetById(id);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public decimal? Insert(MembersModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException("Member name cannot be an empty string.");

                if (model.Occupation == null || model.Occupation.Id <= 0)
                    throw new ApplicationException("Invalid occupation selected.");

                return _MembersRepository.Insert(model);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
        }

        public void UpdateById(MembersModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException($"Member name cannot be an empty string. Member Id was {model.Id}.");

                if (model.Occupation == null || model.Occupation.Id <= 0)
                    throw new ApplicationException($"Invalid occupation selected. Member Id was {model.Id}.");

                _MembersRepository.UpdateById(model);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
        }

        public decimal? CalculatePremium(MembersModel model)
        {
            try
            {
                if (model.DOB == null || model.Occupation == null || model.Occupation.OccupationRating == null)
                {
                    return 0.00M;
                }

                int ageInYears = (int)Math.Round(Math.Abs(model.DOB.Subtract(DateTime.Now).TotalDays / 365.25));

                decimal premium = (model.SumInsured * model.Occupation.OccupationRating.Factor * ageInYears) / 1000 * 12;

                return premium;
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