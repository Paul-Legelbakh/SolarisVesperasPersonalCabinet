namespace PersonalCabinet.DataBase
{
    public class Settings : ISettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}