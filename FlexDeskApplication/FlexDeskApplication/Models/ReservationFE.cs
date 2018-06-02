using BusinessLogicLayer;
using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class ReservationFE
    {

        public long ReservationId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }
        public int? Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreationDate { get; set; }
        public long FlexDeskId { get; set; }
        public long UserId { get; set; }

        public FlexDesk FlexDesk { get; set; }
        public string NameCreator { get; set; }

        public ReservationFE(){}

        public ReservationFE(IFlexDeskBll flexDeskBll, Reservation res)
        {
            ReservationId = res.ReservationId;
            StartDate = res.StartDate;
            EndDate = res.EndDate;
            Creator = res.Creator;
            CreationDate = res.CreationDate;
            FlexDeskId = res.FlexDeskId;
            UserId = res.UserId;
            FlexDesk = flexDeskBll.GetFlexDeskById(res.FlexDeskId);

        }

        public IEnumerable<ReservationFE> GetReservations(IFlexDeskBll flexDeskBll, IEnumerable<Reservation> reservations)
        {
            List<ReservationFE> reservationsFE = new List<ReservationFE>();
            foreach (var item in reservations)
            {
                reservationsFE.Add(new ReservationFE(flexDeskBll,item));
            }

            return reservationsFE;
        }
    }
}
