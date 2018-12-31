namespace AppMetricsCoreDemo
{
    public class AppMetricsOptions
    {
        public bool Enable { get; set; } = true;
        public string DataBase { get; set; }
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string App { get; set; }
        public string Env { get; set; }
    }
}
