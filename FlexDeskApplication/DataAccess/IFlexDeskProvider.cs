using System.Collections.Generic;
using DataAccessLayer.Model.FlexDeskDb;

namespace DataAccessLayer
{
    public interface IFlexDeskProvider
    {
        IEnumerable<FlexDesk> Get();

        //List<FlexDesk> GetAll();
        //bool Add(FlexDesk flexdesk);
        FlexDesk GetById(long flexdeskId);

    }
}
