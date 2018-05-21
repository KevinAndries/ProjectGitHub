using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public interface IReservationBll
    {
        IEnumerable<Reservation> ShowAllReservations();
        Reservation GetReservationById(long id);
        void CreateReservation(Reservation reservation);
        void UpdateReservation(long id, Reservation reservation);
        void DeleteReservation(long id);
    }
}
