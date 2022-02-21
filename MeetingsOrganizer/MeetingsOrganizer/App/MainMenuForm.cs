using MeetingsOrganizer.Models;

namespace MeetingsOrganizer.App
{
    public class MainMenuForm
    {
        private CreateMeetingForm meetingForm = new CreateMeetingForm();
        private ListAllMeetingsForm allMeetingsForm = new ListAllMeetingsForm();
        private SelectMeetingForm selectMeetingForm = new SelectMeetingForm();

        public void Run(Person loggedInPerson)
        {
            bool runMainMenu = true;
            while (runMainMenu)
            {
                Console.Clear();
                Console.WriteLine("Main menu.");
                Console.WriteLine("Select meeting command. Create, Select, GetAll, logout");
                Console.WriteLine("1. Create.");
                Console.WriteLine("2. Select.");
                Console.WriteLine("3. Logout.");
                Console.WriteLine("4. GetAll.");
                Console.WriteLine("GetAll has additional parameters:");
                Console.WriteLine("- getall -desc <your search term> - will return all meetings that has search term in its description.");
                Console.WriteLine("- getall -rp <name surname> - will return all meetings where specified person is meeting host.");
                Console.WriteLine("- getall -cat <category> - return all meetings with specified category. Possible categories: codemonkey, hub, short, teambuilding.");
                Console.WriteLine("- getall -type <type> - return all meetings with specified type. Possible types: live, inperson.");
                Console.WriteLine("- getall -date <yyyy-mm-dd> - return all metings where specified date is in meetings bounds.");
                Console.WriteLine("- getall -ppl <number> - return all meetigs where meeting participants count is greater than specified number.");
                string action = Console.ReadLine();

                switch (action.ToLower())
                {
                    case "logout":
                        runMainMenu = false;
                        break;
                    case "create":
                        meetingForm.Run(loggedInPerson);
                        break;
                    case "select":
                        selectMeetingForm.Run(loggedInPerson);
                        break;
                    default:
                        allMeetingsForm.Run(action);
                        break;
                }
                Console.WriteLine("Press ant key to continue...");
                Console.ReadKey();
            }
        }
    }
}
