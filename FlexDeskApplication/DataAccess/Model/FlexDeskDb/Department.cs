using System.Collections.Generic;

namespace DataAccessLayer.Model.FlexDeskDb
{
    public partial class Department
    {
        public Department()
        {
            FlexDesk = new HashSet<FlexDesk>();
            User = new HashSet<User>();
        }

        public long DepartmentId { get; set; }
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
        public string Svg { get; set; }
        public long FloorId { get; set; }

        public Floor Floor { get; set; }
        public ICollection<FlexDesk> FlexDesk { get; set; }
        public ICollection<User> User { get; set; }
    }
}
