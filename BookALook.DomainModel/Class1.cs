using BookALook.Classes;
using System.Data.Entity;

namespace BookALook.DomainModel
{
    public class BookALookContext: DbContext
    {
        public DbSet<Bodice> Bodices { get; set; }
        public DbSet<Skirt> Skirts { get; set; }
        public DbSet<Overlay> Overlays { get; set; }
    }
}
