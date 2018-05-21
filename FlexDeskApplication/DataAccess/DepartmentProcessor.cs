using System.Data.SqlClient;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class DepartmentProcessor : IDepartmentProcessor
    {
        //private readonly string connectionString;

        //public DepartmentProcessor(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}

        private readonly string FlexDeskConnection;

        public DepartmentProcessor(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }




        public void Create(Department department)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "INSERT INTO Department (Name, DepartmentCode, SVG, FloorId) VALUES (@Name, @DepartmentCode, @SVG, @FloorId)", 
                    new {department.Name, department.DepartmentCode, department.Svg, department.FloorId});

            }
        }


       public void Update(Department department)

        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "UPDATE Department SET Name=@Name, DepartmentCode=@DepartmentCode, SVG=@SVG, FloorId=@FloorId WHERE departmentId=@DepartmentId",
                    new { department.DepartmentId, department.Name, department.DepartmentCode, department.Svg, department.FloorId });
            }
        }

        public void Delete(long departmentId)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "DELETE FROM Department WHERE departmentId=@DepartmentId",
                    new { departmentId = departmentId });
            }
        }
    }
}
