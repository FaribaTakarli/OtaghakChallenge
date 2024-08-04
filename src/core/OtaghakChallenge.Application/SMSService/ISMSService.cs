using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application.SMSServices
{
    public interface ISMSService
    {
        void Send(string message);
    }
}
