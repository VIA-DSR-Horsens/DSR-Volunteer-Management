using auth_API;
using auth_API.DAO;
using auth_API.DAO.Implementations;
using auth_API.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<IAuthLogic, AuthLogic>();
builder.Services.AddSingleton<AuthSingleton>();
builder.Services.AddGrpc();
builder.Services.AddDbContext<AuthenticationContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// grpc stuff
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapGrpcService<AuthLogic>()
		.RequireHost("*:4567");
});

app.MapControllers();

app.Run();