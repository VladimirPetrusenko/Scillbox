using System;
using System.Collections.Generic;

namespace SkillProfiWebApplication.ViewModels
{
    public class RequestsPageModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RequestStatus { get; set; }
        public string DateRange { get; set; }
        public IEnumerable<Request> Requests { get; set; }
        public IEnumerable<string> Statuses { get; set; }
    }
}
