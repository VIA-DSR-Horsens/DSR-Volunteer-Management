using System.Threading.Tasks;
using Grpc.Net.Client;
using VolunteerManager;
using Microsoft.AspNetCore.Components.Authorization;
using VolunteerManager.Authentication;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:4566");
var client = new CreateEvent.CreateEventChannel(channel);
var reply = await client.SayHelloAsync(
    new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>();

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
