using System;

namespace Dapper.Application.Interfaces.Account
{
    public interface IUserAccesor
    {
        string GetCurrentUser();
    }
}
