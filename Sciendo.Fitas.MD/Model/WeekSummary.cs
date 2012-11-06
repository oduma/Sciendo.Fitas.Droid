using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sciendo.Fitas.Model
{
    public class WeekSummary
    {
        public static readonly string WEEK_SUMMARIES_TEMPLATE=@"Data\weektemplates.xml";

        public int WeekId { get; set; }

        public int RunDue { get; set; }

        public int WalkDue { get; set; }

        public Status Status { get; set; }

        public int NoOfSerries { get; set; }

        public int NoOfDays { get; set; }
    }
}
