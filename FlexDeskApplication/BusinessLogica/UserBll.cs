using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;

namespace BusinessLogicLayer
{
    public class UserBll : IUserBll
    {
        private readonly IUserProvider userProvider;
        private readonly IUserProcessor userProcessor;

        public UserBll(IUserProvider userProvider, IUserProcessor userProcessor)
        {
            this.userProvider = userProvider;
            this.userProcessor = userProcessor;
        }




        public IEnumerable<User> ShowAllUsers()
        {

            var users = userProvider.Get();
            return users;
        }

        public User GetUserById(long id)
        {
            var user = userProvider.GetById(id);
            return user;
        }

        public void CreateUser(User user)
        {
            {
                userProcessor.Create(user);

                var lstUser = new List<User>();
                lstUser = (List<User>)userProvider.Get();
                lstUser.Add(user);
            }
        }


        public void UpdateUser(long id, User user)
        {
            userProcessor.Update(user);
        }

        public void DeleteUser(long id)
        {
            userProcessor.Delete(id);
        }



    }
}
