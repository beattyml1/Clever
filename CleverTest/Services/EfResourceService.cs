using Clever.Repository;
using Clever.Service;
using CleverTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleverTest.Services
{
    public interface IEfResourceService<T>: IResourceService<int, T, T, T, T, T, T, bool, bool>
    {

    }

    public class EfResourceService<T>: ResourceService<int, T, T, T, T, T, T, bool, bool, SessionData, T, T, T, T, T, T, bool, bool>, IEfResourceService<T>
        where T : class, new()
    {
        public EfResourceService(IContextService<SessionData> context, IRepository<int, T, T, T, T, T, T, bool, bool> resourceService): base(context, resourceService)
        {

        }
    }


    public interface IEfViewResourceService<T> : IViewService<int, T, ListViewModel<T>, ViewModel<T>, ViewModel<T>, T, T, bool, bool, T, T, T>
    {

    }

    public class EfViewResourceService<T> : ViewService<int, T, ListViewModel<T>, ViewModel<T>, ViewModel<T>, T, T, T, T, bool, bool, T, SessionData>, IEfViewResourceService<T>
        where T : class, new()
    {
        public EfViewResourceService(IEfResourceService<T> resourceService): base(resourceService)
        {

        }
    }
}