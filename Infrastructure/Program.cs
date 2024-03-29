using Infrastructure.Infrastructures;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllersWithViews();
//we are asking the builder to add controllers with views in webmvc because it needs views to display.
//but in productcatalogapi we just said addcontrollers because there is no views in productcatalogapi.

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IHttpClient, HttpClientClass>();

//this code tells that IHttpClient Interface is connecting with the class HttpclientClass.In future if we want to change the class for the interface 
//IHttpClient we can remove HttpClientClass class and add new class and update that class name only here.that is enough.
//singleton means only one instance created and use it throughout the project whenever we need it. 
//the purpose of HttpClient is send url to microservices and get the results. just like postman. we dont need to create mant instances for that.
//one is enough.just like open one safari and open multiple pages in that one safari.
//Transient is opposite to singleton.create instance whenever you need one.you can create many instances as per our need.

builder.Services.AddTransient<ICatalog, CatalogClass>();
builder.Services.AddTransient<IIdentityService<ApplicationUser>, Identityservice>();

var identityUrl = configuration["IdentityUrl"];
var callBackUrl = configuration["CallBackUrl"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = identityUrl.ToString();
    options.SignedOutRedirectUri = callBackUrl.ToString();
    options.ClientId = "mvc";
    options.ClientSecret = "secret";
    options.RequireHttpsMetadata = false;
    options.SaveTokens = true;
    options.ResponseType = "code id_token";
    options.GetClaimsFromUserInfoEndpoint = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("order");
    options.Scope.Add("basket");
    options.TokenValidationParameters = new TokenValidationParameters()
    {

        NameClaimType = "name",
        RoleClaimType = "role",
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

//in this mapcontrollerroute we are setting the program to start from Index method in the Catalog controller.thatis startup for the WebMVC project.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}");

app.Run();
