
using Microsoft.AspNetCore.Identity;
using System;

namespace alpha3.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfilePictureUrl { get; set; }
        public UserType UserType { get; set; }

        // Adding the CoachProfile property
        public int? CoachProfileId { get; set; }
        public virtual CoachProfile CoachProfile { get; set; }

        public virtual ICollection<MemberSchedule> MemberSchedules { get; set; } = new List<MemberSchedule>();

        public virtual ICollection<Schedule> Schedules { get; set; }

       
    }

    public enum UserType
    {
        Member,
        Coach,
        Admin
    }
}
