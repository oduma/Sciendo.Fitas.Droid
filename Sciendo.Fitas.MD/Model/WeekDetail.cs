using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sciendo.Fitas.Model
{
    public class WeekDetail: WeekSummary
    {
        public List<Day> DaySummaries { get; set; }
    }
}
