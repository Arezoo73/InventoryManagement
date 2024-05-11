using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Model.Entities
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

    }
}
