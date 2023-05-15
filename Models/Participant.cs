using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompassMeetingApp.Models
{
    public class Participant
    {
        public int Id { get; set; }
        [BindProperty]
        public string FirstName { get; set; } = string.Empty;
        [BindProperty]
        public string LastName { get; set; } = string.Empty;
        [BindProperty]
        public int? RoomIdBooked { get; set; } = 0;
        [BindProperty]
        public bool? BookedARoom { get; set; } = false;


        public List<SelectListItem> Status { set; get; }

        public int? SelectedStatus { set; get; }

    }
}
