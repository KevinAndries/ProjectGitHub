using System.Collections.Generic;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IDepartmentProvider
    {
        IEnumerable<Department> Get();

        //List<Department> GetAll();
        //bool Add(Department department);
        Department GetById(long departmentId);

    }
}
