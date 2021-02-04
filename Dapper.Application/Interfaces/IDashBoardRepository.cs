using System;
using Dapper.Core.Model;

namespace Dapper.Application.Interfaces
{
    public interface IDashBoardRepository : IGenericDapperRepository<ViewDashBoard>
    {
    }
}
