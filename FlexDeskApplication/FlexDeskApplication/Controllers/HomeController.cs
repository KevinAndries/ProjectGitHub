﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using BusinessLogicLayer;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.AspNetCore.Http;
using MVC.Models;

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
            try
            {
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
            }
            catch (Exception)
            {
                HttpContext.Session.SetInt32("admin", 0);
                HttpContext.Session.SetInt32("language", 0);
                ViewData["sessionData"] = new int?[] { 0, 0 };
            }
            if (HttpContext.Session.GetInt32("userId")>0)
            {
                return RedirectToAction("Index", "Reservation");
            }
            else
            {
                return View(new LoginViewModel {Dictionary= new Dictionary(HttpContext.Session.GetInt32("language")) });
            }
            
        }

        public IActionResult Login(LoginViewModel lvm)
        {
            HttpContext.Session.SetInt32("admin", 0);
            try
            {
                User user = userBll.ShowAllUsers().First(u => u.Login == lvm.UserLogin);

                if (user != null && user.Password == lvm.Password)
                {
                    HttpContext.Session.SetInt32("userId", (int)user.UserId);
                    if (user.Administrator > 0)
                    {
                        HttpContext.Session.SetInt32("admin", 1);
                    }
                    return RedirectToAction("Index", "Reservation", new ReservationViewModel());
                }
                else
                {
                    TempData["Message"] = "Login Mislukt";
                    return RedirectToAction("Index", "Home", user);
                }

            }
            catch (Exception)
            {
                TempData["Message"] = "Login Mislukt";
                return RedirectToAction("Index", "Home");
            }
        }
        
        public IActionResult Language()
        {
            //0 = NL, 1 = FR
            if (HttpContext.Session.GetInt32("language")==0 || HttpContext.Session.GetInt32("language")==null )
            {
                HttpContext.Session.SetInt32("language", 1);
            }
            else
            {
                HttpContext.Session.SetInt32("language", 0);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetInt32("userId", 0);
            HttpContext.Session.SetInt32("admin", 0);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
