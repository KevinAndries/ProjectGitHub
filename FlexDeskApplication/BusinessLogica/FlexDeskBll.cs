using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;

namespace BusinessLogicLayer
{
    public class FlexDeskBll : IFlexDeskBll
    {
        private readonly IFlexDeskProvider flexdeskProvider;
        private readonly IFlexDeskProcessor flexdeskProcessor;

        public FlexDeskBll(IFlexDeskProvider flexdeskProvider, IFlexDeskProcessor flexdeskProcessor)
        {
            this.flexdeskProvider = flexdeskProvider;
            this.flexdeskProcessor = flexdeskProcessor;
        }


        public IEnumerable<FlexDesk> ShowAllFlexdesks()
        {
            var flexdesks = flexdeskProvider.Get();
            return flexdesks;
        }


        public FlexDesk GetFlexDeskById(long id)
        {
            var flexdesk = flexdeskProvider.GetById(id);
            return flexdesk;
        }


        public void CreateFlexDesk(FlexDesk flexdesk)
        {
            flexdeskProcessor.Create(flexdesk);

            var lstFlexDesk = new List<FlexDesk>();
            lstFlexDesk = (List<FlexDesk>)flexdeskProvider.Get();
            lstFlexDesk.Add(flexdesk);
        }


        public void UpdateFlexDesk(long id, FlexDesk flexdesk)
        {
            flexdeskProcessor.Update(flexdesk);
        }

        public void DeleteFlexDesk(long id)
        {
            flexdeskProcessor.Delete(id);
        }
    }
}
