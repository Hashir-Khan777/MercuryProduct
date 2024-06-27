using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    /// Returns a copy of this model with all fields set to null. This is used to make sure we don't accidentally change fields that are in the audit log
    public class AuditLogModel
    {
        public int Id { get; set; }

        public string entity_name { get; set; } = string.Empty;

        public string action_type { get; set; } = string.Empty;

        public string? user_role { get; set; } = string.Empty;

        public string? user_id { get; set; } = string.Empty;

        public ApplicationUser? user { get; set; }

        [Required]
        public DateTime created_at { get; set; }
    }
}
