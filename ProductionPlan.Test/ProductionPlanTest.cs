using Newtonsoft.Json;
using ProductionPlan.Api.Model;
using ProductionPlan.Api.Services;

namespace ProductionPlan.Test
{
    public class ProductionPlanTest
    {
        private readonly IProductionPlanService productionPlanService;

        public ProductionPlanTest(IProductionPlanService productionPlanService) => this.productionPlanService = productionPlanService;

        [Fact]
        public void Trivial()
        {
            var fuels = new Fuels(1d, 1d, 1d, 100);
            var powerPlants = new List<PowerPlant>
            {
                new("gas", PlantType.gasfired, 1, 0, 100),
                new("jet", PlantType.TurboJet, 1, 0, 100),
                new("wind", PlantType.WindTurbine, 1, 0, 100),
            };
            var request = new ProductionPlanRequest(100, fuels, powerPlants);

            var productionPlan = productionPlanService.GetProductionPlan(request);

            Assert.NotNull(productionPlan);
            Assert.NotEmpty(productionPlan);
            Assert.Equal("wind", productionPlan.First().Name);
            Assert.Equal(100, productionPlan.First().P);
            Assert.All(productionPlan.Skip(1), item => Assert.Equal(0, item.P));
        }

        [Fact]
        public void WindGas()
        {
            var fuels = new Fuels(1d, 1d, 1d, 50);
            var powerPlants = new List<PowerPlant>
            {
                new("gas", PlantType.gasfired, 1, 0, 100),
                new("jet", PlantType.TurboJet, 1, 20, 100),
                new("wind", PlantType.WindTurbine, 1, 30, 100),
            };
            var request = new ProductionPlanRequest(100, fuels, powerPlants);

            var productionPlan = productionPlanService.GetProductionPlan(request).ToList();

            Assert.NotNull(productionPlan);
            Assert.NotEmpty(productionPlan);
            var gas = productionPlan.Single(x => x.Name == "gas");
            var jet = productionPlan.Single(x => x.Name == "jet");
            var wind = productionPlan.Single(x => x.Name == "wind");
            Assert.Equal(0, productionPlan.IndexOf(wind));
            Assert.Equal(50, wind.P);
            Assert.Equal(1, productionPlan.IndexOf(jet));
            Assert.Equal(50, jet.P);
            Assert.Equal(2, productionPlan.IndexOf(gas));
            Assert.Equal(0, gas.P);
        }

        [Fact]
        public void SkipStrongPlants()
        {
            var fuels = new Fuels(1d, 1d, 1d, 50);
            var powerPlants = new List<PowerPlant>
            {
                new("gas", PlantType.gasfired, 1, 50, 100),
                new("jet", PlantType.TurboJet, 1, 70, 100),
                new("gas2", PlantType.gasfired, 0.5, 40, 100),
            };
            var request = new ProductionPlanRequest(40, fuels, powerPlants);

            var productionPlan = productionPlanService.GetProductionPlan(request).ToList();

            Assert.NotNull(productionPlan);
            Assert.NotEmpty(productionPlan);
            var gas2 = productionPlan.Single(x => x.Name == "gas2");
            Assert.Equal(2, productionPlan.IndexOf(gas2));
            Assert.Equal(40, gas2.P);
        }

        [Fact]
        public void Payload3()
        {
            var payloadFile = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "example_payloads/payload3.json"));
            Assert.NotNull(payloadFile);
            var request = JsonConvert.DeserializeObject<ProductionPlanRequest>(payloadFile);
            Assert.NotNull(request);
            var responseFile = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "example_payloads/response3.json"));
            Assert.NotNull(responseFile);
            var expectedResponse = JsonConvert.DeserializeObject<IReadOnlyCollection<ProductionPlanItem>>(responseFile);
            Assert.NotNull(expectedResponse);

            var productionPlan = productionPlanService.GetProductionPlan(request).ToList();

            Assert.NotNull(productionPlan);
            Assert.NotEmpty(productionPlan);
            Assert.Equivalent(expectedResponse, productionPlan);
        }
    }
}