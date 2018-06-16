using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using DataAccessLayer.Model.FlexDeskDb;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using MVC.Models;

namespace MVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IAbsenceBll absenceBll;
        private readonly IReservationBll reservationBll;
        private readonly IUserBll userBll;
        private readonly IBuildingBll buildingBll;
        private readonly IFloorBll floorBll;
        private readonly IDepartmentBll departmentBll;
        private readonly IFlexDeskBll flexDeskBll;
        private User activeUser;
        private ReservationViewModel rvm;

        public ReservationController(IAbsenceBll absenceBll, IReservationBll reservationBll, IUserBll userBll, IFloorBll floorBll, IBuildingBll buildingBll, IDepartmentBll departmentBll, IFlexDeskBll flexDeskBll)
        {
            this.absenceBll = absenceBll;
            this.reservationBll = reservationBll;
            this.userBll = userBll;
            this.buildingBll = buildingBll;
            this.floorBll = floorBll;
            this.departmentBll = departmentBll;
            this.flexDeskBll = flexDeskBll;
            this.rvm = new ReservationViewModel();
        }


        public IActionResult Index(ReservationViewModel rvm)
        {

            try
            {
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
                this.rvm = rvm;
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                rvm.ActiveUser = activeUser;
                rvm.Dictionary = new Dictionary(HttpContext.Session.GetInt32("language"));
                if (rvm.UserCode!=null)
                {
                    rvm.User = userBll.ShowAllUsers().FirstOrDefault(u => u.Login == rvm.UserCode);
                    if (rvm.User == null)
                    {
                        ViewData["message2"] = "User not found";
                        rvm.User = activeUser;
                        rvm.UserCode = activeUser.Login;
                    }
                }
                else
                {
                    rvm.User = activeUser;
                    rvm.UserCode = activeUser.Login;
                }

                rvm.UserId = rvm.User.UserId;
                rvm.Start = DateTime.Today;
                rvm.End = DateTime.Today;
                IEnumerable<Reservation> reservations = reservationBll.ShowAllReservations().Where(r => r.UserId == rvm.User.UserId && r.EndDate >= DateTime.Today);
                rvm.Reservations = new ReservationFE().GetReservations(flexDeskBll, reservations);

                ViewData["message"] = rvm.Message;
                return View(rvm);

            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }

        }
        //show floor with desks
        public IActionResult FlexDesks(ReservationViewModel rvm)
        {    

            try
            {
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
                this.rvm = rvm;
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                rvm.ActiveUser = activeUser;
                rvm.Dictionary = new Dictionary(HttpContext.Session.GetInt32("language"));
                if (rvm.UserId==0)
                {
                    rvm.User = activeUser;
                }
                rvm.User = userBll.GetUserById(rvm.UserId);
                rvm.UserCode = rvm.User.Login;

                if (rvm.Start==null)
                {
                    rvm.Start = DateTime.Today;
                }
                if (rvm.End==null)
                {
                    rvm.End = rvm.Start;
                }
                                
                // Check if dates are ok
                string message = rvm.CheckDates(HttpContext.Session.GetInt32("language"));
                if (rvm.DatesOK==false)
                {
                    IEnumerable<Reservation> reservations = reservationBll.ShowAllReservations().Where(r => r.UserId == activeUser.UserId && r.EndDate >= DateTime.Today);
                    rvm.Reservations = new ReservationFE().GetReservations(flexDeskBll, reservations);
                    //exception: Back to List
                    rvm.Message = message;
                    return RedirectToAction("Index", rvm);
                }

                UpdateRvm(rvm);                             
                
                if (rvm.Reservations.Any(r => r.UserId == rvm.UserId))
                {
                    ReservationFE reservation = rvm.Reservations.First(r => r.UserId == rvm.UserId);
                    //Exception: User has already reservation in this period
                     return RedirectToAction("Delete", routeValues: new { id = reservation.ReservationId, message = new Dictionary(HttpContext.Session.GetInt32("language")).Label25 });
                }

                IEnumerable<Absence> afwezigheden = absenceBll.ShowAllAbsences().Where(a => a.UserId == rvm.UserId);

                if (afwezigheden.Any(a=> (a.StartDate>= rvm.Start && a.StartDate <=rvm.End) || (a.EndDate>= rvm.Start && a.EndDate <= rvm.End)) ||
                    afwezigheden.Any(a=> (rvm.Start >= a.StartDate && rvm.Start <= a.EndDate) || (rvm.End >= a.StartDate && rvm.End <= a.EndDate)))
                {
                    //exception: User has already absence in this period;
                    rvm.Message = rvm.Dictionary.Label29;
                    return RedirectToAction("Index", rvm);
                }
                return View(rvm);              

            }
            catch (Exception)
            {
                rvm.Floors = floorBll.ShowAllFloors().Where(floor => floor.BuildingId == rvm.Building.BuildingId).ToList();
                return RedirectToAction("Index", "Home");
            }

        }

        // add necessary data reservationviewmodel
        private void UpdateRvm(ReservationViewModel rvm)
        {
            if (rvm.Start == null)
            {
                rvm.Start = DateTime.Today;
                rvm.End = DateTime.Today;
            }
            if (rvm.User.Department==null)
            {
                rvm.User.Department = departmentBll.ShowAllDepartments().FirstOrDefault(d => d.DepartmentId == rvm.User.DepartmentId);
            }
            rvm.Building = buildingBll.GetBuildingById(floorBll.GetFloorById(departmentBll.GetDepartmentById(rvm.User.DepartmentId).FloorId).BuildingId);

            rvm.Floors = floorBll.ShowAllFloors().Where(floor => floor.BuildingId == rvm.Building.BuildingId).ToList();            
            foreach (var floor in rvm.Floors)
            {
                floor.Department = departmentBll.ShowAllDepartments().Where(d => d.FloorId == floor.FloorId).ToList();
                foreach (var d in floor.Department)
                {
                    d.FlexDesk = flexDeskBll.ShowAllFlexdesks().Where(fd => fd.DepartmentId == d.DepartmentId).ToList();
                }
                rvm.AddDeskIds(floor);
            }
            rvm.Reservations = new ReservationFE().GetReservations(flexDeskBll, reservationBll.ShowAllReservations().Where(r => ((r.StartDate >= rvm.Start && r.StartDate <= rvm.End) || (r.EndDate >= rvm.Start && r.EndDate <= rvm.End)) || ((rvm.Start >= r.StartDate && rvm.Start <= r.EndDate) || (rvm.End >= r.StartDate && rvm.End <= r.EndDate))));
            
            rvm.DefaultDesks = DefaultDesks((DateTime)rvm.Start, (DateTime)rvm.End);
        }

        public IActionResult NewReservation(long flexDesk, DateTime start, DateTime end, long user)
        {
            activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};

            if (user == activeUser.UserId || activeUser.Administrator > 0)
            {
                Reservation res = new Reservation();
                res.FlexDeskId = flexDesk;
                res.UserId = user;
                res.StartDate = start;
                res.EndDate = end;
                res.UserId = user;
                res.Creator = (int)activeUser.UserId;
                res.CreationDate = DateTime.Now;
                reservationBll.CreateReservation(res);

                rvm.UserId = user;
                rvm.User = userBll.GetUserById(user);
                rvm.UserCode = rvm.User.Login;
                rvm.Start = res.StartDate;
                rvm.End = res.EndDate;
                rvm.ActiveUser = activeUser;
                rvm.Dictionary = new Dictionary(HttpContext.Session.GetInt32("language"));
                rvm.ReservationFloor = floorBll.GetFloorById(departmentBll.GetDepartmentById(flexDeskBll.GetFlexDeskById(res.FlexDeskId).DepartmentId).FloorId);
                rvm.ReservationUser = new ReservationFE(flexDeskBll, res);
                rvm.ReservationUser.NameCreator = activeUser.FirstName + " " + activeUser.Name;
                UpdateRvm(rvm);
                return View(rvm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            

        }
       

        public IActionResult Delete(long id, string message)
        {
            ReservationFE res = new ReservationFE(flexDeskBll,reservationBll.GetReservationById(id));
            activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
            res.NameCreator = activeUser.FirstName + " " + activeUser.Name;
            
            rvm.ReservationUser = res;
            rvm.UserId = res.UserId;
            rvm.User = userBll.GetUserById(res.UserId);
            rvm.UserCode = rvm.User.Login;
            rvm.ReservationFloor = floorBll.GetFloorById(departmentBll.GetDepartmentById(flexDeskBll.GetFlexDeskById(res.FlexDeskId).DepartmentId).FloorId);

            rvm.Dictionary = new Dictionary(HttpContext.Session.GetInt32("language"));

            if (message==""||message==null)
            {
                message = rvm.Dictionary.Label5 +" "+ rvm.Dictionary.Label19;
            }
            ViewData["Title"] = message;


            rvm.ActiveUser = activeUser;
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};

            UpdateRvm(rvm);

            return View(rvm);
        }      


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id, ReservationViewModel rvm)
        {
            activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
            Reservation res = reservationBll.GetReservationById(id);

            if (res.UserId == activeUser.UserId || activeUser.Administrator > 0)
            {
                reservationBll.DeleteReservation(id);

                rvm.UserId = res.UserId;
                rvm.User = userBll.GetUserById(res.UserId);
                rvm.UserCode = rvm.User.Login;
                
            }

            rvm.ActiveUser = activeUser;
            rvm.Dictionary = new Dictionary(HttpContext.Session.GetInt32("language"));
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
            return RedirectToAction("Index", rvm);
        }

        private List<int?> DefaultDesks(DateTime start, DateTime end)
        {
            List<int?> desks = new List<int?>();

            foreach (var user in userBll.ShowAllUsers())
            {
                if (user.DefaultDesk > 0)
                {
                    IEnumerable<Absence> absences = absenceBll.ShowAllAbsences().Where(a => a.UserId == user.UserId);
                    bool present = false;
                    for (int i = 0; i < (end - start).TotalDays+1; i++)
                    {
                        if (absences.FirstOrDefault(ab=> ab.StartDate<=start.AddDays(i) && ab.EndDate >= start.AddDays(i)) == null)
                        {
                            present = true;
                        }
                    }

                    if (present)
                    {
                        desks.Add(user.DefaultDesk);
                    }
                }
            }
            return desks;
        }   

        public IActionResult ReservationsFlexDesk(long flexDeskId)
        {
            try
            {
                ReservationViewModel rvm = new ReservationViewModel
                {
                    Reservations = new ReservationFE().GetReservations(flexDeskBll, reservationBll.ShowAllReservations().Where(r => r.FlexDeskId == flexDeskId && r.EndDate >= DateTime.Today))
                };
                foreach (var item in rvm.Reservations)
                {
                    User user = userBll.GetUserById(item.UserId);
                    item.UserName = user.FirstName + " " + user.Name;
                }
                ViewData["Title"] = "Reservations " + flexDeskBll.GetFlexDeskById(flexDeskId).FlexDeskCode;
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
                return View(rvm);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "FlexDesk");
            }
        }

        public IActionResult ReservationsUser(long userId)
        {
            try
            {
                ReservationViewModel rvm = new ReservationViewModel
                {
                    Reservations = new ReservationFE().GetReservations(flexDeskBll, reservationBll.ShowAllReservations().Where(r => r.UserId == userId && r.EndDate >= DateTime.Today))
                };
                
                foreach (var item in rvm.Reservations)
                {
                    User user1 = userBll.GetUserById(item.UserId);
                    item.UserName = user1.FirstName + " " + user1.Name;
                }
                User user2 = userBll.GetUserById(userId);
                ViewData["Title"] = "Reservations " + user2.FirstName + " " + user2.Name;
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
                return View(rvm);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "FlexDesk");
            }
        }

    }
}