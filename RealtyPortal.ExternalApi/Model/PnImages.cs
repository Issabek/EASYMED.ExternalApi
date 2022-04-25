using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyPortal.ExternalApi.Model
{
    public partial class PnImages
    {

        public Guid id { get; set; }

        public string name { get; set; }

        public string ext { get; set; }

        public long image_size { get; set; }

        public DateTime upload_date { get; set; }

        public long upload_user_id { get; set; }

        public Guid link_id { get; set; }

        public byte[] image_data { get; set; }

        public int type { get; set; }

        public byte[] image_data_small { get; set; }

        public byte[] image_data_very_small { get; set; }

        public Guid? timeline_id { get; set; }
    }
}
