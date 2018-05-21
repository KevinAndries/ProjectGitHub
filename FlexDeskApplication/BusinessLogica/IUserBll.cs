using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public interface IUserBll
    {
        IEnumerable<User> ShowAllUsers();
        User GetUserById(long id);
        void CreateUser(User user);
        void UpdateUser(long id, User user);
        void DeleteUser(long id);
    }
}
