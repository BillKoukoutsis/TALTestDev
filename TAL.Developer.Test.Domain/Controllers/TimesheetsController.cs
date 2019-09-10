using TAL.Developer.Test.Domain.Services;
using System.Web.Http;
using TAL.Developer.Test.Domain.Models;
using System;

namespace TAL.Developer.Test.Domain.Controllers
{
    public class TimesheetsController : ApiController
    {
        private readonly ITimesheetsService _timesheetsService;

        public TimesheetsController(ITimesheetsService timesheetsService)
        {
            _timesheetsService = timesheetsService;
        }

        [HttpDelete, Route("api/timesheets/deletebyid/{id:int}")]
        public IHttpActionResult DeleteById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _timesheetsService.DeleteById(id);

            return Ok(id);
        }

        [HttpDelete, Route("api/timesheets/deletetestdata")]
        public IHttpActionResult DeleteTestData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _timesheetsService.DeleteTestData();

            return Ok();
        }

        [HttpGet, Route("api/timesheets/getlist")]
        public IHttpActionResult GetList()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _timesheetsService.GetList();

            return Ok(result);
        }

        [HttpPost, Route("api/timesheets/reportbydate")]
        public IHttpActionResult ReportByDate([FromBody] DateTime? reportDate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _timesheetsService.ReportByDate(reportDate);

            return Ok(result);
        }

        [HttpGet, Route("api/timesheets/getbyid/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _timesheetsService.GetById(id);

            return Ok(result);
        }

        [HttpPost, Route("api/timesheets/insert")]
        public IHttpActionResult Insert([FromBody]TimesheetsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _timesheetsService.Insert(model);

            return Ok(result);
        }

        [HttpPost, Route("api/timesheets/updatebyid")]
        public IHttpActionResult UpdateById(TimesheetsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _timesheetsService.UpdateById(model);

            return Ok(true);
        }

    }
}
