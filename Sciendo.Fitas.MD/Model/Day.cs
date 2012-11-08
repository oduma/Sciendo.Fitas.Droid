using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Sciendo.Fitas.Model
{
    public class Day
    {
        public int DayId { get; set; }
        public Status Status { get; set; }
        public int WeekId { get; set; }

        public decimal Distance { get; set; }
        public int NoOfSteps { get; set; }
        public decimal Speed { get; set; }

        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public WeekSummary WeekSummary { get; set; }
    }
}
