using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyPortal.ExternalApi.Model
{
    public partial class PnStatus
    {
        public Guid id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public int type { get; set; }
    }
}
