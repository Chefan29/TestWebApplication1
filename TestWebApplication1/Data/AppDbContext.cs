using Microsoft.EntityFrameworkCore;    
namespace TestWebApplication1.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<NoteEntity> Notes => Set<NoteEntity>();
    }
}
