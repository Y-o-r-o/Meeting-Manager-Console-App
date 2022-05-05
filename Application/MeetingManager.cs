using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;

namespace Application
{
    public class MeetingManager
    {
        private List<Meeting> meetings;
        private JsonSerializer<List<Meeting>> meetingsSerializer;

        public MeetingManager()
        {
            meetingsSerializer = new JsonSerializer<List<Meeting>>() { FileName = "meetings" };
            meetings = meetingsSerializer.deserialize();
            if (meetings is null) meetings = new();
        }

        public Result createMeeting(Person creator)
        {
            Meeting meeting = new Meeting();

            meeting.ResponsiblePerson = creator;

            Console.WriteLine("Enter meeting name: ");
            meeting.Name = new Name(BetterConsole.readLine());

            Console.WriteLine("Enter meeting description: ");
            meeting.Description = new Description(BetterConsole.readLine());

            Console.WriteLine("Enter category: ");
            meeting.Category = new Category(BetterConsole.readLine());

            Console.WriteLine("Enter type: ");
            meeting.Type = new Application.Models.Type(BetterConsole.readLine());

            Console.WriteLine("Enter start date: ");
            string startDate = BetterConsole.readLine();
            Console.WriteLine("Enter end date: ");
            string endDate = BetterConsole.readLine();

            meeting.FromToDateTime = new FromToDateTime(startDate, endDate);

            storeMeeting(meeting);

            BetterConsole.writeLineAndWaitForKeypress("Meeting successfully added.");
            return Result.Success();
        }

        private void storeMeeting(Meeting meeting)
        {
            meetings.Add(meeting);
            meetingsSerializer.serialize(meetings);
        }



        public Result deleteMeeting(Person creator)
        {
            Console.WriteLine("Enter name of meeting you want to select: ");
            Result<Meeting> result = meetings.GetMeetingByName(BetterConsole.readLine());

            if (!result.IsSuccess)
                return Result.Failure(result.Error);

            var meeting = result.Value;

            if (meeting.ResponsiblePerson == creator)
            {
                meetings.Remove(meeting);
                meetingsSerializer.serialize(meetings);

                Console.WriteLine("Meeting is deleted.");
                return Result.Success();
            }

            return Result.Failure("You can't delete this meetin, because you are not responsible for it.");
        }

        public Result addAPersonToMeeting()
        {
            Console.WriteLine("Enter name of meeting you want to select: ");
            Result<Meeting> result = meetings.GetMeetingByName(BetterConsole.readLine());

            if (!result.IsSuccess)
                return Result.Failure(result.Error);

            var meeting = result.Value;

            Console.WriteLine("Enter name of attendee you want to add: ");
            Name name = new Name(BetterConsole.readLine());

            if (meeting.ResponsiblePerson.Username.Equals(name))
                return Result.Failure("You can't add responsible person as attendee.");

            List<Person> meetingAttendees = meeting.Attendees;

            if (meetingAttendees.FirstOrDefault(attendee => attendee.Username.Equals(name)) is not null)
                return Result.Failure("Person is already in this meeting.");

            warnIfMeetingsIntersects(name, meeting);

            meetingAttendees.Add(new Person(name));
            meetingsSerializer.serialize(meetings);

            return Result.Success();
        }

        private void warnIfMeetingsIntersects(Name name, Meeting meeting)
        {
            var sameNameMeetings = meetings.FindAll(meeting => meeting.Attendees.Any(attendee => attendee.Username.Equals(name)));
            Meeting? intersectingMeeting;
            try
            {
                intersectingMeeting = sameNameMeetings.FirstOrDefault(m => m.FromToDateTime.StartDate < meeting.FromToDateTime.EndDate &&
                    meeting.FromToDateTime.StartDate < m.FromToDateTime.EndDate);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Meeting arrangment datetime is null.");
            }

            if (intersectingMeeting is null)
                return;
            Console.WriteLine($"Warning: Meeting {meeting.Name} intersects with {intersectingMeeting.Name}");
        }

        public Result removeAPersonFromMeeting()
        {
            Console.WriteLine("Enter name of meeting you want to select: ");
            Result<Meeting> result = meetings.GetMeetingByName(BetterConsole.readLine());

            if (!result.IsSuccess)
                return Result.Failure(result.Error);

            var meeting = result.Value;


            Console.WriteLine("Enter name of attendee you want to remove: ");
            Name name = new Name(BetterConsole.readLine());

            if (meeting.ResponsiblePerson.Username.Equals(name))
                return Result.Failure("You can't remove responsible personfrom meeting.");

            List<Person> meetingAttendees = meeting.Attendees;

            if (meetingAttendees.FirstOrDefault(attendee => attendee.Username.Equals(name)) is null)
                return Result.Failure("This person doesn't exist in this meeting.");

            meetingAttendees.Remove(new Person(name));
            meetingsSerializer.serialize(meetings);

            Console.WriteLine("Person successfully removed. ");
            return Result.Success();
        }

        public Result listAllMeetings()
        {
            List<Meeting> selectedMeetings = meetings;
            string input;

            Console.WriteLine("Type fragments from description to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
                selectedMeetings = selectedMeetings.GetMeetingsByDescription(input);

            Console.WriteLine("Type responsible person name to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
                selectedMeetings = selectedMeetings.GetMeetingsByResponsiblePerson(input);

            Console.WriteLine("Type category to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
                selectedMeetings = selectedMeetings.GetMeetingByCategory(input);

            Console.WriteLine("Type type to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
                selectedMeetings = selectedMeetings.GetMeetingByType(input);

            Console.WriteLine("Type start date (yyyy/mm/dd) to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
            {
                Result<List<Meeting>> result = selectedMeetings.GetMeetingByStartDate(input);
                if (result.IsSuccess)
                    selectedMeetings = result.Value;
                else return Result.Failure(result.Error);
            }

            Console.WriteLine("Type end date (yyyy/mm/dd) to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
            {
                Result<List<Meeting>> result = selectedMeetings.GetMeetingByEndDate(input);
                if (result.IsSuccess)
                    selectedMeetings = result.Value;
                else return Result.Failure(result.Error);
            }

            Console.WriteLine("Type attendees count to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
            {
                Result<List<Meeting>> result = selectedMeetings.GetMeetingByAttendeesCount(input);
                if (result.IsSuccess)
                    selectedMeetings = result.Value;
                else return Result.Failure(result.Error);
            }

            selectedMeetings.Print();
            BetterConsole.waitForKeypress();

            return Result.Success();
        }

    }
}