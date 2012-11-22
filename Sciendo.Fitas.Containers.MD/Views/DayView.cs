using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Views;
using MonoCross.Droid;
using Sciendo.Fitas.Model;
using Android.Dialog;
using MonoCross.Navigation;
using Sciendo.Fitas.Container.MD.Facebook;

namespace Sciendo.Fitas.Containers.MD.Views
{
    [Activity(Label = "Day", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class DayView:MXDialogActivityView<Day>
    {
        public override void Render()
        {
            this.Root = new RootElement(string.Format("Day {0} Week {1}", Model.DayId, Model.WeekId)) {
                new Section(string.Format("(Run: {0} min - Walk {1} min)x{2}", 
                    Model.WeekSummary.RunDue, Model.WeekSummary.WalkDue, Model.WeekSummary.NoOfSerries)) {
                    new StringElement("Status:", Model.Status.ToString()),
                    new StringElement("Started at:", Model.StartedAt.ToString()),
                    new StringElement("Finished at:", Model.FinishedAt.ToString()),
                    new StringElement("Distance:",Model.Distance.ToString()),
                    new StringElement("Speed:",Model.Speed.ToString()),
                    new StringElement("No of steps:",Model.NoOfSteps.ToString())
                }
            };
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            
            MenuInflater.Inflate((Model.Status==Status.Done)?Resource.Menu.menu_day_done:Resource.Menu.menu_day_not_done, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.start:
                    {
                        this.Navigate(string.Format("Activity/{0},{1}/GET", Model.WeekId, Model.DayId));
                        return true;
                    }
                case Resource.Id.facebook:
                    {
                        string message = Resources.GetString(Resource.String.FacebookMessage);
                        using(Facebook faceBook = new Facebook((Activity)this))
                        {
                            faceBook.PostUpdate(message);
                            return true;
                        }
                    }
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
