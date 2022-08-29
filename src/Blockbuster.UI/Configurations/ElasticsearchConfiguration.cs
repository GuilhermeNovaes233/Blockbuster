﻿using Nest;

namespace Blockbuster.UI.Configurations
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultIndex = configuration["ElasticsearchSettings:defaultIndex"];
            var basicAuthUser = configuration["ElasticsearchSettings:username"];
            var basicAuthPassword = configuration["ElasticsearchSettings:password"];

            var settings = new ConnectionSettings(new Uri(configuration["ElasticsearchSettings:uri"]));

            if (!string.IsNullOrEmpty(defaultIndex))
                settings = settings.DefaultIndex(defaultIndex);

            if (!string.IsNullOrEmpty(basicAuthUser) && !string.IsNullOrEmpty(basicAuthPassword))
                settings = settings.BasicAuthentication(basicAuthUser, basicAuthPassword);

            settings.EnableApiVersioningHeader();

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}