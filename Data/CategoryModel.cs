using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public bool deleted { get; set; } = false;

        public string Name { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public List<ProductModel> Proucts { get; set; }
    }
}
