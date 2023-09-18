using E_commerce_2.Auth.Models;
using E_commerce_2.Auth.Models.Interface;
using E_commerce_2.Auth.Models.Services;
using E_commerce_2.Data;
using E_commerce_2.Models.Interface;
using E_commerce_2.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDbContext<TheMarketDBContext>
(opions => opions.UseSqlServer(connString));
// Add services to the container.

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<TheMarketDBContext>();

// failed trials - accessing path
// new for cookies auth
builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/auth/index";
});

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();
builder.Services.AddTransient<IUser, UserServices>();


builder.Services.AddTransient<ICategories, CategoriesServices>();
builder.Services.AddTransient<IProduct, ProductService>();
builder.Services.AddTransient<ICategoriesProduct, CategoriesProductService>();


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


app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();







