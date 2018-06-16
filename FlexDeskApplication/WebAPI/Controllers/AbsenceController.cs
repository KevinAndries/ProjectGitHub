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


        //Ophalen BusinessLogica die doorgegeven wordt aan de WebApi Controller Klasse om een dependency te creëeren    
        private readonly IAbsenceBll absenceBll;


        public AbsenceController(IAbsenceBll absenceBll)
        {
            this.absenceBll = absenceBll;
        }


        //Hieronder wordt de routering bepaalt door bepaalde action methods toe te wijzen. 
        //Deze zullen dan de requesten die binnenkomen behandelen en de juiste routering parameters meegeven (=attributeRouting)

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
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
        public void Post([FromBody]Absence absence)
        {
            absenceBll.CreateAbsence(absence);

        }

        // PUT api/Absence/5
        [HttpPut("{id}")]
        //implementatie REST protocol voor met de JSON code langs front-end overweg te kunnen [FromBody]
        //Het [FromBody] attribuut zal gebruikt worden om het content type te bepalen
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