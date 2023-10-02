namespace ProductionPlan.Api.Model;

public record PowerPlant(string Name, PlantType Type, double Efficiency, int PMin, int PMax)
{
    public string Name { get; } = Name;
    public PlantType Type { get; } = Type;
    public double Efficiency { get; } = Efficiency;
    public int PMin { get; } = PMin;
    public int PMax { get; } = PMax;
}

public enum PlantType
{
    GasFired = 0,
    TurboJet = 1,
    WindTurbine = 2
}