using System.Data.SqlClient;
using Dapper;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public class UserProcessor : IUserProcessor
    {
        //private readonly string connectionString;

        //public UserProcessor(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}


        private readonly string FlexDeskConnection;

        public UserProcessor(IDBConnection dbConnection)
        {
            this.FlexDeskConnection = dbConnection.connectionString;
        }

        public void Create(User user)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "INSERT INTO [dbo].[User] (FirstName, Name, Login, Email, Password, StartDate, EndDate, DefaultDesk, Phone, Administrator, DepartmentID) VALUES (@FirstName, @Name, @Login, @Email, @Password, @StartDate, @EndDate, @DefaultDesk, @Phone, @Administrator, @DepartmentID)", 
                    new {user.FirstName, user.Name, user.Login, user.Email, user.Password, user.StartDate, user.EndDate, user.DefaultDesk, user.Phone, user.Administrator, user.DepartmentId});

            }
        }


       public void Update(User user)

        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "UPDATE [dbo].[User] SET FirstName=@FirstName, Name=@Name, Login=@Login, Email=@Email, Password=@Password, StartDate=@StartDate, EndDate=@EndDate, DefaultDesk=@DefaultDesk, Phone=@Phone, Administrator=@Administrator, DepartmentId=@DepartmentId WHERE userId=@UserId",
                    new { user.UserId, user.FirstName, user.Name, user.Login, user.Email, user.Password, user.StartDate, user.EndDate, user.DefaultDesk, user.Phone, user.Administrator, user.DepartmentId });
            }
        }

        public void Delete(long userId)
        {
            using (var connection = new SqlConnection(FlexDeskConnection))
            {
                connection.Execute(
                    "DELETE FROM [dbo].[User] WHERE userId=@UserId",
                    new { userId = userId });
            }
        }
    }
}
