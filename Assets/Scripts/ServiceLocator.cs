using System.Collections.Generic;
using System;

namespace Clock
{
    public static class ServiceLocator
    {
        private static readonly IDictionary<Type, object> _services = 
                                           new Dictionary<Type, object>();
        public static void RegisterService<T>(T service)
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                _services.Add(typeof(T), service);
            }
            else
            {
                throw new Exception("Service already registered");
            }
        }
        public static T GetService<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch
            {
                throw new Exception("Requested service not found.");
            }
        }
    }
}
