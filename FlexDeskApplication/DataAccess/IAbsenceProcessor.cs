using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IAbsenceProcessor
    {
       void Create(Absence absence);

        void Update(Absence absence);

        void Delete(long absenceId);
    }
}
