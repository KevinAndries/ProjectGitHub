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


        //Ophalen BusinessLogica die doorgegeven wordt aan de WebApi Controller Klasse om een dependency te creëeren
        private readonly IDepartmentBll departmentBll;

        public  DepartmentController(IDepartmentBll departmentBll)
        {
            this.departmentBll = departmentBll;
        }


        //Hieronder wordt de routering bepaalt door bepaalde action methods toe te wijzen. 
        //Deze zullen dan de requesten die binnenkomen behandelen en de juiste routering parameters meegeven (=attributeRouting)

        // GET api/Department
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            return departmentBll.ShowAllDepartments();       
        }

        // GET api/Department/5
        [HttpGet("{id}", Name = "DeartmentGet")]
        public Department Get(long id)
        {
            return departmentBll.GetDepartmentById(id);
        }

        // POST api/Department
        [HttpPost]
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        public void Post([FromBody]Department department)
        {
            departmentBll.CreateDepartment(department);

        }

        // PUT api/Department/5
        [HttpPut("{id}")]
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        public void Put(long id, [FromBody]Department department)
        {
            department.DepartmentId = id;
            departmentBll.UpdateDepartment(id, department);
        }

        // DELETE api/Department/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            departmentBll.DeleteDepartment(id);
        }
    }
}