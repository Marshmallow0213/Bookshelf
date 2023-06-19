using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC5_UsedBookProject.Data;
using CoreMVC5_UsedBookProject.Interfaces;
using CoreMVC5_UsedBookProject.Services;
using CoreMVC5_UsedBookProject.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace CoreMVC5_UsedBookProject
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
            services.AddControllersWithViews();
            //�[�JCookie����, �P�ɳ]�w�ﶵ
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    //�w�]�n�J���Һ��}��Account/Login, �Y�Q�ܧ�~�ݭn�]�wLoginPath
                    options.LoginPath = new PathString("/SellerAccount/Login/");
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/SellerAccount/Forbidden/";
                });
            services.AddDbContext<ProductContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ProductContext")));
            services.AddDbContext<AdminAccountContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AdminAccountContext")));
            services.AddTransient<AccountService>();
            services.AddScoped<SellerService>();
            services.AddScoped<SellerRepository>();
            services.AddScoped<AdminAccountService>();
            services.AddSingleton<IHashService, HashService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images"
            });
            app.UseRouting();

            app.UseAuthentication(); //����

            app.UseAuthorization();  //���v

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
