using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;

namespace BusinessLogicLayer
{
    public class DepartmentBll : IDepartmentBll
    {


        private readonly IDepartmentProvider departmentProvider;
        private readonly IDepartmentProcessor departmentProcessor;

        public DepartmentBll(IDepartmentProvider departmentProvider, IDepartmentProcessor departmentProcessor)
        {
            this.departmentProvider = departmentProvider;
            this.departmentProcessor = departmentProcessor;
        }




        public IEnumerable<Department> ShowAllDepartments()
        {

            var departments = departmentProvider.Get();

            return departments;
        }

        public Department GetDepartmentById(long id)
        {
            var department = departmentProvider.GetById(id);
            return department;
        }

        public void CreateDepartment(Department department)
        {
            departmentProcessor.Create(department);

            var lstDepartment = new List<Department>();
            lstDepartment = (List<Department>)departmentProvider.Get();
            lstDepartment.Add(department);
        }

        public void UpdateDepartment(long id, Department department)
        {
            departmentProcessor.Update(department);
        }

        public void DeleteDepartment(long id)
        {
            departmentProcessor.Delete(id);
        }
    }
}
