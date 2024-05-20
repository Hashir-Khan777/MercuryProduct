using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    public class ImageModel
    {
        public int Id { get; set; }

        public int? veh_id { get; set; }

        [Required]
        public string type { get; set; } = string.Empty;

        [Required]
        public string file_name { get; set; } = string.Empty;

        [Required]
        public string file_path { get; set; } = string.Empty;

        [Required]
        public string server_name { get; set; } = string.Empty;

        public CarModel? car { get; set; }
    }
}
