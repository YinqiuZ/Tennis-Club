using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace alpha3.Models 
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public List<MemberSchedule> MemberSchedules { get; set; }

        public Member()
        {
            Name = string.Empty;
            Email = string.Empty;
            MemberSchedules = new List<MemberSchedule>();
        }
    }
}
