using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class FlexDeskViewModel
    {
        public long FlexDeskId { get; set; }
        public string Name { get; set; }
        public string FlexDeskCode { get; set; }
        public string Svg { get; set; }
        public long DepartmentId { get; set; }

        public long FloorId { get; set; }
        public long BuildingId { get; set; }

        public IEnumerable<FlexDesk> FlexDesks { get; set; }
    }
}
