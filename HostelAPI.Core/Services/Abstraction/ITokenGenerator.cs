using HostelAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Services.Abstraction
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AppUser user);
    }
}
