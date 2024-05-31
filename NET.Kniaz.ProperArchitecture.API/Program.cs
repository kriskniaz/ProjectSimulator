using Microsoft.EntityFrameworkCore;
using NET.Kniaz.ProperArchitecture.Domain.Abstractions;
using NET.Kniaz.ProperArchitecture.Domain.Entities;
using NET.Kniaz.ProperArchitecture.Application.CommandHandlers;
using NET.Kniaz.ProperArchitecture.Persistence;
using NET.Kniaz.ProperArchitecture.Application.Abstractions;
using NET.Kniaz.ProperArchitecture.Application.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevDB")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICommandHandler<CurrencyCommand>, CurrencyCommandHandler>();
builder.Services.AddScoped<ICommandHandler<ResourceCommand>, ResourceCommandHandler>();
builder.Services.AddScoped<ICommandHandler<ProjectCommand>, ProjectCommandHandler>();
builder.Services.AddScoped<ICommandHandler<TeamCommand>, TeamCommandHandler>();
builder.Services.AddScoped<ICommandHandler<TeamMemberCommand>, TeamMemberCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CommitCommand>, CommitCommandHandler>();
builder.Services.AddScoped<ICommandHandler<StoryCommand>, StoryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<SprintCommand>, SprintCommandHandler>();


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
