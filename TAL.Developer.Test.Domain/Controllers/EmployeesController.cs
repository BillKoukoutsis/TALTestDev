using TAL.Developer.Test.Domain.Services;
using System.Web.Http;
using TAL.Developer.Test.Domain.Models;

namespace TAL.Developer.Test.Domain.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpDelete, Route("api/employees/deletebyid/{id:int}")]
        public IHttpActionResult DeleteById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _employeesService.DeleteById(id);

            return Ok(id);
        }

        [HttpDelete, Route("api/employees/deletetestdata")]
        public IHttpActionResult DeleteTestData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _employeesService.DeleteTestData();

            return Ok();
        }

        [HttpGet, Route("api/employees/getlist")]
        public IHttpActionResult GetList()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _employeesService.GetList();

            return Ok(result);
        }

        [HttpGet, Route("api/employees/getbyid/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _employeesService.GetById(id);

            return Ok(result);
        }

        [HttpPost, Route("api/employees/getbycredentials")]
        public IHttpActionResult GetByCredentials([FromBody] CredentialsModel credentials)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _employeesService.GetByCredentials(credentials);

            return Ok(result);
        }

        [HttpPost, Route("api/employees/insert")]
        public IHttpActionResult Insert([FromBody]EmployeesModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _employeesService.Insert(model);

            return Ok(result);
        }

        [HttpPost, Route("api/employees/updatebyid")]
        public IHttpActionResult UpdateById(EmployeesModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _employeesService.UpdateById(model);

            return Ok(true);
        }

    }
}
