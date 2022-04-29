using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EASYMED.ExternalApi.Enums;

namespace EASYMED.ExternalApi.Model
{
    public class qrBase
    {
        public string description { get; set; }
        public DateTime expiry_date { get; set; }
        public Organisation organisation { get; set; }
        public Document document { get; set; }
    }
    public class Organisation
    {
        public string nameRu { get; set; }
        public string nameKz { get; set; }
        public string nameEn { get; set; }
    }
    public class Document
    {
        public string uri { get; set; }
        public string auth_type { get; set; }
        public string auth_token { get; set; }
    }
    public class docsNSign
    {
        public List<DocumentsToSign> documentsToSign { get; set; }
        public string signMethod { get; set; }
        public string applicationId { get; set; }
    }
    public class DocumentsToSign
    {
        public int id { get; set; }
        public string nameRu { get; set; }
        public string nameKz { get; set; }
        public string nameEn { get; set; }
        public document document { get; set; }
    }
    public class document
    {
        public file file { get; set; }
    }
    public class file
    {
        public string mime { get; set; }
        public string data { get; set; }
    }

}
