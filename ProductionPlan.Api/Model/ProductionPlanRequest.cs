namespace ProductionPlan.Api.Model;

public record ProductionPlanRequest(int Load, Fuels Fuels, List<PowerPlant> PowerPlants)
{
    public int Load { get; } = Load;
    public Fuels Fuels { get; } = Fuels;
    public List<PowerPlant> PowerPlants { get; } = PowerPlants;
}