using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace alpha3.Models
{
    public class Coach
    {
        [Key]
       

        public string Name { get; set; }
        public string Biography { get; set; }
        public virtual List<Schedule> CoachingSchedules { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public Coach()
        {
            Name = string.Empty;
            Biography = string.Empty;
            CoachingSchedules = new List<Schedule>();
        }
    }
}
