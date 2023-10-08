using Microsoft.AspNetCore.Mvc.Rendering;

namespace alpha3.Models
{
    public class ScheduleCreateViewModel
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string CoachId { get; set; }

        // Assuming you want to populate a dropdown list with available coaches
        public List<SelectListItem> AvailableCoaches { get; set; }
    }
}
