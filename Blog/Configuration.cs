namespace Blog
{
    public static class Configuration
    {
        // Token
        public static string JwtKey { get; set; } = "f17eff9df7ff4f249f958a75300b13af=Mz";
        public static string ApiKeyName = "api_key";
        public static string ApiKey = "curso_api_55eafc16-c8dc-4c0c-a831-44da3c1a1f27";
        public static SmptConfiguration Smtp = new();

        public class SmptConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
