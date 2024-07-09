namespace ObdLogApi.Models
{
    public partial class ObdData
    {
        public string VinSerial { get; set; }
        public int? SpeedKmH { get; set; }
        public int? Rpm { get; set; }
        public double? ThrottlePercentage { get; set; }
        public int? CoolantTempCels { get; set; }
        public double? EngLoadPercentage { get; set; }
        public double? AbsLoadPercentage { get; set; }
        public double? Latitude { get; set; }
        public double? Longitute { get; set; }
    }
}
