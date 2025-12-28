using System.Collections.Generic;

public class ServiceLocator
{
    private static ServiceLocator _instance;
    public static ServiceLocator Instance => _instance ??= new ServiceLocator();

    private readonly Dictionary<string, IService> _services;

    private ServiceLocator()
    {
        _services = new Dictionary<string, IService>();
    }

    public void Register<T>(T service) where T : IService
    {
        string name = typeof(T).Name;

        if (_services.ContainsKey(name))
        {
            return;
        }
        _services.Add(name, service);
    }

    public T GetService<T>() where T : IService
    {
        string name = typeof(T).Name;

        if (_services.ContainsKey(name))
        {
            return (T)_services[name];
        }
        else
        {
            return default;
        }
    }

    public void Unregister<T>() where T : IService
    {
        string name = typeof(T).Name;

        if (!_services.ContainsKey(name))
        {
            return;
        }
        _services.Remove(name);
    }
}