using System.Text.Json;
using DatabaseEFC;
using DatabaseEFC.DAO;
using DatabaseEFC.DAO.Implementations;
using DatabaseEFC.Utils;

public class Program {
    /// <summary>
    /// Prints an error to console and the inner error (if it exists) of the exception
    /// </summary>
    /// <param name="e">The exception whose error to print</param>
    public static void PrintError(Exception e)
    {
        Console.WriteLine("=====> An error occured! <=====");
        Console.WriteLine("Error msg: "+e.Message);
        Console.WriteLine("Error source: "+e.Source);
        if (e.InnerException != null)
        {
            Console.WriteLine("Error inner msg: "+ e.InnerException.Message);
            Console.WriteLine("Error inner source: "+ e.InnerException.Source);
        }
        Console.WriteLine();
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // registering stuff
        builder.Services.AddScoped<IUserDao, UserEfcDao>();
        builder.Services.AddScoped<IEventDao, EventEfcDao>();
        builder.Services.AddScoped<IShiftDao, ShiftEfcDao>();
        builder.Services.AddDbContext<ManagementContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    /// <summary>
    /// Gets necessary info from the provided file path to connect to the database
    /// </summary>
    /// <param name="path">Path to JSON file</param>
    /// <returns>Username, Password and Host of the PostgreSQL database</returns>
    public static DbCredentials GetDbCredentials(string path)
    {
        // creating template file at provided path
        if (!File.Exists(path))
        {
            // default credentials
            var creds = new DbCredentials
            {
                Username = "postgres", Password = "postgres", Host = "localhost" , Database = "postgres", Schema = "public"
            };
            
            string serialized = JsonSerializer.Serialize(creds);
            var newFile = File.CreateText(path);
            newFile.Write(serialized);
            newFile.Close();
        }

        StreamReader sr = new StreamReader(path);
        var data = JsonSerializer.Deserialize<DbCredentials>(sr.ReadToEnd());
        return data;
    }
}