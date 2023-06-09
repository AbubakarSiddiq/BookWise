using BookWise.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace BookWise.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.Configure<ServiceOptions>(this.Configuration.GetSection("Services"));
            services.AddScoped<IServiceOptionsService, ServiceOptionsService>();

            services.AddMvc()
                    .AddJsonOptions(opt => opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://bookwise.angular.app:4200");
                    //spa.UseProxyToSpaDevelopmentServer(this.Configuration["NG_DEV_SERVER"]);
                }
                else
                {
                    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                    {
                        OnPrepareResponse = context =>
                        {
                            if (context.File.Name == "index.html")
                            {
                                context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                                context.Context.Response.Headers.Add("Max-Age", "0");
                            }
                        },
                    };
                }
            });
        }
    }
}
