using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using KidsListService.DataObjects;
using KidsListService.Models;

namespace KidsListService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
             config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            Database.SetInitializer(new KidsListInitializer());
        }
    }

    public class KidsListInitializer : ClearDatabaseSchemaIfModelChanges<KidsListContext>
    {
        protected override void Seed(KidsListContext context)
        {
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
            };
            List<Parent> parents = new List<Parent>
            {
                new Parent { Id = Guid.NewGuid().ToString(), Name = "fons" },
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }
            foreach (Parent parent in parents)
            {
                context.Set<Parent>().Add(parent);
            }

            base.Seed(context);
        }

    }
}

