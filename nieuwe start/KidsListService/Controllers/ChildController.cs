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
    public class ChildController : TableController<Child>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            KidsListContext context = new KidsListContext();
            DomainManager = new EntityDomainManager<Child>(context, Request, Services);
        }

        // GET tables/Children
        public IQueryable<Child> GetAllChildren()
        {
            return Query();
        }

        // GET tables/Children/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Child> GetChild(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Children/48D68C86-6EA6-4C25-AA33-223FC9A27959
       /* public Task<Child> PatchChild(string id, Delta<Parent> patch)
        {
            return UpdateAsync(id, patch);
        }*/

        // POST tables/Children
        public async Task<IHttpActionResult> PostChild(Child child)
        {
            Child current = await InsertAsync(child);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Children/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteChild(string id)
        {
            return DeleteAsync(id);
        }
    }
}