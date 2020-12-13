using System.Threading.Tasks;


namespace WebApplication8.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task SaveChanges()
        {
            await db.SaveChangesAsync();
        }
    }
}
