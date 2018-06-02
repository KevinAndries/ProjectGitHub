using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class UserViewModel
    {
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

        public long FloorId { get; set; }
        public long BuildingId { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
