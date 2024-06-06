using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    /* The NoteModel class represents a note entity with various properties related to notes and associated
    entities. */
    public class NoteModel
    {
        public int Id { get; set; }

        public int? veh_id { get; set; }

        public int? sf_id { get; set; }

        public int? doc_id { get; set; }

        [Required]
        public string note { get; set; } = string.Empty;

        public string? created_by_id { get; set; } = string.Empty;

        public string type { get; set; } = string.Empty;

        public string corrected { get; set; } = string.Empty;

        public DateTime corrected_time { get; set; }

        public string? corrected_by_id { get; set; } = string.Empty;

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public CarModel? vehicle { get; set; }

        public ApplicationUser? created_by { get; set; }

        public ApplicationUser? corrected_by { get; set; }

        public StateFormModel? state_form { get; set; }

        public DocModel? doc { get; set; }
    }
}
