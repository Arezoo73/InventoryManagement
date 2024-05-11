using InventoryManagement.Data.DatabaseContexts;
using InventoryManagement.Data.IRepository;
using InventoryManagement.Model.Entities;

namespace InventoryManagement.Data.Repository
{
    public class InventoryItemRepository : IInventoryItemRepository
    {
        private readonly DataContext _dbContext;

        public InventoryItemRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<InventoryItem> GetByIdAsync(int itemId) => await _dbContext.Items.FindAsync(itemId);

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }

}

