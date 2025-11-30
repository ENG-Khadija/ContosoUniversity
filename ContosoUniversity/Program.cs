using ContosoUniversity.Data; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ≈÷«›… „—‘Õ «” À‰«¡«  ’›Õ… „ÿÊ— ﬁ«⁄œ… «·»Ì«‰« 
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // 2. «·Õ’Ê· ⁄·Ï ”Ì«ﬁ ﬁ«⁄œ… «·»Ì«‰« 
        var context = services.GetRequiredService<SchoolContext>();

        // 3.  ÿ»Ìﬁ «· ‰ﬁ·« 
        context.Database.Migrate();

        // 4.  ÂÌ∆… «·»Ì«‰«  «·√Ê·Ì…
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// ... »ﬁÌ… „·› Program.cs ...
app.Run();

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

app.Run();
