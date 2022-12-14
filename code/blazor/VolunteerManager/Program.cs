using Grpc.Net.Client;
using Microsoft.AspNetCore.Components.Authorization;
using VolunteerManager.Authentication;
using VolunteerManager.Services;


// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:4566");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthManager, AuthManagerImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

// Authorization policies
builder.Services.AddAuthorization(options => {
    options.AddPolicy("Manager", a =>
        a.RequireAuthenticatedUser().RequireClaim("IsManager", true.ToString()));
    
    options.AddPolicy("Administrator", a =>
        a.RequireAuthenticatedUser().RequireClaim("IsAdministrator", true.ToString()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();