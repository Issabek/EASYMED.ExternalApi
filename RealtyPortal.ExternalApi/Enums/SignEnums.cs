using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyPortal.ExternalApi.Enums
{
    public enum AuthtenticationType
    {
        Token,
        Eds,
        None
    }
    public enum SignMethod
    {
        XML,
        CMS_WITH_DATA,
        CMS_SIGN_ONLY
    }

    public enum RentSubsidyFileType
    {
        ConsGetData = 10,
        GCVPAgreement = 15,
        EstateAbsenceCertificate = 3,
        ApplicationForPayment = 17,
        LeaseContract = 16,
        Notification = 1010
    }
    public enum RentSubsidyType
    {
        Tenant,
        Landlord,
        None
    }

    public enum RentSubsidyStatuses
    {
        APP_AGREEMENT,
        ON_REVISION,
        APP_ON_SECOND_SIGN,
        APPS_ON_FIRST_SIGN,
        APP_SECOND_VERIFICATION,
        PASS_CREATED,
        LANDLORD_APPROVED,
        APP_DENIED,
        APP_FIRST_VERIFICATION,
        RENT_FINISH,
        APPS_SIGHNED,
        ON_PAYMENT,
    }
}
