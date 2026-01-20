using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Application;
using TriviaGame.Application.Auth;
using TriviaGame.Application.Categoria;
using TriviaGame.Application.Trivia;
using TriviaGame.Infrastructure.Persistence.DbContext;
using TriviaGame.Infrastructure.Persistence.Repositories;


namespace TriviaGame.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            // Connection 
            services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITriviaRepository, TriviaRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
