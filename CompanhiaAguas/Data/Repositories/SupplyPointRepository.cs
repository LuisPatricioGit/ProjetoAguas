using CompanhiaAguas.Data.Entities;

namespace CompanhiaAguas.Data.Repositories
{
    public class SupplyPointRepository : GenericRepository<SupplyPoint>, ISupplyPointRepository
    {
        private readonly DataContext _context;
        public SupplyPointRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
