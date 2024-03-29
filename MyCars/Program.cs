using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyCars.Data;
using MyCars.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
                .AddDefaultUI(); ;



builder.Services.AddRazorPages();

builder.Services.AddSingleton<PathService>();
builder.Services.AddSingleton<ImageService>();

var app = builder.Build();

var scope = app.Services.CreateScope();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var roleExist = await roleManager.RoleExistsAsync("Admin");
    if (!roleExist)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var adminUser = await userManager.FindByEmailAsync("admin@emg.com");
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = "admin@emg.com",
            Email = "admin@emg.com",
        };
        Console.WriteLine("Creatiiiiing");

        var admin = await userManager.CreateAsync(adminUser, "Admin@123");
        if (admin.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            Console.WriteLine("Create {0}", adminUser.UserName);
        }
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }


    if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
