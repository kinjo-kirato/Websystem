using WebEmployeeManagement.Infrastructures.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<Microsoft.AspNetCore.Mvc.Razor.RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("/Presentations/Views/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Presentations/Views/Shared/{0}.cshtml");
});

var app = builder.Build();

DbAccsess.Initialize();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
