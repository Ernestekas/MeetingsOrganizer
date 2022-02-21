namespace MeetingsOrganizer.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Person ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
