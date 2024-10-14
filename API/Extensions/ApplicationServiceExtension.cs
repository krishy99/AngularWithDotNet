using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AppApplicationServices(this IServiceCollection Services,IConfiguration Configuration)
    {
        Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        Services.AddEndpointsApiExplorer();
        Services.AddSwaggerGen();
        Services.AddDbContext<ApplicationDBContext>(
            options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
        return Services;
    }

}
