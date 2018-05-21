using System.Data.SqlClient;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class AbsenceProcessor : IAbsenceProcessor
    {

        private readonly string FlexDeskConnection;

        public AbsenceProcessor(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }


        public void Create(Absence absence)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "INSERT INTO Absence (StartDate, EndDate, Creator, CreationDate, Status, StatusDate, Description, UserId) VALUES (@StartDate, @EndDate, @Creator, @CreationDate, @Status, @StatusDate, @Description, @UserId)", 
                    new {absence.StartDate, absence.EndDate, absence.Creator, absence.CreationDate, absence.Status, absence.StatusDate, absence.Description, absence.UserId});

            }
        }


       public void Update(Absence absence)

        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "UPDATE Absence SET StartDate=@StartDate, EndDate=@EndDate, Creator=@Creator, CreationDate=@CreationDate, Status=@Status, StatusDate=@StatusDate, Description=@Description, UserId=@UserId WHERE absenceId=@AbsenceId",
                    new { absence.AbsenceId, absence.StartDate, absence.EndDate, absence.Creator, absence.CreationDate, absence.Status, absence.StatusDate, absence.Description, absence.UserId });
            }
        }

        public void Delete(long absenceId)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "DELETE FROM Absence WHERE absenceId=@AbsenceId",
                    new { absenceId = absenceId });
            }
        }
    }
}
