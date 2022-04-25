using Microsoft.Extensions.Configuration;

namespace RealtyPortal.ExternalApi.Model.DAL
{
    public abstract class DB
    {
        public IConfiguration _config;
        public abstract string connectionString { get; set; }
        public abstract int connectionTimeOut { get; set; }
    }
}
