using System.Collections.Generic;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    
    [Route("api/FlexDesks")]
    public class FlexDeskController : Controller
    {

        private readonly IFlexDeskBll flexDeskBll;

        public FlexDeskController(IFlexDeskBll flexDeskBll)
        {
            this.flexDeskBll = flexDeskBll;
        }


        // GET api/FlexDesk
        [HttpGet]
        public IEnumerable<FlexDesk> Get()
        {
            return flexDeskBll.ShowAllFlexdesks();
 
        }

        // GET api/FlexDesk/5
        [HttpGet("{id}", Name = "FlexDeskGet")]
        public FlexDesk Get(long id)
        {
            return flexDeskBll.GetFlexDeskById(id);
        }

        // POST api/Flexdesk
        [HttpPost]
        public void Post([FromBody]FlexDesk flexdesk)
        {
            flexDeskBll.CreateFlexDesk(flexdesk);
        }

        // PUT api/Flexdesk/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]FlexDesk flexdesk)
        {
            flexdesk.FlexDeskId = id;
            flexDeskBll.UpdateFlexDesk(id, flexdesk);           
        }

        // DELETE api/Flexdesks/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            flexDeskBll.DeleteFlexDesk(id);
        }
    }
}