﻿using bruno.Klir.WebApi.Common.Mapping;

namespace bruno.Klir.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "_allowSpecificOrigins",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200");
                                  });
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMappings();

            return services;
        }

    }
}
