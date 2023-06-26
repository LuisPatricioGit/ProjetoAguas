namespace CompanhiaAguas.Data.Entities
{
    public class Tier : IEntity
    {
        public int Id { get; set; }
        public int TierValue { get; set; }
        public double UnitValue { get; set; }
        public double MinRange { get; set; }
        public double MaxRange { get; set; }

        public ContractType ContractType { get; set; }
    }
}