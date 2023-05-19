using BookmarkRepository.Data;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.EntityFrameworkCore;


namespace BookmarkRepository
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            builder.Services.AddDbContext<BookmarkContext>(options =>
                options.UseSqlServer(connectionString)
            );

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
            builder.Services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });

            builder.Services.AddRazorPages();

            builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();

            //builder.Services.AddSingleton<BookmarkRepositoryService>();
            builder.Services.AddHttpClient();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.Run();
        }
    }
}