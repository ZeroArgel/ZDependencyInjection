namespace ZDependencyInjection
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class ZServices : IZServices
    {
        public ZServices()
        {
            _configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile(@"appsettings.json", true)
                .Build();
            Services = new List<ZDependencyInjection>();
        }
        private List<ZDependencyInjection> Services { get; }
        private readonly IConfiguration _configuration;
        #region OnAddService
        public void OnAddService<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            var serviceName = typeof(TService).Name;
            if (Services.Any(x => x.Name.ToUpper().Equals(serviceName.ToUpper()))) return;
            var implement = typeof(TImplementation);
            var instance = (TService)Activator.CreateInstance(implement);
            Services.Add(new ZDependencyInjection(instance, serviceName));
        }
        #endregion
        #region OnAddService
        public void OnAddService<TService, TImplementation>(TImplementation implementation)
            where TService : class
            where TImplementation : class, TService
        {
            var serviceName = typeof(TService).Name;
            if (Services.Any(x => x.Name.ToUpper().Equals(serviceName.ToUpper()))) return;
            Services.Add(new ZDependencyInjection(implementation, serviceName));
        }
        #endregion
        #region OnGetService
        public TService OnGetService<TService>()
            where TService : class
        {
            var name = typeof(TService).Name;
            return Services.Any(x => x.Name.ToUpper().Equals(name.ToUpper()))
                ? (TService)Services.First(x => x.Name.ToUpper().Equals(name.ToUpper())).Service
                : throw new ArgumentException("This services not exist.");
        }
        #endregion
        #region OnGet
        /// <summary>
        /// Get your model from app settings. TModel is the key section.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public TModel OnGet<TModel>() =>
            _configuration == null
                ? throw new ArgumentException("configure your appsetting first and before use this function.")
                : _configuration.GetSection(typeof(TModel).Name).Get<TModel>();
        #endregion
        #region OnGet
        /// <summary>
        /// Get your model from app settings.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TModel OnGet<TModel>(string key) =>
            _configuration == null 
                ? throw new ArgumentException("configure your appsetting first and before use this function.")
                : _configuration.GetSection(key).Get<TModel>();
        #endregion
    }
}