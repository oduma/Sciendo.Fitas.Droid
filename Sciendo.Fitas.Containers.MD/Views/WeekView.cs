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
    [Activity(Label = "Week", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class WeekView: MXListActivityView<WeekDetail>
    {
        class DayAdapter : ArrayAdapter<Day>
        {
            List<Day> _items;

            public DayAdapter(Context context, int textViewResourceId, List<Day> items)
                : base(context, textViewResourceId, items)
            {
                _items = items;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View v = convertView;
                if (v == null)
                {
                    LayoutInflater li = (LayoutInflater)this.Context.GetSystemService(Context.LayoutInflaterService);
                    v = li.Inflate(Android.Resource.Layout.SimpleListItem2, null);
                }

                Day day = _items[position];

                if (day != null)
                {
                    TextView tt = (TextView)v.FindViewById(Android.Resource.Id.Text1);
                    if (tt != null)
                        tt.Text = "Day " + day.DayId.ToString();
                    TextView bt = (TextView)v.FindViewById(Android.Resource.Id.Text2);
                    if (bt != null && day.Status.ToString() != null)
                        bt.Text = string.Format(" No of series {0} {1}", day.WeekSummary.NoOfSerries / 2, day.Status);
                }
                return v;

            }
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {

            base.OnListItemClick(l, v, position, id);
            this.Navigate(string.Format("Day/{0},{1}", Model.WeekId,Model.DaySummaries[position].DayId));

        }

        public override void Render()
        {
            Title = string.Format("Week {0} (Run: {1} min - Walk {2} min)x{3} {4}", Model.WeekId,
                Model.RunDue, Model.WalkDue, Model.NoOfSerries, Model.Status);
            ListView.Adapter = new DayAdapter(this, 0, Model.DaySummaries);

        }
    }
}