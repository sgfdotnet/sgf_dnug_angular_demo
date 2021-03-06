﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularDemoApp.Controllers
{
    public class ListController : ApiController
    {
        [HttpGet]
        [Route("api/v1/lists")]
        public List<ListView> Get()
        {
            return InMemoryData.Lists;
        }

        [HttpPost]
        [Route("api/v1/lists/{listId}/item")]
        public HttpResponseMessage AddItem(int listId, NameView input)
        {
            var list = InMemoryData.Lists.FirstOrDefault(x => x.Id == listId);
            if (list != null)
            {
                list.Items.Add(input.Name);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("api/v1/lists")]
        public ListView AddList(NameView input)
        {
            var list = new ListView
            {
                Id = InMemoryData.GetNextId(),
                Name = input.Name,
                Items = new List<string>()
            };

            InMemoryData.Lists.Add(list);

            return list;
        }
    }

    public static class InMemoryData
    {
        public static List<ListView> Lists = new List<ListView>
        {
            new ListView {Id = 1, Name = "api list 1", Items = new List<string> {"api item 1", "api item 2"}},
            new ListView {Id = 2, Name = "api list 2", Items = new List<string> {"api item 3", "api item 4"}},
        };

        public static int GetNextId()
        {
            return Lists.Max(x => x.Id) + 1;
        }
    }

    public class ListView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Items { get; set; } 
    }

    public class NameView
    {
        public string Name { get; set; }
    }
}