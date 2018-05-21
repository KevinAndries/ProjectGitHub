using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class FlexDeskProvider : IFlexDeskProvider
    {

        //private readonly string FlexDeskConnection;

        //public FlexDeskProvider(string FlexDeskConnection)
        //{
        //    this.FlexDeskConnection = FlexDeskConnection;
        //}

        private readonly string FlexDeskConnection;

        public FlexDeskProvider(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }


        public IEnumerable<FlexDesk> Get()
        {

            IEnumerable<FlexDesk> flexdesks;

            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                flexdesks = connection.Query<FlexDesk>("SELECT FlexDeskId, Name, FlexDeskCode, SVG, DepartmentId FROM FlexDesk");
                var departments = connection.Query<Department>(
                "SELECT * FROM Department WHERE DepartmentID in @Ids",
                new { Ids = flexdesks.Select(c => c.DepartmentId).Distinct() });
                foreach (var flexdesk in flexdesks)
                {
                    flexdesk.Department = departments.Single((x => x.DepartmentId == flexdesk.DepartmentId));
                }
            }       
            return flexdesks;
        }



        public FlexDesk GetById(long flexdeskId)
        {

            FlexDesk flexdesk;

            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                flexdesk = connection.Query<FlexDesk>("SELECT FlexDeskId, Name, FlexDeskCode, SVG, DepartmentId FROM FlexDesk WHERE flexdeskId=@FlexDeskId", 
                    new{flexdeskId = flexdeskId}).FirstOrDefault();
            }
            return flexdesk;
        }


    }
}
