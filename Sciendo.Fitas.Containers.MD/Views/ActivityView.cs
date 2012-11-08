using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Sciendo.Fitas.Model;
using MonoCross.Droid;

using MonoCross.Navigation;
using Android.Dialog;


namespace Sciendo.Fitas.Containers.MD.Views
{
    [Activity(Label = "Current Activity", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class ActivityView:MXDialogActivityView<ActivityInProgress>
    {
        private StringElement _currentDateTime= new StringElement("",string.Format("{0:HH:mm:ss}",DateTime.Now));
        System.Timers.Timer _timer = new System.Timers.Timer(1000);
        public override void Render()
        {
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;

            this.Root = new RootElement("Current Activity") {
                new Section("Day " + Model.CurrentDay.DayId + " Week " + Model.CurrentDay.WeekId) {
                    new StringElement("Series", Model.SeriesId.ToString()),
                    new StringElement("Run: ", Model.CurrentDay.WeekSummary.RunDue.ToString()),
                    new StringElement("Walk", Model.CurrentDay.WeekSummary.WalkDue.ToString()),
                    new StringElement("Repeat:",Model.CurrentDay.WeekSummary.NoOfSerries.ToString()),
                    _currentDateTime
                }
            };

        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RunOnUiThread(delegate
            {
                _currentDateTime.Value = string.Format("{0:HH:mm:ss}", DateTime.Now);
            });
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_activity, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.start_activity:
                    {
                        this.Navigate(string.Format("Activity/START"));
                        return true;
                    }
                case Resource.Id.history:
                    {
                        this.Navigate(string.Format("Weeks"));
                        return true;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}