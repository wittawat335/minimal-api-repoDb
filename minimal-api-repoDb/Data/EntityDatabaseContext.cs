using Microsoft.EntityFrameworkCore;

namespace minimal_api_repoDb.Data;

public class EntityDatabaseContext : DbContext
{
    public EntityDatabaseContext(DbContextOptions<EntityDatabaseContext> options) : base(options)
    {
    }
   
}
