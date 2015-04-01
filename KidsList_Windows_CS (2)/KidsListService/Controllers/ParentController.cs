using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
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

        // GET tables/PARENTS
        public IQueryable<Parent> GetAllParents()
        {
            return Query();
        }

        // GET tables/PARENTS/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Parent> GetParent(string ID)
        {
           
            return Lookup(ID);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        /*public Task<TodoItem> PatchTodoItem(string id, Delta<TodoItem> patch)
        {
            return UpdateAsync(id, patch);
        }*/

        // POST tables/TodoItem
       /* public async Task<IHttpActionResult> PostTask(LIST item)
        {
            TodoItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }*/

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
      /*  public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }*/

    }
}
