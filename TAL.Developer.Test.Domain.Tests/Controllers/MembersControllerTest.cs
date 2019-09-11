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
    public class MembersControllerTest
    {
        private NLogger _logger;
        private TALTestEntities _entities;
        private MembersRepository _repository;
        private MembersService _service;
        private MembersController _controller;
        private Startup _owinStartup;
        private Action<IAppBuilder> _owinStartupAction;
        private IList<decimal?> _primaryKeyPool = new List<decimal?>();

        public MembersControllerTest()
        {
            _logger = new NLogger();
            _entities = new TALTestEntities();
            _repository = new MembersRepository(_entities);
            _service = new MembersService(_logger, _repository);
            _controller = new MembersController(_service);
            _owinStartup = new Startup();
            _owinStartupAction = new Action<IAppBuilder>(_owinStartup.Configuration);
        }

        [TestInitialize]
        public void Initialize()
        {
            // Assumes insert works
            using (var server = TestServer.Create(_owinStartupAction))
            {
                OkNegotiatedContentResult<decimal?> pk0 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new MembersModel()
                {
                    Name = "Mr Bill Koukoutsis [TEST]",
                    DOB = new DateTime(1969, 1, 28),
                    SumInsured = 100000M,
                    Occupations = new OccupationsModel
                    {
                        Id = 1 // Cleaner
                    },
                });
                _primaryKeyPool.Add(pk0.Content);
                OkNegotiatedContentResult<decimal?> pk1 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new MembersModel()
                {
                    Name = "Mr Jack Black [TEST]",
                    DOB = new DateTime(1975, 11, 2),
                    SumInsured = 1000000M,
                    Occupations = new OccupationsModel
                    {
                        Id = 3 // Author
                    },
                });
                _primaryKeyPool.Add(pk1.Content);
                OkNegotiatedContentResult<decimal?> pk2 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new MembersModel()
                {
                    Name = "Ms Jane Doe [TEST]",
                    DOB = new DateTime(1985, 6, 18),
                    SumInsured = 2000000M,
                    Occupations = new OccupationsModel
                    {
                        Id = 6 // Florist
                    },
                });
                _primaryKeyPool.Add(pk2.Content);
                OkNegotiatedContentResult<decimal?> pk3 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new MembersModel()
                {
                    Name = "Mr John Doe [TEST]",
                    DOB = new DateTime(1985, 6, 18),
                    SumInsured = 1000000M,
                    Occupations = new OccupationsModel
                    {
                        Id = 5 // Mechanic
                    },
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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<MembersModel>>));
            OkNegotiatedContentResult<IEnumerable<MembersModel>> results = (OkNegotiatedContentResult<IEnumerable<MembersModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<MembersModel>));
            IEnumerable<MembersModel> content = (IEnumerable<MembersModel>)results.Content;

            var initContentCount = content.Count();

            _controller.DeleteById((int)_primaryKeyPool[0]);

            response = _controller.GetList();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<MembersModel>>));
            results = (OkNegotiatedContentResult<IEnumerable<MembersModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<MembersModel>));
            content = (IEnumerable<MembersModel>)results.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<MembersModel>>));
            OkNegotiatedContentResult<IEnumerable<MembersModel>> results = (OkNegotiatedContentResult<IEnumerable<MembersModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<MembersModel>));
            IEnumerable<MembersModel> content = (IEnumerable<MembersModel>)results.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<MembersModel>));
            OkNegotiatedContentResult<MembersModel> result = (OkNegotiatedContentResult<MembersModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(MembersModel));
            MembersModel content = (MembersModel)result.Content;

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
            OkNegotiatedContentResult<decimal?> pk4 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new MembersModel()
            {
                Name = "Mr Ray Price [TEST]",
                DOB = new DateTime(1955, 9, 11),
                SumInsured = 333000M,
                Occupations = new OccupationsModel
                {
                    Id = 5 // Mechanic
                },
            });
            _primaryKeyPool.Add(pk4.Content);

            var response = _controller.GetById((int)_primaryKeyPool[4]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<MembersModel>));
            OkNegotiatedContentResult<MembersModel> result = (OkNegotiatedContentResult<MembersModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(MembersModel));
            MembersModel content = (MembersModel)result.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<MembersModel>));
            OkNegotiatedContentResult<MembersModel> result = (OkNegotiatedContentResult<MembersModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(MembersModel));
            MembersModel content = (MembersModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[2]);

            string originalName = content.Name;
            DateTime originalDOB = content.DOB;
            decimal originalSumInsured = content.SumInsured;
            int originalOccupationId = content.Occupations.Id;


            content.Name += " [Name Update]";
            content.DOB = new DateTime(1941, 1, 1);
            content.SumInsured = 777000M;
            content.Occupations.Id = 4; // Farmer

            _controller.UpdateById(content);

            response = _controller.GetById((int)_primaryKeyPool[2]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<MembersModel>));
            result = (OkNegotiatedContentResult<MembersModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(MembersModel));
            content = (MembersModel)result.Content;

            Assert.AreEqual(content.Name, originalName + " [Name Update]");
            Assert.AreEqual(content.DOB, new DateTime(1941, 1, 1));
            Assert.AreEqual(content.SumInsured, 777000M);
            Assert.AreEqual(content.Occupations.Id, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Member name cannot be an empty string.")]
        public void InsertNullNameTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new MembersModel()
            {
                Name = null,
                DOB = new DateTime(2000, 1, 1),
                SumInsured = 121000M,
                Occupations = new OccupationsModel
                {
                    Id = 1 // Cleaner
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Invalid occupation selected.")]
        public void InsertNullMemberRatingTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new MembersModel()
            {
                Name = "Mr Joe Bloe [TEST]",
                DOB = new DateTime(2000, 1, 1),
                SumInsured = 121000M,
                Occupations = null
            });
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Member name cannot be an empty string. Member Id was {}.")]
        public void UpdateNullNameTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<MembersModel>));
            OkNegotiatedContentResult<MembersModel> result = (OkNegotiatedContentResult<MembersModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(MembersModel));
            MembersModel content = (MembersModel)result.Content;

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
        [ExpectedException(typeof(ApplicationException), "Invalid occupation selected. Member Id was {}.")]
        public void UpdateNullMemberRatingsTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<MembersModel>));
            OkNegotiatedContentResult<MembersModel> result = (OkNegotiatedContentResult<MembersModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(MembersModel));
            MembersModel content = (MembersModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.Occupations = null;

            _controller.UpdateById(content);
        }


    }
}
