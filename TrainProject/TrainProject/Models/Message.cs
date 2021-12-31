using System;
using System.Collections.Generic;

#nullable disable

namespace TrainProject.Models
{
    public partial class Message
    {
        public int MessId { get; set; }
        public int Usid { get; set; }
        public int Urid { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; }

        public virtual User Ur { get; set; }
        public virtual User Us { get; set; }
    }
}
