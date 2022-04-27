using System.ServiceModel.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml;

using RealtyPortal.ExternalApi.Model;

namespace RealtyPortal.ExternalApi.Services
{
    public class X509
    {
        public static X509ServiceCertificateAuthentication Ignore()
        {
            return new X509ServiceCertificateAuthentication()
            {
                CertificateValidationMode = X509CertificateValidationMode.None,
                RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck
            };
        }
    }

    public class BindingResult
    {
        public static BasicHttpBinding GetBinding(string url)
        {
            BasicHttpBinding result = new BasicHttpBinding();
            result.MaxBufferSize = int.MaxValue;
            result.ReaderQuotas = XmlDictionaryReaderQuotas.Max;
            result.MaxReceivedMessageSize = int.MaxValue;
            result.AllowCookies = true;

            if (url.Contains("https://"))
            {
                result.Security.Mode = BasicHttpSecurityMode.Transport;
            }
            else
            {
                result.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            }

            return result;
        }
    }



}
