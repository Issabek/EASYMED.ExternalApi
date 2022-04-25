// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealtyPortal.ExternalApi.Enums;
using Newtonsoft.Json;
using RealtyPortal.ExternalApi.Model;
using RealtyPortal.ExternalApi.Model.DAL;
using CBSProxy_ClientWS;
using CBSProxy_TokenWS;
using System.Configuration;
using RealtyPortal.ExternalApi.Services;

namespace RealtyPortal.ExternalApi.Model.DAL
{
    public class QrDal : DB
    {


        public QrDal() : this(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()) { }

        public QrDal(IConfiguration config)
        {
        }

        public override string connectionString
        {
            get => Crypt.Decrypt(_config.GetValue<string>("ConnectionStrings:SessionAuthConnectionString"), "DCStrPN10012018", 0);
            set => throw new NotImplementedException();
        }

        public override int connectionTimeOut
        {
            get => _config.GetValue<int>("CommandTimeout");
            set => throw new NotImplementedException();
        }



    }
}
