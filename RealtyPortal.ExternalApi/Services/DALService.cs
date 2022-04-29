using EasyMED.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using EASYMED.ExternalApi.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EASYMED.ExternalApi.Services
{
    public class MongoDBService
    {

    //"DoctorCollectionName": "Doctor",
    //"ReceiptCollectionName": "Receipt",
    //"PatientCollectionName": "Patient",
    //"QRCollectionName": "QR",
    //"MedicamentsCollectionName": "Medicaments",
    //"SMSCollectionName": "TFAAuth"


        private readonly IMongoCollection<TFAAuth> _SMSCollection;
        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _SMSCollection = database.GetCollection<TFAAuth>(mongoDBSettings.Value.SMSCollection);
          }

        public async Task<bool> SaveOTP(TFAAuth sms)
        {
            try
            {
                 await _SMSCollection.FindOneAndDeleteAsync(sms.PhoneNumber);
                 await _SMSCollection.InsertOneAsync(sms);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<TFAAuth> GetOTP(string phone) 
        {
            var result = (await _SMSCollection.Find(f => f.PhoneNumber == phone)?.FirstOrDefaultAsync());
            if (result != null)
            {
                await _SMSCollection.FindOneAndDeleteAsync(phone);
                return result;
            }

            return null;
        }
    }
}
