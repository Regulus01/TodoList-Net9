using System.Reflection;
using Application.Interface;
using Application.Mapper;
using Application.Services;
using Domain.Interface;
using Infra.CrossCutting.Notification.Bus;
using Infra.CrossCutting.Notification.Interface;
using Infra.Data.Context;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Infra.CrossCutting.IoC;

public static class DependencyInjection
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepository(services);
        AddDbContext(services, configuration);
        AddMapper(services);
        AddAppServices(services);
        AddSwagger(services);
        
        services.AddScoped<INotify, Notify>();
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var xmlFileService = $"{Assembly.Load("TodoListNet9.Api").GetName().Name}.xml";
            var xmlPathService = Path.Combine(AppContext.BaseDirectory, xmlFileService);
            options.IncludeXmlComments(xmlPathService);
            
            var xmlFileApplication = $"{Assembly.Load("TodoListNet9.Application").GetName().Name}.xml";
            var xmlPathApplication = Path.Combine(AppContext.BaseDirectory, xmlFileApplication);
            options.IncludeXmlComments(xmlPathApplication);
            
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ToDo API",
                Description = "An ASP.NET Core Web API for managing ToDo Items using .net 9",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Email",
                    Email = "example@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });
        });
    }

    private static void AddAppServices(IServiceCollection services)
    {
        services.AddScoped<ITaskListAppService, TaskListAppService>();
        services.AddScoped<ITaskItemAppService, TaskItemAppService>();
    }

    private static void AddMapper(IServiceCollection services)
    {
        var mapper = MappingConfiguration.RegisterMappings().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    private static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<ITaskListRepository, TaskListRepository>();
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}