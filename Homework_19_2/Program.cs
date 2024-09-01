using Homework_20.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<AccountService>(provider => {
    var clientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var client = clientFactory.CreateClient("APIClient");
    return new AccountService(client, builder.Configuration);
});

builder.Services.AddScoped<ContactService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Domain = "localhost";
        options.Cookie.Path = "/";
        options.Cookie.HttpOnly = true;
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.Name = "SessionToken"; 
        options.Cookie.SameSite = SameSiteMode.None;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
    });

builder.Services.AddHttpClient("APIClient", client => {
    client.BaseAddress = new Uri(builder.Configuration["API:BaseUrl"]);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();