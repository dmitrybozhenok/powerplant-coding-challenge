using Microsoft.AspNetCore.Mvc;
using ProductionPlan.Api.Model;
using ProductionPlan.Api.Services;

namespace ProductionPlan.Api.Controllers
{
    [ApiController]
    public class ProductionPlanController : ControllerBase
    {
        private readonly ILogger<ProductionPlanController> logger;
        private readonly IProductionPlanService productionPlanService;


        public ProductionPlanController(ILogger<ProductionPlanController> logger, IProductionPlanService productionPlanService)
        {
            this.logger = logger;
            this.productionPlanService = productionPlanService;
        }

        [HttpPost("productionplan")]
        public IReadOnlyCollection<ProductionPlanItem> GetProductionPlan([FromBody] ProductionPlanRequest request)
        { 
            logger.LogInformation("Production plan request received: " + request);
            return productionPlanService.GetProductionPlan(request);
        }
    }
}