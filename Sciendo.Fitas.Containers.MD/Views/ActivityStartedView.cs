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
using MonoCross.Droid;
using Sciendo.Fitas.Model;
using Android.Dialog;
using MonoCross.Navigation;

namespace Sciendo.Fitas.Containers.MD.Views
{
    [Activity(Label = "Current Activity", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class ActivityStartedView : MXDialogActivityView<ActivityInProgress>
    {
        private StringElement _currentDateTime = new StringElement("", string.Format("{0:HH:mm:ss}", DateTime.Now));
        private StringElement _timeLeft;
        System.Timers.Timer _timer = new System.Timers.Timer(1000);
        double _currentSeriesTime;

        public override void Render()
        {
            _timeLeft = new StringElement("Left: ", string.Format("{0}:{1}", 
                Model.ActivityRemainingTime.Minutes, 
                Model.ActivityRemainingTime.Seconds));
            _currentSeriesTime = Model.ActivityRemainingTime.TotalMilliseconds;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;
            this.Root = new RootElement("Current Activity") {
                new Section("Day " + Model.CurrentDay.DayId + " Week " + Model.CurrentDay.WeekId) {
                    new StringElement("Series", Model.SeriesId.ToString()),
                    new StringElement("Status: ", Model.SeriesStatus.ToString()),
                    new StringElement("Time to finish:", string.Format("{0:HH:mm:ss}",Model.TargetFinishBy)),
                    _timeLeft,
                    _currentDateTime
                }
            };

        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RunOnUiThread(delegate
            {

                Model.ActivityRemainingTime = Model.ActivityRemainingTime.Subtract(new TimeSpan(0, 0, 1));
                _currentDateTime.Value = string.Format("{0:HH:mm:ss}", DateTime.Now);
                _timeLeft.Value = string.Format("{0}:{1}", Model.ActivityRemainingTime.Minutes, Model.ActivityRemainingTime.Seconds);
                if (Model.ActivityRemainingTime.TotalMilliseconds <= 0)
                {
                    _timer.Enabled = false;
                    this.Navigate(string.Format("Activity/{0}/CHANGE", Model.SeriesId));
                }
            });
        }
    }
}