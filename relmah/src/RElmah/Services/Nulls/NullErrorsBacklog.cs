﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RElmah.Common;
using RElmah.Foundation;
using RElmah.Models;

namespace RElmah.Services.Nulls
{
    class NullErrorsBacklog : IErrorsBacklog
    {
        public Task Store(ErrorPayload payload)
        {
            return Task.FromResult((object)null);
        }

        public Task<ValueOrError<Recap>> GetApplicationsRecap(IEnumerable<Application> apps, Func<IEnumerable<ErrorPayload>, int> processor)
        {
            return Task.FromResult((ValueOrError<Recap>)null);
        }
    }
}
