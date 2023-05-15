using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompassMeetingApp.Models
{
    public class MeetingRoom
    {
        public int Id { get; set; }
        
        [BindProperty] 
        public string Name { get; set; } = string.Empty;
        [BindProperty] 
        public string Location { get; set; } = string.Empty;
        [BindProperty] 
        public string? DayBooked { get; set; } = string.Empty;
        [BindProperty] 
        public string? TimeBooked { get; set; } = string.Empty;
        [BindProperty] 
        public bool? isBooked { get; set; } = false;
    }
}
