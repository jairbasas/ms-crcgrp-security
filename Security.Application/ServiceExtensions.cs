using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Security.Application.Behaviors;
using System.Reflection;

namespace Security.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
