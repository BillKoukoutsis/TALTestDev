using TAL.Developer.Test.Domain.Services;
using System.Web.Http;
using TAL.Developer.Test.Domain.Models;

namespace TAL.Developer.Test.Domain.Controllers
{
    public class GroupsController : ApiController
    {
        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        [HttpDelete, Route("api/groups/deletebyid/{id:int}")]
        public IHttpActionResult DeleteById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _groupsService.DeleteById(id);

            return Ok(id);
        }

        [HttpDelete, Route("api/groups/deletetestdata")]
        public IHttpActionResult DeleteTestData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _groupsService.DeleteTestData();

            return Ok();
        }

        [HttpGet, Route("api/groups/getlist")]
        public IHttpActionResult GetList()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _groupsService.GetList();

            return Ok(result);
        }

        [HttpGet, Route("api/groups/getbyid/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _groupsService.GetById(id);

            return Ok(result);
        }

        [HttpPost, Route("api/groups/insert")]
        public IHttpActionResult Insert([FromBody]GroupsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _groupsService.Insert(model);

            return Ok(result);
        }

        [HttpPost, Route("api/groups/updatebyid")]
        public IHttpActionResult UpdateById(GroupsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _groupsService.UpdateById(model);

            return Ok(true);
        }

    }
}
