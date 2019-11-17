using BookStore.Common;
using BookStore.WebApi;
using BookStore.WebApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;

namespace BookStore
{
    public class Startup
    {
        private Container _container = new Container();
        private readonly string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_myAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddMvc();
            
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore()
                .AddControllerActivation()
                .AddViewComponentActivation();
            });

            _container.Register(typeof(IDeliveryServiceGenerator), typeof(DeliveryServiceGenerator), Lifestyle.Transient);

            string dataSource = Configuration.GetSection("AppSettings")["DataSource"] ?? "";
            var dataSourceEnum = (DataSourceType)Enum.Parse(typeof(DataSourceType), dataSource);

            var dataSourceInjector = new DataSourceInjector(_container);
            dataSourceInjector.InjectServices(dataSourceEnum);

            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(_myAllowSpecificOrigins);

            _container.Verify();

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
