using CompanhiaAguas.Data.Entities;
using System.Linq;

namespace CompanhiaAguas.Data.Repositories
{
    public interface IConsumptionTableRepository : IGenericRepository<ConsumptionTable>
    {
       public IQueryable GetAllWithTiers();
    }
}
