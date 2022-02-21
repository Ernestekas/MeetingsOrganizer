using MeetingsOrganizer.Models;
using MeetingsOrganizer.Services;

namespace MeetingsOrganizer.App
{
    public class SelectMeetingForm
    {
        private SelectedMeetingMenuForm selectedMeetingForm = new SelectedMeetingMenuForm();
        private MeetingService meetingService = new MeetingService();
        private PersonService personService = new PersonService();
        private List<Meeting> meetings;

        public void Run(Person user)
        {
            bool runSelect = true;
            while (runSelect)
            {
                meetings = meetingService.GetAll();
                Console.Clear();
                Console.WriteLine("Enter a command. Type meeting number to select that meeting or type 'exit' return to main menu.");
                ListAllMeetings();

                string action = Console.ReadLine();
                switch (action)
                {
                    case "exit":
                        runSelect = false;
                        break;
                    default:
                        TrySelectList(action, user);
                        break;
                }
            }
        }

        private void ListAllMeetings()
        {
            List<Person> people = personService.GetAll();
            List<ScheduledMeeting> schedules = people.SelectMany(s => s.ScheduledMeetings).ToList();

            for (int i = 0; i < meetings.Count; i++)
            {
                Meeting meeting = meetings[i];
                Console.WriteLine($"" +
                    $"{i}. " +
                    $"{meeting.Name} - " +
                    $"{meeting.Description} - " +
                    $"{meeting.ResponsiblePerson.Name} " +
                    $"{meeting.ResponsiblePerson.Surname} - " +
                    $"{meeting.Category} - " +
                    $"{meeting.Type} - From: " +
                    $"{meeting.StartDate.ToShortDateString()} To: " +
                    $"{meeting.EndDate.ToShortDateString()} - Participants: {schedules.Count(p => p.MeetingId == meeting.Id)}"
                    );
            }
        }

        private void TrySelectList(string action, Person user)
        {
            if(int.TryParse(action, out int x))
            {
                switch (x >= 0 && x < meetings.Count)
                {
                    case true:
                        selectedMeetingForm.Run(meetings[x], user);
                        break;
                    case false:
                        Console.WriteLine("Why you doing this? No such list number.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid command.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
