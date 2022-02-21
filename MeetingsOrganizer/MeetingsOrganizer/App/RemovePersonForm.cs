using MeetingsOrganizer.Models;
using MeetingsOrganizer.Services;

namespace MeetingsOrganizer.App
{
    public class RemovePersonForm
    {
        private PersonService personService = new PersonService();
        private MeetingService meetingService = new MeetingService();

        public void Run(Meeting meeting)
        {
            List<Person> participants = meetingService.GetMeetingParticipants(meeting);
            Console.Clear();
            Console.WriteLine($"{meeting.Name}");
            Console.WriteLine();
            Console.WriteLine("Select one of participant to remove from meeting.");
            ListAllParticipants(participants);
            Console.WriteLine();
            Console.WriteLine("Select meeting participant by entering participant number:");
            string action = Console.ReadLine();

            if(int.TryParse(action, out int x))
            {
                switch (x >= 0 && x < participants.Count && meeting.ResponsiblePerson.Id != participants[x].Id)
                {
                    case true:
                        personService.RemovePersonFromMeeting(meeting, participants[x]);
                        Console.WriteLine($"{participants[x].Name} {participants[x].Surname} removed from a {meeting.Name} meeting.");
                        break;
                    case false:
                        if(meeting.ResponsiblePerson.Id == participants[x].Id)
                        {
                            Console.WriteLine($"Can't remove {participants[x].Name} {participants[x].Surname} as this person is host of this meeting.");
                        }
                        else
                        {
                            Console.WriteLine("There is no person with number you specified.");
                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("No person removed. Returning to meeting menu.");
            }
        }

        private void ListAllParticipants(List<Person> participants)
        {
            for(int i = 0; i < participants.Count; i++)
            {
                Console.WriteLine($"{i} - {participants[i].Name} {participants[i].Surname}");
            }
        }
    }
}
