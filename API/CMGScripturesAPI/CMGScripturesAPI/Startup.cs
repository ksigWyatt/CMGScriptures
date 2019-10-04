using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CMGScriptures.Core.System.ExceptionHandler;
using CMGScripturesAPI.Core;
using CMGScripturesAPI.Repos;
using CMGScripturesAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace CMGScripturesAPI
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // grab the settings from our AppSettings.json
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Startup dependencies

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Register the Configuration to use our app settings from the json file
            services.Configure<AppSettings>(Configuration);

            #endregion

            #region Init Connection to MongoDB

            // Add to our Config object so it can be injected later
            services.Configure<AppSettings>(options => Configuration.GetSection(nameof(AppSettings.MongoConnectionString)).Bind(options));

            var mongoConfig = Configuration.GetSection(nameof(AppSettings.MongoConnectionString));

            if (mongoConfig == null)
            {
                throw new ArgumentNullException($"IConfiguration.{nameof(AppSettings.MongoConnectionString)}",
                    string.Format(APIMessages.ConnectionMissingFromAppSettings, nameof(AppSettings.MongoConnectionString)));
            }

            var mongoConnection = Configuration[nameof(AppSettings.MongoConnectionString)];

            #endregion

            #region Init Connection to CMG API

            // Add to our Config object so it can be injected later
            services.Configure<AppSettings>(options => Configuration.GetSection(nameof(AppSettings.CMGApiUrl)).Bind(options));

            var cmgApiUrl = Configuration.GetSection(nameof(AppSettings.CMGApiUrl));

            if (cmgApiUrl == null)
            {
                throw new ArgumentNullException($"IConfiguration.{nameof(AppSettings.CMGApiUrl)}",
                    string.Format(APIMessages.ConnectionMissingFromAppSettings, nameof(AppSettings.CMGApiUrl)));
            }

            #endregion

            #region Configure Logging

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            #endregion

            #region Swagger Docs

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CMG Scriptures API", Version = "v1" });
            });

            // Preserve Casing of JSON Objects
            services.AddMvc()
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            #endregion

            #region Register for Dipendency Injection

            #region Services

            services.AddTransient(typeof(IScripturesService), typeof(ScripturesService));

            #endregion

            #region Repositories

            services.AddTransient(typeof(IScriptureRepo), typeof(ScriptureRepo));

            #endregion

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region Add Middleware

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // add exception handling 
            app.ConfigureCustomExceptionMiddleware();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMG Scriptures API v1");
                c.RoutePrefix = "swagger"; // enable swagger at ~/swagger  
            });

            // we need to tell CORS to allow us to receive requests from anywhere
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Enable Model View Controller
            app.UseMvc();

            #endregion
        }
    }
}
