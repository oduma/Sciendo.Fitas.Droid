using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoCross.Navigation;
using Sciendo.Fitas.Model;
using Sciendo.Fitas.Data;

namespace Sciendo.Fitas.Controllers
{
    public class DayController : MXController<Day>
    {
        public override string Load(Dictionary<string, string> parameters)
        {
            string perspective = ViewPerspective.Default;

            int weekId;
            int dayId;
            string dId=null;
            string wId = null;
            parameters.TryGetValue("DayId", out dId);
            parameters.TryGetValue("WeekId", out wId);
            int.TryParse(wId,out weekId);
            int.TryParse(dId,out dayId);

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
                        Model = XmlDataStore.GetWeekDays(
                            XmlDataStore.GetWeekHistorySummaries().First(w => w.WeekId == weekId)).First(d=>d.DayId==dayId);

                        break;
                    }
            }
            return perspective;
        }

    }
}
