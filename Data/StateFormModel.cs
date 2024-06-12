using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    /* The StateFormModel class represents a form state with properties for Id, doc, created_at,
    updated_at, and notes. */
    public class StateFormModel
    {
        public int Id { get; set; }

        public DocModel? doc { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public int? CompanyId { get; set; }

        public CompanyModel? Company { get; set; }

        public List<NoteModel>? notes { get; set; }
    }
}
