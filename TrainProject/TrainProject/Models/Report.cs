using System;
using System.Collections.Generic;

#nullable disable

namespace TrainProject.Models
{
    public partial class Report
    {
        public Report()
        {
            UserReports = new HashSet<UserReport>();
        }

        public int Id { get; set; }
        public int? DurationOfMeeting { get; set; }
        public DateTime MeetingStartTime { get; set; }
        public DateTime MeetingEndTime { get; set; }

        public virtual ICollection<UserReport> UserReports { get; set; }
    }
}
