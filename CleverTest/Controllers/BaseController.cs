using Clever.Service;
using Clever.WebView;
using CleverTest.Services;

namespace CleverTest.Controllers
{
    public class BaseEfController<T> : ViewResourceController<int, T, ListViewModel<T>, ViewModel<T>, ViewModel<T>, T, T, T, T, bool, bool, T, SessionData>
    {
        public BaseEfController(IEfViewResourceService<T> service): base(service)
        {

        }
    }
}