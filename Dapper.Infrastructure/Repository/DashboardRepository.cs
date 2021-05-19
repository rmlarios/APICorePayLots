using System;
using Dapper.Application.Interfaces;
using Dapper.Application.Interfaces.Account;
using Dapper.Core;
using Dapper.Core.Model;
using Dapper.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;

namespace Dapper.Infrastructure.Repository
{
    public class DashboardRepository : GenericDapperRepository<ViewDashBoard> , IDashBoardRepository
    {
        public DashboardRepository(IConfiguration configuration, IUserAccesor userAccessor, PayLotsDBContext context):base(configuration,context)
        {
            
        }

    }
}
