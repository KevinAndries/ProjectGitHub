using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class DepartmentViewModel
    {
        public long DepartmentId { get; set; }
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
        public string Svg { get; set; }
        public string FloorCode { get; set; }
        public long FloorId { get; set; }
        public int NumberOfDesks { get; set; }

        public long BuildingId { get; set; }

        public IEnumerable<Department> Departments { get; set; }
    }
}
