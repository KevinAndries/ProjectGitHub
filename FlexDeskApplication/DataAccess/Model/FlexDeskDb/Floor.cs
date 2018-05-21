using System.Collections.Generic;

namespace DataAccessLayer.Model.FlexDeskDb
{
    public partial class Floor
    {
        public Floor()
        {
            Department = new HashSet<Department>();
        }

        public long FloorId { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public string FloorCode { get; set; }
        public string Svg { get; set; }
        public long BuildingId { get; set; }

        public Building Building { get; set; }
        public ICollection<Department> Department { get; set; }
    }
}
