using ProductionPlan.Api.Model;

namespace ProductionPlan.Api.Services
{
    public interface IProductionPlanService
    {
        public IReadOnlyCollection<ProductionPlanItem> GetProductionPlan(ProductionPlanRequest request);
    }

    public class ProductionPlanService : IProductionPlanService
    {
        public IReadOnlyCollection<ProductionPlanItem> GetProductionPlan(ProductionPlanRequest request)
        {
            var fuels = request.Fuels;
            var remainingLoad = (double)request.Load;
            var powerPlants = request.PowerPlants;
            var wind = fuels.Wind / 100f;
            var prices = new Dictionary<PlantType, double>()
            {
                { PlantType.GasFired, fuels.Gas + fuels.Co2 * 0.3 },
                { PlantType.TurboJet, fuels.Kerosine },
                { PlantType.WindTurbine, 0 }
            };

            if (remainingLoad < powerPlants.Min(GetPMin))
                throw new ArgumentException("Minimal possible power output is greater than requested load");
            
            if (remainingLoad > powerPlants.Sum(GetPMax))
                throw new ArgumentException("Maximum possible power output is less than requested load");

            var meritOrdered = request.PowerPlants.OrderBy(x => prices[x.Type] / x.Efficiency).ThenByDescending(GetPMax);

            var result = new List<ProductionPlanItem>();

            foreach (var plant in meritOrdered)
            {
                var item = GetProductionPlanItem(plant);
                result.Add(item);
                remainingLoad -= item.P;
            }

            return result;

            double GetPMax(PowerPlant plant) => plant.Type == PlantType.WindTurbine ? plant.PMax * wind : plant.PMax;

            double GetPMin(PowerPlant plant) => plant.Type == PlantType.WindTurbine ? plant.PMax * wind : plant.PMin;

            ProductionPlanItem GetProductionPlanItem(PowerPlant plant)
            {
                if (remainingLoad <= GetPMax(plant) && remainingLoad >= GetPMin(plant))
                    return new ProductionPlanItem(plant.Name, remainingLoad);

                if (remainingLoad < GetPMin(plant))
                    return new ProductionPlanItem(plant.Name, 0);

                return new ProductionPlanItem(plant.Name, Math.Round(GetPMax(plant), 1));
            }
        }
    }
}
