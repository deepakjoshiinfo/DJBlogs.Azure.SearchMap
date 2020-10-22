using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJBlogs.Azure.Search.Utility
{
    public class AppConstant
    {
        private static IConfigurationBuilder _builder;
        private static IConfigurationRoot _configuration;
        public static string SearchServiceUri;
        public static string SearchServiceQueryApiKey;
        public static string SearchIndexName;
        public static string MapSubscriptionKey;
        static AppConstant()
        {
            _builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            _configuration = _builder.Build();
            //To get data from appsettings.json
            SearchServiceUri = _configuration["SearchServiceUri"];
            SearchServiceQueryApiKey = _configuration["SearchServiceQueryApiKey"];
            SearchIndexName = _configuration["SearchIndexName"];
            MapSubscriptionKey = _configuration["MapSubscriptionKey"];
        }
    }
}
