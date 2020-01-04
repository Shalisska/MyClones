using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC.MyClones.Hubs;

namespace MVC.MyClones
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //var conf = Configuration.GetSection("ConnectionStrings").GetChildren();

            var executionFolder = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            AssemblyLoadContext.Default.Resolving += (AssemblyLoadContext context, AssemblyName assembly) =>
            {
                // DISCLAIMER: NO PROMISES THIS IS SECURE. You may or may not want this strategy. It's up to
                // you to determine if allowing any assembly in the directory to be loaded is acceptable. This
                // is for demo purposes only.
                return context.LoadFromAssemblyPath(Path.Combine(executionFolder, $"{assembly.Name}.dll"));
            };

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();
            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });
            services.AddAutofac();

            var builder = new ContainerBuilder();
            ServiceRegistrator.Register(builder);
            builder.Populate(services);

            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();


            //app.UseSignalR(r => { r.MapHub<TestHub>("/test"); });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<TestHub>("/test");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
