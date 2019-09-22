using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nanr.Data;
using Nanr.Api.Managers;
using Nanr.Api.Filters;

namespace Nanr.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AuthFilter));
            });
            services.AddDbContext<NanrDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NanrDatabase")));
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IClickManager, ClickManager>();
            services.AddScoped<IPurchaseManager>(s =>
            {
                return new PurchaseManager(s.GetRequiredService<NanrDbContext>(), Configuration.GetValue<string>("squareApiBase"), Configuration.GetValue<string>("squareApiKey"), s.GetRequiredService<IEmailManager>());
            });
            services.AddScoped<IEmailManager>(s =>
            {
                return new EmailManager(Configuration.GetValue<string>("sendGridKey"));
            });
            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped<ITagManager, TagManager>();
            services.AddScoped<IContactManager, ContactManager>();
            services.AddSingleton(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseCors(config =>
                {
                    config.AllowAnyHeader();
                    config.AllowAnyMethod();
                    config.WithOrigins("*");
                });
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseCors(config =>
                {
                    config.AllowAnyHeader();
                    config.AllowAnyMethod();
                    config.WithOrigins("*");
                });
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "tags",
                   pattern: "tags/{id}",
                   defaults: new { controller = "Tags", action = "Index" });
                endpoints.MapControllers();
            });
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            {
                using (var context = serviceScope.ServiceProvider.GetService<NanrDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
