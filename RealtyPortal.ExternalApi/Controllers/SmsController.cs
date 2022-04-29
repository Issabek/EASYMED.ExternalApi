using EasyMED.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using EASYMED.ExternalApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EasyMED.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _environment;
        public static string token;
        public SmsService _smsService;
        public static Dictionary<Guid, string> statusList;
        public SmsController(IConfiguration config, IHostingEnvironment environment, MongoDBService mongoDBService, SmsService smsService)
        {
            _config = config;
            _environment = environment;
            _mongoDBService = mongoDBService;
            _smsService = smsService;
        }

        [Route("Auth/SendSms")]
        [HttpPost]
        public async Task<bool> SendSms(string phone)
        {
            try
            {
                string msg = "EASYMED: Vash kod podtverjdeniya ";
                int otp = 0;
                     _smsService.GenerateOTP(phone, out otp);
                if (otp != 0)
                {
                    msg = msg + otp;
                    await _smsService.SendSMS(msg, "EASYMED",phone);

                    return true;
                }
                return false;
            }

            catch (InvalidProgramException ex2)
            {
                return false;

            }
            catch (Exception ex)
            {
                return false;

            }
        }        
        
        [Route("Auth/CheckOtp")]
        [HttpPost]
        public async Task<bool> CheckOtp(string phone, int otp)
        {
            try
            {
                  bool isValid =await _smsService.OtpAuth(phone, otp);
                return isValid;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        [Route("Auth/CheckBalance")]
        [HttpPost]
        public async Task<string> CheckBalance()
        {
            try
            {
                var res = await _smsService.CheckBalance();
                return res.data.balance + " " + res.data.currency;
            }
            catch(Exception ex)
            {
                return ex.Message;

            }
        }
    }
}
