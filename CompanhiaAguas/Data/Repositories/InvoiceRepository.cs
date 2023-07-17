using CompanhiaAguas.Data.Entities;

namespace CompanhiaAguas.Data.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly DataContext _context;
        public InvoiceRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
