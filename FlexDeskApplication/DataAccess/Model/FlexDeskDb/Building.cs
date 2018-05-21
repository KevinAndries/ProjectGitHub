using System.Collections.Generic;

namespace DataAccessLayer.Model.FlexDeskDb
{
    public partial class Building
    {
        public Building()
        {
            Floor = new HashSet<Floor>();
        }

        public long BuildingId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int? Number { get; set; }
        public int? ZipCode { get; set; }
        public string City { get; set; }
        public string BuildingCode { get; set; }
        public string Svg { get; set; }

        public ICollection<Floor> Floor { get; set; }
    }
}
