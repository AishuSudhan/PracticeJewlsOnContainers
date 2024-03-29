﻿using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Infrastructure.Infrastructures;
using Infrastructure.Models;

namespace Infrastructure.Services
{
    public class CatalogClass : ICatalog
    {
        private readonly IHttpClient _httpclient;
        private readonly string _baseurl;
        public CatalogClass(IConfiguration config,IHttpClient httpclient)
        {
            _httpclient = httpclient;
            _baseurl = $"{config["CatalogUrl"]}/api/catalog";

        }
        public async Task<Catalog> GetAllItems(int pagenumber, int pagesize,int? brand,int? type)
        {
           var customurl= APIpaths.Catalog.GetallItems(_baseurl, pagenumber, pagesize,brand,type);
            var datastring = await _httpclient.GetStringAsync(customurl);//we are sending this url to the method in httpclientclass.
                                                                         //this is the line actually gets results from microservice.
                                                                         //so that it will get the results and the we can do deserialization in next line of code.
            return JsonConvert.DeserializeObject<Catalog>(datastring);
        }

        public async Task<IEnumerable<SelectListItem>> Getbrands()
        {
           var customUrl= APIpaths.Catalog.Getallbrands(_baseurl);
            var datastring =await  _httpclient.GetStringAsync(customUrl);
            var item = new List<SelectListItem>()
            { //this code will add a new element "All" in the dropdown view.
                new SelectListItem
                {
                    Value = null,
                    Text = "All",
                    Selected = true
                }
            };
               var brands= JArray.Parse(datastring);//here we are converting the json array in to objects(deserialization).
            foreach(var items in brands)
            {
                item.Add(new SelectListItem
                {
                    Value = items.Value<string>("id"),
                    Text = items.Value<string>("brand")
                });
            }
            return item;
            
        }

        public async Task<IEnumerable<SelectListItem>> Gettypes()
        {
            var typesurl= APIpaths.Catalog.Getalltypes(_baseurl);
            var datastring = await _httpclient.GetStringAsync(typesurl);
            var items = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };
            var Types = JArray.Parse(datastring);
            foreach(var item in Types)
            {
                items.Add(new SelectListItem
                {
                    Value = item.Value<string>("id"),
                    Text = item.Value<string>("type")
                });
            }
            return items;
        }
    }
}
