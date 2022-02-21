using MeetingsOrganizer.Models;
using MeetingsOrganizer.Services;

namespace MeetingsOrganizer.App
{
    public class CreateMeetingForm
    {
        private MeetingService meetingService = new MeetingService();

        public void Run(Person user)
        {
            bool runCreate = true;
            while (runCreate)
            {
                Meeting meeting = new Meeting();

                Console.Clear();
                Console.WriteLine("Create new meeting.");

                bool runNameDescr = true;
                while (runNameDescr)
                {
                    Console.WriteLine("Meeting name:");
                    meeting.Name = Console.ReadLine();
                    meeting.ResponsiblePerson = user;

                    Console.WriteLine("Description:");
                    meeting.Description = Console.ReadLine();

                    if(!string.IsNullOrWhiteSpace(meeting.Name) && !string.IsNullOrWhiteSpace(meeting.Description))
                    {
                        runNameDescr = false;
                    }
                    else
                    {
                        Console.WriteLine("Name and Description required.");
                    }
                }
                

                bool runCategory = true;
                while (runCategory)
                {
                    Console.WriteLine("Select meeting category: CodeMokey, Hub, Short, TeamBuilding.");
                    meeting.Category = Console.ReadLine();

                    switch(new List<string>() {"CodeMonkey", "Hub", "Short", "TeamBuilding" }.Contains(meeting.Category))
                    {
                        case true:
                            runCategory = false;
                            break;
                        case false:
                            Console.WriteLine("You must select existing category.");
                            break;
                    }
                }

                bool runType = true;
                while (runType)
                {
                    Console.WriteLine("Select meeting type: Live, InPerson.");
                    meeting.Type = Console.ReadLine();
                    Console.WriteLine(meeting.Type);
                    switch(new List<string>() { "Live", "InPerson" }.Contains(meeting.Type))
                    {
                        case true:
                            runType = false;
                            break;
                        case false:
                            Console.WriteLine("You must select existing type.");
                            break;
                    }
                }

                bool runDate = true;
                while (runDate)
                {
                    Console.WriteLine("Select meeting start date:");
                    meeting.StartDate = GetDate();

                    Console.WriteLine("Select meeting end date:");
                    meeting.EndDate = GetDate();

                    if(meeting.EndDate >= meeting.StartDate)
                    {
                        runDate = false;
                    }
                    else
                    {
                        Console.WriteLine("End date must be greater or equal to start date.");
                        Console.ReadKey();
                    }
                }
                

                meetingService.CreateMeeting(meeting);
                Console.WriteLine("Meeting created.");
                Console.ReadLine();

                runCreate = false;
            }
        }

        private DateTime GetDate()
        {
            DateTime date = new DateTime();
            bool runDate = true;
            while (runDate)
            {
                Console.WriteLine("Year:");
                string year = Console.ReadLine();

                Console.WriteLine("Month:");
                string month = Console.ReadLine();

                Console.WriteLine("Day:");
                string day = Console.ReadLine();

                if (DateTime.TryParse($"{year}-{month}-{day}", out DateTime d))
                {
                    date = d;
                    runDate = false;
                }
                else
                {
                    Console.WriteLine("Entered date is incorect.");
                }
            }

            return date;
        }
    }
}
