using DataAccessLayer.Model.FlexDeskDb;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class AbsenceViewModel
    {
        public User ActiveUser { get; set; }

        public long UserId { get; set; }
        public string UserCode { get; set; }
        public User User { get; set; }

        public long AbsenceId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }
        public int? Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreationDate { get; set; }
        public int? Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StatusDate { get; set; }
        public string Description { get; set; }

        public IEnumerable<AbsenceFE> Absences { get; set; }
        public bool ConflictReservatie { get; set; }

        public Dictionary Dictionary { get; set; }
    }
}
