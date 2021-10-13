using HostelAPI.Model.Mail;
using HostelAPI.Utilities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelAPI.Core.Services.Abstraction
{
    public interface IMailService
    {
        Task<Response<string>> SendEmailAsync(MailRequest mailRequest);
    }
}
