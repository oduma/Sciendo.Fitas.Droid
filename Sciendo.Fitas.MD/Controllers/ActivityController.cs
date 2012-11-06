using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoCross.Navigation;
using Sciendo.Fitas.Model;
using Sciendo.Fitas.Data;

namespace Sciendo.Fitas.Controllers
{
    public class ActivityController:MXController<ActivityInProgress>
    {
        public override string Load(Dictionary<string, string> parameters)
        {
            string perspective = 
                ViewPerspective.Default;

            string seriesId = null;
            parameters.TryGetValue("SeriesId", out seriesId);

            int weekId=0;
            int dayId=0;

            if (string.IsNullOrEmpty(seriesId))
            {
                string sDayId = null;
                string sWeekId = null;
                //probably was launched with a specific day in mind
                parameters.TryGetValue("DayId", out sDayId);
                parameters.TryGetValue("WeekId", out sWeekId);

                int.TryParse(sDayId, out dayId);
                int.TryParse(sWeekId, out weekId);
            }
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
                        GetWeekAndDay(out dayId, out weekId);
                        Model = GetSeriesForDay(dayId, weekId);
                        break;
                    }
                case "START":
                    {
                        if(dayId!=0 && weekId!=0)
                            Model = GetSeriesForDay(dayId,weekId);
                        Model.CurrentDay.StartedAt = DateTime.Now;
                        Model.SeriesStatus = SeriesStatus.Running;
                        Model.ActivityRemainingTime = new TimeSpan(0, 0, Model.CurrentDay.WeekSummary.RunDue);
                        Model.TargetFinishBy = Model.CurrentDay.StartedAt.Value.AddSeconds(Model.CurrentDay.WeekSummary.RunDue);

                        perspective = "STARTED";
                        break;
                    }
                case "CHANGE":
                    {
                        if (FinishSeries(Convert.ToInt32(seriesId)))
                        {
                            MXContainer.Instance.Redirect("Activity/DONE");
                            //Finished the whole day so go to home
                        }
                        else
                            //continue with the next series
                            perspective = "STARTED";
                        break;
                    }
                case "DONE":
                    {
                        perspective = "DONE";
                        break;
                    }
            }
            return perspective;
        }

        private bool FinishSeries(int requestedDayId)
        {
            if (Model.CurrentDay.WeekSummary.NoOfSerries > Model.SeriesId)
            {
                bool running = (Model.SeriesStatus == SeriesStatus.Running);
                AdvanceToNewSeries(running ?
                    SeriesStatus.Walking : SeriesStatus.Running, running ?
                    Model.CurrentDay.WeekSummary.WalkDue : Model.CurrentDay.WeekSummary.RunDue,Model.SeriesId+1);
                return false;
            }
            else
            {
                Model.CurrentDay.FinishedAt = DateTime.Now;
                CalculateDayPerformance();
                XmlDataStore.SaveFinishedDay(Model.CurrentDay);
                if (Model.CurrentDay.DayId == Model.CurrentDay.WeekSummary.NoOfDays)
                    Model.CurrentDay.WeekSummary.Status = Status.Done;
                else
                    Model.CurrentDay.WeekSummary.Status = Status.InProgress;
                XmlDataStore.SaveWeekHistory(Model.CurrentDay.WeekSummary);
                return true;
            }
            
        }

        private void CalculateDayPerformance()
        {
            Model.CurrentDay.Distance = 0;
            Model.CurrentDay.Speed = 0;
            Model.CurrentDay.NoOfSteps = 0;
            Model.CurrentDay.Status = Status.Done;
        }

        private void AdvanceToNewSeries(SeriesStatus newSeriesStatus, int seriesLength, int newSeriesId)
        {
            Model.SeriesId = newSeriesId;
            Model.SeriesStatus = newSeriesStatus;
            Model.ActivityRemainingTime = new TimeSpan(0, 0, seriesLength);
            Model.TargetFinishBy = Model.TargetFinishBy.AddSeconds(seriesLength);
        }

        ActivityInProgress GetSeriesForDay(int dayId, int weekId)
        {

            WeekSummary weekSummary=XmlDataStore.GetWeekSummaries(weekId).First();


            return new ActivityInProgress
            {
                SeriesId = 1,
                SeriesStatus = SeriesStatus.NotStarted,
                CurrentDay =
                new Day
                {
                    DayId = dayId,
                    WeekId = weekId,
                    WeekSummary = weekSummary,
                }
            };
        }

        private static void GetWeekAndDay(out int dayId, out int weekId)
        {

            weekId = 1;
            Day day = XmlDataStore.GetLastFinishedDay();
            if (day == null)
            {
                dayId = 1;
                weekId = 1;
            }
            else if (day.DayId == 5)
            {
                //jump to next week
                dayId = 1;
                weekId = day.WeekId + 1;
            }
            else
            {
                dayId = day.DayId + 1;
                weekId = day.WeekId;
            }

        }


    }
}
