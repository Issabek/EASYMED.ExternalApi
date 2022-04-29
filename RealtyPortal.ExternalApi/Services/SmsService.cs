using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using EASYMED.ExternalApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using System.Web;
using EasyMED.Model;

namespace EasyMED.Services
{
    public class SmsService 
    {
        private readonly MongoDBService _mongoDBService;

        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _environment;
        public static string token;
        public MongoClient _dbClient;
        public static Dictionary<Guid, string> statusList;
        private RestRequest request = new RestRequest();
        private static Random rnd = new Random();

        public SmsService(IConfiguration config, IHostingEnvironment environment, MongoDBService mongoDBService)
        {
            _config = config;
            _environment = environment;
            _mongoDBService = mongoDBService;
        }

        private string GetApiKey()
        {
            return _config.GetValue<string>("SMSAuth:Key");
        }
        public async Task<BalanceResponse> CheckBalance()
        {
            try
            {
                RestClient _client = new RestClient();
                #region RequestBody
                var balanceUri = _config.GetValue<string>("SMSAuth:CheckBalanceUri");
                var uriBuilder = new UriBuilder(balanceUri);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["output"] = "json";
                query["api"] = "v1";
                query["apiKey"] = GetApiKey();
                uriBuilder.Query = query.ToString();
                RestRequest request = new RestRequest(uriBuilder.Uri.AbsoluteUri);
                #endregion
                var result = await _client.GetAsync<BalanceResponse>(request);
                return result;
            }
            catch(Exception ex)
            {
                var s = 5;

            }
            return null;
        }        
        
        public async Task SendSMS(string urlEncodedMsg, string from, string phone)
        {
    
                RestClient _client = new RestClient();
                #region RequestBody
                var balanceUri = _config.GetValue<string>("SMSAuth:SendSMSUri");
                var uriBuilder = new UriBuilder(balanceUri);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["recipient"] = phone;
                query["text"] = urlEncodedMsg;
                query["apiKey"] = GetApiKey();
                uriBuilder.Query = query.ToString();
                RestRequest request = new RestRequest(uriBuilder.Uri.AbsoluteUri);
                #endregion
                var result = await _client.GetAsync(request);
                if (result.Content==null || !result.Content.Contains("\"code\":0,\""))
                    throw new InvalidProgramException();
                var resT = result;
           
        }  
        
        public bool GenerateOTP(string phone, out int code)
        {
            try
            {
                int Otp = rnd.Next(1001, 9999);
                TFAAuth sms = new TFAAuth() {PhoneNumber=phone, OtpCode=Otp.ToString()};
                code = Otp;
                _mongoDBService.SaveOTP(sms);
                return true;
            }
            catch(Exception ex)
            {
                var s = 5;
                code = 0; 
                return false;
            }
        }        
        public async Task<bool> OtpAuth(string phone, int otp)
        {
            try
            {
                TFAAuth sms = await _mongoDBService.GetOTP(phone);
                return sms.OtpCode==otp.ToString();
            }
            catch(Exception ex)
            {
                var s = 5;
                return false;
            }
        }
    }
}
