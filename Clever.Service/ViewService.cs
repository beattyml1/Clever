using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clever.Function;

namespace Clever.Service
{
    public interface IViewService<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TPut, TPost, TEditResult, TDeleteResult, TCreateResult>
    {
        ServiceOption<TIndexViewModel> Index();
        ServiceOption<TIndexViewModel> Index(TQuery query);
        ServiceOption<TDetailViewModel> Details(TId id);
        ServiceOption<TEditorViewModel> Create();
        ServiceOption<TEditorViewModel> Edit(TId id);
        ServiceOption<TEditResult> Edit(TId id, TPut collection);
        ServiceOption<TDeleteResult> Delete(TId id);
        ServiceOption<TCreateResult> Create(TPost collection);
    }

    public class ViewService<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TGetOne, TGetMany, TPut, TPost, TEditResult, TDeleteResult, TCreateResult, TSession>
        : IViewService<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TPut, TPost, TEditResult, TDeleteResult, TCreateResult>
        where TIndexViewModel: ListViewModel<TGetMany>, new()
        where TDetailViewModel : ViewModel<TGetOne>, new()
        where TEditorViewModel : ViewModel<TGetOne>, new()
        where TGetOne: new()
    {
        protected readonly IResourceService<TId, TGetOne, TGetMany, TQuery, TPost, TCreateResult, TPut, TEditResult, TDeleteResult, TSession> resourceService;
        public ViewService(IResourceService<TId, TGetOne, TGetMany, TQuery, TPost, TCreateResult, TPut, TEditResult, TDeleteResult, TSession> resourceService)
        {
            this.resourceService = resourceService;
        }

        public ServiceOption<TEditorViewModel> Create()
        {
            return new TEditorViewModel { Model = new TGetOne() };
        }

        public ServiceOption<TCreateResult> Create(TPost collection)
        {
            return resourceService.Post(collection);
        }

        public ServiceOption<TDeleteResult> Delete(TId id)
        {
            return resourceService.Delete(id);
        }

        public ServiceOption<TDetailViewModel> Details(TId id)
        {
            return resourceService.Get(id).PossiblyTransform(val => new TDetailViewModel { Model =  val });
        }

        public ServiceOption<TEditorViewModel> Edit(TId id)
        {
            return resourceService.Get(id).PossiblyTransform(val => new TEditorViewModel { Model = val });
        }

        public ServiceOption<TEditResult> Edit(TId id, TPut collection)
        {
            return resourceService.Put(id, collection);
        }

        public ServiceOption<TIndexViewModel> Index()
        {
            return resourceService.Get(default(TQuery)).PossiblyTransform(val => new TIndexViewModel { Models = val });
        }

        public ServiceOption<TIndexViewModel> Index(TQuery query)
        {
            return resourceService.Get(query).PossiblyTransform(val => new TIndexViewModel { Models = val });
        }
    }
}
