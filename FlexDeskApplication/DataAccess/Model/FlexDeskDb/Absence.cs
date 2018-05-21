using System;
using System.Collections.Generic;

namespace DataAccessLayer.Model.FlexDeskDb
{
    public partial class Absence
    {
        public long AbsenceId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Creator { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }

        public User User { get; set; }
    }
}
