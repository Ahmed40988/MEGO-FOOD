using NetTopologySuite.Geometries;


namespace Web.Domain.Addresses
{
    public abstract class BaseAddress
    {
        public Guid Id { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string? Address { get; private set; }
        public Point Location { get; private set; } = default!;

        protected BaseAddress() { }

        protected BaseAddress(
            double latitude,
            double longitude,
            string? address)
        {
            Latitude = latitude;
            Longitude = longitude;

            Location = new Point(longitude, latitude)
            {
                SRID = 4326
            };
            Address = address;

        }

        public void UpdateCoordinates(double latitude, double longitude, string? address)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;

            Location = new Point(longitude, latitude)
            {
                SRID = 4326
            };
        }
    }
}
