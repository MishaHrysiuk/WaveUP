using WaveUP.Domain.Entities;
using System.Data.Entity;

namespace WaveUP.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Instrument> Instruments { get; set; }
    }
}