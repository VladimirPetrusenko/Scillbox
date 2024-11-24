using System;

namespace SkillProfiDesktopApplication
{
    public class Request
    {
        public int Id { get; set; }
        public string GuestName { get; set; }
        public string Email { get; set; }
        public string RequestText { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Request(int Id, string GuestName, string Email, string RequestText, string Status, DateTime CreatedAt)
        {
            this.Id = Id;
            this.GuestName = GuestName;
            this.Email = Email;
            this.RequestText = RequestText;
            this.Status = Status;
            this.CreatedAt = CreatedAt;
        }

        public Request()
        {

        }
    }
}
