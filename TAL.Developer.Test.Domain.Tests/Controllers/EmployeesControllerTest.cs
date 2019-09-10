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
    public class EmployeesControllerTest
    {
        private NLogger _logger;
        private TALTestEntities _entities;
        private EmployeesRepository _repository;
        private EmployeesService _service;
        private EmployeesController _controller;
        private Startup _owinStartup;
        private Action<IAppBuilder> _owinStartupAction;
        private IList<decimal?> _primaryKeyPool = new List<decimal?>();

        public EmployeesControllerTest()
        {
            _logger = new NLogger();
            _entities = new TALTestEntities();
            _repository = new EmployeesRepository(_entities);
            _service = new EmployeesService(_logger, _repository);
            _controller = new EmployeesController(_service);
            _owinStartup = new Startup();
            _owinStartupAction = new Action<IAppBuilder>(_owinStartup.Configuration);
        }

        [TestInitialize]
        public void Initialize()
        {
            // Assumes insert works
            using (var server = TestServer.Create(_owinStartupAction))
            {
                OkNegotiatedContentResult<decimal?> pk0 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
                {
                        Name = "Mr John Doe [TEST]",
                        Username = "johnd",
                        Password = "password",
                        GroupId = 1,
                        TimezoneId = 1,
                        Rate = 10.0M
                });
                _primaryKeyPool.Add(pk0.Content);
                OkNegotiatedContentResult<decimal?> pk1 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
                {
                    Name = "Ms Jane Doe [TEST]",
                    Username = "janed",
                    Password = "password",
                    GroupId = 1,
                    TimezoneId = 1,
                    Rate = 11.0M
                });
                _primaryKeyPool.Add(pk1.Content);
                OkNegotiatedContentResult<decimal?> pk2 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
                {
                    Name = "Mr Jack Palance [TEST]",
                    Username = "jackp",
                    Password = "password",
                    GroupId = 1,
                    TimezoneId = 1,
                    Rate = 33.0M
                });
                _primaryKeyPool.Add(pk2.Content);
                OkNegotiatedContentResult<decimal?> pk3 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
                {
                    Name = "Mr Bill Koukoutsis [TEST]",
                    Username = "billk",
                    Password = "password",
                    GroupId = 1,
                    TimezoneId = 1,
                    Rate = 34.0M
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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<EmployeesModel>>));
            OkNegotiatedContentResult<IEnumerable<EmployeesModel>> results = (OkNegotiatedContentResult<IEnumerable<EmployeesModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<EmployeesModel>));
            IEnumerable<EmployeesModel> content = (IEnumerable<EmployeesModel>)results.Content;

            var initContentCount = content.Count();

            _controller.DeleteById((int)_primaryKeyPool[0]);

            response = _controller.GetList();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<EmployeesModel>>));
            results = (OkNegotiatedContentResult<IEnumerable<EmployeesModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<EmployeesModel>));
            content = (IEnumerable<EmployeesModel>)results.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<EmployeesModel>>));
            OkNegotiatedContentResult<IEnumerable<EmployeesModel>> results = (OkNegotiatedContentResult<IEnumerable<EmployeesModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<EmployeesModel>));
            IEnumerable<EmployeesModel> content = (IEnumerable<EmployeesModel>)results.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

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
            OkNegotiatedContentResult<decimal?> pk4 = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
            {
                Name = "Mr Tim Simms [TEST]",
                Username = "tims",
                Password = "password",
                GroupId = 1,
                TimezoneId = 1,
                Rate = 35.0M
            });
            _primaryKeyPool.Add(pk4.Content);

            var response = _controller.GetById((int)_primaryKeyPool[4]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

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
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[2]);

            string originalName = content.Name;
            string originalUsername = content.Username;
            string originalPassword = content.Password;
            int? originalGroupId = content.GroupId;
            int? originalTimezoneId = content.TimezoneId;
            decimal originalRate = content.Rate;

            content.Name += " [Name Update]";
            content.Username += " [Username Update]";
            content.Password += " [Password Update]";
            content.GroupId = 1;
            content.TimezoneId = 1;
            content.Rate = 33.33M;

            _controller.UpdateById(content);

            response = _controller.GetById((int)_primaryKeyPool[2]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            content = (EmployeesModel)result.Content;

            Assert.AreEqual(content.Name, originalName + " [Name Update]");
            Assert.AreEqual(content.Username, originalUsername + " [Username Update]");
            Assert.AreEqual(content.Password, originalPassword + " [Password Update]");
            Assert.AreEqual(content.GroupId, 1);
            Assert.AreEqual(content.TimezoneId, 1);
            Assert.AreEqual(content.Rate, 33.33M);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot insert an empty string employee name.")]
        public void InsertNullNameTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
            {
                Name = null
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot insert an empty string employee name.")]
        public void InsertEmptyNameTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
            {
                Name = ""
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot insert an empty string employee username.")]
        public void InsertNullUsernameTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
            {
                Name = "Mr Clint Eastwood [TEST]",
                Username = null
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot insert an empty string employee username.")]
        public void InsertEmptyUsernameTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
            {
                Name = "Mr Clint Eastwood [TEST]",
                Username = ""
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot insert an empty string employee password.")]
        public void InsertNullPasswordTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
            {
                Name = "Mr Clint Eastwood [TEST]",
                Username = "clinte",
                Password = null
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot insert an empty string employee password.")]
        public void InsertEmptyPasswordTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
            {
                Name = "Mr Clint Eastwood [TEST]",
                Username = "clinte",
                Password = ""
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Invalid group selected.")]
        public void InsertNullGroupTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
            {
                Name = "Mr Clint Eastwood [TEST]",
                Username = "clinte",
                Password = "password",
                GroupId = null
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Invalid timezone selected.")]
        public void InsertNullTimezoneTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_controller.Insert(new EmployeesModel()
            {
                Name = "Mr Clint Eastwood [TEST]",
                Username = "clinte",
                Password = "password",
                GroupId = 1,
                TimezoneId = null
            });
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot update employee name with an empty string. Employee Id was {}.")]
        public void UpdateNullNameTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

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
        [ExpectedException(typeof(ApplicationException), "Cannot update employee name with an empty string. Employee Id was {}.")]
        public void UpdateEmptyNameTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.Name = "";

            _controller.UpdateById(content);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot update employee username with an empty string. Employee Id was {}")]
        public void UpdateNullUsernameTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.Username = null;

            _controller.UpdateById(content);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot update employee username with an empty string. Employee Id was {}")]
        public void UpdateEmptyUsernameTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.Username = "";

            _controller.UpdateById(content);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot update employee password with an empty string. Employee Id was {}")]
        public void UpdateNullPasswordTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.Password = null;

            _controller.UpdateById(content);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot update employee password with an empty string. Employee Id was {}")]
        public void UpdateEmptyPasswordTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.Password = "";

            _controller.UpdateById(content);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Invalid group selected. Employee Id was {}.")]
        public void UpdateNullGroupIdTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.GroupId = null;

            _controller.UpdateById(content);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Invalid timezone selected. Employee Id was {}.")]
        public void UpdateNullTimezoneTest()
        {
            var response = _controller.GetById((int)_primaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EmployeesModel>));
            OkNegotiatedContentResult<EmployeesModel> result = (OkNegotiatedContentResult<EmployeesModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(EmployeesModel));
            EmployeesModel content = (EmployeesModel)result.Content;

            Assert.AreEqual(content.Id, _primaryKeyPool[3]);

            content.TimezoneId = null;

            _controller.UpdateById(content);
        }

    }
}
