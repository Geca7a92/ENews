namespace ENews.Web
{
    using System.Reflection;

    using CloudinaryDotNet;
    using ENews.Common;
    using ENews.Data;
    using ENews.Data.Common;
    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Data.Repositories;
    using ENews.Data.Seeding;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Services.Mapping;
    using ENews.Services.Messaging;
    using ENews.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddRazorPages().AddXmlSerializerFormatters();

            var cloudinaryAccount = new Account(
                this.configuration["Cloudinary:AppName"],
                this.configuration["Cloudinary:AppKey"],
                this.configuration["Cloudinary:AppSecret"]);

            var cloudinary = new Cloudinary(cloudinaryAccount);

            services.AddSingleton(cloudinary);

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IArticlesService, ArticlesService>();
            services.AddTransient<IGalleriesService, GalleriesService>();
            services.AddTransient<ISubCategoriesService, SubCategoriesService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IPagingService, PagingService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IAddressesService, AddressesService>();
            services.AddTransient<IImagesService, ImagesService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    // dbContext.Database.EnsureDeleted();
                }

                //dbContext.Database.Migrate();

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();  
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("articlePreview", "Article/{id?}/{title}", new { controller = "Articles", action = "Index" });
                        endpoints.MapControllerRoute(GlobalConstants.CategoryRoute, "{data}", new { controller = "Categories", action = "Index" });
                        endpoints.MapControllerRoute(GlobalConstants.SubCategoryRoute, "{subData}/{data}", new { controller = "SubCategories", action = "Index" });
                        endpoints.MapControllerRoute(GlobalConstants.UsernameRoute, "articles/{data}", new { controller = "Users", action = "Articles" });
                        endpoints.MapControllerRoute(GlobalConstants.LocalRoute, GlobalConstants.Country, new { controller = "Categories", action = "Local" });
                        endpoints.MapControllerRoute(GlobalConstants.LocalByRegionRoute, $"{GlobalConstants.Country}/{{data}}", new { controller = "Categories", action = "LocalByRegion" });
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
