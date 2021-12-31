using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TrainProject.Models
{
    public partial class User
    {
        public User()
        {
            MessageUrs = new HashSet<Message>();
            MessageUs = new HashSet<Message>();
            UserReports = new HashSet<UserReport>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? UserRolesId { get; set; }
        public int? MessId { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }

        public virtual UserRole UserRoles { get; set; }
        public virtual ICollection<Message> MessageUrs { get; set; }
        public virtual ICollection<Message> MessageUs { get; set; }
        public virtual ICollection<UserReport> UserReports { get; set; }
    }
}
