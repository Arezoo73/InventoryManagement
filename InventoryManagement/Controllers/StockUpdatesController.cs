using InventoryManagement.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockUpdatesController : ControllerBase
    {

        private readonly IInventoryItemService service;

        public StockUpdatesController(IInventoryItemService service)
        {
            this.service = service;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStockAsync(int itemId, int quantity) 
        {
           await service.UpdateStockAsync(itemId, quantity);
            return Ok();
        }
    }
}
