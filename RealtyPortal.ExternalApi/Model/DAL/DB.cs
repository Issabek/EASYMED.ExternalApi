using Microsoft.Extensions.Configuration;

namespace EASYMED.ExternalApi.Model.DAL
{
    public class MongoDBSettings
    {
        //"DoctorCollectionName": "Doctor",
        //"ReceiptCollectionName": "Receipt",
        //"PatientCollectionName": "Patient",
        //"QRCollectionName": "QR",
        //"MedicamentsCollectionName": "Medicaments",
        //"SMSCollectionName": "TFAAuth"
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string SMSCollection { get; set; } = null!;
        public string MedicamentsCollection { get; set; } = null!;
        public string QRCollection { get; set; } = null!;
        public string PatientCollection { get; set; } = null!;
        public string ReceiptCollection { get; set; } = null!;
        public string DoctorCollection { get; set; } = null!;

    }
}
