﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.FluentValidation;
using WebApplication3.Models.ViewModels;
using WebApplication3.Models.ViewModels.Positions;

namespace WebApplication3 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            DbAccess.GetInstance().SetConnectionString(connectionString);
            services.AddControllersWithViews()
                .AddFluentValidation(fv => 
                { fv.RegisterValidatorsFromAssemblyContaining<PositionValidator>();
                    fv.ImplicitlyValidateChildProperties = true; // Автоматический поиск валидаторов для дочерних сложных типов: must not use SetValidator
                    fv.ImplicitlyValidateRootCollectionElements = true; // Автоматическая проверка коллекций сложных типов: must not create TValidator : AbstractValidator<IList<T>>
                }); // Add Fluent Validation
            services.AddCors(options => options.AddDefaultPolicy(x => x.AllowAnyOrigin()));
            services.AddTransient<IValidator<Position>, PositionValidator>();
            services.AddTransient<IValidator<PositionsViewModel>, PositionsViewModelValidator>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors();
            app.UseHttpsRedirection();
            //app.UseCulture();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Order}/{action=Index}/{id?}");
            });
        }
    }
}
