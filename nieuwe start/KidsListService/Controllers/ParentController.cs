using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using KidsListService.DataObjects;
using KidsListService.Models;

namespace KidsListService.Controllers
{
    public class ParentController : TableController<Parent>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            KidsListContext context = new KidsListContext();
            DomainManager = new EntityDomainManager<Parent>(context, Request, Services);
        }

        // GET tables/TodoItem
        public IQueryable<Parent> GetAllParents()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Parent> GetParent(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Parent> PatchParent(string id, Delta<Parent> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostParent(Parent parent)
        {
            Parent current = await InsertAsync(parent);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteParent(string id)
        {
            return DeleteAsync(id);
        }
    }
}