using MeetingsOrganizer.Models;
using MeetingsOrganizer.Repositories;

namespace MeetingsOrganizer.Services
{
    public class PersonService
    {
        private PersonRepository personRepository = new PersonRepository();
        private MeetingRepository meetingRepository = new MeetingRepository();

        public Person CheckPerson(string name, string surname)
        {
            List<Person> people = personRepository.GetAll();
            Person filePerson = people.FirstOrDefault(p => p.Name == name && p.Surname == surname);
            
            if(filePerson == null)
            {
                Person person = new Person()
                {
                    Name = name,
                    Surname = surname
                };

                personRepository.AddNew(person);
                
            }

            return personRepository.GetAll().FirstOrDefault(p => p.Name == name && p.Surname == surname);
        }

        public List<Person> GetAll()
        {
            return personRepository.GetAll();
        }

        public string AssignPersonToAMeeting(Person person, Meeting meeting, DateTime participantDate)
        {
            List<Meeting> allMeetings = meetingRepository.GetAll();

            string returnMessage = "";
            ScheduledMeeting scheduledMeeting = new ScheduledMeeting()
            {
                MeetingId = meeting.Id,
                StartDate = participantDate
            };

            List<Person> people = personRepository.GetAll();
            Person filePerson = people.FirstOrDefault(p => p.Name == person.Name && p.Surname == person.Surname);
            
            returnMessage = CheckSchedules(scheduledMeeting, filePerson);
            
            filePerson.ScheduledMeetings.Add(scheduledMeeting);
            people = people.Where(p => p.Id != filePerson.Id).ToList();
            people.Add(filePerson);

            personRepository.Update(people);

            return returnMessage;
        }

        public void RemoveScheduledMeeting(Meeting meeting)
        {
            List<Person> people = personRepository.GetAll();
            foreach(Person person in people)
            {
                person.ScheduledMeetings = person.ScheduledMeetings.Where(s => s.MeetingId != meeting.Id).ToList();
            }

            personRepository.Update(people);
        }

        public void RemovePersonFromMeeting(Meeting meeting, Person person)
        {
            List<Person> people = personRepository.GetAll().Where(p => p.Id != person.Id).ToList();
            person.ScheduledMeetings = person.ScheduledMeetings.Where(s => s.MeetingId != meeting.Id).ToList();

            people.Add(person);
            personRepository.Update(people);
        }

        private string CheckSchedules(ScheduledMeeting scheduledMeeting, Person person)
        {
            string message = "";
            List<int> meetingIds = person.ScheduledMeetings.Select(p => p.MeetingId).ToList();
            List<Meeting> personMeetings = meetingRepository.GetAll().Where(m => meetingIds.Contains(m.Id)).ToList();
            foreach (Meeting m in personMeetings)
            {
                if (m.StartDate <= scheduledMeeting.StartDate && scheduledMeeting.StartDate <= m.EndDate)
                {
                    message = "This person already assigned to meeting at this date.";
                    break;
                }
            }

            return message;
        }
    }
}
