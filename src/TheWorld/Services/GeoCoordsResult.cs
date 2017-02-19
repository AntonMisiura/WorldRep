namespace TheWorld.Services
{
    public class GeoCoordsResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
    }
}