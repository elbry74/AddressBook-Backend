using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Address_Book.Models;
using AutoMapper;
using AddressBook.Common.ViewModels;
using AddressBook.Application.AddressApp;
using AddressBook.Application.DepartmentApp;
using AddressBook.Application.JobApp;
using AddressBook.EF.UnitOfWork;
using AddressBook.DB.Models;
using AddressBook.Application.Login;

namespace Address_Book_Project
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Address_Book_Project", Version = "v1" });
            });

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            // Configure AutoMapper in your application startup
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddressBooks, AddressVM>();
                cfg.CreateMap<AddressVM, AddressBooks>();

                cfg.CreateMap<Departments, Departmentviewmodel>();
                cfg.CreateMap<Departmentviewmodel, Departments>();

                cfg.CreateMap<Jobs, Jobviewmodel>();
                cfg.CreateMap<Jobviewmodel, Jobs>();

                cfg.CreateMap<Login, LoginViewModel>();
                cfg.CreateMap<LoginViewModel, Login>();

                cfg.CreateMap<Register, RegisterViewModel>();
                cfg.CreateMap<RegisterViewModel, Register>();
            });

            services.AddSingleton<IMapper>(sp => configuration.CreateMapper());

            // Add CORS services
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            services.AddScoped<UnitOfWork>();

            // This means a new instance will be created for each HTTP request.
            services.AddScoped<IManageAddress, ManageAddress>();
            services.AddScoped<IManageDepartment, ManageDepartment>();
            services.AddScoped<IManageJob, ManageJob>();
            services.AddScoped<IManageLogin, ManageLogin>();
            services.AddScoped<IManageRegister, ManageRegister>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Address_Book_Project v1"));
            }

            // Enable CORS
            app.UseCors("AllowOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}