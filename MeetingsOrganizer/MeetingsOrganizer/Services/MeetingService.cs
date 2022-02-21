using MeetingsOrganizer.Models;
using MeetingsOrganizer.Repositories;

namespace MeetingsOrganizer.Services
{
    public class MeetingService
    {
        private MeetingRepository meetingRepository = new MeetingRepository();
        private PersonRepository personRepository = new PersonRepository();
        private PersonService personService = new PersonService();

        public void CreateMeeting(Meeting meeting)
        {
            meetingRepository.AddNew(meeting);
            personService.AssignPersonToAMeeting(meeting.ResponsiblePerson, meeting, meeting.StartDate);
        }

        public List<Meeting> GetAll()
        {
            return meetingRepository.GetAll();
        }

        public List<Person> GetMeetingParticipants(Meeting meeting)
        {
            return personService.GetAll().Where(p => p.ScheduledMeetings.Select(s => s.MeetingId).Contains(meeting.Id)).ToList();
        }

        public void RemoveMeeting(Meeting meeting)
        {
            meetingRepository.Remove(meeting);
            personService.RemoveScheduledMeeting(meeting);
        }

        public List<Meeting> GetMeetingsByParticipantsCount(int count)
        {
            List<Meeting> filteredMeetings = new List<Meeting>();
            List<Meeting> meetings = GetAll();

            foreach(var meet in meetings)
            {
                int mCount = GetMeetingParticipants(meet).Count();
                if(mCount > count)
                {
                    filteredMeetings.Add(meet);
                }
            }

            return filteredMeetings;
        }

        public List<Meeting> GetMeetingsByType(string type)
        {
            return GetAll().Where(m => m.Type.ToLower() == type).ToList();
        }

        public List<Meeting> GetMeetingsByCategory(string category)
        {
            return GetAll().Where(m => m.Category.ToLower() == category).ToList();
        }

        public List<Meeting> GetByDescriptionTerm(string term)
        {
            Console.WriteLine(GetAll()[0].Description);
            return GetAll().Where(m => m.Description.ToLower().Contains(term)).ToList();
        }

        public List<Meeting> GetByPersonName(string fullName)
        {
            return meetingRepository.GetAll().Where(m => (m.ResponsiblePerson.Name.ToLower() + " " + m.ResponsiblePerson.Surname.ToLower()) == fullName).ToList();
        }

        public List<Meeting> GetByDate(DateTime date)
        {
            Console.WriteLine(date.ToString());
            return meetingRepository.GetAll().Where(m => m.StartDate <= date && date <= m.EndDate).ToList();
        }
    }
}
