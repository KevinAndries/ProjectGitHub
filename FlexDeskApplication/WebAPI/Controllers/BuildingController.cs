﻿using System.Collections.Generic;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;


namespace WebAPI.Controllers
{
   
    [Route("api/Building")]
    public class BuildingController : Controller
    {


        private readonly IBuildingBll buildingBll;

        public BuildingController(IBuildingBll buildingBll)
        {

            this.buildingBll = buildingBll;
        }


        // <summary>
        // Vraag hier een overzicht met alle gebouwen.
        // </summary>
        // <returns>Lijst met alle gebouwen</returns>
        // GET api/Building
        [HttpGet]
        public IEnumerable<Building> Get()
        {
            //return buildingProvider.Get();

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
        public void Post([FromBody]Building building)
        {

            buildingBll.CreateBuilding(building);
        }



        // PUT api/Building/5
        [HttpPut("{id}")]
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