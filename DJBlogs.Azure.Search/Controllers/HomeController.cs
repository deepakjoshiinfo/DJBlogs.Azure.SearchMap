using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DJBlogs.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using Azure.Search.Documents.Indexes;
using Azure;
using Azure.Search.Documents;
using DJBlogs.Azure.Search.Utility;

namespace DJBlogs.Azure.Search.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static SearchClient _searchClient;
        private static SearchIndexClient _indexClient;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(SearchData model)
        {
            try
            {
                // Ensure the search string is valid.
                if (model.searchText == null)
                {
                    model.searchText = "";
                }

                // Make the Azure Cognitive Search call.
                await RunQueryAsync(model).ConfigureAwait(false);
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = "1" });
            }

            return View(model);
        }
      
        private async Task<ActionResult> RunQueryAsync(SearchData model)
        {
                _indexClient = new SearchIndexClient(new Uri(AppConstant.SearchServiceUri), new AzureKeyCredential(AppConstant.SearchServiceQueryApiKey));
                _searchClient = _indexClient.GetSearchClient(AppConstant.SearchIndexName);
            var options = new SearchOptions()
            {
                IncludeTotalCount = true
            };
            options.Select.Add("HotelName");
            options.Select.Add("Description");
            options.Select.Add("Location");
            options.Select.Add("Address");

            model.resultList = await _searchClient.SearchAsync<Hotel>(model.searchText, options).ConfigureAwait(false);
            // Display the results.
            return View("Index", model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
