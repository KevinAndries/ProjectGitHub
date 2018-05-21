using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BusinessLogicLayer
{
    public class ReservationBll : IReservationBll
    {
        private readonly IReservationProvider reservationProvider;
        private readonly IReservationProcessor reservationProcessor;

        public ReservationBll(IReservationProvider reservationProvider, IReservationProcessor reservationProcessor)
        {
            this.reservationProvider = reservationProvider;
            this.reservationProcessor = reservationProcessor;
        }




        public IEnumerable<Reservation> ShowAllReservations()
        {

            var reservations = reservationProvider.Get();

            return reservations;
        }

        public Reservation GetReservationById(long id)
        {
            var reservation = reservationProvider.GetById(id);
            return reservation;
        }

        public void CreateReservation(Reservation reservation)
        {
            {
                reservationProcessor.Create(reservation);

                var lstReservation = new List<Reservation>();
                lstReservation = (List<Reservation>)reservationProvider.Get();
                lstReservation.Add(reservation);
            }
        }

        public void UpdateReservation(long id, Reservation reservation)
        {
            reservationProcessor.Update(reservation);
        }

        public void DeleteReservation(long id)
        {
            reservationProcessor.Delete(id);
        }
    }
}

