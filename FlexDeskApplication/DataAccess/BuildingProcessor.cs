using System.Data.SqlClient;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class BuildingProcessor : IBuildingProcessor
    {
        private readonly string FlexDeskConnection;

        public BuildingProcessor(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }




        public void Create(Building building)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "INSERT INTO Building (Name, Street, Number, City, ZipCode, BuildingCode, SVG) VALUES (@Name, @Street, @Number, @City, @ZipCode, @BuildingCode, @SVG)", 
                    new {building.Name, building.Street, building.Number ,building.City, building.ZipCode, building.BuildingCode, building.Svg});

            }
        }


       public void Update(Building building)

        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "UPDATE Building SET Name=@Name, Street=@Street, Number=@Number, City=@City, ZipCode=@ZipCode, BuildingCode=@BuildingCode, SVG=@SVG WHERE buildingId=@BuildingId",
                    new { building.BuildingId, building.Name, building.Street, building.Number, building.City, building.ZipCode, building.BuildingCode, building.Svg });
            }
        }

        public void Delete(long buildingId)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "DELETE FROM Building WHERE buildingId=@BuildingId",
                    new { buildingId = buildingId });
            }
        }


    }
}
