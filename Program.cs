using eTickets.Data;
using eTickets.Data.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eTickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


           builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Services Configuration

            builder.Services.AddScoped<IActorsService, ActorsService>();
            builder.Services.AddScoped<IProducerService, ProducersService>();
            builder.Services.AddScoped<ICinemasService,CinemasServices>();
            builder.Services.AddScoped<IMoviesService,MoviesService>();
            builder.Services.AddScoped<IOrdersService,OrdersService>();
            builder.Services.AddHttpContextAccessor(); // Required for accessing session
            builder.Services.AddSession(); // Enables session usage
            builder.Services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetShoppingCart(sp));


            //Authentication and Authorization Configuration

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddMemoryCache(); // Required for session state
            builder.Services.AddSession();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //{
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireLowercase = true;
            //});


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();


            //Authentication
            app.UseAuthentication();
            app.UseAuthorization();


            AppDbInitializer.Seed(app);
            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");

            AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
            app.Run();
            
        }
    }
}
