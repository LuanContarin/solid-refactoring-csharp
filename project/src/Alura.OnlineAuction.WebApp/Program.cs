using Alura.OnlineAuctions.WebApp.Data;
using Alura.OnlineAuctions.WebApp.Data.EfCore;
using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Seeding;
using Alura.OnlineAuctions.WebApp.Services.Handlers;
using Alura.OnlineAuctions.WebApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

DatabaseGenerator.Seed();

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddTransient<IAuctionDao, AuctionDao>();
builder.Services.AddTransient<ICategoryDao, CategoryDao>();

builder.Services.AddTransient<IAdminService, ArchiveAdminService>();
builder.Services.AddTransient<IProductService, DefaultProductService>();

builder.Services
    .AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Custom
app.UseDeveloperExceptionPage();
app.UseStatusCodePagesWithRedirects("/Home/StatusCode/{0}");

app.Run();