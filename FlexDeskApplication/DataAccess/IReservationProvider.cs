using System.Collections.Generic;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IReservationProvider
    {
        IEnumerable<Reservation> Get();

        //List<Reservation> GetAll();
        //bool Add(Reservation reservation);
        Reservation GetById(long reservationId);

    }
}
