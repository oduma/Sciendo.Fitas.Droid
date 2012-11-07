using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using MonoCross.Droid;
using Sciendo.Fitas.Model;
using MonoCross.Navigation;


using System.Collections.Generic;

namespace Sciendo.Fitas.Containers.MD
{
    [Activity(Label = "Fit as you are", MainLauncher = true, 
        Theme = "@android:style/Theme.Black.NoTitleBar", 
        Icon = "@drawable/icon", 
        NoHistory=true)]
    public class SplashScreenActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Splash);

            CheckFiles(ApplicationContext);

            MXDroidContainer.Initialize(new Sciendo.Fitas.App(), this.ApplicationContext);

            // activity view
            MXDroidContainer.AddView<ActivityInProgress>(typeof(Views.ActivityView), ViewPerspective.Default);

            //// activity in progress view
            //MXDroidContainer.AddView<ActivityInProgress>(new Views.ActivityStartedView(), "STARTED");

            //// activity view
            //MXDroidContainer.AddView<ActivityInProgress>(new Views.ActivityDoneView(), "DONE");

            //weeks list view
            MXDroidContainer.AddView<List<WeekSummary>>(typeof(Views.WeeksListView), ViewPerspective.Default);

            //week view
            MXDroidContainer.AddView<WeekDetail>(typeof(Views.WeekView), ViewPerspective.Default);

            ////day view
            //MXDroidContainer.AddView<Day>(new Views.DayView(), ViewPerspective.Default);

            // navigate to first view
            MXDroidContainer.Navigate(null, MXContainer.Instance.App.NavigateOnLoad);


        }

        public void CheckFiles(Context context)
        {
            string documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            string dataDirectory = Path.Combine(documents, "Xml");
            if (!Directory.Exists(dataDirectory))
                Directory.CreateDirectory(dataDirectory);

            string dataFile = Path.Combine(documents, @"Xml/weektemplates.xml");
            if (File.Exists(dataFile))
                return;

            Stream input = context.Assets.Open(@"Xml/weektemplates.xml");
            FileStream output = File.Create(dataFile);
            CopyStream(input, output);
            input.Close();
            output.Close();
        }

        /// <summary>
        /// Copies the contents of input to output. Doesn't close either stream.
        /// </summary>
        public void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

    }

}

