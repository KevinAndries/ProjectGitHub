using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class UserProvider : IUserProvider
    {



        private readonly string FlexDeskConnection;

        public UserProvider(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }


        public IEnumerable<User> Get()
        {

            IEnumerable<User> users;


            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                users = connection.Query<User>(
                    "SELECT [UserID],[FirstName],[Name],[Login],[Email],[Password],[StartDate],[EndDate],[DefaultDesk],[Phone],[Administrator],[DepartmentID] FROM [dbo].[User]");

                var departments = connection.Query<Department>(
                    "SELECT * FROM Department WHERE DepartmentID in @Ids",
                    new {Ids = users.Select(c => c.DepartmentId).Distinct()});
                foreach (var user in users)
                {
                    user.Department = departments.Single((x => x.DepartmentId == user.DepartmentId));
                }
            }

            return users;
        }





        public User GetById(long userId)
        {

            User user;
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                user = connection.Query<User>("SELECT UserID,FirstName,Name,Login,Email,Password,StartDate,EndDate,DefaultDesk,Phone,Administrator,DepartmentID FROM [dbo].[User] WHERE userId=@UserID",
                    new {userId = userId}).FirstOrDefault();
            }

            return user;
        }


    }
}
