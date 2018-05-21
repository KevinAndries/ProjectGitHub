using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using DataAccessLayer.Model.FlexDeskDb;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;

namespace MVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationBll reservationBll;
        private readonly IUserBll userBll;
        private readonly IBuildingBll buildingBll;
        private readonly IFloorBll floorBll;
        private readonly IDepartmentBll departmentBll;
        private readonly IFlexDeskBll flexDeskBll;
        private User activeUser;
        private ReservationViewModel rvm;

        public ReservationController(IReservationBll reservationBll, IUserBll userBll, IFloorBll floorBll, IBuildingBll buildingBll, IDepartmentBll departmentBll, IFlexDeskBll flexDeskBll)
        {
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
            this.rvm = rvm;
            this.activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
            this.rvm.ActiveUser = activeUser;

            if (rvm.LoginUser != null)
            {
                UpdateRvm(rvm);
            }
            else
            {
                rvm.User = activeUser;
                rvm.LoginUser = activeUser.Login;
                rvm.Start = DateTime.Today;
                rvm.End = DateTime.Today;
            }
            return View(rvm);

        }

        
        public IActionResult MakeReservation(long flexDesk, DateTime start, DateTime end, int user)
        {
            this.activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
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
                rvm.LoginUser = rvm.User.Login;
                rvm.Start = res.StartDate;
                rvm.End = res.EndDate;

                UpdateRvm(rvm);

            }
            return RedirectToAction("Index", rvm);
        }


        private void UpdateRvm(ReservationViewModel rvm)
        {
            rvm.User = userBll.ShowAllUsers().FirstOrDefault(u => u.Login == rvm.LoginUser);      
            rvm.Building = buildingBll.GetBuildingById(floorBll.GetFloorById(departmentBll.GetDepartmentById(rvm.User.DepartmentId).FloorId).BuildingId);
            rvm.Floors = floorBll.ShowAllFloors().Where(floor => floor.BuildingId == rvm.Building.BuildingId).ToList();
            foreach (var floor in rvm.Floors)
            {
                floor.Department = departmentBll.ShowAllDepartments().Where(d => d.FloorId == floor.FloorId).ToList();
                foreach (var d in floor.Department)
                {
                    d.FlexDesk = flexDeskBll.ShowAllFlexdesks().Where(fd => fd.DepartmentId == d.DepartmentId).ToList();
                }
            }
            rvm.Reservations = reservationBll.ShowAllReservations().Where(r => (r.StartDate >= rvm.Start && r.StartDate <= rvm.End) || (r.EndDate >= rvm.Start && r.EndDate <= rvm.End)).ToList();
            rvm.UserId = rvm.User.UserId;

        }

        public IActionResult EditReservation(long reservationId)
        {
            Reservation res = reservationBll.GetReservationById(reservationId);
            
            rvm.ReservationUser = res;
            rvm.UserId = res.UserId;
            rvm.User = userBll.GetUserById(res.UserId);
            rvm.LoginUser = rvm.User.Login;
            rvm.ReservationFloor = floorBll.GetFloorById(departmentBll.GetDepartmentById(flexDeskBll.GetFlexDeskById(res.FlexDeskId).DepartmentId).FloorId);

            UpdateRvm(rvm);

            return View(rvm);
        }

        public IActionResult Delete(long reservationId)
        {
            this.activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
            Reservation res = reservationBll.GetReservationById(reservationId);

            if (res.UserId == activeUser.UserId || activeUser.Administrator > 0)
            {
                reservationBll.DeleteReservation(reservationId);

                rvm.UserId = res.UserId;
                rvm.User = userBll.GetUserById(res.UserId);
                rvm.LoginUser = rvm.User.Login;

                UpdateRvm(rvm);
            }

            return RedirectToAction("Index", rvm);
        }

    }
}