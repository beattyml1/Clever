using Clever.Service;
using Clever.WebView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using React.Web;
using React;
using System.Web.Razor;
using System.IO;

namespace Clever.Web.React
{
    public class ReactViewResourceController<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TGetOne, TGetMany, TPut, TPost, TEditResult, TDeleteResult, TCreateResult, TSession>
        : ViewResourceController<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TGetOne, TGetMany, TPut, TPost, TEditResult, TDeleteResult, TCreateResult, TSession>
    {
        public ReactViewResourceController(IViewService<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TPut, TPost, TEditResult, TDeleteResult, TCreateResult, TGetOne, TGetMany> viewService)
            : base(viewService)
        {
        }

        //public void DoRazorThings()
        //{
        //    var host = new RazorEngineHost(new CSharpRazorCodeLanguage());
        //    host.DefaultBaseClass = typeof(int).FullName; //TODO: Get proper template base class
            
        //    host.DefaultNamespace = "RazorOutput";
        //    host.DefaultClassName = "Template";
            
        //    host.NamespaceImports.Add("System");
        //    host.NamespaceImports.Add("System.IO");

        //    var razorEngine = new RazorTemplateEngine(host);
            
        //    razorEngine.ParseTemplate(new StringReader("")).Document.
        //}
    }
}
