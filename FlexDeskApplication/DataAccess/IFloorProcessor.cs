using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IFloorProcessor
    {
       void Create(Floor floor);

        void Update(Floor floor);

        void Delete(long floorId);
    }
}
