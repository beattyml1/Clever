using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clever.WebView;
using CleverTest.Services;

namespace CleverTest.Controllers
{
    public class IndexController : BaseEfController<Todo>
    {
        public IndexController(IEfViewResourceService<Todo> service): base(service)
        {

        }
    }
}