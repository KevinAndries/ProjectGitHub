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
    public class BuildingController : Controller
    {
        private readonly IBuildingBll buildingBll;
        private readonly IFloorBll floorBll;
        private readonly IUserBll userBll;
        private User activeUser;

        public BuildingController(IBuildingBll buildingBll, IFloorBll floorBll, IUserBll userBll)
        {
            this.buildingBll = buildingBll;
            this.floorBll = floorBll;
            this.userBll = userBll;
        }
        // GET: Building
        public ActionResult Index()
        {
            return View(buildingBll.ShowAllBuildings());
        }

        // GET: Building/Details/5
        public ActionResult Details(int id)
        {
            return View(buildingBll.GetBuildingById(id));
        }

        // GET: Building/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Building/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BuildingViewModel bvm)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                if (activeUser.Administrator > 0)
                {
                    Building building = new Building
                    {
                        BuildingCode = bvm.BuildingCode,
                        City = bvm.City,
                        Name = bvm.Name,
                        Street = bvm.Street,
                        Number = bvm.Number,
                        ZipCode = bvm.ZipCode
                    };
                    buildingBll.CreateBuilding(building);
                    long buildingId = buildingBll.ShowAllBuildings().FirstOrDefault(b => b.Name == building.Name).BuildingId;
                    
                    
                    for (int i = 0; i < bvm.NumberOfFloors; i++)
                    {
                        Floor floor = new Floor();
                        floor.BuildingId = buildingId;
                        floor.Name = building.Name + " " + i;
                        floor.FloorCode = building.BuildingCode + i.ToString("00");
                        floor.Number = i;
                        floorBll.CreateFloor(floor);
                    }

                    return RedirectToAction(nameof(Index));
                 }
                else
                {
                    return View();
                }
             }
             catch
             {

                    return View();
              }
            
        }

        // GET: Building/Edit/5
        public ActionResult Edit(int id)
        {
            return View(buildingBll.GetBuildingById(id));
        }

        // POST: Building/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, Building building)
        {
            try
            {
                activeUser = userBll.GetUserById((long)HttpContext.Session.GetInt32("userId"));
                if (activeUser.Administrator > 0)
                {
                    buildingBll.UpdateBuilding(id, building);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Building/Delete/5
        public ActionResult Delete(int id)
        {
            return View(buildingBll.GetBuildingById(id));
        }

        // POST: Building/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, Building building)
        {
            try
            {
                DeleteBuilding(id);
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void DeleteBuilding(long id)
        {
            activeUser = userBll.GetUserById((long) HttpContext.Session.GetInt32("userId"));
            if (activeUser.Administrator>0)
            {
                foreach (Floor floor in floorBll.ShowAllFloors().Where(f => f.BuildingId == id))
                {
                    floorBll.DeleteFloor(floor.FloorId);
                }
                buildingBll.DeleteBuilding(id);
            }
        }  

        public IActionResult Floors(long id)
        {
            return RedirectToAction("Index", "Floor", new { id = id });
        }

    }
} 