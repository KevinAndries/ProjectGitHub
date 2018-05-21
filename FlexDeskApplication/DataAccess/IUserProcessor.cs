using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IUserProcessor
    {
       void Create(User user);

        void Update(User user);

        void Delete(long userId);
    }
}
