using System;

namespace SkillProfiDesktopApplication
{
    public class RequestFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RequestStatus { get; set; }

        public RequestFilter(DateTime StartDate, DateTime EndDate, string RequestStatus)
        {
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.RequestStatus = RequestStatus;
        }

        public RequestFilter()
        {

        }
    }
}
