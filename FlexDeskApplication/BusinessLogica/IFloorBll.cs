using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public interface IFloorBll
    {
        IEnumerable<Floor> ShowAllFloors();
        Floor GetFloorById(long id);
        void CreateFloor(Floor floor);
        void UpdateFloor(long id, Floor floor);
        void DeleteFloor(long id);
    }
}
