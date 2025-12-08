using MCRental_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MCRentalDBContextConnection") ?? throw new InvalidOperationException("Connection string 'MCRentalDBContextConnection' not found.");;

builder.Services.AddDbContext<MCRental_Models.MCRentalDBContext>();

builder.Services.AddDefaultIdentity<Gebruiker>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MCRentalDBContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();
//config restful API
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MCRental_Web", Version = "v1" }); });

builder.Services.AddLocalization(builder => { builder.ResourcesPath = "Translation"; });
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        MCRental_Models.MCRentalDBContext context = services.GetRequiredService<MCRental_Models.MCRentalDBContext>();
        MCRental_Models.MCRentalDBContext.seeder(context);
    }
    catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "Er is een fout bij het seeden van de database.");
    }
}

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MCRental_Web v1"));
}

//middleware localisatize
var supportedCultures = new[] { "nl", "fr", "en" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseEndpoints(ep => ep.MapControllers());

app.MapRazorPages();


app.Run();
