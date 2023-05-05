using EncryptConfiguration.Utils;
using Miqo.EncryptedJsonConfiguration;

var builder = WebApplication.CreateBuilder(args);

var key = Convert.FromBase64String("H+uLqEie+Px4PUmInDYl4WlQ/8vCUyeTpHZkv4Fn6UY=");
ConfigurationManager configuration = builder.Configuration;
configuration.AddEncryptedJsonFile("appsettings.ejson", key);
builder.Services.AddJsonEncryptedSettings<ConnectionStrings>(configuration, "ConnectionStrings");
builder.Services.AddJsonEncryptedSettings<Authentication>(configuration, "Authentication");
builder.Services.AddJsonEncryptedSettings<JwtSettings>(configuration, "JwtSettings");

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Secrets}/{action=Default}/{id?}");

app.Run();
