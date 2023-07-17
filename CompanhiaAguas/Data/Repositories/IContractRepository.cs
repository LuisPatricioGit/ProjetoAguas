using CompanhiaAguas.Data.Entities;
using System.Linq;

namespace CompanhiaAguas.Data.Repositories
{
    public interface IContractRepository : IGenericRepository<Contract>
    {
        public IQueryable GetAllWithClients();

        //IJEnumerable<SelectListItem> GetComboContractTypes();
    }
}
