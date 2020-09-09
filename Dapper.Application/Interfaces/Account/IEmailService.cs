using System;
using System.Threading.Tasks;
using Dapper.Application.DTOs.Email;

namespace Dapper.Application.Interfaces.Account
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
