using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using SkillProfiServiceApplication.Interfaces;
using SkillProfiServiceApplication.DataContext;

namespace SkillProfiServiceApplication.Data
{
    public class RequestServiceData: IRequestServiceData
    {
        public static DbContextOptions<RequestServiceContext> options = new DbContextOptions<RequestServiceContext>();

        RequestServiceContext db = new RequestServiceContext(options);

        private DateTime zeroDate = new DateTime(0001, 1, 1, 0, 0, 0);

        public IEnumerable<Request> GetAllRequests()
        {
            IEnumerable<Request> requests = db.Requests.ToList();
            return requests;
        }

        public void AddRequest(Request request)
        {
            db.Requests.Add(request);
            db.SaveChanges();
        }

        public IEnumerable<Request> GetRequests(RequestFilter requestFilter)
        {
            IQueryable<Request> requests = db.Requests; 
            
            if (requestFilter.StartDate != zeroDate)
            {
                requests = requests.Where(r => r.CreatedAt >= requestFilter.StartDate.Value);
            }

            if (requestFilter.EndDate != zeroDate)
            {
                requests = requests.Where(r => r.CreatedAt <= requestFilter.EndDate.Value);
            }

            if (requestFilter.RequestStatus != null)
            {
                requests = requests.Where(r => r.Status == requestFilter.RequestStatus);
            }

            IEnumerable<Request> requests1 = requests; //IQueryable c десериализацией проблемы, появляется ошибка
            return requests1;
        }

        public void ChangeRequestStatus(Request request)
        {
            db.Requests.Update(request);
            db.SaveChanges();
        }

        public Request FindRequest(int id)
        {
            Request request = db.Requests.Find(id);
            return request;
        }
    }
}
