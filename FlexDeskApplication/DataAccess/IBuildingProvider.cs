using System.Collections.Generic;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IBuildingProvider
    {
        IEnumerable<Building> Get();

        //List<Building> GetAll();
        //bool Add(Building building);
        Building GetById(long buildingId);
        
    }

}
