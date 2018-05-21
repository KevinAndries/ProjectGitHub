using System.Collections.Generic;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IAbsenceProvider
    {
        IEnumerable<Absence> Get();

        //List<Absence> GetAll();
        //bool Add(Absence absence);
        Absence GetById(long absenceId);

    }
}
