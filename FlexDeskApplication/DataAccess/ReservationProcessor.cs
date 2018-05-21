using System.Data.SqlClient;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class ReservationProcessor : IReservationProcessor
    {
        //private readonly string connectionString;

        //public  ReservationProcessor(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}

        private readonly string FlexDeskConnection;

        public ReservationProcessor(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }


        public void Create(Reservation reservation)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "INSERT INTO Reservation (StartDate, EndDate, Creator, CreationDate, Status, StatusDate, Description, UserId, FlexDeskId) VALUES (@StartDate, @EndDate, @Creator, @CreationDate, @Status, @StatusDate, @Description, @UserId, @FlexDeskId)",
                    new {reservation.StartDate, reservation.EndDate, reservation.Creator, reservation.CreationDate, reservation.Status, reservation.StatusDate, reservation.Description, reservation.UserId, reservation.FlexDeskId });

            }
        }


       public void Update(Reservation reservation)

        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "UPDATE Reservation SET StartDate=@StartDate, EndDate=@EndDate, Creator=@Creator, CreationDate=@CreationDate, Status=@Status, StatusDate=@StatusDate, Description=@Description, UserId=@UserId, FlexDeskId=@FlexDeskId WHERE reservationId=@ReservationId",
                    new { reservation.ReservationId, reservation.StartDate, reservation.EndDate, reservation.Creator, reservation.CreationDate, reservation.Status, reservation.StatusDate, reservation.Description, reservation.UserId, reservation.FlexDeskId });
            }
        }

        public void Delete(long reservationId)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "DELETE FROM Reservation WHERE reservationId=@ReservationId",
                    new { reservationId = reservationId });
            }
        }
    }
}
