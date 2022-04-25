using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyPortal.ExternalApi.Model
{
    public partial class PnClient
    {
        public int Id { get; set; }
        public string clientCode { get; set; }
        public string clientId { get; set; }
        public string clientIIN { get; set; }
        public long userId { get; set; }
        public string clientFIO { get; set; }
    }
}
