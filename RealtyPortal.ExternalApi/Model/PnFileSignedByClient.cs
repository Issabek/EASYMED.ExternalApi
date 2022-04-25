using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyPortal.ExternalApi.Model
{
    public class PnFileSignedByClient
    {
        public int FileSignedByClientId { get; set; }
        public byte[] PDFFile { get; set; }
    }
}
