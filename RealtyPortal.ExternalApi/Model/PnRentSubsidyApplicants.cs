using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyPortal.ExternalApi.Model
{
    public partial class PnRentSubsidyApplicants
    {

        public Guid TenantId { get; set; }

        public string TenantIIN { get; set; }

        public string ColvirAppNum { get; set; }

        public Nullable<Guid> LandLord_id { get; set; }

        public string LandLord_Iin { get; set; }

        public Nullable<DateTime> CreateDate { get; set; }

        public long TenantUserId { get; set; }

        public Nullable<Guid> StatusId { get; set; }

        public virtual PnStatus Statuses { get; set; }
        public Nullable<bool> HasRelativesLoaded { get; set; }
        public Nullable<double> RentArea { get; set; }
        public Nullable<int> FamilyMembersCount { get; set; }
        public string Region { get; set; }
        public Nullable<decimal> MaxSubsidyAmount { get; set; }
        public string ReviewResultMsg { get; set; }

    }

    public class Reg
    {
        public DateTime startdate { get; set; }
        public string finishdate { get; set; }
        public string regnum { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string branch { get; set; }
        public string branchn { get; set; }
        public string dep { get; set; }
        public string depn { get; set; }
    }

    public class Usr
    {
        public object emp { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string branch { get; set; }
        public string branchn { get; set; }
        public string dep { get; set; }
        public string depn { get; set; }
        public object pos { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string comment { get; set; }
        public string dec { get; set; }
        public string decn { get; set; }
    }

    public class Sys
    {
        public string view { get; set; }
        public string role { get; set; }
        public string type { get; set; }
        public string reply { get; set; }
        public string status { get; set; }
        public string processguid { get; set; }
        public string requestguid { get; set; }
        public string actual { get; set; }
        public string version { get; set; }
    }

    public class Dep
    {
        public int depId { get; set; }
        public int cbsDepId { get; set; }
        public string cbsDepCode { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Branch
    {
        public string city { get; set; }
        public Dep dep { get; set; }
        public int depId { get; set; }
        public int cbsDepId { get; set; }
        public string cbsDepCode { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Initiator
    {
        public string code { get; set; }
        public string cbsCode { get; set; }
        public string aisUserCode { get; set; }
        public object tabNumber { get; set; }
        public string fullName { get; set; }
        public object shortName { get; set; }
        public object position { get; set; }
        public Branch branch { get; set; }
        public object decide { get; set; }
        public object phone { get; set; }
        public object email { get; set; }
        public bool haveRoleToShow { get; set; }
        public bool haveRoleToEdit { get; set; }
        public bool haveRoleVideoConsultant { get; set; }
    }

    public class BPMAccount
    {
    }

    public class Affiliated
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool isAffiliated { get; set; }
        public string comment { get; set; }
    }

    public class IdCard
    {
        public string identificationNumber { get; set; }
        public string issuingAuthority { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Phone
    {
        public string cTypeCode { get; set; }
        public string cTypeName { get; set; }
        public string aTypeId { get; set; }
        public string aTypeName { get; set; }
        public string contactValue { get; set; }
    }

    public class MioDetails
    {
        public string iin { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public Nullable<DateTime> birthDate { get; set; }
        public Nullable<DateTime> dateOfRegistration { get; set; }
        public object category { get; set; }
        public object subCategory { get; set; }
        public AddressObl addressObl { get; set; }
        public AddressReg addressReg { get; set; }
        public string address { get; set; }
        public object contact { get; set; }
        public Nullable<bool> status { get; set; }
    }

    public class AddressObl
    {
        public string code { get; set; }
        public string name { get; set; }
    }
    public class AddressReg
    {
        public string code { get; set; }
        public string name { get; set; }
    }
    public class RelationShip
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Gcvp
    {
        public object url { get; set; }
        public object error { get; set; }
        public DateTime lastDate { get; set; }
        public int payAmountSum { get; set; }
        public bool isActive { get; set; }
        public bool isCalced { get; set; }
        public bool isOk { get; set; }
    }

    public class Client
    {
        public string id { get; set; }
        public string depId { get; set; }
        public string code { get; set; }
        public string iin { get; set; }
        public string fullName { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string middleName { get; set; }
        public object shortName { get; set; }
        public bool isIp { get; set; }
        public bool isResident { get; set; }
        public DateTime birthDate { get; set; }
        public string placeOfBirth { get; set; }
        public string registrationAddress { get; set; }
        public string residenceAddress { get; set; }
        public Affiliated affiliated { get; set; }
        public IdCard idCard { get; set; }
        public List<Phone> phones { get; set; }
        public MioDetails mioDetails { get; set; }
        public RelationShip relationShip { get; set; }
        public object spouseType { get; set; }
        public bool isHaveEstate { get; set; }
        public List<object> files { get; set; }
        public object pSex { get; set; }
        public int age { get; set; }
        public int numberOfFamily { get; set; }
        public object confidant { get; set; }
        public Gcvp gcvp { get; set; }
        public List<object> incomes { get; set; }
        public List<object> checkFlags { get; set; }
    }

    public class ContractParameters
    {
        public string address { get; set; }
        public string rka { get; set; }
        public string cadastralNumber { get; set; }
        public Nullable<double> totalArea { get; set; }
        public Nullable<double> livingArea { get; set; }
        public Nullable<double> landArea { get; set; }
        public int? monthlyPayment { get; set; }
        public string landLordIin { get; set; }
        public string landLordName { get; set; }
        public string requisites_IBAN { get; set; }
        public object requisites_BIK { get; set; }
        public int paymentDay { get; set; }
        public int period { get; set; }
        public object startDate { get; set; }
        public object endDate { get; set; }
        public LandLord landLord { get; set; }
        public object reality { get; set; }
    }
    public class LandLord
    {
        public string id { get; set; }
        public string depId { get; set; }
        public string code { get; set; }
        public string iin { get; set; }
        public string fullName { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string middleName { get; set; }
        public object shortName { get; set; }
        public bool isIp { get; set; }
        public bool isResident { get; set; }
        public DateTime birthDate { get; set; }
        public string placeOfBirth { get; set; }
        public string registrationAddress { get; set; }
        public string residenceAddress { get; set; }
        public Affiliated affiliated { get; set; }
        public IdCard idCard { get; set; }
        public List<Phone> phones { get; set; }
        public MioDetails mioDetails { get; set; }
        public RelationShip relationShip { get; set; }
        public List<object> realities { get; set; }
        public List<object> studentInfoList { get; set; }
        public object spouseType { get; set; }
        public List<object> files { get; set; }
        public object pSex { get; set; }
        public Citizenship citizenship { get; set; }
        public int age { get; set; }
        public int numberOfFamily { get; set; }
        public object confidant { get; set; }
        public Gcvp gcvp { get; set; }
        public List<Income> incomes { get; set; }
        public List<CheckFlag> checkFlags { get; set; }
    }

    public class Citizenship
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class PnRentSubsidyApplicantsBPM
    {
        public string guid { get; set; }
        public string code { get; set; }
        public string ver { get; set; }
        public string vguid { get; set; }
        public string id { get; set; }
        public int step { get; set; }
        public Reg reg { get; set; }
        public Usr usr { get; set; }
        public Sys sys { get; set; }
        public string formCode { get; set; }
        public Initiator initiator { get; set; }
        public object credAdminGB { get; set; }
        public BPMAccount account { get; set; }
        public List<UserFile> userFiles { get; set; }
        public Client client { get; set; }
        public List<FamilyMember> familyMembers { get; set; }
        public string systemCode { get; set; }
        public ContractParameters contractParameters { get; set; }
        public string resolution { get; set; }
        public Nullable<double> incomeThreshold { get; set; }
        public Nullable<double> incomeSum { get; set; }
        public Nullable<double> maxRentSum { get; set; }
        public Nullable<double> maxRentArea { get; set; }
        public int coefficient { get; set; }
        public List<CheckFlag> checkFlags { get; set; }
        public MioDetails mioDetails { get; set; }

    }

    public class Account
    {
    }

    public class Type
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class UserFile
    {
        public string id { get; set; }
        public Type type { get; set; }
        public object name { get; set; }
        public object base64Str { get; set; }
        public object url { get; set; }
        public bool isArchive { get; set; }
        public bool isSystem { get; set; }
        public string ownerIin { get; set; }
    }

    public class IncomeType
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Income
    {
        public string id { get; set; }
        public IncomeType incomeType { get; set; }
        public bool isAuto { get; set; }
        public double amount { get; set; }
        public object file { get; set; }
    }

    public class CheckFlag
    {
        public string code { get; set; }
        public bool isChecked { get; set; }
        public string message { get; set; }
        public bool value { get; set; }
    }

    public class FamilyMember
    {
        public object id { get; set; }
        public object depId { get; set; }
        public object code { get; set; }
        public string iin { get; set; }
        public string fullName { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string middleName { get; set; }
        public string shortName { get; set; }
        public bool isIp { get; set; }
        public bool isResident { get; set; }
        public DateTime birthDate { get; set; }
        public object placeOfBirth { get; set; }
        public object registrationAddress { get; set; }
        public object residenceAddress { get; set; }
        public object affiliated { get; set; }
        public object idCard { get; set; }
        public List<object> phones { get; set; }
        public object mioDetails { get; set; }
        public RelationShip relationShip { get; set; }
        public object spouseType { get; set; }
        public bool isHaveEstate { get; set; }
        public List<object> files { get; set; }
        public object pSex { get; set; }
        public int age { get; set; }
        public int numberOfFamily { get; set; }
        public object confidant { get; set; }
        public object gcvp { get; set; }
        public List<object> incomes { get; set; }
        public List<CheckFlag> checkFlags { get; set; }
    }

}

