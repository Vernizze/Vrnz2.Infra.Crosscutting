using Vrnz2.Infra.CrossCutting.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Vrnz2.Infra.CrossCutting.Utils
{
    public static class DependencyInjectionHandler
    {
        #region Variables

        private static IServiceCollection _services = null;
        private static ServiceProvider _provider = null;

        #endregion

        #region Methods

        public static IServiceCollection AddDIHandler(this IServiceCollection services)
        {
            _services ??= services;

            _provider ??= _services.BuildServiceProvider();

            return _services;
        }

        public static TInstance GetInstance<TInstance>()
            => _provider.IsNull() ? default(TInstance) : _provider.GetService<TInstance>();

        public static IEnumerable<TInstance> GetInstances<TInstance>()
            => _provider.IsNull() ? default(IEnumerable<TInstance>) : _provider.GetServices<TInstance>();

        public static void Dispose()
        {
            if (_provider.IsNotNull())
            {
                _provider.Dispose();
                _provider = null;
            }

            if (_services.IsNotNull())
            {
                _services.Clear();
                _services = null;
            }
        }

        #endregion
    }
}
