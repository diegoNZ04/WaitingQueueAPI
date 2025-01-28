

using Microsoft.EntityFrameworkCore;
using QueueSystem.Infra.Data;
using QueueSystem.Infra.Repositories;
using QueueSystem.Infra.Repositories.Interfaces;
using QueueSystem.Infra.Services;
using QueueSystem.Infra.Services.Interfaces;

namespace QueueSystem.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            /*
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            */
            services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase("QueueDb"));


            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientService, ClientService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(options =>
                {
                    app.UseSwagger();
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}