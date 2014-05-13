﻿using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using RElmah.Domain;
using RElmah.Server.Hubs;

namespace RElmah.Server.Services
{
    public class ErrorsDispatcher : IErrorsDispatcher
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IHubContext _context = GlobalHost.ConnectionManager.GetHubContext<Frontend>();

        public ErrorsDispatcher(IConfigurationProvider configuration)
        {
            _configuration = configuration;
        }

        public Task Dispatch(ErrorPayload payload)
        {
            var groups = _configuration.ExtractGroups(payload);
            return _context.Clients.All.dispatch(payload);
        }
    }
}
