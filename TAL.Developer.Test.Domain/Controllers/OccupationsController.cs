using TAL.Developer.Test.Domain.Services;
using System.Web.Http;
using TAL.Developer.Test.Domain.Models;

namespace TAL.Developer.Test.Domain.Controllers
{
    public class OccupationsController : ApiController
    {
        private readonly IOccupationsService _occupationsService;

        public OccupationsController(IOccupationsService occupationsService)
        {
            _occupationsService = occupationsService;
        }

        [HttpDelete, Route("api/occupations/deletebyid/{id:int}")]
        public IHttpActionResult DeleteById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _occupationsService.DeleteById(id);

            return Ok(id);
        }

        [HttpDelete, Route("api/occupations/deletetestdata")]
        public IHttpActionResult DeleteTestData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _occupationsService.DeleteTestData();

            return Ok();
        }

        [HttpGet, Route("api/occupations/getlist")]
        public IHttpActionResult GetList()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _occupationsService.GetList();

            return Ok(result);
        }

        [HttpGet, Route("api/occupations/getbyid/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _occupationsService.GetById(id);

            return Ok(result);
        }

        [HttpPost, Route("api/occupations/insert")]
        public IHttpActionResult Insert([FromBody]OccupationsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _occupationsService.Insert(model);

            return Ok(result);
        }

        [HttpPost, Route("api/occupations/updatebyid")]
        public IHttpActionResult UpdateById(OccupationsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _occupationsService.UpdateById(model);

            return Ok(true);
        }

    }
}
