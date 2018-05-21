using System.Data.SqlClient;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class FlexDeskProcessor : IFlexDeskProcessor
    {
        //private readonly string connectionString;

        //public FlexDeskProcessor(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}

        private readonly string FlexDeskConnection;

        public FlexDeskProcessor(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }




        public void Create(FlexDesk flexdesk)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "INSERT INTO FlexDesk (Name, FlexDeskCode, SVG, DepartmentId) VALUES (@Name, @FlexDeskCode, @SVG, @DepartmentId)", 
                    new {flexdesk.Name, flexdesk.FlexDeskCode, flexdesk.Svg, flexdesk.DepartmentId});

            }
        }


       public void Update(FlexDesk flexdesk)

        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "UPDATE FlexDesk SET Name=@Name, FlexDeskCode=@FlexDeskCode, SVG=@SVG, DepartmentId=@DepartmentId WHERE flexdeskId=@FlexDeskId",
                    new { flexdesk.FlexDeskId, flexdesk.Name, flexdesk.FlexDeskCode, flexdesk.Svg, flexdesk.DepartmentId });
            }
        }

        public void Delete(long flexdeskId)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "DELETE FROM FlexDesk WHERE flexdeskId=@FlexDeskId",
                    new { flexdeskId = flexdeskId });
            }
        }
    }
}
