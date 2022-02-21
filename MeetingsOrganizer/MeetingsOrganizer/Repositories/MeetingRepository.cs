using MeetingsOrganizer.Models;
using Newtonsoft.Json;

namespace MeetingsOrganizer.Repositories
{
    public class MeetingRepository
    {
        private string meetingsData = "./meetingsData.json";

        public MeetingRepository()
        {
            if (!File.Exists(meetingsData))
            {
                File.WriteAllText(meetingsData, "[]");
            }
        }

        public void AddNew (Meeting meeting)
        {
            List<Meeting> meetings = GetAll();
            if(meetings.Count == 0)
            {
                meeting.Id = 1;
            }
            else
            {
                meeting.Id = meetings.Select(m => m.Id).Max() + 1;
            }
            
            meetings.Add(meeting);

            Update(meetings);
        }

        public void Remove(Meeting meeting)
        {
            List<Meeting> meetings = GetAll();
            meetings = meetings.Where(p => p.Id == meeting.Id).ToList();

            Update(meetings);
        }

        public List<Meeting> GetAll()
        {
            return JsonConvert.DeserializeObject<List<Meeting>>(File.ReadAllText(meetingsData));
            
        }

        private void Update(List<Meeting> updatedMeeting)
        {
            string json = JsonConvert.SerializeObject(updatedMeeting);
            File.WriteAllText(meetingsData, json);
        }
    }
}