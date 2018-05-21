using System.Data.SqlClient;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class FloorProcessor : IFloorProcessor
    {

        private readonly string FlexDeskConnection;

        public FloorProcessor(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }




        
        public void Create(Floor floor)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "INSERT INTO Floor (Name, Number, FloorCode, SVG, BuildingId) VALUES (@Name, @Number, @FloorCode, @SVG, @BuildingId)", 
                    new {floor.Name, floor.Number, floor.FloorCode, floor.Svg, floor.BuildingId});

            }
        }


       public void Update(Floor floor)

        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "UPDATE Floor SET Name=@Name, Number=@Number, FloorCode=@FloorCode, SVG=@SVG, BuildingId=@BuildingId WHERE floorId=@FloorId",
                    new { floor.FloorId, floor.Name, floor.Number, floor.FloorCode, floor.Svg, floor.BuildingId });
            }
        }

        public void Delete(long floorId)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "DELETE FROM Floor WHERE floorId=@FloorId",
                    new { floorId = floorId });
            }
        }
    }
}
