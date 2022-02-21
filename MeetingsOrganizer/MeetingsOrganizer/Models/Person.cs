namespace MeetingsOrganizer.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<ScheduledMeeting> ScheduledMeetings { get; set; } = new List<ScheduledMeeting>();
    }
}
