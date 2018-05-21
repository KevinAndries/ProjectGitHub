using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using BusinessLogicLayer.CustomValidations;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;


namespace BusinessLogicLayer
{


    public class BuildingBll : IBuildingBll
    {
        private readonly IBuildingProvider buildingProvider;
        private readonly IBuildingProcessor buildingProcessor;


        public BuildingBll(IBuildingProvider buildingProvider, IBuildingProcessor buildingProcessor)
        {
            this.buildingProvider = buildingProvider;
            this.buildingProcessor = buildingProcessor;
        }





        public IEnumerable<Building> ShowAllBuildings()
        {
            
            var buildings = buildingProvider.Get();

            return buildings;
        }


        public Building GetBuildingById(long id)
        {
            var building = buildingProvider.GetById(id);
            return building;
        }

        public void CreateBuilding(Building building)

        {
            buildingProcessor.Create(building);

            var lstBuilding = new List<Building>();
            lstBuilding = (List<Building>) buildingProvider.Get();
            lstBuilding.Add(building);

        }


        public void UpdateBuilding(long id, Building building)
        {

            buildingProcessor.Update(building);
        }

        public void DeleteBuilding(long id)
        {
            buildingProcessor.Delete(id);
        }
    }
}




//if (bCode.HasDuplicates())
//{
//    throw new Exception("Er zitten duplicaten in de lijst");
//}


//if (bName.HasDuplicates())
//{
//    throw new Exception("Er zitten duplicaten in de lijst");
//}
