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
    public class OccupationRatingsControllerTest
    {
        private NLogger _logger;
        private TALTestEntities _entities;
        private OccupationRatingsRepository _repository;
        private OccupationRatingsService _service;
        private OccupationRatingsController _controller;
        private Startup _owinStartup;
        private Action<IAppBuilder> _owinStartupAction;
        private IList<decimal?> _primaryKeyPool = new List<decimal?>();

        public OccupationRatingsControllerTest()
        {
            _logger = new NLogger();
            _entities = new TALTestEntities();
            _repository = new OccupationRatingsRepository(_entities);
            _service = new OccupationRatingsService(_logger, _repository);
            _controller = new OccupationRatingsController(_service);
            _owinStartup = new Startup();
            _owinStartupAction = new Action<IAppBuilder>(_owinStartup.Configuration);
        }

        [TestInitialize]
        public void Initialize()
        {
            // Assumes insert works
            using (var server = TestServer.Create(_owinStartupAction))
            {
                OkNegotiatedContentResult<decimal?> pk0 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationRatingsModel()
                {
                    Name = "Medium Manual [TEST]",
                    Factor = 1.15M
                });
                _primaryKeyPool.Add(pk0.Content);
                OkNegotiatedContentResult<decimal?> pk1 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationRatingsModel()
                {
                    Name = "Medium Automatic [TEST]",
                    Factor = 1.25M
                });
                _primaryKeyPool.Add(pk1.Content);
                OkNegotiatedContentResult<decimal?> pk2 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationRatingsModel()
                {
                    Name = "Blue Collar [TEST]",
                    Factor = 1.35M
                });
                _primaryKeyPool.Add(pk2.Content);
                OkNegotiatedContentResult<decimal?> pk3 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationRatingsModel()
                {
                    Name = "Black Collar [TEST]",
                    Factor = 1.45M
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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<OccupationRatingsModel>>));
            OkNegotiatedContentResult<IEnumerable<OccupationRatingsModel>> results = (OkNegotiatedContentResult<IEnumerable<OccupationRatingsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<OccupationRatingsModel>));
            IEnumerable<OccupationRatingsModel> content = (IEnumerable<OccupationRatingsModel>)results.Content;

            var initContentCount = content.Count();

            _controller.DeleteById((int)_primaryKeyPool[0]);

            response = _controller.GetList();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<OccupationRatingsModel>>));
            results = (OkNegotiatedContentResult<IEnumerable<OccupationRatingsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<OccupationRatingsModel>));
            content = (IEnumerable<OccupationRatingsModel>)results.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<OccupationRatingsModel>>));
            OkNegotiatedContentResult<IEnumerable<OccupationRatingsModel>> results = (OkNegotiatedContentResult<IEnumerable<OccupationRatingsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<OccupationRatingsModel>));
            IEnumerable<OccupationRatingsModel> content = (IEnumerable<OccupationRatingsModel>)results.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationRatingsModel>));
            OkNegotiatedContentResult<OccupationRatingsModel> result = (OkNegotiatedContentResult<OccupationRatingsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationRatingsModel));
            OccupationRatingsModel content = (OccupationRatingsModel)result.Content;

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
            OkNegotiatedContentResult<decimal?> pk4 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationRatingsModel()
            {
                Name = "Red Collar [TEST]",
                Factor = 1.45M
            });
            _primaryKeyPool.Add(pk4.Content);

            var response = _controller.GetById((int)_primaryKeyPool[4]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationRatingsModel>));
            OkNegotiatedContentResult<OccupationRatingsModel> result = (OkNegotiatedContentResult<OccupationRatingsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationRatingsModel));
            OccupationRatingsModel content = (OccupationRatingsModel)result.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationRatingsModel>));
            OkNegotiatedContentResult<OccupationRatingsModel> result = (OkNegotiatedContentResult<OccupationRatingsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationRatingsModel));
            OccupationRatingsModel content = (OccupationRatingsModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[2]);

            string originalName = content.Name;
            decimal originalFactor = content.Factor;

            content.Name += " [Name Update]";
            content.Factor = 33.33M;

            _controller.UpdateById(content);

            response = _controller.GetById((int)_primaryKeyPool[2]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationRatingsModel>));
            result = (OkNegotiatedContentResult<OccupationRatingsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationRatingsModel));
            content = (OccupationRatingsModel)result.Content;

            Assert.AreEqual(content.Name, originalName + " [Name Update]");
            Assert.AreEqual(content.Factor, 33.33M);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Occupation ratings name cannot be an empty string.")]
        public void InsertNullNameTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new OccupationRatingsModel()
            {
                Name = null,
                Factor = 33.33M
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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<OccupationRatingsModel>));
            OkNegotiatedContentResult<OccupationRatingsModel> result = (OkNegotiatedContentResult<OccupationRatingsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(OccupationRatingsModel));
            OccupationRatingsModel content = (OccupationRatingsModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.Name = null;

            _controller.UpdateById(content);
        }


    }
}
