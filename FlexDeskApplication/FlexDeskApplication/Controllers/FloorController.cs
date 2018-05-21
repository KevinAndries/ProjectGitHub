using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer;
using DataAccessLayer.Model.FlexDeskDb;

namespace MVC.Controllers
{
    public class FloorController : Controller
    {
        private readonly IFloorBll floorBll;
        private readonly IUserBll userBll;
        private User activeUser;

        public FloorController(IFloorBll floorBll, IUserBll userBll)
        {
            this.floorBll = floorBll;
            this.userBll = userBll;
        }

        // GET: Floor
        public ActionResult Index(long id)
        {
            if (id == 0)
            {
                return View(floorBll.ShowAllFloors());
            }
            else
            {
                return View(floorBll.ShowAllFloors().Where(f => f.BuildingId==id));
            }
            
        }

        // GET: Floor/Details/5
        public ActionResult Details(int id)
        {
            return View(floorBll.GetFloorById(id));
        }

        // GET: Floor/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Floor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Floor floor)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                if (activeUser.Administrator > 0)
                {
                    floorBll.CreateFloor(floor);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Floor/Edit/5
        public ActionResult Edit(int id)
        {
            return View(floorBll.GetFloorById(id));
        }

        // POST: Floor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, Floor floor)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                if (activeUser.Administrator > 0)
                {
                    floorBll.UpdateFloor(id, floor);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Floor/Delete/5
        public ActionResult Delete(int id)
        {
            return View(floorBll.GetFloorById(id));
        }

        // POST: Floor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Floor floor)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                if (activeUser.Administrator > 0)
                {
                    floorBll.DeleteFloor(id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}