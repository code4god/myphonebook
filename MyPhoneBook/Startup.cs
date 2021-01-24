using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyPhoneBook.DataLayer;
using MyPhoneBook.DataLayer.Repository;
using MyPhoneBook.DataLayer.Repository.Interfaces;
using System;
using Microsoft.OpenApi.Models;

namespace MyPhoneBook
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
            string connectionString = Configuration.GetConnectionString("PhoneBookConnectionString");
            SetMigrate(connectionString);
            services.AddDbContextPool<MyPhoneBookContext>(options => options.UseSqlServer(connectionString));
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPhoneBookRepository, PhoneBookRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "MyPhoneBook.API", Version = "v1" });
            });
        }

        private void SetMigrate(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyPhoneBookContext>();
            optionsBuilder.UseSqlServer(
                connectionString, 
                builder=> builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));

            using (var context = new MyPhoneBookContext(optionsBuilder.Options))
            {
                context.Database.Migrate();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyPhoneBook.API");
            });
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
