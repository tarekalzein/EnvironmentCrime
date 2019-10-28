using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using EnvironmentCrime.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace EnvironmentCrime
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration conf)
        {
            Configuration = conf;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddSession();
            services.AddTransient<IErrandRepository, EFErrandRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseMvcWithDefaultRoute();
            app.UseStatusCodePages();
            app.UseStaticFiles();
        }
    }
}
