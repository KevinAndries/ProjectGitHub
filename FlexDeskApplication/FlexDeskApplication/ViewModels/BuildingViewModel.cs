using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class BuildingViewModel
    {
        public long BuildingId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int? Number { get; set; }
        public int? ZipCode { get; set; }
        public string City { get; set; }
        public string BuildingCode { get; set; }
        public string Svg { get; set; }
        public int NumberOfFloors { get; set; }
    }
}
