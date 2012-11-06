using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoCross.Navigation;
using Sciendo.Fitas.Model;
using Sciendo.Fitas.Data;

namespace Sciendo.Fitas.Controllers
{
    public class WeeksListController : MXController<List<WeekSummary>>
    {
        public override string Load(Dictionary<string, string> parameters)
        {
            string perspective = ViewPerspective.Default;

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
                        Model = XmlDataStore.GetWeekHistorySummaries();
                        break;
                    }
            }
            return perspective;
        }
    }
}
