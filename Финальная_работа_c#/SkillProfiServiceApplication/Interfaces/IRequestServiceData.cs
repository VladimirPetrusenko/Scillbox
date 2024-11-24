using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillProfiServiceApplication.Interfaces
{
    public interface IRequestServiceData
    {
        IEnumerable<Request> GetAllRequests();

        IEnumerable<Request> GetRequests(RequestFilter requestFilter);

        void AddRequest(Request request);

        void ChangeRequestStatus(Request request);

        Request FindRequest(int phoneBookId);
    }
}
