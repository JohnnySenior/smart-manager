//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartManager.Brokers.Loggings;
using SmartManager.Brokers.Spreadsheets;
using SmartManager.Brokers.Storages;
using SmartManager.Services.Foundations.Applicants;
using SmartManager.Services.Foundations.Groups;
using SmartManager.Services.Foundations.Spreadsheets;
using SmartManager.Services.Foundations.Users;
using SmartManager.Services.Orchestrations;
using SmartManager.Services.Proccessings.Applicants;
using SmartManager.Services.Proccessings.Groups;
using SmartManager.Services.Proccessings.Spreadsheets;
using SmartManager.Services.Proccessings.Users;

namespace SmartManager
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
            services.AddControllersWithViews();
            services.AddDbContext<IStorageBroker, StorageBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
            services.AddTransient<IOrchestrationService, OrchestrationService>();
            services.AddTransient<IApplicantProcessingService, ApplicantProcessingService>();
            services.AddTransient<IApplicantService, ApplicantService>();
            services.AddTransient<ISpreadsheetsProcessingService, SpreadsheetsProcessingService>();
            services.AddTransient<ISpreadsheetService, SpreadsheetService>();
            services.AddTransient<ISpreadsheetBroker, SpreadsheetBroker>();
            services.AddTransient<IGroupProcessingService, GroupProcessingService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IUserProcessingService, UserProcessingService>();
            services.AddTransient<IUserService, UserService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
