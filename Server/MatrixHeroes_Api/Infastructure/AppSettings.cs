namespace MatrixHeroes_Api.Infastructure
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public CorsData Cors { get; set; }
        public struct CorsData
        {
            public string PolicyName { get; set; }
            public string[] AllowedHosts { get; set; }
        }
    }
}