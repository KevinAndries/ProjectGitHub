using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class BuildingProvider : IBuildingProvider
    {


        private readonly string FlexDeskConnection;

        public BuildingProvider(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }




        public IEnumerable<Building> Get()
        {

            IEnumerable<Building> buildings;

            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                buildings = connection.Query<Building>("SELECT BuildingId, Name, Street, Number, City, ZipCode, BuildingCode, SVG FROM Building");
            }

            return buildings;
        }



        public Building GetById(long buildingId)
        {
            Building building;
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                building = connection.Query<Building>("SELECT BuildingId, Name, Street, Number, City, ZipCode, BuildingCode, SVG FROM Building WHERE buildingId=@BuildingId", 
                    new {buildingId = buildingId}).FirstOrDefault();
            }

            return building;

        }
    }
}
