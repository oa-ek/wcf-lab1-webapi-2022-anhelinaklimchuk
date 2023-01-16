using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Repos;
using Pharmacy.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("PharmacyDbContextConnection");
builder.Services.AddDbContext<PharmacyDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
    option.Cookie.IsEssential = true;
});

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
}).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PharmacyDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<CategoryRepository>();
builder.Services.AddTransient<UsersRepository>();
builder.Services.AddTransient<SubCategoryRepository>();
builder.Services.AddTransient<CatalogRepository>();
builder.Services.AddTransient<MedicamentsRepository>();
builder.Services.AddTransient<BrendRepository>();
builder.Services.AddTransient<CountryRepository>();
builder.Services.AddTransient<ProductLineRepository>();
builder.Services.AddTransient<SubCategoryMedicamentsRepository>();
builder.Services.AddTransient<OrderRepository>();
builder.Services.AddTransient<Service>();
//builder.Services.AddTransient<CartViewRepository>();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
