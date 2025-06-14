using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackendTest.Domain
{
    public class Product
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Price { get; set; }

        public int Stock { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
