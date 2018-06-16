using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer;
using DataAccessLayer.Model.FlexDeskDb;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserBll userBll;
        private User activeUser;

        public UserController(IUserBll userBll)
        {
            this.userBll = userBll;
        }
        // GET: User
        public ActionResult Index(long departmentId, long floorId, long buildingId)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};

            if (departmentId == 0)
            {
                return View(new UserViewModel { Users = userBll.ShowAllUsers() });
            }
            else
            {
                return View(new UserViewModel { Users = userBll.ShowAllUsers().Where(f => f.DepartmentId == departmentId), BuildingId = buildingId, DepartmentId = departmentId, FloorId = floorId });
            }
        }

        //Reset password
        public ActionResult ResetPassword(long id)
        {
            try
            {
                User user = userBll.GetUserById(id);
                user.Password = "FlexDesk";
                userBll.UpdateUser(id, user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }            
        }

        // GET: User/Details/5
        public ActionResult Details(long id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
            return View(userBll.GetUserById(id));
        }

        // GET: User/Create
        public ActionResult Create(long departmentId)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
            return View(new User { DepartmentId = departmentId });
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            userBll.CreateUser(user);
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                
                if (activeUser.Administrator > 0)
                {
                    //userBll.CreateUser(user);
                }                

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
            return View(userBll.GetUserById(id));
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, User user)
        {
            user.Password = userBll.GetUserById(id).Password;
            //userBll.UpdateUser(id, user);
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                
                if (activeUser.Administrator > 0)
                {
                    user.UserId = id;
                    user.Password = userBll.GetUserById(id).Password;
                    userBll.UpdateUser(id, user);
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(long id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
            return View(userBll.GetUserById(id));
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, User user)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                
                if (activeUser.Administrator > 0)
                {
                    userBll.DeleteUser(id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
                return View();
            }
        }
    }
}