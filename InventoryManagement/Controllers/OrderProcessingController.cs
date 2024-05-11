using InventoryManagement.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Core.IServices;

namespace InventoryManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProcessingController : ControllerBase
    {
        private readonly IProcessItemQuantityService service;

        public OrderProcessingController(IProcessItemQuantityService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> UpdateStockAsync(int itemId, int quantity)
        {
            await service.ProcessItemQuantityAsync(itemId, quantity);
            return Ok();
        }
    }
    
}
