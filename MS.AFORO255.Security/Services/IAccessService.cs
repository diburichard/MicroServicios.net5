using MS.AFORO255.Security.Models;
using System.Collections.Generic;

namespace MS.AFORO255.Security.Services
{
    public interface IAccessService
    {
        IEnumerable<Access> GetAll();
        bool Validate(string userName, string password);

    }
}
