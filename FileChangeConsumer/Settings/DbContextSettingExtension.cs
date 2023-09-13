using FileChangeConsumer.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace FileChangeConsumer.Settings
{
    public static class DbContextSettingExtension
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = $"server={configuration["DBServer"]};user={configuration["DBUser"]};password={configuration["DBPassword"]};database={configuration["DataBase"]}";

            services.AddDbContext<FileChangeContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23))));

            return services;
        }
    }
}
