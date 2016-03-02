using System.Collections.Generic;

namespace Clever.Service
{
    public interface IViewService<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TPut, TPost, TEditResult, TDeleteResult, TCreateResult, TGetOne, TGetMany>
    {
        ServiceOption<TIndexViewModel> Index();
        ServiceOption<TIndexViewModel> Index(TQuery query);
        ServiceOption<TDetailViewModel> Details(TId id);
        ServiceOption<TEditorViewModel> Create();
        ServiceOption<TEditorViewModel> Edit(TId id);
        ServiceOption<TEditResult> Edit(TId id, TPut collection);
        ServiceOption<TDeleteResult> Delete(TId id);
        ServiceOption<TCreateResult> Create(TPost collection);

        ServiceOption<TGetOne> Data(TId id);
        ServiceOption<IEnumerable<TGetMany>> Data();
        ServiceOption<IEnumerable<TGetMany>> Data(TQuery query);
    }

    public class ViewService<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TGetOne, TGetMany, TPut, TPost, TEditResult, TDeleteResult, TCreateResult, TSession>
        : IViewService<TId, TQuery, TIndexViewModel, TDetailViewModel, TEditorViewModel, TPut, TPost, TEditResult, TDeleteResult, TCreateResult, TGetOne, TGetMany>
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

        public virtual ServiceOption<TEditorViewModel> Create()
        {
            return new TEditorViewModel { Model = new TGetOne() };
        }

        public virtual ServiceOption<TCreateResult> Create(TPost collection)
        {
            return resourceService.Post(collection);
        }

        public virtual ServiceOption<TDeleteResult> Delete(TId id)
        {
            return resourceService.Delete(id);
        }

        public virtual ServiceOption<TDetailViewModel> Details(TId id)
        {
            return resourceService.Get(id).PossiblyTransform(val => new TDetailViewModel { Model =  val });
        }

        public virtual ServiceOption<TEditorViewModel> Edit(TId id)
        {
            return resourceService.Get(id).PossiblyTransform(val => new TEditorViewModel { Model = val });
        }

        public virtual ServiceOption<TEditResult> Edit(TId id, TPut collection)
        {
            return resourceService.Put(id, collection);
        }

        public virtual ServiceOption<TIndexViewModel> Index()
        {
            return resourceService.Get(default(TQuery)).PossiblyTransform(val => new TIndexViewModel { Models = val });
        }

        public virtual ServiceOption<TIndexViewModel> Index(TQuery query)
        {
            return resourceService.Get(query).PossiblyTransform(val => new TIndexViewModel { Models = val });
        }

        public ServiceOption<TGetOne> Data(TId id)
        {
            return resourceService.Get(id);
        }
        
        public ServiceOption<IEnumerable<TGetMany>> Data()
        {
            return resourceService.Get(default(TQuery));
        }

        public ServiceOption<IEnumerable<TGetMany>> Data(TQuery query)
        {
            return resourceService.Get(query);
        }
    }
}
