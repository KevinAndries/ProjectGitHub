using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using DataAccessLayer.Model.FlexDeskDb;
using BusinessLogicLayer;

namespace MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentBll departmentBll;
        private readonly IFlexDeskBll flexDeskBll;
        private readonly IFloorBll floorBll;
        private readonly IUserBll userBll;
        private User activeUser;

        public DepartmentController(IDepartmentBll departmentBll, IFlexDeskBll flexDeskBll, IFloorBll floorBll, IUserBll userBll)
        {
            this.departmentBll = departmentBll;
            this.flexDeskBll = flexDeskBll;
            this.floorBll = floorBll;
            this.userBll = userBll;
        }
        // GET: Department
        public ActionResult Index(long floorId, long buildingId)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};

            if (floorId == 0)
            {
                return View(new DepartmentViewModel { Departments = departmentBll.ShowAllDepartments() });
            }
            else
            {
                return View(new DepartmentViewModel { Departments = departmentBll.ShowAllDepartments().Where(d => d.FloorId == floorId), FloorId=floorId, BuildingId=buildingId });
            }
        }

        // GET: Department/Details/5
        public ActionResult Details(long id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
            return View(departmentBll.GetDepartmentById(id));
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentViewModel dvm)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};

                if (activeUser.Administrator > 0)
                {
                    Department department = new Department
                    {
                        Name = dvm.Name,
                        DepartmentCode = dvm.DepartmentCode,
                        FloorId = floorBll.ShowAllFloors().FirstOrDefault(f => f.FloorCode == dvm.FloorCode).FloorId,
                        Svg = dvm.Svg,
                    };

                    departmentBll.CreateDepartment(department);
                    long departmentId = departmentBll.ShowAllDepartments().FirstOrDefault(d => d.DepartmentCode == department.DepartmentCode).DepartmentId;
                    
                    for (int i = 1; i < dvm.NumberOfDesks; i++)
                    {
                        FlexDesk desk = new FlexDesk();
                        desk.DepartmentId = departmentId;
                        desk.FlexDeskCode = department.DepartmentCode + i.ToString("000"); ;
                        desk.Name = department.Name + " " + i.ToString("000");
                        flexDeskBll.CreateFlexDesk(desk);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(long id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
            return View(departmentBll.GetDepartmentById(id));
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, Department department)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};

                if (activeUser.Administrator > 0)
                {
                    departmentBll.UpdateDepartment(id, department);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Delete/5
        public ActionResult Delete(long id)
        {
            ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};
            return View(departmentBll.GetDepartmentById(id));
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, Department department)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                ViewData["sessionData"] = new int?[] { HttpContext.Session.GetInt32("admin"), HttpContext.Session.GetInt32("language")};

                if (activeUser.Administrator > 0)
                {
                    foreach (FlexDesk desk in flexDeskBll.ShowAllFlexdesks().Where(f=>f.DepartmentId==id))
                    {
                        flexDeskBll.DeleteFlexDesk(desk.FlexDeskId);
                    }
                    departmentBll.DeleteDepartment(id);
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