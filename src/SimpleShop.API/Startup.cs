using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SimpleShop.API.Core.Middlewares;
using SimpleShop.API.Filters;
using SimpleShop.Infrastructure;

namespace SimpleShop.API
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
            //Todo: For review
            //Authentication 
            //services.AddAuthentication(options => {  });

            //Application modules
            services.AddApplicationModule();
            services.AddInfrastructureModule(Configuration.GetConnectionString("DbConnectionString"));

            //Controllers
            services.AddControllers(options =>
            {
                options.Filters.Add<HandleApiExceptionFilter>();
            })
            .AddNewtonsoftJson(x => x.UseMemberCasing());

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleShop.Api", Version = "v1" });
                //c.AddSecurityDefinition("Bearer", SwaggerExtension.GetOpenApiSecurityScheme());
                //c.AddSecurityRequirement(SwaggerExtension.GetOpenApiSecurityRequirement());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleShop.Api v1"));
            }
            
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            //Todo: for review //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
