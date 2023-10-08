using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace alpha3.Models
{
    public class Schedule
    {
        // Primary Key
        public int Id { get; set; }

        // Basic properties
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        // Relationship to Coach (ApplicationUser) without CoachId
        public virtual ApplicationUser Coach { get; set; }

        // Many-to-many relationship to ApplicationUser
        public virtual ICollection<MemberSchedule> MemberSchedules { get; set; } = new List<MemberSchedule>();
        [NotMapped]
        public virtual ICollection<ApplicationUser> EnrolledMembers { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        // Constructor to initialize collections
        public Schedule()
        {
            MemberSchedules = new List<MemberSchedule>();
        }
    }
}
