using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;

namespace Test_app
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<IDbService, DbService>();

            //Generate packages with the following command in the package manager, set serviceLayer project as startup and target.
            //Scaffold-DbContext "Server=dispatch-rds-qa.acqa.foreflight.com;Port=5432;Database=eopmaster;User Id=eopuser;Password=curlyLeop@rd87;" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir .\Models\
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                pgnsqlOptions =>
                {
                    pgnsqlOptions.EnableRetryOnFailure();
                    pgnsqlOptions.SetPostgresVersion(new Version("9.6"));
                }));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}