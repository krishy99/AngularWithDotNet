using System.Security.Cryptography.X509Certificates;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class ApplicationDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users{get;set;}
}
