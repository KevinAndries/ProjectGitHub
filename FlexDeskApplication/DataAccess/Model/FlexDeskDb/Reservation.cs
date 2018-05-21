using System;

namespace DataAccessLayer.Model.FlexDeskDb
{
    public partial class Reservation
    {
        public long ReservationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public int? Requestor { get; set; }
        public int? Creator { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public long FlexDeskId { get; set; }

        public FlexDesk FlexDesk { get; set; }
        public User User { get; set; }
    }
}
