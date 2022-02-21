using MeetingsOrganizer.Models;
using MeetingsOrganizer.Services;

namespace MeetingsOrganizer.App
{
    public class ListAllMeetingsForm
    {
        private MeetingService meetingService = new MeetingService();

        public void Run(string action)
        {
            if (!string.IsNullOrWhiteSpace(action))
            {
                List<string> actionWithParams = action.ToLower().Split(' ').ToList();
                switch (actionWithParams[0])
                {
                    case "getall":
                        if(actionWithParams.Count > 1)
                        {
                            CheckGetAllParams(actionWithParams);
                        }
                        else
                        {
                            PrintAll(meetingService.GetAll());
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }

        public void CheckGetAllParams(List<string> actionWithParams)
        {
            Console.WriteLine("Search results:");
            switch (actionWithParams[1])
            {
                case "-desc":
                    SearchByDescription(actionWithParams);
                    break;
                case "-rp":
                    SearchByHost(actionWithParams);
                    break;
                case "-cat":
                    SearchByCategory(actionWithParams);
                    break;
                case "-type":
                    SearchByType(actionWithParams);
                    break;
                case "-date":
                    SearchByDate(actionWithParams);
                    break;
                case "-ppl":
                    SearchByPeopleCount(actionWithParams);
                    break;
                default:
                    Console.WriteLine("Invalid parameter.");
                    Console.ReadKey();
                    break;
            }
        }

        private void PrintAll(List<Meeting> meetings)
        {
            for (int i = 0; i < meetings.Count; i++)
            {
                Meeting meeting = meetings[i];
                Console.WriteLine($"" +
                    $"{i + 1} - " +
                    $"{meeting.Name} - " +
                    $"{meeting.Description} - " +
                    $"{meeting.ResponsiblePerson.Name} {meeting.ResponsiblePerson.Surname} - " +
                    $"{meeting.Category} - {meeting.Type} - " +
                    $"from: {meeting.StartDate.ToShortDateString()} " +
                    $"to: {meeting.EndDate.ToShortDateString()} - Participants: {meetingService.GetMeetingParticipants(meeting).Count()}");
            }
        }

        private void SearchByPeopleCount(List<string> actionWithParams)
        {
            if(actionWithParams.Count() == 3)
            {
                if(int.TryParse(actionWithParams[2], out int count))
                {
                    PrintAll(meetingService.GetMeetingsByParticipantsCount(count));
                }
                else
                {
                    Console.WriteLine("List by participants count failed. Invalid number format.");
                }
            }
            else
            {
                Console.WriteLine("Wrong command format.");
            }
        }

        private void SearchByType(List<string> actionWithParams)
        {
            if(actionWithParams.Count == 3)
            {
                switch (actionWithParams[2])
                {
                    case "live":
                        PrintAll(meetingService.GetMeetingsByType("live"));
                        break;
                    case "inperson":
                        PrintAll(meetingService.GetMeetingsByType("inperson"));
                        break;
                    default:
                        Console.WriteLine("Invalid type.");
                        break;
                }
            }
        }

        private void SearchByCategory(List<string> actionWithParams)
        {
            if(actionWithParams.Count == 3)
            {
                switch (actionWithParams[2])
                {
                    case "codemonkey":
                        PrintAll(meetingService.GetMeetingsByCategory("codemonkey"));
                        break;
                    case "hub":
                        PrintAll(meetingService.GetMeetingsByCategory("hub"));
                        break;
                    case "short":
                        PrintAll(meetingService.GetMeetingsByCategory("short"));
                        break;
                    case "teambuilding":
                        PrintAll(meetingService.GetMeetingsByCategory("teambuilding"));
                        break;
                    default:
                        Console.WriteLine("Wrong category.");
                        break;
                }
            }
        }

        private void SearchByDescription(List<string> actionWithParams)
        {
            if(actionWithParams.Count > 2)
            {
                actionWithParams.RemoveRange(0, 2);
                string term = string.Join(" ", actionWithParams);

                PrintAll(meetingService.GetByDescriptionTerm(term));
            }
            else
            {
                Console.WriteLine("No search term was specified.");
            }
        }

        private void SearchByHost(List<string> actionWithParams)
        {
            if(actionWithParams.Count > 2)
            {
                actionWithParams.RemoveRange(0, 2);
                string fullName = string.Join(" ", actionWithParams);

                PrintAll(meetingService.GetByPersonName(fullName));
            }
            else
            {
                Console.WriteLine("No name was specified");
            }
        }

        private void SearchByDate(List<string> actionWithParams)
        {
            if(actionWithParams.Count == 3)
            {
                if(DateTime.TryParse(actionWithParams[2], out DateTime dateTime))
                {
                    PrintAll(meetingService.GetByDate(dateTime));
                }
                else
                {
                    Console.WriteLine("Wrong date format.");
                }
            }
            else
            {
                Console.WriteLine("Wrong command.");
            }
        }
    }
}
