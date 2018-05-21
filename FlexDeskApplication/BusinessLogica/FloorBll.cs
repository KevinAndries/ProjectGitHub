using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;

namespace BusinessLogicLayer
{
    public class FloorBll : IFloorBll
    {
   

        private readonly IFloorProvider floorProvider;
        private readonly IFloorProcessor floorProcessor;

        public FloorBll(IFloorProvider floorProvider, IFloorProcessor floorProcessor)
        {
            this.floorProvider = floorProvider;
            this.floorProcessor = floorProcessor;
        }




        public IEnumerable<Floor> ShowAllFloors()
        {

            var floors = floorProvider.Get();
            
            return floors;
        }


        public Floor GetFloorById(long id)
        {
            var floor = floorProvider.GetById(id);
            return floor;
        }

        public Floor GetFlexDesksById(long deskid)
        {
            var flexdesk = floorProvider.GetFlexDeskById(deskid);
            return flexdesk;
            //var lstFlexDesks = new List<FlexDesk>();
            //lstFlexDesks = (List<(FlexDesk>)floorProvider.GetFlexDeskById(deskid);
            //lstFlexDesks.Add(flexdesk);

        }

        public void CreateFloor(Floor floor)
        {
            floorProcessor.Create(floor);

            var lstFloor = new List<Floor>();
            lstFloor = (List<Floor>)floorProvider.Get();
            lstFloor.Add(floor);
        }

        public void UpdateFloor(long id, Floor floor)
        {
            floorProcessor.Update(floor);
        }

        public void DeleteFloor(long id)
        {
            floorProcessor.Delete(id);
        }


    }
}
