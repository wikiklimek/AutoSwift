using DotNetWebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetWebApp.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Session;

namespace DotNetWebApp
{
	public class Program
	{
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDistributedMemoryCache(); 
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); 
                options.Cookie.HttpOnly = true;  
                options.Cookie.IsEssential = true; 
            });

            //BazyDanych
            var connectionString = builder.Configuration.GetConnectionString("LogInConnection") ?? throw new InvalidOperationException("Connection string 'LogInConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
			builder.Services.AddDbContext<CarContext>(options => options.UseInMemoryDatabase("Cars"));

			//email
			builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddTransient<IEmailSender, EmailSender>();

			//dodanie jako naszej Identity naszego CustomUsera (zamiast IdentityUser)
			builder.Services.AddIdentity<CustomUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
							.AddDefaultTokenProviders()
							.AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddScoped<IUserClaimsPrincipalFactory<CustomUser>, CustomUserClaimsPrincipalFactory>();

			//dodanie autentykacji defaultowej (IdentityUser) i tej z google
			builder.Services.AddAuthentication(o =>
			                 {
				                 o.DefaultScheme = IdentityConstants.ApplicationScheme;
				                 o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
			                 })
							 .AddCookie()
							 .AddGoogle(options =>
							 {
								 options.ClientId = "xxx";
								 options.ClientSecret = "xxx";
							 });



			// Add Controllers with Views and Razor Pages
			builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
			// builder.Services.AddServerSideBlazor();


			var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
			builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: MyAllowSpecificOrigins,
					  policy =>
					  {
						  policy.WithOrigins("https://localhost:7136")
						  .AllowAnyHeader()
						  .AllowAnyMethod();
					  });
			});
			var app = builder.Build();


			if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
			}
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

			app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();


           app.UseRouting();

            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=CarApi}/{action=Index}/{id?}");
            app.MapRazorPages();
           // app.MapBlazorHub();
           // app.MapFallbackToController("Blazor", "Home");

            app.Run();
        }

    }
}
