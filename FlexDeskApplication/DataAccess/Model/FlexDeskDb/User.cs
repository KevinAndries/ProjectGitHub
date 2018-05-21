using System;
using System.Collections.Generic;

namespace DataAccessLayer.Model.FlexDeskDb
{
    public partial class User
    {
        public User()
        {
            Absence = new HashSet<Absence>();
            Reservation = new HashSet<Reservation>();
        }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DefaultDesk { get; set; }
        public string Phone { get; set; }
        public int? Administrator { get; set; }
        public long DepartmentId { get; set; }

        public Department Department { get; set; }
        public ICollection<Absence> Absence { get; set; }
        public ICollection<Reservation> Reservation { get; set; }
    }
}
