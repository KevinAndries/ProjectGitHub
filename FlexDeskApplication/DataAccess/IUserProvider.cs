using System.Collections.Generic;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IUserProvider
    {
        IEnumerable<User> Get();

        //List<User> GetAll();
        //bool Add(User user);
        User GetById(long userId);
    }
}
