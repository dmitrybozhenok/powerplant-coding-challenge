using Newtonsoft.Json;

namespace ProductionPlan.Api.Model
{
    public record Fuels(double Gas, double Kerosine, double Co2, int Wind)
    {
        [JsonProperty("gas(euro/MWh)")]
        public double Gas { get; set; } = Gas;

        [JsonProperty("kerosine(euro/MWh)")]
        public double Kerosine { get; set; } = Kerosine;

        [JsonProperty("co2(euro/ton)")]
        public double Co2 { get; set; } = Co2;

        [JsonProperty("wind(%)")]
        public int Wind { get; set; } = Wind;
    }
}
