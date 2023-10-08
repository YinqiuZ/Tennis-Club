
using System;
using System.ComponentModel.DataAnnotations;

namespace alpha3.Models
{
    public class CoachProfile
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } // Foreign key to the ApplicationUser
        public virtual ApplicationUser User { get; set; } // Navigation property to the user

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(500)]
        public string Biography { get; set; }

        public DateTime DateJoined { get; set; } = DateTime.Now;

        public string ProfilePictureUrl { get; set; }

        // Additional properties can be added as needed
    }
}
