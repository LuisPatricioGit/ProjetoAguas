using System.Linq;
using CompanhiaAguas.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanhiaAguas.Data.Repositories
{
    public class ContractTypeRepository : GenericRepository<ContractType>, IContractTypeRepository
    {
        private readonly DataContext _context;
        public ContractTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
