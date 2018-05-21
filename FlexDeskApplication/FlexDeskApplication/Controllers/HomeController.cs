using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using BusinessLogicLayer;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.AspNetCore.Http;

namespace FlexDeskApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserBll userBll;

        public HomeController( IUserBll userBll )
        {
            this.userBll = userBll;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(LoginViewModel lvm)
        {
            User user = userBll.ShowAllUsers().First(u => u.Login == lvm.UserLogin);

            if (user != null && user.Password == lvm.Password)
            {
                HttpContext.Session.SetInt32("userId", (int)user.UserId);
                if (user.Administrator>0)
                {
                    HttpContext.Session.SetInt32("admin", 1);
                }
                return RedirectToAction("Index", "Reservation", new ReservationViewModel());
            }
            else
            {
                TempData["Message"] = "Login Mislukt";
                return RedirectToAction("Index", "Home", user);
                //return View();
            }
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
