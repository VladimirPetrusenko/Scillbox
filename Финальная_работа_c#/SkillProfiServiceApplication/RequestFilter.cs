using System;

namespace SkillProfiServiceApplication
{
    public class RequestFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? RequestStatus { get; set; }
    }
}
