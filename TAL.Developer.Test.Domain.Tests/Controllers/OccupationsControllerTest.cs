using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TAL.Developer.Test.Domain.Controllers;
using TAL.Developer.Test.Domain.Services;
using TAL.Developer.Test.Domain.Logging;
using TAL.Developer.Test.Domain.Repositories;
using Owin;
using Microsoft.Owin.Testing;
using System.Web.Http.Results;
using System.Collections.Generic;
using System.Linq;
using TAL.Developer.Test.Domain.Models;
using System.Data.SqlClient;

namespace TAL.Developer.Test.Domain.Tests.Controllers
{
    [TestClass]
    public class OccupationsControllerTest
    {
        private NLogger _logger;
        private TALTestEntities _entities;
        private OccupationsRepository _repository;
        private OccupationsService _service;
        private OccupationsController _controller;
        private Startup _owinStartup;
        private Action<IAppBuilder> _owinStartupAction;
        private IList<decimal?> _primaryKeyPool = new List<decimal?>();

        public OccupationsControllerTest()
        {
            _logger = new NLogger();
            _entities = new TALTestEntities();
            _repository = new OccupationsRepository(_entities);
            _service = new OccupationsService(_logger, _repository);
            _controller = new OccupationsController(_service);
            _owinStartup = new Startup();
            _owinStartupAction = new Action<IAppBuilder>(_owinStartup.Configuration);
        }

        [TestInitialize]
        public void Initialize()
        {
            // Assumes insert works
            using (var server = TestServer.Create(_owinStartupAction))
            {
                OkNegotiatedContentResult<decimal?> pk0 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationsModel()
                {
                    Name = "Engineer [TEST]",
                    OccupationRatings = new OccupationRatingsModel()
                    {
                        Id = 1 // Professional; 1.0
                    }
                });
                _primaryKeyPool.Add(pk0.Content);
                OkNegotiatedContentResult<decimal?> pk1 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationsModel()
                {
                    Name = "Chemist [TEST]",
                    OccupationRatings = new OccupationRatingsModel()
                    {
                        Id = 1 // Professional; 1.0
                    }
                });
                _primaryKeyPool.Add(pk1.Content);
                OkNegotiatedContentResult<decimal?> pk2 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationsModel()
                {
                    Name = "Butcher [TEST]",
                    OccupationRatings = new OccupationRatingsModel()
                    {
                        Id = 4 // Heavy Manual; 1.75
                    }
                });
                _primaryKeyPool.Add(pk2.Content);
                OkNegotiatedContentResult<decimal?> pk3 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationsModel()
                {
                    Name = "Cobbler [TEST]",
                    OccupationRatings = new OccupationRatingsModel()
                    {
                        Id = 3 // Light Manual; 1.50
                    }
                });
                _primaryKeyPool.Add(pk3.Content);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _controller.DeleteTestData();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 0
        /// </remarks>
        [TestMethod]
        public void DeleteByIdTest()
        {
            var response = _controller.GetList();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<OccupationsModel>>));
            OkNegotiatedContentResult<IEnumerable<OccupationsModel>> results = (OkNegotiatedContentResult<IEnumerable<OccupationsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<OccupationsModel>));
            IEnumerable<OccupationsModel> content = (IEnumerable<OccupationsModel>)results.Content;

            var initContentCount = content.Count();

            _controller.DeleteById((int)_primaryKeyPool[0]);

            response = _controller.GetList();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<OccupationsModel>>));
            results = (OkNegotiatedContentResult<IEnumerable<OccupationsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<OccupationsModel>));
            content = (IEnumerable<OccupationsModel>)results.Content;

            var currentContentCount = content.Count();

            Assert.AreEqual(initContentCount - 1, currentContentCount);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetListTest()
        {
            var response = _controller.GetList();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<OccupationsModel>>));
            OkNegotiatedContentResult<IEnumerable<OccupationsModel>> results = (OkNegotiatedContentResult<IEnumerable<OccupationsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<OccupationsModel>));
            IEnumerable<OccupationsModel> content = (IEnumerable<OccupationsModel>)results.Content;

            Assert.IsTrue(content.Count() > 0);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 1
        /// </remarks>
        [TestMethod]
        public void GetByIdTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[1]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationsModel>));
            OkNegotiatedContentResult<OccupationsModel> result = (OkNegotiatedContentResult<OccupationsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationsModel));
            OccupationsModel content = (OccupationsModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[1]);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Generates _primaryKeyPool index 4
        /// </remarks>
        [TestMethod]
        public void InsertTest()
        {
            OkNegotiatedContentResult<decimal?> pk4 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationsModel()
            {
                Name = "Pilot [TEST]",
                OccupationRatings = new OccupationRatingsModel()
                {
                    Id = 1 // Professional; 1.0
                }
            });
            _primaryKeyPool.Add(pk4.Content);

            var response = _controller.GetById((int)_primaryKeyPool[4]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationsModel>));
            OkNegotiatedContentResult<OccupationsModel> result = (OkNegotiatedContentResult<OccupationsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationsModel));
            OccupationsModel content = (OccupationsModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[4]);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 2
        /// </remarks>
        [TestMethod]
        public void UpdateByIdTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[2]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationsModel>));
            OkNegotiatedContentResult<OccupationsModel> result = (OkNegotiatedContentResult<OccupationsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationsModel));
            OccupationsModel content = (OccupationsModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[2]);

            string originalName = content.Name;
            decimal originalOccupationRatingId = content.OccupationRatings.Id;

            content.Name += " [Name Update]";
            content.OccupationRatings.Id = 2; // White Collar, 1.25

            _controller.UpdateById(content);

            response = _controller.GetById((int)_primaryKeyPool[2]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationsModel>));
            result = (OkNegotiatedContentResult<OccupationsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationsModel));
            content = (OccupationsModel)result.Content;

            Assert.AreEqual(content.Name, originalName + " [Name Update]");
            Assert.AreEqual(content.OccupationRatings.Id, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Occupation name cannot be an empty string.")]
        public void InsertNullNameTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationsModel()
            {
                Name = null,
                OccupationRatings = new OccupationRatingsModel()
                {
                    Id = 1 // Professional; 1.0
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Invalid occupation rating selected.")]
        public void InsertNullOccupationRatingTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationsModel()
            {
                Name = "Designer [TEST]",
                OccupationRatings = null
            });
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Occupation ratings name cannot be an empty string. Occupational Rating Id was {}.")]
        public void UpdateNullNameTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationsModel>));
            OkNegotiatedContentResult<OccupationsModel> result = (OkNegotiatedContentResult<OccupationsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationsModel));
            OccupationsModel content = (OccupationsModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.Name = null;

            _controller.UpdateById(content);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Invalid occupation rating selected. Occupational Id was {}.")]
        public void UpdateNullOccupationRatingsTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationsModel>));
            OkNegotiatedContentResult<OccupationsModel> result = (OkNegotiatedContentResult<OccupationsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationsModel));
            OccupationsModel content = (OccupationsModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.OccupationRatings = null;

            _controller.UpdateById(content);
        }


    }
}
