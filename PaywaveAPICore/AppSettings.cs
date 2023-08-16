namespace PaywaveAPICore
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public string MongoDB_ConnectionString { get; set; } = "mongodb+srv://PaywaveDb:6On5Tv29D1Zzm2XA@paywavecluster.qod37wr.mongodb.net/?retryWrites=true&w=majority";

    }
}