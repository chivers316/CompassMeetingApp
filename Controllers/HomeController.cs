using CompassMeetingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Net;

namespace CompassMeetingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<MeetingRoom> GetMeetingRooms()
        {
            List<MeetingRoom> MeetingRooms = new List<MeetingRoom>();
            string sql = "";
            try
            {
                sql = "SELECT Id, Name, Location, DayBooked, TimeBooked, isBooked From MeetingRoom";

                using (SqliteConnection con = new SqliteConnection("Data Source=sqlite.db"))
                {
                    using (SqliteCommand cmd = new SqliteCommand(sql, con))
                    {
                        con.Open();
                        SqliteDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            MeetingRoom NewMeetingRoom = new MeetingRoom();
                            NewMeetingRoom.Id = Convert.ToInt32(reader["Id"]);
                            NewMeetingRoom.Name = Convert.ToString(reader["Name"]);
                            NewMeetingRoom.Location = Convert.ToString(reader["Location"]);
                            NewMeetingRoom.DayBooked = Convert.ToString(reader["DayBooked"]);
                            NewMeetingRoom.isBooked = Convert.ToBoolean(reader["isBooked"]);

                            MeetingRooms.Add(NewMeetingRoom);
                        }
                    }
                }
                return MeetingRooms;
            }
            catch (Exception e)
            {
                string error = e.Message;
                return null;
            }
        }

        public List<Participant> GetParticipants()
        {

            List<Participant> Participants = new List<Participant>();
            string sql = "";
            try
            {
                sql = "SELECT Id, FirstName, LastName, RoomIdBooked, BookedARoom From Participants";

                using (SqliteConnection con = new SqliteConnection("Data Source=sqlite.db"))
                {
                    using (SqliteCommand cmd = new SqliteCommand(sql, con))
                    {
                        con.Open();
                        SqliteDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Participant NewParticipant = new Participant();
                            NewParticipant.Id = Convert.ToInt32(reader["Id"]);
                            NewParticipant.FirstName = Convert.ToString(reader["FirstName"]);
                            NewParticipant.LastName = Convert.ToString(reader["LastName"]);
                            NewParticipant.RoomIdBooked = Convert.ToInt32(reader["RoomIdBooked"] as int?);
                            NewParticipant.BookedARoom = Convert.ToBoolean(reader["BookedARoom"] as bool?);

                            Participants.Add(NewParticipant);
                        }
                    }
                }
                return Participants.ToList();
            }
            catch (Exception e)
            {
                string error = e.Message;
                return null;
            }
        }

        public ActionResult GetParticipantList(string q)
        {
            List<Participant> Participants = new List<Participant>();
            string sql = "";
            try
            {
                sql = "SELECT Id, FirstName, LastName, RoomIdBooked, BookedARoom From Participants";

                using (SqliteConnection con = new SqliteConnection("Data Source=sqlite.db"))
                {
                    using (SqliteCommand cmd = new SqliteCommand(sql, con))
                    {
                        con.Open();
                        SqliteDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Participant NewParticipant = new Participant();
                            NewParticipant.Id = Convert.ToInt32(reader["Id"]);
                            NewParticipant.FirstName = Convert.ToString(reader["FirstName"]);
                            NewParticipant.LastName = Convert.ToString(reader["LastName"]);
                            NewParticipant.RoomIdBooked = Convert.ToInt32(reader["RoomIdBooked"] as int?);
                            NewParticipant.BookedARoom = Convert.ToBoolean(reader["BookedARoom"] as bool?);

                            Participants.Add(NewParticipant);
                        }
                    }
                }
                
                var list = Participants;

                if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                {
                    list = list.Where(x => x.LastName.ToLower().StartsWith(q.ToLower())).ToList();
                }
                return Json(new { items = list }, "...SELECT...");
            }
            catch (Exception e)
            {
                string error = e.Message;
                return null;
            }
        }

        public ActionResult RMA(int Id)
        {
            Participant model = new Participant();

            model.Status = new SelectList(model.Status, "ID",
            "Status").ToList();
            //some an other stuff

            return View(model);
        }
    }
}