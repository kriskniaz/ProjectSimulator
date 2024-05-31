using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Persistence;
using NET.Kniaz.ProperArchitecture.Application.CommandHandlers;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Commands;


namespace NET.Kniaz.ProperArchitecture.Simulator;

class Program
{
    static async Task Main(string[] args)
    {
        // Setup dependency injection
        var serviceProvider = ConfigureServices();

        // Resolve the application entry point and run it
        var app = serviceProvider.GetService<IConsoleApplication>();
        Console.WriteLine("Starting the simulation");
        await app.Run();
        Console.WriteLine("Ending the simulation");
    }

    static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Add services to the container
        services.AddDbContext<DataBaseContext>(options =>
            options.UseSqlServer("Server=.;DataBase=Develop;User Id=sa; Password=galapagos3; Encrypt= True; TrustServerCertificate=True"));
        services.AddScoped<ICommandHandler<ProjectCommand>, ProjectCommandHandler>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICommandHandler<CurrencyCommand>, CurrencyCommandHandler>();
        services.AddScoped<ICommandHandler<ResourceCommand>, ResourceCommandHandler>();
        services.AddScoped<ICommandHandler<TeamCommand>, TeamCommandHandler>();
        services.AddScoped<ICommandHandler<TeamMemberCommand>, TeamMemberCommandHandler>();
        services.AddScoped<ICommandHandler<CommitCommand>, CommitCommandHandler>();
        services.AddScoped<ICommandHandler<StoryCommand>, StoryCommandHandler>();
        services.AddScoped<ICommandHandler<SprintCommand>, SprintCommandHandler>();
        services.AddScoped<ICommandHandler<SimulationResultCommand>, SimulationResultCommandHandler>();


        // Add your application entry point
        services.AddScoped<IConsoleApplication, Simulation>();

        return services.BuildServiceProvider();
    }
}

