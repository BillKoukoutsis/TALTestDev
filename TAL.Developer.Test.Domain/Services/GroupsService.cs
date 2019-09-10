using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using TAL.Developer.Test.Domain.Logging;
using TAL.Developer.Test.Domain.Models;
using TAL.Developer.Test.Domain.Repositories;

namespace TAL.Developer.Test.Domain.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly ILogger _logger;
        private readonly IGroupsRepository _groupsRepository;

        public GroupsService(ILogger logger, IGroupsRepository contactsRepository)
        {
            _logger = logger;
            _groupsRepository = contactsRepository;
        }

        public void DeleteById(int id)
        {
            try
            {
                _groupsRepository.DeleteById(id);
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
                _groupsRepository.DeleteTestData();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public IEnumerable<GroupsModel> GetList()
        {
            try
            {
                return _groupsRepository.GetList();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
           
        }

        public GroupsModel GetById(int id)
        {
            try
            {
                return _groupsRepository.GetById(id);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public decimal? Insert(GroupsModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException("Cannot insert an empty string group name.");

                return _groupsRepository.Insert(model);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
        }

        public void UpdateById(GroupsModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    throw new ApplicationException($"Cannot update group name with an empty string. Group Id was {model.Id}.");

                _groupsRepository.UpdateById(model);
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