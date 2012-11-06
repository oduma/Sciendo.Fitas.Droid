using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoCross.Navigation;
using Sciendo.Fitas.Model;
using Sciendo.Fitas.Data;

namespace Sciendo.Fitas.Controllers
{
    public class WeekController : MXController<WeekDetail>
    {
        public override string Load(Dictionary<string, string> parameters)
        {
            string perspective = ViewPerspective.Default;

            int weekId;
            string wId=null;
            parameters.TryGetValue("WeekId", out wId);
            int.TryParse(wId,out weekId);

            // get the action, assumes 
            string action;
            if (!parameters.TryGetValue("Action", out action))
            {
                // set default action if none specified
                action = "GET";
            }

            switch (action)
            {
                case "GET":
                    {
                        Model = GetWeekDetails(weekId);
                        break;
                    }
            }
            return perspective;
        }

        private WeekDetail GetWeekDetails(int weekId)
        {
            var weekSummary = XmlDataStore.GetWeekHistorySummaries().FirstOrDefault(h=>h.WeekId==weekId);

            var weekDetail = new WeekDetail { NoOfDays = weekSummary.NoOfDays, 
                NoOfSerries = weekSummary.NoOfSerries, 
                RunDue = weekSummary.RunDue, 
                Status = weekSummary.Status, 
                WalkDue = weekSummary.WalkDue, 
                WeekId = weekId };
            weekDetail.DaySummaries=XmlDataStore.GetWeekDays(weekSummary);
            return weekDetail;
        }
    }
}
