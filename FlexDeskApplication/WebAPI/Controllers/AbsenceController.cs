using System.Collections.Generic;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/Absence")]
    public class AbsenceController : Controller
    {


            
        private readonly IAbsenceBll absenceBll;


        public AbsenceController(IAbsenceBll absenceBll)
        {
            this.absenceBll = absenceBll;
        }




        // GET api/Absence
        [HttpGet]
        public IEnumerable<Absence> Get()
        {
            return absenceBll.ShowAllAbsences();
        }

        // GET api/Absence/5
        [HttpGet("{id}", Name = "AbsenceGet")]
        public Absence Get(int id)
        {
            return absenceBll.GetAbsenceById(id);
        }

        // POST api/Absence
        [HttpPost]
        public void Post([FromBody]Absence absence)
        {
            absenceBll.CreateAbsence(absence);

        }

        // PUT api/Absence/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Absence absence)
        {
            absence.AbsenceId = id;
            absenceBll.UpdateAbsence(id, absence);
        }

        // DELETE api/Absence/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            absenceBll.DeleteAbsence(id);
            //absenceProcessor.Delete(id);
        }
    }
}