using TAL.Developer.Test.Domain.Services;
using System.Web.Http;
using TAL.Developer.Test.Domain.Models;

namespace TAL.Developer.Test.Domain.Controllers
{
    public class TimezonesController : ApiController
    {
        private readonly ITimezonesService _timezonesService;

        public TimezonesController(ITimezonesService timezonesService)
        {
            _timezonesService = timezonesService;
        }

        [HttpDelete, Route("api/timezones/deletebyid/{id:int}")]
        public IHttpActionResult DeleteById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _timezonesService.DeleteById(id);

            return Ok(id);
        }

        [HttpDelete, Route("api/timezones/deletetestdata")]
        public IHttpActionResult DeleteTestData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _timezonesService.DeleteTestData();

            return Ok();
        }

        [HttpGet, Route("api/timezones/getlist")]
        public IHttpActionResult GetList()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _timezonesService.GetList();

            return Ok(result);
        }

        [HttpGet, Route("api/timezones/getbyid/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _timezonesService.GetById(id);

            return Ok(result);
        }

        [HttpPost, Route("api/timezones/insert")]
        public IHttpActionResult Insert([FromBody]TimezonesModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _timezonesService.Insert(model);

            return Ok(result);
        }

        [HttpPost, Route("api/timezones/updatebyid")]
        public IHttpActionResult UpdateById(TimezonesModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _timezonesService.UpdateById(model);

            return Ok(true);
        }

    }
}
