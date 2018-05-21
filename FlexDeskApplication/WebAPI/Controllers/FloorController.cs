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

        //private readonly IFloorProvider floorProvider;
        //private readonly IFloorProcessor floorProcessor;
        //private readonly IFloorBll floorBll;

        //public FloorController(IFloorProvider floorProvider, IFloorProcessor floorProcessor, IFloorBll floorBll)
        //{
        //    this.floorProvider = floorProvider;
        //    this.floorProcessor = floorProcessor;
        //    this.floorBll = floorBll;
        //}

        private readonly IFloorBll floorBll;

        public FloorController(IFloorBll floorBll)
        {

            this.floorBll = floorBll;
        }


        // <summary>
        // Vraag hier een overzicht met alle verdiepingen.
        // </summary>
        // <returns>Lijst met alle verdiepingen</returns>
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
            //return floorProvider.GetById(id);
            return floorBll.GetFloorById(id);
        }

        // GET api/Floor/5
        [HttpGet("{deskid}", Name = "DeskGet")]
        public Floor GetDesk(long deskid)
        {
            //return floorProvider.GetById(id);
            return floorBll.GetFlexDesksById(deskid);
        }


        //Methode voor alle floors op te halen

        // POST api/Floor
        [HttpPost]
        public void Post([FromBody]Floor floor)
        {
            //floorProcessor.Create(floor);
            floorBll.CreateFloor(floor);
        }

        // PUT api/Floor/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Floor floor)
        {
            floor.FloorId = id;
            floorBll.UpdateFloor(id, floor);
            //floorProcessor.Update(floor);
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