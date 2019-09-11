using TAL.Developer.Test.Domain.Services;
using System.Web.Http;
using TAL.Developer.Test.Domain.Models;

namespace TAL.Developer.Test.Domain.Controllers
{
    public class OccupationRatingsController : ApiController
    {
        private readonly IOccupationRatingsService _occupationRatingsService;

        public OccupationRatingsController(IOccupationRatingsService occupationRatingsService)
        {
            _occupationRatingsService = occupationRatingsService;
        }

        [HttpDelete, Route("api/occupationratings/deletebyid/{id:int}")]
        public IHttpActionResult DeleteById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _occupationRatingsService.DeleteById(id);

            return Ok(id);
        }

        [HttpDelete, Route("api/occupationratings/deletetestdata")]
        public IHttpActionResult DeleteTestData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _occupationRatingsService.DeleteTestData();

            return Ok();
        }

        [HttpGet, Route("api/occupationratings/getlist")]
        public IHttpActionResult GetList()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _occupationRatingsService.GetList();

            return Ok(result);
        }

        [HttpGet, Route("api/occupationratings/getbyid/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _occupationRatingsService.GetById(id);

            return Ok(result);
        }

        [HttpPost, Route("api/occupationratings/insert")]
        public IHttpActionResult Insert([FromBody]OccupationRatingsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _occupationRatingsService.Insert(model);

            return Ok(result);
        }

        [HttpPost, Route("api/occupationratings/updatebyid")]
        public IHttpActionResult UpdateById(OccupationRatingsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _occupationRatingsService.UpdateById(model);

            return Ok(true);
        }

    }
}
