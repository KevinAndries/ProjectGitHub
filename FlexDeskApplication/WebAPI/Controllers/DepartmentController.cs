using System.Collections.Generic;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/Department")]
    public class DepartmentController : Controller
    {

        //private readonly IDepartmentProvider departmentProvider;
        //private readonly IDepartmentProcessor departmentProcessor;
        //private readonly IDepartmentBll departmentBll;

        //public DepartmentController(IDepartmentProvider departmentProvider, IDepartmentProcessor departmentProcessor, IDepartmentBll departmentBll)
        //{
        //    this.departmentProvider = departmentProvider;
        //    this.departmentProcessor = departmentProcessor;
        //    this.departmentBll = departmentBll;
        //}


        private readonly IDepartmentBll departmentBll;

        public  DepartmentController(IDepartmentBll departmentBll)
        {

            this.departmentBll = departmentBll;
        }



        // GET api/Department
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            //return departmentProvider.Get();
            return departmentBll.ShowAllDepartments();
          
        }

        // GET api/Department/5
        [HttpGet("{id}", Name = "DeartmentGet")]
        public Department Get(long id)
        {
            //return departmentProvider.GetById(id);
            return departmentBll.GetDepartmentById(id);
        }

        // POST api/Department
        [HttpPost]
        public void Post([FromBody]Department department)
        {
            //departmentProcessor.Create(department);
            departmentBll.CreateDepartment(department);

        }

        // PUT api/Department/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Department department)
        {
            department.DepartmentId = id;
            departmentBll.UpdateDepartment(id, department);
            //departmentProcessor.Update(department);
            //return department;
        }

        // DELETE api/Department/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            departmentBll.DeleteDepartment(id);
            //departmentProcessor.Delete(id);
        }
    }
}