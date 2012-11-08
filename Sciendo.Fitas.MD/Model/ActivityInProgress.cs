using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sciendo.Fitas.Model
{
    public class ActivityInProgress
    {
        public TimeSpan ActivityRemainingTime { get; set; }
        public int SeriesId { get; set; }
        public SeriesStatus SeriesStatus { get; set; }
        public DateTime TargetFinishBy { get; set; }
        public Day CurrentDay { get; set; }
    }
}
