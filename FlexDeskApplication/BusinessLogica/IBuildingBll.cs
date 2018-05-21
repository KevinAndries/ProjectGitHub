using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public interface IBuildingBll
    {
        IEnumerable<Building> ShowAllBuildings();
        Building GetBuildingById(long id);
        void CreateBuilding( Building building);
        void UpdateBuilding(long id, Building building);
        void DeleteBuilding(long id);
    }
}
