using Autofac;
using Games.Api.Authentication;
using Games.Core;
using Games.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Games.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiAuthentication(Configuration.GetSection("Api:Authentication"));

            services.AddControllers()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    x.SerializerSettings.Formatting = Formatting.None;
                    x.SerializerSettings.Converters.Add(new StringEnumConverter());
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("Default", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddSwaggerGen(x =>
            {
                var name = GetType().Assembly.GetName().Name;
                x.SwaggerDoc(name, new OpenApiInfo { Title = name });

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });

            });
            services.AddSwaggerGenNewtonsoftSupport();

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterApi(Configuration.GetSection("Api"));
            builder.RegisterCore();
            builder.RegisterInfrastructure(Configuration.GetSection("Infrastructure"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var mobileApiName = GetType().Assembly.GetName().Name;
                c.SwaggerEndpoint($"{mobileApiName}/swagger.json", mobileApiName);
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<HandleExceptionsMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
