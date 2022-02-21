using MeetingsOrganizer.Models;
using MeetingsOrganizer.Services;

namespace MeetingsOrganizer.App
{
    public class AddPersonForm
    {
        private PersonService personService = new PersonService();
        public void Run(Meeting meeting)
        {
            bool runForm = true;
            while (runForm)
            {
                Console.Clear();
                Console.WriteLine("Add a person to a meeting.");
                Console.WriteLine($"Meeting time from {meeting.StartDate.ToShortDateString()} to {meeting.EndDate.ToShortDateString()}");

                Console.WriteLine("Name:");
                string name = Console.ReadLine();

                Console.WriteLine("Surname:");
                string surname = Console.ReadLine();

                switch(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
                {
                    case true:
                        Console.WriteLine("Name and surname are required.");
                        Console.ReadLine();
                        break;
                    case false:
                        Person person = personService.CheckPerson(name, surname);
                        runForm = false;

                        if(person.ScheduledMeetings.Any(s => s.MeetingId == meeting.Id))
                        {
                            Console.WriteLine("This person already added to this meeting.");
                            Console.ReadKey();
                        }
                        else
                        {
                            string warning = personService.AssignPersonToAMeeting(person, meeting, AssignDate(meeting));
                            Console.WriteLine(warning);
                            Console.WriteLine($"{person.Name} {person.Surname} added to a {meeting.Name} meeting.");
                            Console.ReadKey();
                        }
                        break;
                }
            }
        }

        private DateTime AssignDate(Meeting meeting)
        {
            DateTime date = new DateTime();

            bool runDate = true;
            while (runDate)
            {
                Console.WriteLine($"Specify when person is expected to participate in a meeting. Meeting date {meeting.StartDate.ToShortDateString()} - {meeting.EndDate.ToShortDateString()}:");
                Console.WriteLine($"Year:");
                string year = Console.ReadLine();
                Console.WriteLine("Month:");
                string month = Console.ReadLine();
                Console.WriteLine("Day:");
                string day = Console.ReadLine();

                switch (DateTime.TryParse($"{year}-{month}-{day}", out DateTime d))
                {
                    case true:
                        runDate = !ValidateAssignedDate(meeting, d);
                        if (!runDate)
                        {
                            date = d;
                        }
                        break;
                    case false:
                        Console.WriteLine("Entered date format is invalid.");
                        break;
                }
            }
            return date;
        }

        private bool ValidateAssignedDate(Meeting meeting, DateTime assignedDate)
        {
            if(meeting.StartDate <= assignedDate && meeting.EndDate >= assignedDate)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Assigned participate date is outside of meeting date.");
                Console.ReadKey();
                return false;
            }
        }
    }
}
