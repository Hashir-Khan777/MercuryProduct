using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    /* The CarModel class represents a car entity with various properties related to car details and
    transaction information. */
    public class CarModel
    {
        public int Id { get; set; }

        public int? cid { get; set; }

        public bool deleted {  get; set; } = false;

        [Required]
        public int? car_year { get; set; }

        [Required]
        public string car_make { get; set; } = string.Empty;

        [Required]
        public string car_model { get; set; } = string.Empty;

        public string vin_no { get; set; } = string.Empty;

        public string car_run { get; set; } = string.Empty;

        public string car_color { get; set; } = string.Empty;

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public string submitted_by { get; set; } = string.Empty;

        [Required]
        public int? offered_ammount { get; set; }

        [Required]
        public string status { get; set; } = string.Empty;

        public string tires_wheel_front { get; set; } = string.Empty;

        public string tires_wheel_rear { get; set; } = string.Empty;

        public string car_not_running_notes { get; set; } = string.Empty;

        public DateTime pickup_date { get; set; }

        public DateTime scheduled_date { get; set; }

        public DateTime? set_date { get; set; } = null;

        public DateTime? pulled_date { get; set; } = null;

        public string? driver_id { get; set; } = string.Empty;

        public string? created_by_id { get; set; } = string.Empty;

        public int? purchase_amount { get; set; }

        public string dnd_notes { get; set; } = string.Empty;

        public string lead_type { get; set; } = string.Empty;

        public string prod_status { get; set; } = "Hold";

        public string section { get; set; } = string.Empty;

        public string row { get; set; } = string.Empty;

        public string pull_type { get; set; } = string.Empty;

        public string pull_type_des { get; set; } = string.Empty;

        public string motor_condition { get; set; } = string.Empty;

        public string vehicle_type { get; set; } = string.Empty;

        public bool title_status { get; set; }

        public bool special_instructions { get; set; }

        public int? checkNo { get; set; }

        public string DL { get; set; } = string.Empty;

        public int? CompanyId { get; set; }

        public CompanyModel? Company { get; set; }

        public CustomerModel? customer { get; set; }

        public ApplicationUser? driver { get; set; }

        public ApplicationUser? created_by { get; set; }

        public List<NoteModel>? notes { get; set; }

        public List<DocModel>? docs { get; set; }

        public string customer_name
        {
            get
            {
                return customer?.cfname + " " + customer?.clname;
            }
        }
    }
}
