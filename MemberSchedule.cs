using alpha3.Models;
using System.ComponentModel.DataAnnotations;

public class MemberSchedule
{
    [Key]
    public int MemberScheduleId { get; set; }

    public string MemberId { get; set; }  // assuming the primary key for Member is of type string
    public int ScheduleId { get; set; }   // assuming the primary key for Schedule is of type int

    public virtual ApplicationUser Member { get; set; }
    public virtual Schedule Schedule { get; set; }
}
