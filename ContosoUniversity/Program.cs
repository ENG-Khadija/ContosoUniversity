using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// ≈÷«›… Œœ„«  ﬁ«⁄œ… «·»Ì«‰«  (SchoolContext)
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ≈÷«›… ›· — √Œÿ«¡ «·„ÿÊ—Ì‰ (· Õ”Ì‰ ﬁ—«¡… √Œÿ«¡ EF)
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ≈÷«›… œ⁄„ MVC (Controllers with Views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

//  ÂÌ∆… ﬁ«⁄œ… «·»Ì«‰«  ⁄‰œ »œ¡ «· ‘€Ì·
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SchoolContext>();
        // «· √ﬂœ „‰  ÿ»Ìﬁ «· —ÕÌ·«  Ê»–— «·»Ì«‰« 
        context.Database.EnsureCreated();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

//  ﬂÊÌ‰ »Ì∆… «· ‘€Ì·
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

//  ﬂÊÌ‰ «·„”«— «·«› —«÷Ì («·–Ì ÌÕ· „‘ﬂ·… 404 «·⁄«„…)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();