using CompanhiaAguas.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CompanhiaAguas.Data.Repositories
{
    public class ConsumptionTableRepository : GenericRepository<ConsumptionTable>, IConsumptionTableRepository
    {
        public readonly DataContext _context;

        public ConsumptionTableRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithTiers()
        {
            return _context.ConsumptionTables.Include(ct => ct.Tiers);
        }
    }
}
