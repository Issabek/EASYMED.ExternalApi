using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealtyPortal.ExternalApi.Enums;
using Newtonsoft.Json;
using RealtyPortal.ExternalApi.Model;
using Microsoft.Extensions.Configuration;
using RealtyPortal.ExternalApi.Services;
using System.Data;
using System.Data.SqlClient;
using MongoDB.Driver;
using log4net;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Web;
using RestSharp;
using System.Net.Http;
using System.Security.Cryptography;
using System.Net;
using MongoDB.Bson;

namespace RealtyPortal.ExternalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QRSignController : ControllerBase
    {
        private readonly IConfiguration _config;
        //private string ConnectionString => _config.GetValue<string>("ConnectionStrings:SessionAuthConnectionString");
        private string ConnectionString
        {
            get
            {
                return _config.GetValue<string>("ConnectionStrings:SessionAuthConnectionString");
            }
        }
        private bool IsTest => _config.GetValue<bool>("IsTest");
        private readonly IHostingEnvironment _environment;
        public static string token;
        public MongoClient _dbClient;
        private readonly ILog _logger = LogManager.GetLogger(typeof(QRSignController));
        public static Dictionary<Guid, string> statusList;
        public QRSignController(IConfiguration config, IHostingEnvironment environment)
        {
            _config = config;
            _environment = environment;
            _dbClient= new MongoClient(ConnectionString);
        }
        [Route("Applications/statuses")]
        [HttpGet]
        public async Task<List<BsonDocument>> GetMongoDBS()
        {

            var dbList = _dbClient.ListDatabases().ToList();

            return dbList;
        }


        //    [Route("Applications/{type}/{applicationGuid}/colvirAppNum")]
        //    [HttpGet]
        //    public async Task<string> GetApplicationColvirNum(Guid applicationGuid, RentSubsidyType type)
        //    {
        //        string result = "";
        //        using (IDbConnection db = new SqlConnection(ConnectionString))
        //        {
        //            string sqlQuery = "";
        //            switch (type)
        //            {
        //                case RentSubsidyType.Landlord:
        //                    sqlQuery = "select ColvirAppNum from pn_RentSubsidy_Applicants where LandLord_id = @id";
        //                    break;
        //                case RentSubsidyType.Tenant:
        //                    sqlQuery = "select ColvirAppNum from pn_RentSubsidy_Applicants where id = @id";
        //                    break;


        //            }
        //            if (!string.IsNullOrEmpty(sqlQuery))
        //                result = await db.QueryFirstOrDefaultAsync<string>(sqlQuery, new { id = applicationGuid });
        //            return result;
        //        }
        //    }



        //    private async Task<string> GetConsentApplicationText(int fileTemplateId, string iin, Guid? appGuid)
        //    {
        //        var applicationText = String.Empty;
        //        var clientWS = new ClientWebService(ClientUrl, await GetToken());
        //        using (IDbConnection db = new SqlConnection(ConnectionString))
        //        {
        //            var template = await db.QueryFirstOrDefaultAsync<string>("select Text from pn_file_template where FileTemplateId = @fileTemplateId", new { fileTemplateId });
        //            var client = await db.QueryFirstOrDefaultAsync<PnClient>("select * from pn_Client where clientIIN = @iin", new { iin });

        //            var clientDetailed = await clientWS.GetClientByCode(client.clientCode, true, true, true, true, true, true);

        //            try
        //            {
        //                switch (fileTemplateId)
        //                {
        //                    case 15:
        //                    case 10:
        //                        {
        //                            applicationText = template
        //                                    .Replace("{FIO}", client.clientFIO)
        //                                    .Replace("{BIRTH_DATE}", clientDetailed.BirthDate.ToString("dd.MM.yyyy"))
        //                                    .Replace("{IIN}", client.clientIIN)
        //                                    .Replace("{CreateDate}", DateTime.Now.ToString());
        //                        }
        //                        break;
        //                    case 18:
        //                        PnRentSubsidyApplicants applicant = await db.QueryFirstAsync<PnRentSubsidyApplicants>("select * from pn_RentSubsidy_Applicants where TenantId=@appGuid", new { appGuid });
        //                        applicationText = template
        //                            .Replace("{CreateDate}", DateTime.Now.ToString())
        //                            .Replace("{Region}", "")
        //                            .Replace("{RentArea}", applicant.RentArea.ToString())
        //                            .Replace("{FIO}", client.clientFIO)
        //                            .Replace("{MaxRentSum}", applicant.MaxSubsidyAmount.ToString());
        //                        break;
        //                    case 16:
        //                        applicationText = template;
        //                        break;
        //                    case 17:
        //                        PnRentSubsidyApplicants app = new PnRentSubsidyApplicants();
        //                        if (appGuid != null)
        //                            app = await db.QueryFirstAsync<PnRentSubsidyApplicants>("select * from pn_RentSubsidy_Applicants where TenantId=@appGuid", new { appGuid });
        //                        if (client != null)
        //                        {
        //                            var identityDocument = clientDetailed.IdentDocuments.Where(x => x.IsDefault).FirstOrDefault();

        //                            applicationText = template
        //                                .Replace("{FIO}", client.clientFIO)
        //                                .Replace("{PHONE}", User.Identity.Name)
        //                                .Replace("{IIN}", client.clientIIN)
        //                                .Replace("{CreateDate}", DateTime.Now.ToString())
        //                                .Replace("{RentArea}", app.RentArea.ToString())
        //                                .Replace("{FamilyMembersCount}", app.FamilyMembersCount.ToString());

        //                        }
        //                        break;

        //                }
        //                return applicationText;
        //            }
        //            catch (Exception ex)
        //            {
        //                return ex.Message;
        //            }
        //        }

        //    }


        //    private async Task<string> GetToken()
        //    {
        //        var authService = new TokenAuthWebService(TokenAuthUrl);
        //        if (!String.IsNullOrEmpty(token) && await authService.CheckToken(token))
        //        {
        //            return token;
        //        }
        //        if (IsTest)
        //        {
        //            token = "7df5b7709586474fa7d358563cac0d1c1a82e005226d40bf9ff0549215fe4858";
        //        }
        //        else
        //        {
        //            token = await authService.GetToken(SystemId, SystemLogin, SystemPassword);
        //        }
        //        return token;
        //    }

        //    [Route("Applications/{type}/{applicationId}/{stepId}/QRBase")]
        //    [HttpGet]
        //    public async Task<string> GetDocumentsToBeSighned(Guid applicationId, Guid stepId, RentSubsidyType type)
        //    {
        //        string iin = "";
        //        string regNum = "";
        //        Guid fileStepId = Guid.Empty;

        //        using (IDbConnection db = new SqlConnection(ConnectionString))
        //        {
        //            if (type == RentSubsidyType.Landlord)
        //                iin = await db.QueryFirstOrDefaultAsync<string>("select LandLordIIN from pn_RentSubsidy_LandLords where id = @id", new { id = applicationId });
        //            else
        //                iin = await db.QueryFirstOrDefaultAsync<string>("select TenantIIN from pn_RentSubsidy_Applicants where id = @id", new { id = applicationId });

        //            regNum = await db.QueryFirstOrDefaultAsync<string>("select ColvirAppNum from pn_RentSubsidy_Applicants where id = @id", new { id = applicationId });
        //            fileStepId = db.QueryFirstOrDefault<Guid>("select id from pn_RentSubsidy_StepFiles where stepId = @id and subsidyType = @type", new { id = stepId, type = type.ToString() });
        //        }
        //        qrBase newLine = new qrBase();
        //        string curStep = statusList.First(s => s.Key == stepId).Value;
        //        newLine.description = "Сбор согласий для аренды";
        //        newLine.organisation = new Organisation() { nameEn = "House Constraction Savings Bank Of Kazakhstan 'Otbasy bank'", nameRu = "Жилищный Строительный Сберегательный Банк Казахстана 'Отбасы банк'", nameKz = "" };
        //        newLine.expiry_date = DateTime.Now.AddHours(12);
        //        Document doc = new Document() { auth_token = "", auth_type = AuthtenticationType.None.ToString(), uri = Url.ActionLink("GetDocuments", "QRSign", new { iin = iin, lang = "RU-ru", stepFilesId = fileStepId, regNum = regNum, applicationId = applicationId }) };

        //        newLine.document = doc;

        //        return JsonConvert.SerializeObject(newLine);

        //    }


        //    private async Task<string> LoadToBPM(byte[] file, string iin, int fileTemplateId, string regNum, string appGuid, string commonName, string ext)
        //    {
        //        try
        //        {

        //            RestClient _client = new RestClient();
        //            var uploadFileUri = _config.GetValue<string>("RentSubsidy:UploadFileUri");
        //            var uriBuilder = new UriBuilder(uploadFileUri);
        //            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        //            uriBuilder.Query = query.ToString();

        //            RestRequest request = new RestRequest(uriBuilder.Uri.AbsoluteUri);
        //            request.AddJsonBody(new { FileContent = file, FileName = ((RentSubsidyFileType)fileTemplateId).ToString() + ".pdf", SourceSystem = 1, FileType = GetFileType(fileTemplateId), RequestId = appGuid, ClientIIN = iin, ClientName = commonName, IsSignature = false, SignatureContent = "" });
        //            _client.AddDefaultHeader("AuthToken", await GetBPMToken());

        //            if (IsTest)
        //            {
        //                var response = await _client.PutAsync<string>(request);
        //                return response;
        //            }
        //            else
        //            {
        //                var response = await _client.PutAsync(request);
        //                RentRequestResult reqResult = JsonConvert.DeserializeObject<RentRequestResult>(response.Content);

        //                return reqResult.code;
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        return "ERROR";
        //    }


        //    [Route("Applications/{lang}/{iin}/{stepFilesId}/{regNum}/{applicationId}/documents")]
        //    [HttpGet]
        //    public async Task<string> GetDocuments(string iin, string lang, Guid stepFilesId, string regNum, string applicationId)
        //    {
        //        try
        //        {
        //            using (IDbConnection db = new SqlConnection(ConnectionString))
        //            {
        //                List<string> ids = db.QueryFirstOrDefault<string>("select filesToSign from pn_RentSubsidy_StepFiles where id = @id", new { id = stepFilesId })?.Split(',').ToList();
        //                SignMethod signMethod = SignMethod.CMS_SIGN_ONLY;
        //                List<DocumentsToSign> documentsToSign = new List<DocumentsToSign>();
        //                bool hasFile = false;


        //                for (int i = 0; i < ids.Count; i++)
        //                {
        //                    hasFile = (await db.QueryFirstOrDefaultAsync<int>("select top 1 FileSignedByClientId from pn_file_signed_by_client where ClientIIN = @iin and FileTemplateId=@id and RegNum = @regNum", new { iin, id = ids[i], regNum })) > 0 ? true : false;
        //                    if (!hasFile)
        //                    {
        //                        DocumentsToSign newDoc = new DocumentsToSign();
        //                        newDoc.id = i + 1;
        //                        newDoc.nameRu = await db.QueryFirstOrDefaultAsync<string>("select TemplateName from pn_file_template where FileTemplateId = @id", new { id = ids[i] });
        //                        file fileData = new file();
        //                        fileData.mime = "application/pdf";
        //                        var generatedPdf = await GetDocumentDyFileTemplateId(Int32.Parse(ids[i]), iin, lang, string.IsNullOrEmpty(lang) ? true : false, regNum: regNum);
        //                        generatedPdf = WebUtility.HtmlDecode(generatedPdf);
        //                        var pdf = ConvertToPdf(generatedPdf);

        //                        fileData.data = Convert.ToBase64String(pdf);
        //                        document base64Doc = new document() { file = fileData };
        //                        newDoc.document = base64Doc;
        //                        documentsToSign.Add(newDoc);
        //                    }
        //                }
        //                return JsonConvert.SerializeObject(new docsNSign() { signMethod = signMethod.ToString(), documentsToSign = documentsToSign, applicationId = applicationId });
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            return e.Message;
        //        }
        //        return "";
        //    }

        //    [Route("Applications/{lang}/{iin}/{stepFilesId}/{regNum}/{applicationId}/documents")]
        //    [HttpPut]
        //    public async Task<string> PutDocuments(string iin, string lang, Guid stepFilesId, string regNum, string applicationId)
        //    {
        //        try
        //        {
        //            string s = await new StreamReader(Request.HttpContext.Request.Body).ReadToEndAsync();
        //            var DocToSign = JsonConvert.DeserializeObject<docsNSign>(s);

        //            //foreach (var item in DocToSign.documentsToSign)
        //            //{
        //            //    var validateCMS = await ValidateCMSAsync(item.document.file.data, iin);

        //            //    if (!validateCMS)
        //            //    {
        //            //        return "CMS validation failed";
        //            //    }
        //            //}

        //            using (IDbConnection db = new SqlConnection(ConnectionString))
        //            {
        //                PnClient client = await db.QueryFirstOrDefaultAsync<PnClient>("select * from pn_client where clientIIN=@iin", new { iin });
        //                List<string> ids = db.QueryFirstOrDefault<string>("select filesToSign from pn_RentSubsidy_StepFiles where id = @id", new { id = stepFilesId })?
        //                    .Split(',')
        //                    .ToList();
        //                var index = 0;

        //                foreach (DocumentsToSign doc in DocToSign.documentsToSign)
        //                {
        //                    var hasFile = (await db.QueryFirstOrDefaultAsync<int>("select top 1 FileSignedByClientId from pn_file_signed_by_client where ClientIIN = @iin and FileTemplateId=@id and RegNum = @regNum", new { iin, id = ids[index], regNum })) > 0;
        //                    var generatedPdf = "";
        //                    if (!hasFile)
        //                    {
        //                        generatedPdf = await GetDocumentDyFileTemplateId(Int32.Parse(ids[index]), iin, lang, string.IsNullOrEmpty(lang) ? true : false, regNum: regNum);
        //                        generatedPdf = WebUtility.HtmlDecode(generatedPdf);
        //                    }
        //                    var pdf = ConvertToPdf(generatedPdf);

        //                    var check = await db.ExecuteAsync(@"INSERT INTO [dbo].[pn_file_signed_by_client]
        //                                   ([ClientIIN]
        //                                   ,[CreateDate]
        //                                   ,[FileTemplateId]
        //                                   ,[SignedDate]
        //                                   ,[PDFFile]
        //                                   ,[RegNum]
        //                                   ,[Lang]
        //                                   ,[FileHash]
        //                                   ,[FileSign]
        //                                   ,[FileSignedGuid]
        //                                   ,[FileText])
        //                             VALUES
        //                                   (@iin
        //                                   ,@date
        //                                   ,@fileTemplateId
        //                                   ,@sdate
        //                                   ,@fByte
        //                                   ,@regNum
        //                                   ,@lang
        //                                   ,''
        //                                   ,@cms
        //                                   ,@applicationId
        //                                   ,@fileText);", new { iin, date = DateTime.Now, fileTemplateId = ids[doc.id - 1], sdate = DateTime.Now, fByte = pdf, regNum, lang, cms = doc.document.file.data, applicationId = applicationId, fileText = doc.document.file.mime });
        //                    var resultOfBpm = await LoadToBPM(pdf, iin, Int32.Parse(ids[doc.id - 1]), regNum, applicationId, client.clientFIO, ".pdf");
        //                    if(resultOfBpm != "000" && !IsTest)
        //                    {
        //                        return "Failed to load file to BPM";
        //                    }
        //                    index++;
        //                }
        //            }

        //            try
        //            {
        //                var client = new HttpClient();
        //                await client.GetAsync("https://otbasybank.kz/RentSubsidy/ws?token=" + iin);
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            return e.Message;
        //        }
        //        return "success";
        //    }

        //    private async Task<bool> ValidateCMSAsync(string cms, string iin)
        //    {
        //        var requestObj = new
        //        {
        //            version = "1.0",
        //            method = "X509.info",
        //            @params = new
        //            {
        //                cert = cms
        //            }
        //        };
        //        var uploadFileUri = _config.GetValue<string>("NCANodeUrl");

        //        try
        //        {
        //            RestClient client = new RestClient(uploadFileUri)
        //            .AddDefaultHeader(KnownHeaders.Accept, "application/json")
        //            .AddDefaultHeader(KnownHeaders.ContentType, "application/json");
        //            RestRequest request = new RestRequest();
        //            request.AddHeader("Accept", "application/json");
        //            request.AddHeader("Content-Type", "application/json");
        //            request.AddJsonBody(requestObj);

        //            var response = await client.ExecutePostAsync(request);
        //            var result = JsonConvert.DeserializeObject<Root>(response.Content);

        //            return result.result.subject.iin == iin;
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }

        //    [Route("Test")]
        //    [HttpPut]
        //    public async Task<ActionResult> Test(string token, string iin, string url)
        //    {
        //        if(token != "qe114Vsqwgwnwoiwnef@@311f1223e!!")
        //        {
        //            return Forbid();
        //        }

        //        try
        //        {
        //            HttpClientHandler clientHandler = new HttpClientHandler();
        //            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        //            // Pass the handler to httpclient(from you are calling api)
        //            HttpClient client = new HttpClient(clientHandler);
        //            var response = await client.GetAsync($"https://{url}/RentSubsidy/ws?token=" + iin);
        //        }
        //        catch (Exception ex)
        //        {

        //            throw;
        //        }

        //        return Ok();
        //    }

        //    private async Task<string> GetDocumentDyFileTemplateId(int fileTemplateId, string iin, string lang, bool bothInOne = false, string regNum = "", string urlForQr = "")
        //    {
        //        PnFileTemplate fileTemplate = null;
        //        string text = string.Empty;
        //        try
        //        {
        //            using (IDbConnection db = new SqlConnection(ConnectionString))
        //            {
        //                fileTemplate = await db.QueryFirstOrDefaultAsync<PnFileTemplate>("select * from pn_file_template where filetemplateid = @fileTemplateId", new { fileTemplateId });
        //            }
        //            if (fileTemplate != null)
        //            {
        //                if (bothInOne)
        //                {
        //                    text = await GetReplaceFile(fileTemplate.Text, fileTemplateId, iin, regNum, urlForQr, "ru-RU");
        //                    text += await GetReplaceFile(fileTemplate.TextKK, fileTemplateId, iin, regNum, urlForQr, "kk-KZ");
        //                }
        //                else
        //                {
        //                    if (lang == "kk-KZ")
        //                        text = await GetReplaceFile(fileTemplate.TextKK, fileTemplateId, iin, regNum, urlForQr, lang);
        //                    else
        //                        text = await GetReplaceFile(fileTemplate.Text, fileTemplateId, iin, regNum, urlForQr, lang);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            LogicalThreadContext.Properties["UserIin"] = iin;
        //            LogicalThreadContext.Properties["UserId"] = 0;
        //            LogicalThreadContext.Properties["ExInnerMessage"] = ex.InnerException;
        //            LogicalThreadContext.Properties["ExStackTrace"] = ex.StackTrace;
        //            LogicalThreadContext.Properties["Source"] = ex.Source;

        //            _logger.Error(ex.Message, ex);
        //        }
        //        return text;
        //    }

        //    private async Task<string> GetReplaceFile(string text, int templateId, string iin, string regNum = "", string urlForQr = "", string lang = "")
        //    {
        //        string replaceText = text;


        //        try
        //        {
        //            using (IDbConnection db = new SqlConnection(ConnectionString))
        //            {
        //                PnClient client = await db.QueryFirstOrDefaultAsync<PnClient>("select * from pn_client where clientIIN=@iin", new { iin });
        //                PnRentSubsidyApplicants applicant = await db.QueryFirstOrDefaultAsync<PnRentSubsidyApplicants>("select * from pn_rentsubsidy_applicants where colvirappnum = @regnum", new { regnum = regNum });

        //                switch (templateId)
        //                {
        //                    case 1:
        //                        {
        //                            if (client != null)
        //                            {
        //                                replaceText = replaceText
        //                                    .Replace("{ClientFio}", client.clientFIO)
        //                                    .Replace("{ClientIin}", client.clientIIN)
        //                                    .Replace("{ApplicationId}", "")
        //                                    .Replace("{CreateDate}", string.Format("{0:dd.MM.yyyy}", DateTime.Now));
        //                            }
        //                        }
        //                        break;
        //                    case 15:
        //                    case 21:
        //                    case 10:
        //                        if (client != null)
        //                        {
        //                            var clientWS = new ClientWebService(ClientUrl, await GetToken());

        //                            var clientDetailed = await clientWS.GetClientByCode(client.clientCode, true, true, true, true, true, true);
        //                            var identityDocument = clientDetailed.IdentDocuments.Where(x => x.IsDefault).FirstOrDefault();

        //                            replaceText = replaceText
        //                                .Replace("{CreateDate}", DateTime.Now.ToString("dd.MM.yyyy"))
        //                                .Replace("{CreateTime}", DateTime.Now.ToString("HH:mm"))
        //                                .Replace("{FIO}", client.clientFIO)
        //                                .Replace("{BIRTH_DATE}", clientDetailed.BirthDate.ToString("dd.MM.yyyy"))
        //                                .Replace("{DOC_NUM}", identityDocument?.Number ?? "ОТСУТСТВУЕТ ДОКУМЕНТ")
        //                                .Replace("{DOC_DATE}", identityDocument?.DateStart.ToString("dd.MM.yyyy") ?? "ОТСУТСТВУЕТ ДОКУМЕНТ")
        //                                .Replace("{DOC_TYPE}", identityDocument?.TypeName ?? "ОТСУТСТВУЕТ ДОКУМЕНТ")
        //                                .Replace("{ADR_REG}", clientDetailed.EgovAddress?.EGOV_RegFullAddressRU ?? "ОТСУТСТВУЕТ ДОКУМЕНТ")
        //                                .Replace("{IIN}", client.clientIIN);
        //                        }
        //                        return replaceText;

        //                        break;
        //                    case 18:
        //                        replaceText = replaceText
        //                            .Replace("{CreateDate}", DateTime.Now.ToString())
        //                            .Replace("{Region}", applicant.Region)
        //                            .Replace("{RentArea}", applicant.RentArea.ToString())
        //                            .Replace("{FIO}", client.clientFIO)
        //                            .Replace("{MaxRentSum}", applicant.MaxSubsidyAmount.ToString());
        //                        break;
        //                    case 16:
        //                        PnRentSubsidyApplicantsBPM bpmData = await GetAppData(applicant.TenantId);
        //                        if (bpmData.mioDetails != null)
        //                        {
        //                            replaceText = replaceText
        //                               .Replace("{Region}", bpmData.mioDetails.addressObl.name)
        //                               .Replace("{CreateDate}", DateTime.Today.ToShortDateString())
        //                               .Replace("{RegNum}", bpmData.reg.regnum)
        //                               .Replace("{LandLordFio}", bpmData.contractParameters.landLordName)
        //                               .Replace("{LandLordAdr}", bpmData.contractParameters.landLord.mioDetails.address)
        //                               .Replace("{LandLordIIN}", bpmData.contractParameters.landLordIin)
        //                               .Replace("{TenantFio}", bpmData.client.fullName)
        //                               .Replace("{TenantAdr}", bpmData.mioDetails.address)
        //                               .Replace("{TenantIIN}", bpmData.mioDetails.iin)
        //                               .Replace("{LandLordDocNumber}", bpmData.contractParameters.landLord.idCard.identificationNumber)
        //                               .Replace("{LandLordDocIssuer}", bpmData.contractParameters.landLord.idCard.issuingAuthority)
        //                               .Replace("{LandLordDocIssuanceDate}", bpmData.contractParameters.landLord.idCard.startDate.ToShortDateString())
        //                               .Replace("{TenantDocNumber}", bpmData.client.idCard.identificationNumber)
        //                               .Replace("{TenantDocIssuanceDate}", bpmData.client.idCard.startDate.ToShortDateString())
        //                               .Replace("{TenantDocIssuer}", bpmData.client.idCard.issuingAuthority)
        //                               .Replace("{RentAdr}", bpmData.contractParameters.address)
        //                               .Replace("{RentRoomsNum}", "TODO: ZAMENIT'")
        //                               .Replace("{RentArea}", bpmData.contractParameters.totalArea.ToString())
        //                               .Replace("{RentLivingArea}", bpmData.contractParameters.livingArea.ToString())
        //                               .Replace("{RentNotLivingArea}", (bpmData.contractParameters.totalArea - bpmData.contractParameters.livingArea).ToString())
        //                               .Replace("{RentTotalAmount}", bpmData.contractParameters.monthlyPayment.ToString());
        //                        }
        //                        break;
        //                    case 17:
        //                        if (client != null)
        //                        {
        //                            replaceText = replaceText
        //                                .Replace("{FIO}", client.clientFIO)
        //                                .Replace("{PHONE}", User.Identity.Name)
        //                                .Replace("{IIN}", client.clientIIN)
        //                                .Replace("{CreateDate}", DateTime.Now.ToString())
        //                                .Replace("{RentArea}", applicant.RentArea.ToString())
        //                                .Replace("{FamilyMembersCount}", applicant.FamilyMembersCount.ToString());

        //                        }
        //                        break;

        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            LogicalThreadContext.Properties["UserIin"] = iin;
        //            LogicalThreadContext.Properties["UserId"] = 0;
        //            LogicalThreadContext.Properties["ExInnerMessage"] = ex.InnerException;
        //            LogicalThreadContext.Properties["ExStackTrace"] = ex.StackTrace;
        //            LogicalThreadContext.Properties["Source"] = ex.Source;

        //            _logger.Error(ex.Message, ex);

        //            return replaceText;
        //        }


        //        return replaceText.Replace("{CreateDate}", DateTime.Today.ToShortDateString());
        //    }

        //    private string GetNumberString(decimal amount, string lang)
        //    {
        //        return amount == 0
        //               ? string.Format("{0} {1}", amount, GetCurrencyName(lang))
        //               : string.Format("{0:#,#.00#} {1}", amount, GetCurrencyName(lang));
        //    }
        //    private string GetCurrencyName(string lang)
        //    {
        //        if (lang.Equals("kk-KZ"))
        //            return "теңге";
        //        else
        //            return "тенге";
        //    }

        //    private byte[] ConvertToPdf(string html)
        //    {
        //        using (var workStream = new MemoryStream())
        //        {
        //            using (var md5 = MD5.Create())
        //            {
        //                using (var pdfWriter = new iText.Kernel.Pdf.PdfWriter(workStream))
        //                {
        //                    pdfWriter.SetCloseStream(false);
        //                    using (var document = iText.Html2pdf.HtmlConverter.ConvertToDocument(html, pdfWriter))
        //                    {
        //                    }
        //                }
        //                workStream.Position = 0;

        //                var str = new System.Text.StringBuilder();
        //                var computeHash = md5.ComputeHash(workStream);
        //                foreach (var theByte in computeHash)
        //                {
        //                    str.Append(theByte.ToString("x2"));
        //                }

        //                return workStream.ToArray();
        //            }
        //        }
        //    }

        //    private static string Base64Encode(string plainText)
        //    {
        //        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        //        return System.Convert.ToBase64String(plainTextBytes);
        //    }

        //    private static string Base64Decode(string base64EncodedData)
        //    {
        //        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        //        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        //    }

        //    private async Task<string> GetBPMToken()
        //    {
        //        RestClient _client = new RestClient();
        //        string retrieveRentTokenUri;
        //        if (IsTest)
        //        {
        //            retrieveRentTokenUri = _config.GetValue<string>("RentSubsidy:RetrieveRentTokenUri") + _config.GetValue<string>("RentSubsidy:TokenPasswordTest");
        //        }
        //        else
        //        {
        //            retrieveRentTokenUri = _config.GetValue<string>("RentSubsidy:RetrieveRentTokenUri") + _config.GetValue<string>("RentSubsidy:TokenPasswordDeploy");
        //        }
        //        RestRequest request = new RestRequest(retrieveRentTokenUri);
        //        var res = await _client.ExecuteGetAsync(request);
        //        if (IsTest)
        //        {
        //            return res.Content.Trim('\"');
        //        }
        //        else
        //        {
        //            RentRequestResult reqResult = JsonConvert.DeserializeObject<RentRequestResult>(res.Content);
        //            var result = ((string)reqResult.reply).Trim('\"');

        //            return result;
        //        }
        //    }

        //    private async Task<PnRentSubsidyApplicantsBPM> GetAppData(Guid appGuid)
        //    {
        //        try
        //        {
        //            var requestDetailsUri = _config.GetValue<string>("RentSubsidy:RequestDetailsUri");
        //            var uriBuilder = new UriBuilder(requestDetailsUri);
        //            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        //            query["id"] = appGuid.ToString();
        //            uriBuilder.Query = query.ToString();

        //            RestClient _client = new RestClient();
        //            RestRequest request = new RestRequest(uriBuilder.Uri.AbsoluteUri);

        //            _client = new RestClient();
        //            _client.AddDefaultHeader("AuthToken", await GetBPMToken());
        //            string jsonRes = (await _client.ExecuteGetAsync(request)).Content;


        //            var result = JsonConvert.DeserializeObject<PnRentSubsidyApplicantsBPM>(jsonRes);
        //            return result;
        //        }
        //        catch (Exception e)
        //        {
        //            LogicalThreadContext.Properties["UserIin"] = 0;
        //            LogicalThreadContext.Properties["UserId"] = 0;
        //            LogicalThreadContext.Properties["ExInnerMessage"] = e.InnerException;
        //            LogicalThreadContext.Properties["ExStackTrace"] = e.StackTrace;
        //            LogicalThreadContext.Properties["Source"] = e.Source;
        //        }
        //        return null;
        //    }

        //    private int GetFileType(int fileTemplateId)
        //    {
        //        switch (fileTemplateId)
        //        {
        //            case 10:
        //                return 1;
        //            case 15:
        //                return 2;
        //            case 17:
        //                return 4;
        //            case 1010:
        //                return 6;
        //            default:
        //                return 5;
        //        }
        //    }


        //    [HttpGet("GetFileFromFileSign")]
        //    public async Task<ActionResult> GetPdfFileAsync(int id, string token)
        //    {
        //        if (token != "Read123DLVVID!!!!@@@@@@KUL")
        //        {
        //            return Ok();
        //        }
        //        using (IDbConnection db = new SqlConnection(ConnectionString))
        //        {
        //            var test = await db.QueryFirstOrDefaultAsync<PnFileSignedByClient>($"select * from [dbo].[pn_file_signed_by_client] where FileSignedByClientId = {id}");

        //            return File(test.PDFFile, "application/pdf", "Test.pdf");
        //        }
        //    }

        //    [HttpGet("GetStringFromFileSign")]
        //    public async Task<ActionResult> GetPdfFileStringAsync(int id, string token)
        //    {
        //        if (token != "Read123DLVVID!!!!@@@@@@KUL")
        //        {
        //            return Ok();
        //        }
        //        using (IDbConnection db = new SqlConnection(ConnectionString))
        //        {
        //            var test = await db.QueryFirstOrDefaultAsync<PnFileSignedByClient>($"select * from [dbo].[pn_file_signed_by_client] where FileSignedByClientId = {id}");

        //            return Ok(Convert.ToBase64String(test.PDFFile));
        //        }
        //    }
        //}
    }
}
