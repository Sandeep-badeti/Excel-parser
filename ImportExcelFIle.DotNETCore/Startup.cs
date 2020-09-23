using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportExcelFIle.DotNETCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImportExcelFIle.DotNETCore
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

            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins(Configuration["ServiceUrls:AngularUrl"]) //local url
                  /* builder => builder.WithOrigins(Configuration["ServiceUrls:QAUrl"])*///qa url
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowAnyOrigin()
                  .AllowCredentials());

            });
            services.AddMvc();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddControllersWithViews();
            var constr = Configuration.GetConnectionString("ExcelContext");
            services.AddDbContextPool<AppDbContext>(
            options => options.UseSqlServer(constr));
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
  
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=ExportExcel}/{id?}");
            });
        }
    }
}
