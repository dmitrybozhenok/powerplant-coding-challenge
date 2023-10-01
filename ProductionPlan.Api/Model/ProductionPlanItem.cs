namespace ProductionPlan.Api.Model
{
    public record ProductionPlanItem(string Name, double P)
    {
        public string Name { get; } = Name;
        public double P { get; } = P;
    }
}
