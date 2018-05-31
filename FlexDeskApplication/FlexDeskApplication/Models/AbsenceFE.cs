using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class AbsenceFE
    {
        public long AbsenceId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }

        public AbsenceFE(){}

        public AbsenceFE(Absence a)
        {
            AbsenceId = a.AbsenceId;
            StartDate = a.StartDate;
            EndDate = a.EndDate;
            Description = a.Description;
            UserId = a.UserId;
        }

        public IEnumerable<AbsenceFE> GetAbsensesFE(IEnumerable<Absence> absences)
        {
            List<AbsenceFE>absencesFE = new List<AbsenceFE>() ;
            foreach (var item in absences)
            {
                absencesFE.Add(new AbsenceFE(item));
            }
            return absencesFE;
        }
    }
}
