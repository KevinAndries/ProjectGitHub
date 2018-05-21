using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IBuildingProcessor
    {
       void Create(Building building);

        void Update(Building building);

        void Delete(long buildingId);

    }
}
