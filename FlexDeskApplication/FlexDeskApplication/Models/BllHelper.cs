using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class BllHelper
    {
        public IReservationBll ReservationBll { get; set; }
        public IFloorBll FloorBll { get; set; }
        public IBuildingBll BuildingBll { get; set; }
        public IUserBll UserBll { get; set; }
    }
}
