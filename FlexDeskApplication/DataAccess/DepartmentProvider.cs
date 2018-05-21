using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class DepartmentProvider : IDepartmentProvider
    {

        private readonly string FlexDeskConnection;

        public DepartmentProvider(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }



        public IEnumerable<Department> Get()
        {

            IEnumerable<Department> departments;

            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                departments = connection.Query<Department>("SELECT DepartmentId, Name, DepartmentCode, SVG, FloorId FROM Department");
                var floors = connection.Query<Floor>(
                    "SELECT * FROM Floor WHERE FloorID in @Ids",
                    new { Ids = departments.Select(c => c.FloorId).Distinct() });
                foreach (var department in departments)
                {
                    department.Floor = floors.Single(x => x.FloorId == department.FloorId);
                }
            }

            return departments;
        }

        public Department GetById(long departmentId)
        {

            Department department;
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                department = connection.Query<Department>("SELECT DepartmentId, Name, DepartmentCode, SVG, FloorId FROM Department WHERE departmentId=@DepartmentId",
                        new {departmentId = departmentId}).FirstOrDefault();
            }

            return department;
        }

    }
}
