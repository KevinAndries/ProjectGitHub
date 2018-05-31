using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class FloorViewModel
    {
        public long FloorId { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public string FloorCode { get; set; }
        public string Svg { get; set; }
        public long BuildingId { get; set; }

        public IEnumerable<Floor> Floors { get; set; }
    }
}
