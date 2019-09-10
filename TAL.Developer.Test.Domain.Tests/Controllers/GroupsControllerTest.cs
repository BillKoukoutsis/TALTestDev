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
    public class GroupsControllerTest
    {
        private NLogger _logger;
        private TALTestEntities _entities;
        private GroupsRepository _repository;
        private GroupsService _service;
        private GroupsController _controller;
        private Startup _owinStartup;
        private Action<IAppBuilder> _owinStartupAction;
        private IList<decimal?> _primaryKeyPool = new List<decimal?>();

        public GroupsControllerTest()
        {
            _logger = new NLogger();
            _entities = new TALTestEntities();
            _repository = new GroupsRepository(_entities);
            _service = new GroupsService(_logger, _repository);
            _controller = new GroupsController(_service);
            _owinStartup = new Startup();
            _owinStartupAction = new Action<IAppBuilder>(_owinStartup.Configuration);
        }

        [TestInitialize]
        public void Initialize()
        {
            // Assumes insert works
            using (var server = TestServer.Create(_owinStartupAction))
            {
                OkNegotiatedContentResult<decimal?> pk0 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new GroupsModel() { Name = "First Group [TEST]" });
                _primaryKeyPool.Add(pk0.Content);
                OkNegotiatedContentResult<decimal?> pk1 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new GroupsModel() { Name = "Second Group [TEST]" });
                _primaryKeyPool.Add(pk1.Content);
                OkNegotiatedContentResult<decimal?> pk2 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new GroupsModel() { Name = "Third Group [TEST]" });
                _primaryKeyPool.Add(pk2.Content);
                OkNegotiatedContentResult<decimal?> pk3 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new GroupsModel() { Name = "Fourth Group [TEST]" });
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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<GroupsModel>>));
            OkNegotiatedContentResult<IEnumerable<GroupsModel>> results = (OkNegotiatedContentResult<IEnumerable<GroupsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<GroupsModel>));
            IEnumerable<GroupsModel> content = (IEnumerable<GroupsModel>)results.Content;

            var initContentCount = content.Count();

            _controller.DeleteById((int)_primaryKeyPool[0]);

            response = _controller.GetList();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<GroupsModel>>));
            results = (OkNegotiatedContentResult<IEnumerable<GroupsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<GroupsModel>));
            content = (IEnumerable<GroupsModel>)results.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<GroupsModel>>));
            OkNegotiatedContentResult<IEnumerable<GroupsModel>> results = (OkNegotiatedContentResult<IEnumerable<GroupsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<GroupsModel>));
            IEnumerable<GroupsModel> content = (IEnumerable<GroupsModel>)results.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<GroupsModel>));
            OkNegotiatedContentResult<GroupsModel> result = (OkNegotiatedContentResult<GroupsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(GroupsModel));
            GroupsModel content = (GroupsModel)result.Content;

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
            OkNegotiatedContentResult<decimal?> pk4 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new GroupsModel() { Name = "Mr Tim Simms [TEST]" });
            _primaryKeyPool.Add(pk4.Content);

            var response = _controller.GetById((int)_primaryKeyPool[4]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<GroupsModel>));
            OkNegotiatedContentResult<GroupsModel> result = (OkNegotiatedContentResult<GroupsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(GroupsModel));
            GroupsModel content = (GroupsModel)result.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<GroupsModel>));
            OkNegotiatedContentResult<GroupsModel> result = (OkNegotiatedContentResult<GroupsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(GroupsModel));
            GroupsModel content = (GroupsModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[2]);

            string originalName = content.Name;

            content.Name += " [Name Update]";

            _controller.UpdateById(content);

            response = _controller.GetById((int)_primaryKeyPool[2]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<GroupsModel>));
            result = (OkNegotiatedContentResult<GroupsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(GroupsModel));
            content = (GroupsModel)result.Content;

            Assert.AreEqual(content.Name, originalName + " [Name Update]");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot insert an empty string group name.")]
        public void InsertNullNameTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new GroupsModel() { Name = null });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot insert an empty string group name.")]
        public void InsertEmptyNameTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new GroupsModel() { Name = "" });
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot update group name with an empty string. Group Id was {}.")]
        public void UpdateNullNameTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<GroupsModel>));
            OkNegotiatedContentResult<GroupsModel> result = (OkNegotiatedContentResult<GroupsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(GroupsModel));
            GroupsModel content = (GroupsModel)result.Content;

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
        [ExpectedException(typeof(ApplicationException), "Cannot update group name with an empty string.")]
        public void UpdateEmptyNameTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<GroupsModel>));
            OkNegotiatedContentResult<GroupsModel> result = (OkNegotiatedContentResult<GroupsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(GroupsModel));
            GroupsModel content = (GroupsModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.Name = "";

            _controller.UpdateById(content);
        }

    }
}
