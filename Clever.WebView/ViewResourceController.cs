using System;
using System.Web.Mvc;
using Clever.Service;

namespace Clever.WebView
{
    public interface IViewResourceController<TId, TQuery>
    {
        ActionResult Index();
        ActionResult Index(TQuery query);
        ActionResult Details(TId id);
        ActionResult Create();
        ActionResult Edit(TId id);
        ActionResult IndexJson();
        ActionResult DetailsJson(TId id);
        ActionResult CreateJson();
        ActionResult EditJson(TId id);
        ActionResult Delete(TId id);
        ActionResult Edit(TId id, FormCollection collection);
        ActionResult Create(FormCollection collection);
    }

    public class ViewResourceController<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TGetOne, TGetMany, TPut, TPost, TEditResult, TDeleteResult, TCreateResult, TSession> 
        : Controller, IViewResourceController<TId>
    {
        protected readonly IViewService<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TPut, TPost, TEditResult, TDeleteResult, TCreateResult> viewService;
        public ViewResourceController(IViewService<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TPut, TPost, TEditResult, TDeleteResult, TCreateResult> viewService)
        {
            this.viewService = viewService;
        }

        public ActionResult Create()
        {
            return View(viewService.Create());
        }

        [HttpPost]
        public ActionResult Create(TPost collection)
        {
            return Json(viewService.Create(collection));
        }

        public ActionResult CreateJson()
        {
            return Json(viewService.Create());
        }

        [HttpPost]
        public ActionResult Delete(TId id)
        {
            return Json(viewService.Delete(id));
        }

        public ActionResult Details(TId id)
        {
            return View(viewService.Details(id));
        }

        public ActionResult DetailsJson(TId id)
        {
            return Json(viewService.Details(id));
        }

        public ActionResult Edit(TId id)
        {
            return View(viewService.Edit(id));
        }

        [HttpPost]
        public ActionResult Edit(TId id, TPut collection)
        {
            return Json(viewService.Edit(id, collection));
        }

        public ActionResult EditJson(TId id)
        {
            return Json(viewService.Edit(id));
        }

        public ActionResult Index()
        {
            return View(viewService.Index());
        }

        public ActionResult Index(TQuery query)
        {
            return View(viewService.Index(query));
        }

        public ActionResult IndexJson()
        {
            return Json(viewService.Index());
        }

        public ActionResult IndexJson(TQuery query)
        {
            return Json(viewService.Index(query));
        }
    }
}
