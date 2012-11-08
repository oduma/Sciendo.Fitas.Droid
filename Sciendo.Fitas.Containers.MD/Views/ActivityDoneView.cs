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

namespace Sciendo.Fitas.Containers.MD.Views
{
    [Activity(Label = "Activity Done", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class ActivityDoneView : MXDialogActivityView<ActivityInProgress>
    {
        public override void Render()
        {
            this.Root = new RootElement("Well Done!") {
                new Section("Day " + Model.CurrentDay.DayId + " Week " + Model.CurrentDay.WeekId) {
                    new StringElement("Series", Model.SeriesId.ToString()),
                    new StringElement("Run: ", Model.CurrentDay.WeekSummary.RunDue.ToString()),
                    new StringElement("Walk", Model.CurrentDay.WeekSummary.WalkDue.ToString()),
                    new StringElement("Repeat:",Model.CurrentDay.WeekSummary.NoOfSerries.ToString())
                }
            };
        }
    }
}