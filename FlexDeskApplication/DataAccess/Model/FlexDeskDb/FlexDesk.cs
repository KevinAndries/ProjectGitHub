using System.Collections.Generic;

namespace DataAccessLayer.Model.FlexDeskDb
{
    public partial class FlexDesk
    {
        public FlexDesk()
        {
            Reservation = new HashSet<Reservation>();
        }

        public long FlexDeskId { get; set; }
        public string Name { get; set; }
        public string FlexDeskCode { get; set; }
        public string Svg { get; set; }
        public long DepartmentId { get; set; }

        public Department Department { get; set; }
        public ICollection<Reservation> Reservation { get; set; }
    }
}
