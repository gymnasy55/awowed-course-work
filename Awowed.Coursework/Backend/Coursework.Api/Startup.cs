using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coursework.Api.Domain;
using Coursework.Api.Domain.Repositories.Abstract;
using Coursework.Api.Domain.Repositories.EntityFramework;
using Coursework.Api.Service;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Api
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
            Configuration.Bind("Project", new Config());

            services.AddControllers();

            services.AddTransient<IEmployeesRepository, EfEmployeesRepository>();
            services.AddTransient<IInsertionsRepository, EfInsertionsRepository>();
            services.AddTransient<IMetalsRepository, EfMetalsRepository>();
            services.AddTransient<IOrdersRepository, EfOrdersRepository>();
            services.AddTransient<IProdgroupsRepository, EfProdgroupsRepository>();
            services.AddTransient<IProductsRepository, EfProductsRepository>();
            services.AddTransient<IProductssalesRepository, EfProductssalesRepository > ();
            services.AddTransient<ISuppliersRepository, EfSuppliersRepository>();
            services.AddTransient<IUsersRepository, EfUsersRepository>();

            services.AddDbContext<AppDbContext>(x => x.UseMySQL(Config.ConnectionString));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
