using MeetingsOrganizer.Models;
using MeetingsOrganizer.Services;

namespace MeetingsOrganizer.App
{
    public class DeleteMeetingForm
    {
        private MeetingService meetingService = new MeetingService();

        public void Run(Meeting meeting, Person user)
        {
            if(meeting.ResponsiblePerson.Id == user.Id)
            {
                meetingService.RemoveMeeting(meeting);
                Console.WriteLine($"Meeting {meeting.Name} removed.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Authorization failed. Meeting can be removed only by a meeting host.");
                Console.ReadKey();
            }
        }
    }
}
