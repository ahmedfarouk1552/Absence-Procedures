using System;
using System.Collections.Generic;

#nullable disable

namespace TrainProject.Models
{
    public partial class UserReport
    {
        public int UserId { get; set; }
        public int ReportId { get; set; }
        public DateTime JionTimeOfUser { get; set; }
        public DateTime LeaveTimeOfUser { get; set; }
        public string Duration { get; set; }

        public virtual Report Report { get; set; }
        public virtual User User { get; set; }
    }
}
