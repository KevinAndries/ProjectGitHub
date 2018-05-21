using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;

namespace BusinessLogicLayer
{
    public class AbsenceBll : IAbsenceBll
    {
        private readonly IAbsenceProvider absenceProvider;
        private readonly IAbsenceProcessor absenceProcessor;

        public AbsenceBll(IAbsenceProvider absenceProvider, IAbsenceProcessor absenceProcessor)
        {
            this.absenceProvider = absenceProvider;
            this.absenceProcessor = absenceProcessor;
        }

        public IEnumerable<Absence> ShowAllAbsences()
        {
            var absences = absenceProvider.Get();


            return absences;
        }




        public Absence GetAbsenceById(long id)
        {
            var absence = absenceProvider.GetById(id);
            return absence;
        }

        public void CreateAbsence(Absence absence)
        {
            {
                absenceProcessor.Create(absence);

                var lstAbsence = new List<Absence>();
                lstAbsence = (List<Absence>) absenceProvider.Get();
                lstAbsence.Add(absence);
            }
        }

        public void UpdateAbsence(long id, Absence absence)
        {
            absenceProcessor.Update(absence);
            }

        public void DeleteAbsence(long id)
        {
            absenceProcessor.Delete(id);
            }
    }
}
