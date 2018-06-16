using System.Collections.Generic;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/Floor")]
    public class FloorController : Controller
    {

        //Ophalen BusinessLogica die doorgegeven wordt aan de WebApi Controller Klasse om een dependency te creëeren 
        private readonly IFloorBll floorBll;

        public FloorController(IFloorBll floorBll)
        {

            this.floorBll = floorBll;
        }

        //Hieronder wordt de routering bepaalt door bepaalde action methods toe te wijzen. 
        //Deze zullen dan de requesten die binnenkomen behandelen en de juiste routering parameters meegeven (=attributeRouting)

        // GET api/Floor
        [HttpGet]
        public IEnumerable<Floor> Get()
        {
            //return floorProvider.Get();
            return floorBll.ShowAllFloors();
    
        }

        // GET api/Floor/5
        [HttpGet("{id}", Name = "FloorGet")]
        public Floor Get(long id)
        {
            return floorBll.GetFloorById(id);
        }

        //Methode voor alle floors op te halen

        // POST api/Floor
        [HttpPost]
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        public void Post([FromBody]Floor floor)
        {
            floorBll.CreateFloor(floor);
        }

        // PUT api/Floor/5
        [HttpPut("{id}")]
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        public void Put(long id, [FromBody]Floor floor)
        {
            floor.FloorId = id;
            floorBll.UpdateFloor(id, floor);
        }

        // DELETE api/Reservaties/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            floorBll.DeleteFloor(id);
            //floorProcessor.Delete(id);
        }
    }
}