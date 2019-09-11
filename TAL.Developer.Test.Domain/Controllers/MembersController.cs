using TAL.Developer.Test.Domain.Services;
using System.Web.Http;
using TAL.Developer.Test.Domain.Models;

namespace TAL.Developer.Test.Domain.Controllers
{
    public class MembersController : ApiController
    {
        private readonly IMembersService _membersService;

        public MembersController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        [HttpDelete, Route("api/members/deletebyid/{id:int}")]
        public IHttpActionResult DeleteById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _membersService.DeleteById(id);

            return Ok(id);
        }

        [HttpDelete, Route("api/members/deletetestdata")]
        public IHttpActionResult DeleteTestData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _membersService.DeleteTestData();

            return Ok();
        }

        [HttpGet, Route("api/members/getlist")]
        public IHttpActionResult GetList()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _membersService.GetList();

            return Ok(result);
        }

        [HttpGet, Route("api/members/getbyid/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _membersService.GetById(id);

            return Ok(result);
        }

        [HttpPost, Route("api/members/insert")]
        public IHttpActionResult Insert([FromBody]MembersModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _membersService.Insert(model);

            return Ok(result);
        }

        [HttpPost, Route("api/members/updatebyid")]
        public IHttpActionResult UpdateById(MembersModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _membersService.UpdateById(model);

            return Ok(true);
        }

    }
}
