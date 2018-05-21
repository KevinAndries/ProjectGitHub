using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IDepartmentProcessor
    {
       void Create(Department department);

        void Update(Department department);

        void Delete(long departmentId);
    }
}
