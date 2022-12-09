using Grpc.Net.Client;
using VolunteerManager;
using Microsoft.AspNetCore.Components.Authorization;
using VolunteerManager.Authentication;
using VolunteerManager.Services;


Console.WriteLine("press any key to continue...");
Console.ReadLine();
// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:4566");
// The Localhost refuses to connect if using the code that's commented below. Still haven't figured out why it's not working.
//var client = new CreateEventService.CreateEventServiceClient(channel);
//var reply = client.createEvent(new CreateEventInfo
//{
//    EventName = "Halloween Party",
//    EventDate = "28.10.2022",
//    StartTime = 22,
//    EndTime = 2,
//    Location = "VIA University College"
//});
//Console.WriteLine("Event Id: " + reply.EventId);
//Console.WriteLine("press any key to exit");
//Console.ReadLine();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthManager, AuthManagerImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

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