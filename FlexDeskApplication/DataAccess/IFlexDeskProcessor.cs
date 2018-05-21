using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IFlexDeskProcessor
    {
       void Create(FlexDesk flexdesk);

        void Update(FlexDesk flexdesk);

        void Delete(long flexdeskId);
    }
}
