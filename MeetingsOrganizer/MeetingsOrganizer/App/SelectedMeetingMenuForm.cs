using MeetingsOrganizer.Models;
using MeetingsOrganizer.Services;

namespace MeetingsOrganizer.App
{
    public class SelectedMeetingMenuForm
    {
        private DeleteMeetingForm deleteForm = new DeleteMeetingForm();
        private AddPersonForm addPersonForm = new AddPersonForm();
        private RemovePersonForm removePersonForm = new RemovePersonForm();

        private MeetingService meetingService = new MeetingService();
        private Meeting meeting;

        public void Run(Meeting selectedMeeting, Person user)
        {
            meeting = selectedMeeting;
            
            bool runMenu = true;
            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine($"{meeting.Name}");
                Console.WriteLine($"{meeting.Description}");
                Console.WriteLine();
                Console.WriteLine("Participants:");
                ListMeetingParticipants();

                Console.WriteLine();
                Console.WriteLine("Select an action. Type 'delete' to delete a meeting, 'aPerson' to add person to a meeting, 'rPerson' to remove a person from a meeting");
                Console.WriteLine("delete, aPerson, rPerson, exit");
                string action = Console.ReadLine();

                switch (action.ToLower())
                {
                    case "delete":
                        deleteForm.Run(selectedMeeting, user);
                        runMenu = false;
                        break;
                    case "aperson":
                        addPersonForm.Run(meeting);
                        break;
                    case "rperson":
                        removePersonForm.Run(meeting);
                        break;
                    case "exit":
                        runMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void ListMeetingParticipants()
        {
            List<Person> people = meetingService.GetMeetingParticipants(meeting);
            for(int i = 0; i < people.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {people[i].Name} {people[i].Surname}");
            }
        }
    }
}
