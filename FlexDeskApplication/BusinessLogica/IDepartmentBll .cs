using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public interface IDepartmentBll
    {
        IEnumerable<Department> ShowAllDepartments();
        Department GetDepartmentById(long id);
        void CreateDepartment(Department department);
        void UpdateDepartment(long id, Department department);
        void DeleteDepartment(long id);
    }
}
