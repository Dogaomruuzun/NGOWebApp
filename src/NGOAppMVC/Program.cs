using Microsoft.EntityFrameworkCore;
using NGOAppMVC.DBModels;
using Microsoft.AspNetCore.Identity;
using NGOAppMVC.Data;
using NGOAppMVC.Areas.Identity.Data;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DCodeNGOdataNGOsqliteContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatabase")));

builder.Services.AddDbContext<NGOAppMVCContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatabase")));

builder.Services.AddDefaultIdentity<NGOUser>()
    .AddUserStore<NGOUserStore>()
    .AddEntityFrameworkStores<NGOAppMVCContext>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapGet("/pagelist", (IActionDescriptorCollectionProvider provider) =>
{
    var pages = provider.ActionDescriptors.Items
        .Where(ad => ad is PageActionDescriptor)
        .Select(ad => ((PageActionDescriptor)ad).ViewEnginePath)
        .ToList();

    var response = "<h2>List of Pages</h2><ul>";
    foreach (var page in pages)
    {
        response += $"<li><a href='{page}'>{page}</a></li>";
    }
    response += "</ul>";

    return Results.Content(response, "text/html");
});
app.Run();
