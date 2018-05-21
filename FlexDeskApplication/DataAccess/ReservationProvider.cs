using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class ReservationProvider : IReservationProvider
    {

        //private readonly string FlexDeskConnection;

        //public ReservationProvider(string FlexDeskConnection)
        //{
        //    this.FlexDeskConnection = FlexDeskConnection;
        //}

        private readonly string FlexDeskConnection;

        public ReservationProvider(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }


        public IEnumerable<Reservation> Get()
        {

            IEnumerable<Reservation> reservations;

            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                reservations = connection.Query<Reservation>("SELECT ReservationId, StartDate, EndDate, Creator, CreationDate, Status, StatusDate, Description, UserId, FlexDeskId FROM Reservation");
            }

            return reservations;
        }


        public Reservation GetById(long reservationId)
        {

            Reservation reservation;

            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                reservation = connection.Query<Reservation>("SELECT ReservationId, StartDate, EndDate, Creator, CreationDate, Status, StatusDate, Description, UserId, FlexDeskId FROM Reservation WHERE reservationId=@ReservationId",
                    new {reservationId = reservationId}).FirstOrDefault();
            }

            return reservation;
        }

    }
}
