using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class FloorProvider : IFloorProvider
    {

        private readonly string FlexDeskConnection;

        public FloorProvider(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }


        public IEnumerable<Floor> Get()
        {

            IEnumerable<Floor> floors;

            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                floors = connection.Query<Floor>("SELECT FloorId, Name, Number, FloorCode, SVG, BuildingID FROM Floor");
                var buildings = connection.Query<Building>(
                    "SELECT * FROM Building WHERE BuildingId IN @Ids",
                    new { Ids = floors.Select(c => c.BuildingId).Distinct() });
                foreach (var floor in floors)
                {
                    floor.Building = buildings.Single(x => x.BuildingId == floor.BuildingId);
                }
            }

            return floors;
        }


        public Floor GetById(long floorId)
        {

            Floor floor;

            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                floor = connection.Query<Floor>("SELECT FloorId, Name, Number, FloorCode, SVG, BuildingID FROM Floor WHERE floorId=@FloorId", 
                    new {floorId = floorId}).FirstOrDefault();

            }

            return floor;
        }


        // bij het opvragen van een floor moet een lijst gegeven worden van alle flex desks op die floor. 
        // eerst alle departments ophalen en nadien alle flexdesks en toevoegen in één lijst.

        public Floor GetFlexDeskById(long floorId)
        {

            Floor floor;
            Department department;
            FlexDesk flexdeskid;




            using (var connection = new SqlConnection(FlexDeskConnection))
            {

                floor = connection.Query<Floor>("SELECT FloorId, Name, Number, FloorCode, SVG, BuildingID FROM Floor WHERE floorId=@FloorId",
                    new { floorId = floorId }).FirstOrDefault();



                //floor = connection.Query<Floor>("SELECT SVG, value tempSVG, substring(LEFT(value, charindex(', width', value) - 1), CHARINDEX('desk', value) + len('desk'), LEN(value)) as deskid FROM floor CROSS APPLY STRING_SPLIT(REPLACE(SVG, '><', '|'), '|') WHERE floorId = @FloorId and value not in ('[<svg', '/svg>]'); ",
                //   new { floorId = floorId }).FirstOrDefault();



                //Query voor extracten van de flexdesk id's uit svg
                //SELECT SVG, value tempSVG,
                //substring(LEFT(value, charindex(', width', value) - 1), CHARINDEX('desk', value) + len('desk'), LEN(value)) as deskid
                //FROM floor
                //CROSS APPLY STRING_SPLIT(REPLACE(SVG, '><', '|'), '|')
                //WHERE floorId = @FloorId
                //and value not in ('[<svg', '/svg>]');

            }

            return floor;
        }


   





    }
}
