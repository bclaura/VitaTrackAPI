using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using VitaTrackAPI.Models;

namespace VitaTrackAPI
{
    public class VitaTrackContextFactory : IDesignTimeDbContextFactory<VitaTrackContext>
    {
        public VitaTrackContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // root project path
                .AddJsonFile("appsettings.json") // read appsettings.json
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<VitaTrackContext>();
            var connectionString = configuration.GetConnectionString("VitaTrack");

            optionsBuilder.UseNpgsql(connectionString);

            return new VitaTrackContext(optionsBuilder.Options);
        }
    }
}
