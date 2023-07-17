using CompanhiaAguas.Data.Entities;

namespace CompanhiaAguas.Data.Repositories
{
    public class TierRepository : GenericRepository<Tier>, ITierRepository
    {
        private readonly DataContext _context;
        public TierRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
