using Microsoft.Extensions.DependencyInjection;
using ProductionPlan.Api.Services;

namespace ProductionPlan.Test;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IProductionPlanService, ProductionPlanService>();
    }
}