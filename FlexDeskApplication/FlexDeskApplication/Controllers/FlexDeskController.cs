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
    public class FlexDeskController : Controller
    {
        private readonly IFlexDeskBll flexDeskBll;
        private readonly IUserBll userBll;
        private User activeUser;

        public FlexDeskController(IFlexDeskBll flexDeskBll, IUserBll userBll)
        {
            this.flexDeskBll = flexDeskBll;
            this.userBll = userBll;
        }
        // GET: FlexDesk
        public ActionResult Index(long departmentId, long floorId, long buildingId)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};

            if (departmentId == 0)
            {
                return View(new FlexDeskViewModel { FlexDesks = flexDeskBll.ShowAllFlexdesks() });
            }
            else
            {
                return View(new FlexDeskViewModel { FlexDesks = flexDeskBll.ShowAllFlexdesks().Where(f => f.DepartmentId == departmentId), BuildingId = buildingId, DepartmentId = departmentId, FloorId = floorId });
            }
        }

        // GET: FlexDesk/Details/5
        public ActionResult Details(long id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
            return View(flexDeskBll.GetFlexDeskById(id));
        }

        // GET: FlexDesk/Create
        public ActionResult Create(long departmentId)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
            return View(new FlexDesk { DepartmentId = departmentId });
        }

        // POST: FlexDesk/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlexDesk desk)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                if (activeUser.Administrator > 0)
                {
                    flexDeskBll.CreateFlexDesk(desk);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: FlexDesk/Edit/5
        public ActionResult Edit(long id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
            return View(flexDeskBll.GetFlexDeskById(id));
        }

        // POST: FlexDesk/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, FlexDesk desk)
        {
            flexDeskBll.UpdateFlexDesk(id, desk);
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                if (activeUser.Administrator > 0)
                {
                    desk.FlexDeskId = id;
                    flexDeskBll.UpdateFlexDesk(id, desk);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
                return View();
            }
        }

        // GET: FlexDesk/Delete/5
        public ActionResult Delete(int id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language") };
            return View(flexDeskBll.GetFlexDeskById(id));
        }

        // POST: FlexDesk/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FlexDesk desk)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                if (activeUser.Administrator > 0)
                {
                    flexDeskBll.DeleteFlexDesk(id);
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