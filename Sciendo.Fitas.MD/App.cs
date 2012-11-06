using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoCross.Navigation;
using Sciendo.Fitas.Controllers;

namespace Sciendo.Fitas
{
    public class App:MXApplication
    {
        public override void OnAppLoad()
        {
            Title = "Fitness Assistant";
            ActivityController activityController = new ActivityController();
            NavigationMap.Add("Activity", activityController);
            NavigationMap.Add("Activity/{Action}", activityController);
            NavigationMap.Add("Activity/{SeriesId}/{Action}", activityController);
            NavigationMap.Add("Activity/{WeekId},{DayId}/{Action}", activityController);
            NavigationMap.Add("Weeks", new WeeksListController());
            NavigationMap.Add("Weeks/{WeekId}", new WeekController());
            NavigationMap.Add("Day/{WeekId},{DayId}", new DayController());
            NavigateOnLoad = "Activity";
        }
    }
}
