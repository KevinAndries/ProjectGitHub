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


        //Ophalen BusinessLogica die doorgegeven wordt aan de WebApi Controller Klasse om een dependency te creëeren
        private readonly IUserBll userBll;

        public UserController( IUserBll userBll)
        {
            this.userBll = userBll;
        }


        //Hieronder wordt de routering bepaalt door bepaalde action methods toe te wijzen. 
        //Deze zullen dan de requesten die binnenkomen behandelen en de juiste routering parameters meegeven (=attributeRouting)

        // GET api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userBll.ShowAllUsers();
        }

        // GET api/User/5
        [HttpGet("{id}", Name = "UserGet")]
        public User Get(long id)
        {

            return userBll.GetUserById(id);
        }

        // POST api/User
        [HttpPost]
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        public void Post([FromBody]User user)
        {
            userBll.CreateUser(user);

        }

        // PUT api/User/5
        [HttpPut("{id}")]
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        public void Put(long id, [FromBody]User user)
        {
            user.UserId = id;
            userBll.UpdateUser(id, user);
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            userBll.DeleteUser(id);
        }
    }
}