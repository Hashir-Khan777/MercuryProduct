using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    public class StateFormModel
    {
        public int Id { get; set; }

        public DocModel? doc { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public List<NoteModel>? notes {  get; set; }
    }
}
