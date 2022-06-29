namespace ZDependencyInjection
{
    public class ZDependencyInjection
    {
        public ZDependencyInjection(object service, string name)
        {
            Service = service;
            Name = name;
        }
        public object Service { get; }
        public string Name { get; }
    }
}