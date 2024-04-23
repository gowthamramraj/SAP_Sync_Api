using Sha_Chiper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SAP_Sync_Api.Controllers
{
    public class Connectivity
    {
        


            public String Secret_Key;
            public String Sql_Server;
            public String Sql_UserName;
            public String Sql_Password;
            public String Sql_Database;
            public String Api_username;
            public String Api_password;
            //public String from_mail;
            public String to_mail;
            public String autoexit;
            public Int32 Branch;
            public Int32 BranchVT;
            public Int32 BranchTS;
            public String server;
            public String db_name;
            public String db_version;
            public String db_username;
            public String secret_key;
            public String db_password;
            public String sap_username;
            public String sap_password;
            public String sap_licenseserver;
            public String winauth;

            public Int32 seriesINV;
            public Int32 seriesDEL;
            public Int32 seriesGRN;
            public Int32 seriesORDR;
            public Int32 seriesORDRVT;
            public Int32 seriesORDRTS;
            public Int32 buid;

            public String bucode;
            public String companycode;
            public String baseaddress;
            public String intent;
            public String PostingMethod;


            public Int32 expensecode;

            public Connectivity()
            {

                Secret_Key = ConfigurationManager.AppSettings["secretkey"].ToString();
                Sql_Server = ConfigurationManager.AppSettings["sql_server"].ToString();
                Sql_UserName = ConfigurationManager.AppSettings["sql_username"].ToString();
                Sql_Password = Cipher.Decrypt(ConfigurationManager.AppSettings["sql_password"].ToString(), Secret_Key);
                Sql_Database = ConfigurationManager.AppSettings["sql_database"].ToString();
                //Api_baseAddress = ConfigurationManager.AppSettings["Api_serverurl"].ToString();
                //Api_posturl = ConfigurationManager.AppSettings["Api_posturl"].ToString();
                Api_username = ConfigurationManager.AppSettings["Api_user"].ToString();
                Api_password = Sha_Chiper.Cipher.Decrypt(ConfigurationManager.AppSettings["Api_pass"].ToString(), Secret_Key);
                to_mail = ConfigurationManager.AppSettings["to_mail"].ToString();

                autoexit = ConfigurationManager.AppSettings["autoexit"].ToString();


                winauth = ConfigurationManager.AppSettings["winauth"].ToString();
                server = ConfigurationManager.AppSettings["myservername"].ToString();
                db_name = ConfigurationManager.AppSettings["mydbname"].ToString();
                db_version = ConfigurationManager.AppSettings["mydbversion"].ToString();
                db_username = ConfigurationManager.AppSettings["userid"].ToString();
                secret_key = ConfigurationManager.AppSettings["secret"].ToString();
                db_password = Cipher.Decrypt(ConfigurationManager.AppSettings["mypwd"].ToString(), secret_key);
                sap_username = ConfigurationManager.AppSettings["sapuserid"].ToString();
                sap_password = Cipher.Decrypt(ConfigurationManager.AppSettings["sappassword"].ToString(), secret_key);
                sap_licenseserver = ConfigurationManager.AppSettings["saplicense"].ToString();
                seriesINV = Convert.ToInt32(ConfigurationManager.AppSettings["seriesINV"].ToString());
                seriesDEL = Convert.ToInt32(ConfigurationManager.AppSettings["seriesDEL"].ToString());
                seriesGRN = Convert.ToInt32(ConfigurationManager.AppSettings["seriesGRN"].ToString());
                seriesORDR = Convert.ToInt32(ConfigurationManager.AppSettings["seriesORDR"].ToString());
                seriesORDRVT = Convert.ToInt32(ConfigurationManager.AppSettings["seriesORDRVT"].ToString());
                seriesORDRTS = Convert.ToInt32(ConfigurationManager.AppSettings["seriesORDRTS"].ToString());
                //companycode = ConfigurationManager.AppSettings["companycode"].ToString();
                Branch = Convert.ToInt32(ConfigurationManager.AppSettings["branch"].ToString());
                BranchVT = Convert.ToInt32(ConfigurationManager.AppSettings["branchVT"].ToString());
                BranchTS = Convert.ToInt32(ConfigurationManager.AppSettings["branchTS"].ToString());
                buid = Convert.ToInt32(ConfigurationManager.AppSettings["buid"].ToString());
                expensecode = Convert.ToInt32(ConfigurationManager.AppSettings["expensecode"].ToString());
                bucode = Convert.ToString(ConfigurationManager.AppSettings["bucode"].ToString());
                baseaddress = ConfigurationManager.AppSettings["baseaddress"].ToString();
                PostingMethod = ConfigurationManager.AppSettings["PostingMethod"].ToString();
                intent = ConfigurationManager.AppSettings["intent"].ToString();





            }
      
    }
}