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
    [Activity(Label = "Week", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class WeekView: MXDialogActivityView<WeekSummary>
    {

        public override void Render()
        {
            this.Root = new RootElement("Week") {
                new Section("Week ") {
                }
            };
        }
    }
}