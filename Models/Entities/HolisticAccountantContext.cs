using Microsoft.EntityFrameworkCore;

namespace HolisticAccountant.Models.Entities
{
    public class HolisticAccountantContext : DbContext
    {
        public HolisticAccountantContext(DbContextOptions<HolisticAccountantContext> opt) : base(opt)
        {
            
        }

        //public DbSet<Command> Commands { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}