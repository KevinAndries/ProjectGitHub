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

        //private readonly IAbsenceProvider absenceProvider;
        //private readonly IAbsenceProcessor absenceProcessor;
        //private readonly IUserProvider userProvider;

        //private readonly IAbsenceBll absenceBll;


        //public AbsenceController(IAbsenceProvider absenceProvider, IAbsenceProcessor absenceProcessor, IUserProvider userProvider, IAbsenceBll absenceBll)
        //{
        //    this.absenceProvider = absenceProvider;
        //    this.absenceProcessor = absenceProcessor;
        //    this.userProvider = userProvider;

        //    this.absenceBll = absenceBll;

        //}

            
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

            //var absence = absenceBll.GetAbsenceById(id);
            //absence.User = userProvider.GetById(absence.UserId);
            //return absence;
            return absenceBll.GetAbsenceById(id);

        }

        // POST api/Absence
        [HttpPost]
        public void Post([FromBody]Absence absence)
        {
            absenceBll.CreateAbsence(absence);
            //absence.User = userProvider.GetById(absence.UserId);
            //absenceProcessor.Create(absence);
        }

        // PUT api/Absence/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Absence absence)
        {
            absence.AbsenceId = id;
            absenceBll.UpdateAbsence(id, absence);
            //absenceProcessor.Update(absence);
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