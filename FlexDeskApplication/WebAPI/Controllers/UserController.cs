using System.Collections.Generic;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/User")]
    public class UserController : Controller
    {


        //private readonly IUserProvider userProvider;
        //private readonly IUserProcessor userProcessor;
        //private readonly IUserBll userBll;

        //public UserController(IUserProvider userProvider, IUserProcessor userProcessor, IUserBll userBll)
        //{
        //    this.userProvider = userProvider;
        //    this.userProcessor = userProcessor;
        //    this.userBll = userBll;
        //}


        private readonly IUserBll userBll;

        public UserController( IUserBll userBll)
        {

            this.userBll = userBll;
        }




        // GET api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            //return userProvider.Get();
            return userBll.ShowAllUsers();
        }

        // GET api/User/5
        [HttpGet("{id}", Name = "UserGet")]
        public User Get(long id)
        {
            //return userProvider.GetById(id);
            return userBll.GetUserById(id);
        }

        // POST api/User
        [HttpPost]
        public void Post([FromBody]User user)
        {
            userBll.CreateUser(user);
            //userProcessor.Create(user);
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]User user)
        {
            user.UserId = id;
            userBll.UpdateUser(id, user);
            //userProcessor.Update(user);
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            userBll.DeleteUser(id);
            //userProcessor.Delete(id);
        }
    }
}