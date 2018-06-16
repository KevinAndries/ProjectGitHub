using System.Collections.Generic;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;


namespace WebAPI.Controllers
{
   
    [Route("api/Building")]
    public class BuildingController : Controller
    {

        //Ophalen BusinessLogica die doorgegeven wordt aan de WebApi Controller Klasse om een dependency te creëeren 
        private readonly IBuildingBll buildingBll;

        public BuildingController(IBuildingBll buildingBll)
        {

            this.buildingBll = buildingBll;
        }


        //Hieronder wordt de routering bepaalt door bepaalde action methods toe te wijzen. 
        //Deze zullen dan de requesten die binnenkomen behandelen en de juiste routering parameters meegeven (=attributeRouting)

        // GET api/Building
        [HttpGet]
        public IEnumerable<Building> Get()
        {
            return buildingBll.ShowAllBuildings();
        }

        // GET api/Building/5
        [HttpGet("{id}", Name = "BuildingGet")]
        public Building Get(long id)
        {

            return buildingBll.GetBuildingById(id);
            
            
        }

        // POST api/Building
        [HttpPost]
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        public void Post([FromBody]Building building)
        {
            buildingBll.CreateBuilding(building);
        }



        // PUT api/Building/5
        [HttpPut("{id}")]
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        public void Put(long id, [FromBody]Building building)
        {
            building.BuildingId = id;
            buildingBll.UpdateBuilding(id, building);
        }

        // DELETE api/Building/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            buildingBll.DeleteBuilding(id);
        }

    }
}