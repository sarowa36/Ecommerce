using DataAccessLayer.Base;
using DataAccessLayer.RepositoryEvents.Events;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryEvents
{
    public static class RepositoryEventSetter<T> where T :class
    {
        public static void SetRepositoryEvents<TRepo>(TRepo trepo) where TRepo :class
        {
            var baseInterfaceTypes=typeof(T).GetInterfaces();
            foreach (var interfaceType in baseInterfaceTypes)
            {
               var eventClassType= Assembly.GetExecutingAssembly().DefinedTypes.FirstOrDefault(x =>!x.IsInterface && typeof(IRepositoryEvent<>).MakeGenericType(interfaceType).IsAssignableFrom(x));
                if(eventClassType!= null) { 
                var eventClassObj=Activator.CreateInstance(eventClassType);
                var invokeMethod = eventClassType.GetMethod("Invoke");
                    invokeMethod= invokeMethod.MakeGenericMethod(trepo.GetType(), typeof(T));
                invokeMethod.Invoke(eventClassObj, new object[]{ trepo });
                }
            }
        }

    }
}
