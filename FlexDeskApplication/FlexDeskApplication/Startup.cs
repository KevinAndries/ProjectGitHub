using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer;
using BusinessLogicLayer;
using System.IO;
using FlexDeskApplication.Models;

namespace FlexDeskApplication
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

            //services.AddDbContext<FlexDesksDbEntities>(options =>
            //  options.UseSqlServer(Configuration.GetConnectionString("FlexDeskConnection")));

            //services.AddDbContext<FlexDesksDbEntities>(options =>
            //    options.UseInMemoryDatabase()
            //);


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<FlexDesksDbEntities>()
                .AddDefaultTokenProviders();

            services.AddMvc().AddSessionStateTempDataProvider(); 
            services.AddSession();


            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionString = config["ConnectionStrings:FlexDeskConnection"];
            //var connectionString = config["ConnectionStrings:localFlexDeskConnection"];



            services.AddSingleton<IDBConnection>(f => new DBConnection { connectionString = connectionString });
            //services.AddTransient<IBuildingProvider>(f =>
            //    new BuildingProvider(config["ConnectionString:FlexDeskConnection"]));


            services.AddTransient<IBuildingProvider, BuildingProvider>();
            services.AddTransient<IBuildingProcessor, BuildingProcessor>();
            services.AddTransient<IBuildingBll, BuildingBll>();

            services.AddTransient<IFloorProvider, FloorProvider>();
            services.AddTransient<IFloorProcessor, FloorProcessor>();
            services.AddTransient<IFloorBll, FloorBll>();

            services.AddTransient<IDepartmentProvider, DepartmentProvider>();
            services.AddTransient<IDepartmentProcessor, DepartmentProcessor>();
            services.AddTransient<IDepartmentBll, DepartmentBll>();

            services.AddTransient<IFlexDeskProvider, FlexDeskProvider>();
            services.AddTransient<IFlexDeskProcessor, FlexDeskProcessor>();
            services.AddTransient<IFlexDeskBll, FlexDeskBll>();

            services.AddTransient<IUserProvider, UserProvider>();
            services.AddTransient<IUserProcessor, UserProcessor>();
            services.AddTransient<IUserBll, UserBll>();

            services.AddTransient<IReservationProvider, ReservationProvider>();
            services.AddTransient<IReservationProcessor, ReservationProcessor>();
            services.AddTransient<IReservationBll, ReservationBll>();

            services.AddTransient<IAbsenceProvider, AbsenceProvider>();
            services.AddTransient<IAbsenceProcessor, AbsenceProcessor>();
            services.AddTransient<IAbsenceBll, AbsenceBll>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
