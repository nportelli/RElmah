using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace RElmah.Queries.Backend
{
    public class ConfigurationBusQuery : IBackendQuery
    {
        private readonly bool _skipEventsFromBackend;

        public ConfigurationBusQuery(bool skipEventsFromBackend = false)
        {
            _skipEventsFromBackend = skipEventsFromBackend;
        }

        public Task<IDisposable> Run(RunTargets targets)
        {
            var clusters = targets.DomainPublisher.GetClustersSequence()
                .Where(p => !_skipEventsFromBackend || !p.FromBackend)
                .Subscribe(payload =>
            {
                targets.BackendNotifier.Cluster(payload);
            });

            var applications = targets.DomainPublisher.GetApplicationsSequence()
                .Where(p => !_skipEventsFromBackend || !p.FromBackend)
                .Subscribe(payload =>
            {
                targets.BackendNotifier.Application(payload);
            });

            var users = targets.DomainPublisher.GetUsersSequence()
                .Where(p => !_skipEventsFromBackend || !p.FromBackend)
                .Subscribe(payload =>
            {
                targets.BackendNotifier.User(payload);
            });

            var clusterApplications = targets.DomainPublisher.GetClusterApplicationsSequence()
                .Where(p => !_skipEventsFromBackend || !p.FromBackend)
                .Subscribe(payload =>
            {
                targets.BackendNotifier.ClusterApplication(payload);
            });

            var clusterUsers = targets.DomainPublisher.GetClusterUsersSequence()
                .Where(p => !_skipEventsFromBackend || !p.FromBackend)
                .Subscribe(payload =>
            {
                targets.BackendNotifier.ClusterUser(payload);
            });

            return Task.FromResult((IDisposable)new CompositeDisposable(clusters, applications, users, clusterApplications, clusterUsers));
        }
    }
}