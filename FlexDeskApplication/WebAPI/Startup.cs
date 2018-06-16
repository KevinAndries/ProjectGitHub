using System;
using System.IO;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Model.FlexDeskDb;
using FlexDeskApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace WebAPI
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
            //Hier wordt de connectionString opgeroepen vanuit de appSettings om de connectie naar de database uit te voeren. 

            services.AddDbContext<FlexDesksDbEntities>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FlexDeskConnection")));

 


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<FlexDesksDbEntities>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionString = config["ConnectionString:FlexDeskConnection"];

            services.AddSingleton<IDBConnection>(f => new DBConnection { connectionString = connectionString });
            //services.AddTransient<IBuildingProvider>(f =>
            //    new BuildingProvider(config["ConnectionString:FlexDeskConnection"]));


            services.AddTransient<IBuildingProvider, BuildingProvider>();
            services.AddTransient<IBuildingProcessor, BuildingProcessor>();
            services.AddTransient<IBuildingBll, BuildingBll>();

            services.AddTransient<IFloorProvider,FloorProvider>();
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







            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "FlexDeskApplication",
                    Description = "Test WebApi FlexDesk Application",
                    //TermsOfService = "None",
                    //Contact = new Contact { Name = "Shayne Boyer", Email = "", Url = "https://twitter.com/spboyer" },
                    //License = new License { Name = "Use under LICX", Url = "https://example.com/license" }
                });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "WebApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDeveloperExceptionPage();

            app.UseSwagger();



            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlexDeskApplication");
            });

            app.UseMvc();
        }
    }
}
