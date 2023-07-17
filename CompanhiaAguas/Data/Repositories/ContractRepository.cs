using CompanhiaAguas.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CompanhiaAguas.Data.Repositories
{
    public class ContractRepository : GenericRepository<Contract>, IContractRepository
    {
        private readonly DataContext _context;

        public ContractRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithClients()
        {
            return _context.Contracts.Include(c => c.Client);
        }

        /*
        public IJEnumerable<SelectListItem> GetContractTypes()
        {
            var list = _context.Contracts.Select(c => new SelectListItem
            {
                Text = c.ContractType,
                Value = c.Id.ToString(),
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a contract...)",
                Value = "0"
            });

            return list;
        }
        */
    }
}
