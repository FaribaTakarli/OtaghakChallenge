
using OtaghakChallenge.Domain.Idp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace OtaghakChallenge.Application.Idp
{
    public interface ITokenService
    {

        Tokens Authenticate(User user);
    }
}
