using Store.Domain.Data;
using Store.Domain.Tools;
using Store.Domain.Tools.Security;

namespace Store.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews()
                            .AddRazorRuntimeCompilation();
            builder.Services.AddStoreDbContext(builder.Configuration);
            builder.Services.AddJwtThings(builder.Configuration);
            builder.Services.AddAuthorization();
            builder.Services.AddTransient<EmailSender>();

            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                if (context.Request.Cookies.TryGetValue("token", out string? token))
                {
                    context.Request.Headers.Authorization = $"Bearer {token}";
                }
                await next();
            });

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<StoreDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception exeption)
                {
                    Console.WriteLine(exeption);
                }
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllerRoute(name: "",
                                   pattern: "Registration",
                                   defaults: new
                                   {
                                       controller = "Auth",
                                       action = "Registration"
                                   });

            app.MapControllerRoute(name: "",
                                   pattern: "Login",
                                   defaults: new
                                   {
                                       controller = "Auth",
                                       action = "Login"
                                   });

            app.MapControllerRoute(name: "",
                                   pattern: "Cart",
                                   defaults: new
                                   {
                                       controller = "Cart",
                                       action = "CartList"
                                   });

            app.MapControllerRoute(name: null,
                                   pattern: "",
                                   defaults: new
                                   {
                                       controller = "Home",
                                       action = "ListOfProducts",
                                       category = (string)null,
                                       page = 1
                                   });

            app.MapControllerRoute(name: null,
                                   pattern: "Page{page}",
                                   defaults: new
                                   {
                                       controller = "Home",
                                       action = "ListOfProducts",
                                       category = (string)null
                                   },
                                   constraints: new { page = @"\d+" });

            app.MapControllerRoute(name: "category",
                                   pattern: "{category}",
                                   defaults: new
                                   {
                                       controller = "Home",
                                       action = "ListOfProducts",
                                       page = 1
                                   });

            app.MapControllerRoute(name: null,
                                   pattern: "{category}/Page{page}",
                                   defaults: new
                                   {
                                       controller = "Home",
                                       action = "ListOfProducts"
                                   },
                                   constraints: new { page = @"\d+" });

            app.MapControllerRoute(name: null,
                                   pattern: "{controller}/{action}");

            app.MapControllerRoute(name: null,
                                   pattern: "Page{page}",
                                   defaults: new
                                   {
                                       controller = "Home",
                                       action = "ListOfProducts"
                                   });

            app.MapControllerRoute(name: "default",
                                   pattern: "{controller=Home}/{action=ListOfProducts}/{id?}");

            app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>  //штука для дебага
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints))); //все прописанные маршруты адресной строки

            app.Run();
        }
    }
}