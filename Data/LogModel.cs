namespace MecuryProduct.Data
{
    /// Creates a new instance of that is used to represent a log model. This is a convenience method
    public class LogModel
    {
        public string exception { get; set; } = string.Empty;

        public string page { get; set; } = string.Empty;

        public string user { get; set; } = string.Empty;

        public string cookies { get; set; } = string.Empty;

        public string sessions { get; set; } = string.Empty;
    }
}
