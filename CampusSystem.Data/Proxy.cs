using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusSystem.Data
{
    public class Proxy : ISubject
    {
        ISubject subject;

        public Proxy(ISubject sub)
        {
            subject = sub;
        }

        public string GetUserId()
        {
            return subject.GetUserId();
        }
    }
}
