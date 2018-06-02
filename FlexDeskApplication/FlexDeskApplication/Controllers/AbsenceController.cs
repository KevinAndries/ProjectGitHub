using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Model.FlexDeskDb;
using BusinessLogicLayer;
using MVC.ViewModels;
using MVC.Models;

namespace MVC.Controllers
{
    public class AbsenceController : Controller
    {
        private readonly IAbsenceBll absenceBll;
        private readonly IUserBll userBll;
        private readonly IReservationBll reservationBll;
        private User activeUser;

        public AbsenceController(IAbsenceBll absenceBll, IReservationBll reservationBll, IUserBll userBll)
        {
            this.absenceBll = absenceBll;
            this.reservationBll = reservationBll;
            this.userBll = userBll;
        }

        // GET: Absence
        public ActionResult Index(AbsenceViewModel avm)
        {
            try
            {
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
                avm.ActiveUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                avm.Dictionary = new Dictionary(HttpContext.Session.GetInt32("language"));

                User user = new User();
                if (avm.UserCode != null && avm.UserCode != "")
                {
                    user = userBll.ShowAllUsers().FirstOrDefault(u => u.Login == avm.UserCode);
                }
                else
                {
                    user = avm.ActiveUser;
                    avm.UserCode = avm.ActiveUser.Login;
                }
                avm.User = user;
                avm.UserId = user.UserId;
                avm.Absences = new AbsenceFE().GetAbsensesFE(absenceBll.ShowAllAbsences().Where(a => a.UserId == avm.UserId && a.EndDate>=DateTime.Today));

                if (avm.ConflictReservatie)
                {
                    ViewData["message"] = avm.Dictionary.Label21;
                }

                return View(avm);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        public ActionResult Create(long id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
            AbsenceViewModel avm = new AbsenceViewModel
            {
                Dictionary = new Dictionary(HttpContext.Session.GetInt32("language")),
                UserId=id
            };
            
            return View(avm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAbsence(AbsenceViewModel avm)
        {
            try
            {

                if (HttpContext.Session.GetInt32("userId")>0)
                {

                    avm.Dictionary = new Dictionary(HttpContext.Session.GetInt32("language"));
                    if (reservationBll.ShowAllReservations().Where(r1 => r1.UserId==avm.UserId).FirstOrDefault(r=>(r.StartDate >= avm.StartDate && r.StartDate<=avm.EndDate)||(r.EndDate>=avm.StartDate&&r.EndDate <= avm.EndDate))==null)
                    {
                        Absence absence = new Absence
                        {
                            UserId = avm.UserId,
                            StartDate = avm.StartDate,
                            EndDate = avm.EndDate,
                            Description = avm.Description,
                            CreationDate = DateTime.Now,
                            Creator = HttpContext.Session.GetInt32("userId")
                        };

                        if (HttpContext.Session.GetInt32("admin") > 0 || avm.UserId == absence.Creator)
                        {
                            absenceBll.CreateAbsence(absence);
                        }
                    }
                    else
                    {
                        avm.ConflictReservatie = true;
                    }
                }             
                
                avm.Absences = new AbsenceFE().GetAbsensesFE(absenceBll.ShowAllAbsences().Where(a => a.UserId == avm.UserId && a.EndDate >= DateTime.Today));
                avm.UserCode = userBll.GetUserById(avm.UserId).Login;
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
                return RedirectToAction(nameof(Index), avm);
            }
            catch
            {
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
                return RedirectToAction(nameof(Index), avm);
            }
        }
        

        // GET: Absence/Delete/5
        public ActionResult Delete(long id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
            Absence a = absenceBll.GetAbsenceById(id);
            return View(new AbsenceViewModel {
                AbsenceId = a.AbsenceId,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Description= a.Description,
                UserId = a.UserId,
                Dictionary = new Dictionary(HttpContext.Session.GetInt32("language"))
            });
        }

        // POST: Absence/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, AbsenceViewModel absence)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                
                if (activeUser.Administrator > 0 || activeUser.UserId==absence.UserId )
                {
                    absenceBll.DeleteAbsence(id);
                }

                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}