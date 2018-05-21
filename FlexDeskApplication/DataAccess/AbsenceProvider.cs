using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class AbsenceProvider : IAbsenceProvider
    {


        private readonly string FlexDeskConnection;

        public AbsenceProvider(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }


        public IEnumerable<Absence> Get()
        {

            IEnumerable<Absence> absences;

            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                absences = connection.Query<Absence>("SELECT AbsenceID, StartDate, EndDate, Creator, CreationDate, Status, StatusDate, Description, UserId FROM Absence");
                var users = connection.Query<User>(
                    "SELECT * FROM [dbo].[User] WHERE UserId IN @Ids",
                    new {Ids = absences.Select(c => c.AbsenceId).Distinct()});
                foreach (var absence in absences)
                {
                    absence.User = users.Single(x => x.UserId == absence.UserId);
                }
            }

            return absences;
        }

        public Absence GetById(long absenceId)
        {

            Absence absence;

            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                absence = connection.Query<Absence>("SELECT AbsenceID, StartDate, EndDate, Creator, CreationDate, Status, StatusDate, Description, UserId FROM Absence WHERE absenceId=@AbsenceId", new {absenceId = absenceId}).FirstOrDefault();
            }

            return absence;
        }
    }
}
