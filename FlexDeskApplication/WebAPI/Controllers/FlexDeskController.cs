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

        //Ophalen BusinessLogica die doorgegeven wordt aan de WebApi Controller Klasse om een dependency te creëeren 
        private readonly IFlexDeskBll flexDeskBll;

        public FlexDeskController(IFlexDeskBll flexDeskBll)
        {
            this.flexDeskBll = flexDeskBll;
        }

        //Hieronder wordt de routering bepaalt door bepaalde action methods toe te wijzen. 
        //Deze zullen dan de requesten die binnenkomen behandelen en de juiste routering parameters meegeven (=attributeRouting)

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
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        public void Post([FromBody]FlexDesk flexdesk)
        {
            flexDeskBll.CreateFlexDesk(flexdesk);
        }

        // PUT api/Flexdesk/5
        [HttpPut("{id}")]
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
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