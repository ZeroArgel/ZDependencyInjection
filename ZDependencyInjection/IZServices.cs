namespace ZDependencyInjection
{
    public interface IZServices
    {
        void OnAddService<TService, TImplementation>() where TService : class
            where TImplementation : class, TService;
        void OnAddService<TService, TImplementation>(TImplementation implementation) where TService : class
            where TImplementation : class, TService;
        TImplementation OnGetService<TImplementation>() where TImplementation : class;
        TModel OnGet<TModel>();
        TModel OnGet<TModel>(string key);
    }
}