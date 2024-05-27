using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    public class DocModel
    {
        public int Id { get; set; }

        public int? veh_id { get; set; }

        public int? sf_id { get; set; }

        [Required]
        public string type { get; set; } = string.Empty;

        [Required]
        public string file_name { get; set; } = string.Empty;

        [Required]
        public string file_path { get; set; } = string.Empty;

        [Required]
        public string short_path { get; set; } = string.Empty;

        [Required]
        public string server_name { get; set; } = string.Empty;

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public CarModel? car { get; set; }

        public StateFormModel? state_form { get; set; }

        public NoteModel? note { get; set; }
    }
}
