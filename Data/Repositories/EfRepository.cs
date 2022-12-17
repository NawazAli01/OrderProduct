
using Zero.EFCoreSpecification;
using Zero.SeedWorks;

namespace ProductApis.Data.Repositories //.Repositories
{
    public class EfRepository<TEntity> : RepositoryBase<TEntity, ApplicationDbContext> where TEntity : Entity, IAggregateRoot
    {
        public EfRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}


