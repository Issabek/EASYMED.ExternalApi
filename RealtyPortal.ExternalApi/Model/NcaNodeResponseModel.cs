using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyPortal.ExternalApi.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Subject
    {
        public string commonName { get; set; }
        public string lastName { get; set; }
        public string country { get; set; }
        public string gender { get; set; }
        public string surname { get; set; }
        public string dn { get; set; }
        public string birthDate { get; set; }
        public string email { get; set; }
        public string iin { get; set; }
        public string organization { get; set; }
    }

    public class Issuer
    {
        public string commonName { get; set; }
        public string country { get; set; }
        public string dn { get; set; }
        public string organization { get; set; }
    }

    public class Chain
    {
        public bool valid { get; set; }
        public string notAfter { get; set; }
        public string keyUsage { get; set; }
        public string serialNumber { get; set; }
        public Subject subject { get; set; }
        public string signAlg { get; set; }
        public string sign { get; set; }
        public string publicKey { get; set; }
        public Issuer issuer { get; set; }
        public string notBefore { get; set; }
        public List<string> keyUser { get; set; }
    }

    public class Result
    {
        public bool valid { get; set; }
        public string notAfter { get; set; }
        public List<Chain> chain { get; set; }
        public string keyUsage { get; set; }
        public string serialNumber { get; set; }
        public Subject subject { get; set; }
        public string signAlg { get; set; }
        public string sign { get; set; }
        public string publicKey { get; set; }
        public Issuer issuer { get; set; }
        public string notBefore { get; set; }
        public List<string> keyUser { get; set; }
    }

    public class Root
    {
        public Result result { get; set; }
        public string message { get; set; }
        public int status { get; set; }
    }


}
