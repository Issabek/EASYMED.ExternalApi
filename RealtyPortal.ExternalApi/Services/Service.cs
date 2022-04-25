using System.ServiceModel.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml;
using CBSProxy_TokenWS;
using CBSProxy_ClientWS;
using RealtyPortal.ExternalApi.Model;
using Client = CBSProxy_ClientWS.Client;

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

    public class TokenAuthWebService
    {
        private string _url { get; set; }

        public TokenAuthWebService(string url)
        {
            _url = url;
        }

        public async Task<string> GetToken(string systemId, string login, string password)
        {
            var client = new TokenAuthWSSoapClient(TokenAuthWSSoapClient.EndpointConfiguration.TokenAuthWSSoap, _url);

            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication = X509.Ignore();
            client.Endpoint.Binding = BindingResult.GetBinding(_url);
            var token = await client.UserLoginAsync(systemId, login, password);

            return token;
        }

        public async Task<bool> CheckToken(string token)
        {
            var client = new TokenAuthWSSoapClient(TokenAuthWSSoapClient.EndpointConfiguration.TokenAuthWSSoap, _url);

            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication = X509.Ignore();
            client.Endpoint.Binding = BindingResult.GetBinding(_url);
            var isValid = await client.CheckTokenAsync(token);

            return isValid;
        }

    }

    public class ClientWebService
    {
        private string _url { get; set; }
        private string _token { get; set; }

        public ClientWebService(string url, string token)
        {
            _url = url;
            _token = token;
        }

        public async Task<Client> GetClientByIin(string iin, bool needDocuments, bool needContracts, bool needDetailedAddress, bool needEGOV_Address, bool needRiskLevel, bool needArchiveData)
        {
            var client = new ClientWSSoapClient(ClientWSSoapClient.EndpointConfiguration.ClientWSSoap, _url);

            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication = X509.Ignore();
            client.Endpoint.Binding = BindingResult.GetBinding(_url);
            var clientCode = await client.GetClientCodeByIINAsync(_token, iin);
            var clientResult = await client.GetClientByCodeAsync(_token, clientCode, needDocuments, needContracts, needDetailedAddress, needEGOV_Address, needRiskLevel, needArchiveData);

            return clientResult;
        }        
        public async Task<Client> GetClientByCode(string clientCode, bool needDocuments, bool needContracts, bool needDetailedAddress, bool needEGOV_Address, bool needRiskLevel, bool needArchiveData)
        {
            var client = new ClientWSSoapClient(ClientWSSoapClient.EndpointConfiguration.ClientWSSoap, _url);

            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication = X509.Ignore();
            client.Endpoint.Binding = BindingResult.GetBinding(_url);

            var clientResult = await client.GetClientByCodeAsync(_token, clientCode, needDocuments, needContracts, needDetailedAddress, needEGOV_Address, needRiskLevel, needArchiveData);

            return clientResult;
        }

    }

}
