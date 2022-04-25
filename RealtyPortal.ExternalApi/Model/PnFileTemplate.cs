using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyPortal.ExternalApi.Model
{
    public partial class PnFileTemplate
    {
        public int FileTemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }
        public string TemplateNameKK { get; set; }
        public string TemplateDescriptionKK { get; set; }
        public string Text { get; set; }
        public string TextKK { get; set; }
        public DateTime CreateDate { get; set; }
        public int FileTypeBpmId { get; set; }
    }
}
