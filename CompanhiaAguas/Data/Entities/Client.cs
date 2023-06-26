using System.Collections.Generic;

namespace CompanhiaAguas.Data.Entities
{
    public class Client : User
    {
        public int ClientId { get; set; }

        public List<Contract> Contracts { get; set; }
    }
}
