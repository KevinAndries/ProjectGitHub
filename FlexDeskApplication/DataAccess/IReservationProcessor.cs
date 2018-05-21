using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IReservationProcessor
    {
       void Create(Reservation reservation);

        void Update(Reservation reservation);

        void Delete(long reservationId);
    }
}
