using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public interface IAbsenceBll
    {
        IEnumerable<Absence> ShowAllAbsences();
        Absence GetAbsenceById(long id);
        void CreateAbsence(Absence absence);
        void UpdateAbsence(long id, Absence absence);
        void DeleteAbsence(long id);
    }
}
