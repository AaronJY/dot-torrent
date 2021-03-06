﻿using DotTorrent.Web.Config;
using DotTorrent.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Microsoft.Extensions.Hosting;
namespace DotTorrent.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton<ITorrentFinderHttpService, TorrentFinderHttpService>();
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddTransient<IRestClient, RestClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();

            //app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
              endpoints.MapControllers();
            });
        }
    }
}
