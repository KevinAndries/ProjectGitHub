using System.Collections.Generic;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IFloorProvider
    {
        IEnumerable<Floor> Get();

        //List<Floor> GetAll();
        //bool Add(Floor floor);
        Floor GetById(long floorId);
        Floor GetFlexDeskById(long deskid);
    }
}
