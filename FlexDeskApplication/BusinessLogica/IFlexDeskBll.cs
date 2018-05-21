using DataAccessLayer.Model.FlexDeskDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public interface IFlexDeskBll
    {
        IEnumerable<FlexDesk> ShowAllFlexdesks();
        FlexDesk GetFlexDeskById(long id);
        void CreateFlexDesk(FlexDesk flexdesk);
        void UpdateFlexDesk(long id, FlexDesk flexdesk);
        void DeleteFlexDesk(long id);
    }
}
