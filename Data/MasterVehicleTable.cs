namespace MecuryProduct.Data
{
    /* The `MasterVehicleTable` class in C# represents a vehicle with properties for Id, make, and model. */
    public class MasterVehicleTable
    {
        public int Id { get; set; }

        public string make { get; set; } = string.Empty;

        public string model { get; set; } = string.Empty;
    }
}
