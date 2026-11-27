using Microsoft.EntityFrameworkCore;
using MVCECommerceCourse.Business.Services;
using MVCECommerceCourse.Business.Services.IServices;
using MVCECommerceCourse.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//ApplicationDbContext is created here and used for DependencyInjection to other class
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:SQLConnection").Value);
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
});
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Customer"})
    .WithStaticAssets();


app.Run();
