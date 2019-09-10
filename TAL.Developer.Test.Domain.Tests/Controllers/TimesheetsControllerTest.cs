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
    public class TimesheetsControllerTest
    {
        private NLogger _logger;
        private TALTestEntities _entities;
        private TimesheetsRepository _tshRepository;
        private TimesheetsService _tshService;
        private TimesheetsController _tshController;

        private EmployeesRepository _empRepository;
        private EmployeesService _empService;
        private EmployeesController _empController;

        private Startup _owinStartup;
        private Action<IAppBuilder> _owinStartupAction;

        private IList<decimal?> _tshPrimaryKeyPool = new List<decimal?>();
        private IList<decimal?> _empPrimaryKeyPool = new List<decimal?>();

        public TimesheetsControllerTest()
        {
            _logger = new NLogger();
            _entities = new TALTestEntities();

            _tshRepository = new TimesheetsRepository(_entities);
            _tshService = new TimesheetsService(_logger, _tshRepository);
            _tshController = new TimesheetsController(_tshService);

            _empRepository = new EmployeesRepository(_entities);
            _empService = new EmployeesService(_logger, _empRepository);
            _empController = new EmployeesController(_empService);

            _owinStartup = new Startup();
            _owinStartupAction = new Action<IAppBuilder>(_owinStartup.Configuration);
        }

        [TestInitialize]
        public void Initialize()
        {
            // Assumes insert works
            using (var server = TestServer.Create(_owinStartupAction))
            {
                OkNegotiatedContentResult<decimal?> pkEmp0 = (OkNegotiatedContentResult<decimal?>)_empController.Insert(new EmployeesModel()
                {
                    Name = "Mr John Doe [TEST]",
                    Username = "johnd",
                    Password = "password",
                    GroupId = 1,
                    TimezoneId = 1,
                    Rate = 10.33M
                });
                _empPrimaryKeyPool.Add(pkEmp0.Content);

                OkNegotiatedContentResult<decimal?> pk0 = (OkNegotiatedContentResult<decimal?>)_tshController.Insert(new TimesheetsModel()
                {
                    EmployeeId = (int)pkEmp0.Content,
                    StartDate = DateTime.Now.AddDays(0)
                });
                _tshPrimaryKeyPool.Add(pk0.Content);
                OkNegotiatedContentResult<decimal?> pk1 = (OkNegotiatedContentResult<decimal?>)_tshController.Insert(new TimesheetsModel()
                {
                    EmployeeId = (int)pkEmp0.Content,
                    StartDate = DateTime.Now.AddDays(1)
                });
                _tshPrimaryKeyPool.Add(pk1.Content);
                OkNegotiatedContentResult<decimal?> pk2 = (OkNegotiatedContentResult<decimal?>)_tshController.Insert(new TimesheetsModel()
                {
                    EmployeeId = (int)pkEmp0.Content,
                    StartDate = DateTime.Now.AddDays(2)
                });
                _tshPrimaryKeyPool.Add(pk2.Content);
                OkNegotiatedContentResult<decimal?> pk3 = (OkNegotiatedContentResult<decimal?>)_tshController.Insert(new TimesheetsModel()
                {
                    EmployeeId = (int)pkEmp0.Content,
                    StartDate = DateTime.Now.AddDays(3)
                });
                _tshPrimaryKeyPool.Add(pk3.Content);
                OkNegotiatedContentResult<decimal?> pk4 = (OkNegotiatedContentResult<decimal?>)_tshController.Insert(new TimesheetsModel()
                {
                    EmployeeId = (int)pkEmp0.Content,
                    StartDate = DateTime.Now.AddDays(4)
                });
                _tshPrimaryKeyPool.Add(pk4.Content);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _tshController.DeleteTestData();

            _empController.DeleteTestData();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 0
        /// </remarks>
        [TestMethod]
        public void DeleteByIdTest()
        {
            var response = _tshController.GetList();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<TimesheetsModel>>));
            OkNegotiatedContentResult<IEnumerable<TimesheetsModel>> results = (OkNegotiatedContentResult<IEnumerable<TimesheetsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<TimesheetsModel>));
            IEnumerable<TimesheetsModel> content = (IEnumerable<TimesheetsModel>)results.Content;

            var initContentCount = content.Count();

            _tshController.DeleteById((int)_tshPrimaryKeyPool[0]);

            response = _tshController.GetList();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<TimesheetsModel>>));
            results = (OkNegotiatedContentResult<IEnumerable<TimesheetsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<TimesheetsModel>));
            content = (IEnumerable<TimesheetsModel>)results.Content;

            var currentContentCount = content.Count();

            Assert.AreEqual(initContentCount - 1, currentContentCount);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetListTest()
        {
            var response = _tshController.GetList();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<TimesheetsModel>>));
            OkNegotiatedContentResult<IEnumerable<TimesheetsModel>> results = (OkNegotiatedContentResult<IEnumerable<TimesheetsModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<TimesheetsModel>));
            IEnumerable<TimesheetsModel> content = (IEnumerable<TimesheetsModel>)results.Content;

            Assert.IsTrue(content.Count() > 0);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void ReportByDateTest()
        {
            var response = _tshController.ReportByDate(null);
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<TimesheetsReportModel>>));
            OkNegotiatedContentResult<IEnumerable<TimesheetsReportModel>> results = (OkNegotiatedContentResult<IEnumerable<TimesheetsReportModel>>)response;
            Assert.IsNotNull(results.Content);
            Assert.IsInstanceOfType(results.Content, typeof(IEnumerable<TimesheetsReportModel>));
            IEnumerable<TimesheetsReportModel> content = (IEnumerable<TimesheetsReportModel>)results.Content;

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
            var response = _tshController.GetById((int)_tshPrimaryKeyPool[1]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<TimesheetsModel>));
            OkNegotiatedContentResult<TimesheetsModel> result = (OkNegotiatedContentResult<TimesheetsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(TimesheetsModel));
            TimesheetsModel content = (TimesheetsModel)result.Content;

            Assert.AreEqual(content.Id, _tshPrimaryKeyPool[1]);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Generates _primaryKeyPool index 5
        /// </remarks>
        [TestMethod]
        public void InsertTest()
        {
            OkNegotiatedContentResult<decimal?> pk5 = (OkNegotiatedContentResult<decimal?>)_tshController.Insert(new TimesheetsModel()
            {
                EmployeeId = (int)_empPrimaryKeyPool[0],
                StartDate = DateTime.Now.AddDays(5),
            });
            _tshPrimaryKeyPool.Add(pk5.Content);

            var response = _tshController.GetById((int)_tshPrimaryKeyPool[4]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<TimesheetsModel>));
            OkNegotiatedContentResult<TimesheetsModel> result = (OkNegotiatedContentResult<TimesheetsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(TimesheetsModel));
            TimesheetsModel content = (TimesheetsModel)result.Content;

            Assert.AreEqual(content.Id, _tshPrimaryKeyPool[4]);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 2
        /// </remarks>
        [TestMethod]
        public void UpdateByIdTest()
        {
            var response = _tshController.GetById((int)_tshPrimaryKeyPool[2]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<TimesheetsModel>));
            OkNegotiatedContentResult<TimesheetsModel> result = (OkNegotiatedContentResult<TimesheetsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(TimesheetsModel));
            TimesheetsModel content = (TimesheetsModel)result.Content;

            Assert.AreEqual(content.Id, _tshPrimaryKeyPool[2]);

            DateTime? expectedEndDate = ((DateTime)content.StartDate).Add(new TimeSpan(1, 0, 0));

            content.EndDate = expectedEndDate;

            _tshController.UpdateById(content);

            response = _tshController.GetById((int)_tshPrimaryKeyPool[2]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<TimesheetsModel>));
            result = (OkNegotiatedContentResult<TimesheetsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(TimesheetsModel));
            content = (TimesheetsModel)result.Content;

            Assert.AreEqual(content.EndDate, expectedEndDate);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 4 for conflict
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(SqlException), "Timesheet already exists for {} and date {} .")]
        public void InsertSameDayTimesheetTest()
        {
            OkNegotiatedContentResult<decimal?> pk = (OkNegotiatedContentResult<decimal?>)_tshController.Insert(new TimesheetsModel()
            {
                EmployeeId = (int)_empPrimaryKeyPool[0],
                StartDate = DateTime.Now.AddDays(4),
            });
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(SqlException), "Timesheet saved start date {} does not match new start date {} .'")]
        public void UpdateStartDateNotMatchOriginalStartDateTest()
        {
            var response = _tshController.GetById((int)_tshPrimaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<TimesheetsModel>));
            OkNegotiatedContentResult<TimesheetsModel> result = (OkNegotiatedContentResult<TimesheetsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(TimesheetsModel));
            TimesheetsModel content = (TimesheetsModel)result.Content;

            Assert.AreEqual(content.Id, _tshPrimaryKeyPool[3]);

            content.StartDate = ((DateTime)content.StartDate).AddDays(1);

            _tshController.UpdateById(content);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(SqlException), "Timesheet end date {} does not match start date {} .")]
        public void UpdateEndDateNotMatchStartDateTest()
        {
            var response = _tshController.GetById((int)_tshPrimaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<TimesheetsModel>));
            OkNegotiatedContentResult<TimesheetsModel> result = (OkNegotiatedContentResult<TimesheetsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(TimesheetsModel));
            TimesheetsModel content = (TimesheetsModel)result.Content;

            Assert.AreEqual(content.Id, _tshPrimaryKeyPool[3]);

            content.EndDate = ((DateTime)content.StartDate).AddDays(1);

            _tshController.UpdateById(content);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Uses _primaryKeyPool index 3
        /// </remarks>
        [TestMethod]
        public void UpdateEndDateTest()
        {
            var response = _tshController.GetById((int)_tshPrimaryKeyPool[3]);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<TimesheetsModel>));
            OkNegotiatedContentResult<TimesheetsModel> result = (OkNegotiatedContentResult<TimesheetsModel>)response;
            Assert.IsNotNull(result.Content);
            Assert.IsInstanceOfType(result.Content, typeof(TimesheetsModel));
            TimesheetsModel content = (TimesheetsModel)result.Content;

            Assert.AreEqual(content.Id, _tshPrimaryKeyPool[3]);

            content.EndDate = ((DateTime)content.StartDate).AddHours(1.1);

            _tshController.UpdateById(content);
        }

    }
}
