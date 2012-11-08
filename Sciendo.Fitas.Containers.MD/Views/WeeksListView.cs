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

namespace Sciendo.Fitas.Containers.MD.Views
{
    [Activity(Label = "Weeks List", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class WeeksListView:MXListActivityView<List<WeekSummary>>
    {
        class WeekAdapter:ArrayAdapter<WeekSummary>
        {
            List<WeekSummary> _items;

            public WeekAdapter(Context context, int textViewResourceId, List<WeekSummary> items)
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

                WeekSummary weekSummary = _items[position];

                if (weekSummary != null)
                {
                    TextView tt = (TextView)v.FindViewById(Android.Resource.Id.Text1);
                    if (tt != null)
                        tt.Text = "Week " + weekSummary.WeekId.ToString();
                    TextView bt = (TextView)v.FindViewById(Android.Resource.Id.Text2);
                    if (bt != null && weekSummary.Status.ToString() != null)
                        bt.Text = string.Format("(Run: {0} min - Walk {1} min)x{2} {3}", weekSummary.RunDue, weekSummary.WalkDue, weekSummary.NoOfSerries/2, weekSummary.Status);
                }
                return v;

            }
        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {

            base.OnListItemClick(l, v, position, id);
            this.Navigate(string.Format("Weeks/{0}", Model[position].WeekId));

        }

        public override void Render()
        {
            ListView.Adapter = new WeekAdapter(this, 0, Model);
        }
    }
}