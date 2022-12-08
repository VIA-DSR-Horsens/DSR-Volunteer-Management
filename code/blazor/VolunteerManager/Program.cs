using Grpc.Net.Client;
using VolunteerManager;


Console.WriteLine("press any key to continue...");
Console.ReadLine();
// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:4566");
var client = new CreateEventService.CreateEventServiceClient(channel);
var reply = client.createEvent(new CreateEventInfo
{
    EventName = "Halloween Party",
    EventDate = "28.10.2022",
    StartTime = 22,
    EndTime = 2,
    Location = "VIA University College"
});
Console.WriteLine("Event Id: " + reply.EventId);
Console.WriteLine("press any key to exit");
Console.ReadLine();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

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